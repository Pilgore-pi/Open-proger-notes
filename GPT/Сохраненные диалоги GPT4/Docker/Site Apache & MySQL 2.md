Я создал проект с 3 образами, которые указаны в файле docker-compose.yml:

```yaml
version: '3.8'

services:
  backend:
    build: ./backend
    container_name: backend-container
    ports:
      - 8001:8080
    volumes:
      - ./backend/site:/var/www/html
    depends_on:
      - db

  db:
    image: mysql:5.7
    container_name: db-container
    volumes:
      - db_data:/var/lib/mysql
      - ./db/init.sql:/docker-entrypoint-initdb.d/init.sql
    environment:
      MYSQL_DATABASE: shop
      MYSQL_USER: login
      MYSQL_PASSWORD: pass
      MYSQL_ROOT_PASSWORD: rootpass
    ports:
      - "3306:3306"

  proxy:
    build:
      context: ./proxy
    container_name: proxy-container
    depends_on:
      - backend
    ports:
      - "8000:8080"
    networks:
      - app-network

volumes:
  db_data:

networks:
  app-network:
    driver: bridge
```

Мой файл конфигурации сервера `my-httpd.conf` выглидит так:

```
<VirtualHost *:8000>
    ServerName localhost

    ProxyRequests Off
    ProxyPreserveHost On

    ProxyPass / http://backend-container:8080/
    ProxyPassReverse / http://backend-container:8080/

    ErrorLog /usr/local/apache2/logs/error.log
    CustomLog /usr/local/apache2/logs/access.log combined
</VirtualHost>
```

Докер файл для сервиса `proxy`:
```dockerfile
FROM httpd:2.4

RUN sed -i \
  -e 's/#LoadModule proxy_module modules\/mod_proxy.so/LoadModule proxy_module modules\/mod_proxy.so/' \
  -e 's/#LoadModule proxy_http_module modules\/mod_proxy_http.so/LoadModule proxy_http_module modules\/mod_proxy_http.so/' \
  /usr/local/apache2/conf/httpd.conf

RUN echo "Include conf/extra/my-httpd.conf" \
    >> /usr/local/apache2/conf/httpd.conf
COPY my-httpd.conf /usr/local/apache2/conf/extra/my-httpd.conf

EXPOSE 80
```

Докер файл для сервиса `backend`:

```dockerfile
FROM php:7.4-apache

COPY ./site/ /var/www/html/

WORKDIR /var/www/html

EXPOSE 8000
```

В итоге я хочу запустить мой сайт в браузере, но когда я выполняю команду `docker-compose up --build`, я получаю следующую ошибку

`AH00558: httpd: Could not reliably determine the server's fully qualified domain name, using 172.18.0.4. Set the 'ServerName' directive globally to suppress this message`

При этом запустить сайт не получается. Как мне исправить это?

---

Сообщение об ошибке, которое вы видите (`AH00558: httpd: Could not reliably determine the server's fully qualified domain name, using 172.18.0.4. Set the 'ServerName' directive globally to suppress this message`), на самом деле является предупреждением Apache и не должно мешать работе вашего сайта. Это предупреждение указывает на то, что Apache не смог надежно определить полное доменное имя сервера, и предлагает установить директиву `ServerName` глобально, чтобы подавить это сообщение. Вы можете добавить `ServerName localhost` в ваш httpd.conf или в любой другой файл конфигурации, который Apache загружает на старте, чтобы избавиться от этого предупреждения. Но это предупреждение не является причиной того, что сайт не работает.

Однако, по вашему описанию, кажется, что проблема заключается не в этом предупреждении. Возможные проблемы на которые стоит обратить внимание:

1. **Порты**: в вашем Dockerfile для `backend` у вас указан `EXPOSE 8000`, хотя конфигурация `docker-compose.yml` ожидает, что `backend` слушает на порту 8080 (`ports: - 8001:8080`). Убедитесь, что ваше приложение в контейнере `backend` действительно запущено и слушает на порту 8080. Если оно слушает на другом порту, вам нужно будет обновить как Dockerfile, так и конфигурацию в `docker-compose.yml` соответствующим образом.

