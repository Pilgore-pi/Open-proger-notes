
> "Batteries included" — в контексте фреймворков означает, что фреймворк содержит встроенные инструменты, которые часто могут использоваться при разработке приложений, например, ORM, GUI, аутентификация, локализация, безопасность и т.д.

## Flask \[[Habr](https://habr.com/ru/articles/783574/)\]

Легковесный широко кастомизируемый веб-фреймворк с низким порогом вхождения

Особенности:

- Минималистичность и легковесность
- Предназначен для разработки малых и средних приложений (напр. микросервисы)
- Весь код выполняется синхронно
- Быстрая разработка
- Поддержка маршрутизации (routing)

Маршрутизация — механизм, позволяющий связать URL-адрес с функциями `python`:

```python
@app.route('/about')
def about():
    return 'This is the about page'
```

Простейший код, который выводит сообщение на экран

```python
from flask import Flask
app = Flask(name)

@app.route('/')
def hello_world():
    return 'Hello, World!'

if name == 'main':
    app.run(debug=True)
```

Пример обработки POST-запроса:

```python
from flask import request

@app.route('/submit', methods=['POST'])
def submit():
    name = request.form['name']
    return f'Hello, {name}'
```

##### Работа с формами

- Для работы с формами в Flask часто используется объект `request`, который позволяет получить доступ к данным, отправленным пользователем.

- Пример HTML-формы:

```html
<form method="post" action="/submit">
    <input type="text" name="name">
    <input type="submit" value="Submit">
</form>
```

Эта форма отправляет данные на маршрут `/submit`, который обрабатывается в приложении

### Стоит ли использовать Flask

- Flask позволяет очень быстро разрабатывать мелкие приложения
- Flask очень прост, что приводит к написанию говнокода
- Как только понадобится функционал, которого нет в Flask требуется подключение доп. модулей. То есть, приложения Flask плохо масшабируются и требуют подключение всё новых зависимостей
- Многие плагины для Flask являются устаревшими и не поддерживаются
- В целом Flask можно использовать для малых проектов и можно найти современные библиотеки необходимые для реализации доп. фич

## Django

- Встроенная ORM
- Готовые средства GUI
- Встроенная система аутентификации
- Встроенный интерфейс администратора
- Поддержка локализации текста и формат дат и времени
- Поддержка маршрутизации
- Наличие системы безопасности, устраняющей уязвимости
- Предназначен для разработки сложных веб-приложений
- Изучение фреймворка займет значительное время
- Фреймворк является самым популярным на рынке (в рамках Python)

```django
<html>
  <head>
    <title>{% translate "Band Listing" %}</title>
  </head>
  <body>
    <h1>{% translate "All Bands" %}</h1>
    <ul>
    {% for band in bands %}
      <li>
        <h2><a href="{{ band.get_absolute_url }}">{{ band.name }}</a></h2>
        {% if band.can_rock %}<p>{% translate "This band can rock!" %}</p>{% endif %}
      </li>
    {% endfor %}
    </ul>
  </body>
</html>
```

- Использование JavaScript не является обязательным при разработке Django-приложений. Однако при необходимости динамически обновлять элементы HTML (AJAX) или при необходимости построения сложного UI требуется использование JS.

## Fast API

Высокопроизводительный веб-фреймворк

- Поддерживает асинхронное программирование
- Поддержка автоматической валидации данных
- Наличие встроенной сериализации
- Поддержка методов аутентификации
- Автогенерация документации для API
- **Быстрая** разработка
- Большая производительность

## Starlite

https://docs.litestar.dev/1/

[[Указатель терминов#^0e00d8|ASGI]]-фреймворк

WSGI is a spec that standardizes a way for traditional web servers (nginx, apache, etc) communicate with web applications and frameworks like flask. Traditional web servers don't understand how to communicate directly with web applications like flask so you need something to sit in the middle, which is where a WSGI server comes into play. Gunicorn is an implementation of a WSGI server that will act as the intermediary between a tradition web server and your flask application. While you could server out your app with just the wsgi server, it is more common to situate it behind a web server (nginx > gunicorn > flask app code).

gunicorn is [Green Unicorn,](https://gunicorn.org/) it acts as an interface between a production web server like nginx and your flask application.

WSGI is a web server gateway interface, the technology that gunicorn implements.

#Web/Frameworks/Python