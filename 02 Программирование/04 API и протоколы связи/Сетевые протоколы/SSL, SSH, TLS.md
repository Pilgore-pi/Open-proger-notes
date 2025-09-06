
Давайте рассмотрим понятия **SSL**, **SSH** и **TLS**, а также их различия:

## SSL (Secure Sockets Layer)

- **Определение**: SSL — это криптографический протокол, обеспечивающий безопасную связь между клиентом и сервером. Он использует асимметричное шифрование для аутентификации, симметричное шифрование для конфиденциальности и коды аутентификации сообщений для целостности данных[^1_1][^1_2].
- **Использование**: Широко использовался для HTTPS, электронной почты и VoIP, но в настоящее время рекомендуется использовать вместо него TLS из-за уязвимостей[^1_1][^1_2].
- **Принцип работы**: Установление соединения, обмен ключами, шифрование/расшифровка и проверка целостности данных[^1_2].


## SSH (Secure Shell)

- **Определение**: SSH — это протокол удаленного администрирования, позволяющий безопасно управлять операционными системами и передавать данные по шифрованному каналу[^1_3][^1_4].
- **Использование**: Используется для удаленного доступа к серверам, выполнения команд и передачи файлов. Работает на TCP-порту 22[^1_3][^1_4].
- **Принцип работы**: Установка защищенного соединения, аутентификация и обмен данными с помощью ключей и криптографических алгоритмов[^1_3][^1_4].


## TLS (Transport Layer Security)

- **Определение**: TLS — это криптографический протокол, обеспечивающий безопасную передачу данных между узлами в сети Интернет. Он является развитием SSL и предлагает более надежные механизмы шифрования и аутентификации[^1_5][^1_6].
- **Использование**: Широко используется для HTTPS, электронной почты и VoIP. Предлагает лучшую производительность и безопасность по сравнению с SSL[^1_5][^1_6].
- **Принцип работы**: Аналогичен SSL, но с улучшенными алгоритмами шифрования и аутентификации. Использует handshake для установления соединения[^1_5][^1_6].


## Основные различия между SSL, SSH и TLS

- **Назначение**: SSL/TLS — для безопасной передачи данных между клиентом и сервером, SSH — для удаленного администрирования.
- **Протоколы и порты**: SSL/TLS работают на порту 443, SSH — на порту 22.
- **Алгоритмы шифрования**: TLS использует более современные и безопасные алгоритмы по сравнению с SSL.
- **Аутентификация**: SSL/TLS обычно используют цифровые сертификаты, SSH — ключи и пароли для аутентификации[^1_7][^1_8].



[^1_1]: https://ru.wikipedia.org/wiki/SSL

[^1_2]: https://gitverse.ru/blog/articles/development/165-protokol-ssl-chto-eto-kak-rabotaet-i-zachem-nuzhen

[^1_3]: https://freehost.com.ua/faq/wiki/chto-takoe-ssh/

[^1_4]: https://skillbox.ru/media/code/ssh-chto-eto-i-kak-im-polzovatsya-gaid-dlya-novichkov/

[^1_5]: https://ru.wikipedia.org/wiki/TLS

[^1_6]: https://ultahost.com/blog/ru/ssl-ili-tls/

[^1_7]: https://www.ssl2buy.com/wiki/ssh-vs-ssl-tls

[^1_8]: https://www.wolfssl.com/tls-vs-ssh-when-to-use-which/

[^1_9]: https://powerdmarc.com/ru/difference-between-ssl-and-tls/

[^1_10]: https://www.appviewx.com/blogs/ssh-vs-ssl-tls-whats-the-difference/

[^1_11]: https://www.ssldragon.com/ru/blog/ssh-vs-ssl/

[^1_12]: https://www.promowebcom.by/analytics/articles/seo/zachem-nuzhen-ssl-sertifikat-na-nbsp-sayte-kak-ego-poluchit-i-nbsp-skolko-stoit/

[^1_13]: https://selectel.ru/blog/what-is-ssl/

[^1_14]: https://habr.com/ru/companies/1cloud/articles/326292/

[^1_15]: https://skillbox.ru/media/code/protokol-ssl-chto-eto-kak-rabotaet-i-zachem-nuzhen/

[^1_16]: https://highload.tech/ssh-protocol/

[^1_17]: https://selectel.ru/blog/what-is-ssh/

[^1_18]: https://beget.com/ru/kb/how-to/ssh/chto-takoe-ssh

[^1_19]: https://ru.hexlet.io/blog/posts/ssh

[^1_20]: https://habr.com/ru/sandbox/166705/

[^1_21]: https://rt-solar.ru/products/solar_webproxy/blog/4812/

[^1_22]: https://neerc.ifmo.ru/wiki/index.php?title=SSL%2FTLS

[^1_23]: https://tls.dxdt.ru

[^1_24]: https://help.reg.ru/support/ssl-sertifikaty/obshchaya-informatsiya-po-ssl/chto-takoye-protokol-bezopasnosti-tls

[^1_25]: https://www.youtube.com/watch?v=k3rFFLmQCuY

[^1_26]: https://aws.amazon.com/ru/compare/the-difference-between-ssl-and-tls/

[^1_27]: https://sky.pro/wiki/javascript/ssl-i-tls-chto-eto-raznica-i-zachem-nuzhny-sertifikaty/

[^1_28]: https://education.yandex.ru/journal/chto-takoe-ssl-sertifikat-i-kak-ego-ustanovit

[^1_29]: https://blog.skillfactory.ru/glossary/ssl/

[^1_30]: https://www.kaspersky.ru/resource-center/definitions/what-is-a-ssl-certificate

[^1_31]: https://skillbox.ru/media/code/sslsertifikaty-chto-eto-takoe-kakie-oni-byvayut-i-zachem-ikh-poluchat/

[^1_32]: https://www.ssl.com/ru/статью/what-is-an-ssl-tls-сертификат/

[^1_33]: https://blog.skillfactory.ru/glossary/ssh/

[^1_34]: https://selectel.ru/blog/ssh-authentication/

[^1_35]: https://skyeng.ru/magazine/wiki/it-industriya/chto-takoe-ssh/

[^1_36]: https://habr.com/ru/articles/724762/

[^1_37]: https://ru.wikipedia.org/wiki/SSH

[^1_38]: https://www.nic.ru/help/ssh-dostup_6767.html

[^1_39]: https://www.cryptopro.ru/sites/default/files/docs/TLS_description.pdf

[^1_40]: https://aws.amazon.com/ru/what-is/ssl-certificate/

[^1_41]: https://skillbox.ru/media/code/protokol-tls-chto-eto-zachem-nuzhen-i-kak-rabotaet/

[^1_42]: https://os.kaspersky.ru/solutions/glossary/kaspersky-thin-client/ssl-tls-certificates/
[^1_43]: https://www.entrust.com/ru/resources/learn/what-is-tls
[^1_44]: https://blog.skillfactory.ru/chto-takoe-tls-i-kak-rabotaet-etot-protokol/
[^1_45]: https://habr.com/ru/articles/258285/
[^1_46]: https://www.ssldragon.com/ru/blog/ssl-vs-tls-differences/

#MERGE_NOTES