2. **Сети**: в вашем `docker-compose.yml`, сервисы `backend` и `db` не принадлежат явно к сети `app-network`, хотя они должны быть частью одной сети с `proxy`, чтобы обеспечить связь между контейнерами. Добавьте `backend` и `db` в сеть `app-network` следующим образом:

   ```yaml
   services:
     backend:
       ...
       networks:
         - app-network
     db:
       ...
       networks:
         - app-network
     proxy:
       ...
       networks:
         - app-network
   ```

3. **Конфигурация Apache** в сервисе `proxy` может быть неправильно настроена для проксирования запросов на `backend`. Однако, на первый взгляд, ваша конфигурация кажется корректной. Убедитесь, что файл `my-httpd.conf` правильно копируется и Apache его читает (как указано в вашем Dockerfile).

4. Если после выполнения вышеуказанных шагов проблема остается, проверьте логи контейнеров, чтобы увидеть, есть ли какие-либо ошибки на стороне `backend` или `proxy`. Это может дать более подробную информацию о том, где именно возникает проблема.

Исправление этих потенциальных проблем поможет вам успешно запустить ваш сайт.

---

Несмотря на то, что я исправил порт в Dockerfile для `backend` и Добавил `backend` и `db` в сеть `app-network`, я все равно не могу открыть сайт в браузере по адресам: `loclahost`, `localhost:8000`, `localhost:8080`

---

Если после исправлений сайт всё еще не открывается, давайте шаг за шагом проверим потенциальные проблемы:

### 1. Проверка конфигурации прокси-сервера

Убедитесь, что ваш прокси-сервер (`proxy`) настроен правильно и слушает порт, который вы указываете в браузере. В вашем случае, согласно `docker-compose.yml`, это порт `8000`. Но конфигурация Dockerfile для `proxy` говорит о том, что Apache внутри контейнера настроен на прослушивание порта 80. Вам следует изменить порт в `docker-compose.yml` для `proxy` на `80:8080`:

```yaml
proxy:
  ...
  ports:
    - "8000:80"  # Изменим порт внутренний на 80, чтобы соответствовать EXPOSE в Dockerfile
```

### 2. Проверка работы сервиса `backend`

Убедитесь, что сервер `backend` работает корректно:

- Проверьте логи сервиса `backend`, чтобы убедиться, что внутри контейнера всё запускается без ошибок.
- Убедитесь, что ваш `backend` слушает на порту `8080`, как это задано в `docker-compose.yml`.

### 3. Проверка доступности сервиса `proxy`

Проверьте, что `proxy` действительно перенаправляет запросы к `backend`:

- Проверьте логи сервиса `proxy`, чтобы увидеть, принимает ли он запросы и успешно ли перенаправляет их на `backend`.
- Убедитесь, что конфигурация прокси Apache (`my-httpd.conf`) правильно указывает на контейнер `backend-container`.

### 4. Проверка сетевых настроек

Убедитесь, что все ваши сервисы находятся в одной сети (`app-network`) и могут общаться друг с другом. Это вы уже сделали, но стоит проверить, применены ли изменения.

### 5. Проверка портов и адреса

Убедитесь, что вы обращаетесь к правильному адресу и порту:

- Ваш прокси-сервер настроен на прослушивание порта `8000` на вашей машине, так что в браузере вы должны использовать `http://localhost:8000`.
- Проверьте, что порты на вашем хосте не блокируются фаерволом или другим программным обеспечением.

### Проверка через командную строку

Вы можете также временно использовать команду `curl` из командной строки вашей системы для локальной проверки доступности:

```bash
curl http://localhost:8000
```

Это должно вернуть содержимое вашего сайта или хотя бы подтвердить, что запрос проходит через прокси к `backend`.

Если вы выполнили все вышеуказанные шаги и сайт всё еще не доступен, проверьте ещё раз логи всех трёх сервисов (`backend`, `db`, `proxy`), возможно, там будут подсказки о том, что именно идет не так.



#GPT #Docker #Web