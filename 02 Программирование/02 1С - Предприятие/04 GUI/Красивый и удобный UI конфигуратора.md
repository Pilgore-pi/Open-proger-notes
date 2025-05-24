
> Это миф (надежда умирает последней)

1. 1С:EDT ([Готовые плагины](https://xn----1-bedvffifm4g.xn--p1ai/news/2022-03-28-twelve-edt-plugins-for-ease-and-speed/))
2. turboconf - расширение на постоянку 7500 или про версия 9к
5. [Шо эта?](https://1cmycloud.com/console/help/executor/docs/topics/doc40002.html)
6. [А ЭТО ШО?](https://v8.1c.ru/platforma/1s-ispolnitel-dlya-administratorov/)
7. **THE BEST: ИСПОЛЬЗОВАНИЕ ОБРАБОТОК** этот UI независим от конкретной версии 1С. Уже существуют обработки: Консоль запросов, Консоль отчетов СКД, Можно и свои создать. Если можна создавать объекты конфигурации таким образом, то ... даааа
8. [CКАЧАНО: Редактор кода на клиенте](https://infostart.ru/1c/tools/1655115/)
10. [Консоль кода GIT Hub](https://github.com/salexdv/bsl_console?tab=readme-ov-file)
11. [СКАЧАНО: Еще одна имба](https://github.com/cpr1c/tools_ui_1c) [про тоже(прокрутить в самый низ)](https://to1c.ru/)
13. Разработка управляемые формы в клиенте
14. [СКАЧАНО: Печатная форма Word](https://infostart.ru/1c/articles/2071187/)
15. [очень Платно: конструктор для создания печатных форм в 1С 8.3](https://infostart.ru/journal/news/mir-1s/infostart-printwizard-konstruktor-dlya-sozdaniya-pechatnykh-form-v-1s-8-3_1951337/)
17. [Конструктор запроса](https://infostart.ru/1c/articles/1278855/)

[Помогаторы (гайд)](https://infostart.ru/1c/articles/2007370/)

[Устарело, Подсистема для прогера](https://infostart.ru/1c/tools/15126/)

[Гайд по расположению окон в конфигураторе](https://infostart.ru/1c/articles/1951860/)

[Синтаксис 1С Обсидиан](https://infostart.ru/1c/tools/1932706/)

**Учесть, что какие-то обработки работают только на ТОЛСТОМ клиенте, а какие-то на любом**

[Оптимизация операций путем отключения режима отладки](https://infostart.ru/1c/articles/1732527/)

```
Вариантов решения очень много, но самый простой – увеличить размер UI, для этого пропишите в edt.ini (поиграйтесь с этими параметрами):
-Dswt.enable.autoScale=true
-Dswt.autoScale=200
-Dswt.autoScale.method=nearest

С другой стороны – можно попробовать напрямую заменить иконки в jar файлах, у меня иконка лежит тут:
“C:\Users\NAME\.eclipse\org.eclipse.platform_4.23.0_1727398449_win32_win32_x86_64\configuration\org.eclipse.osgi\660\0\.cp\icons\full\obj16\brkp_obj.png”
Выйдите из EDT, откройте в редакторе, который поддерживает прозрачность, поменяйте цвет и перезайдите. Пути могут отличаться.
```

UI клиента

https://infostart.ru/1c/articles/1536215/

#1С #1С/UI