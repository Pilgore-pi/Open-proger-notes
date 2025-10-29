Доступ к DOM-дереву происходит через глобальный объект document.

[Источник](https://developer.mozilla.org/en-US/docs/Web/API/Document)
## Свойства

Свойство | Описание
-|-
characterSet | Returns the character set being used by the document
head | возвращает head сайта
body | возвращает body или frameset
images | HTMLCollection всех изображений
links | HTMLCollection всех гиперссылок
scripts | HTMLCollection всех скриптов
styleSheets | StyleSheetList of CSSStyleSheet object\*

Расширения для HTMLDocument:

Свойство | Описание
-|-
cookie | Returns a semicolon-separated list of the cookies for that document or sets a single cookie.
title | the title of the current document
URL | URL as string

## Методы

Метод | Описание
-|-
getElementById() | элемент по идентификатору
getElementsByClassName() | коллекция элементов с указанным классом
getElementsByTagName() | 
getElementsByName() | коллекция элементов с указанным именем
querySelector() | первый элемент, удовлетворяющий селектору или списку селекторов (например ".btn")
hasFocus() | `true`, если документ имеет фокус в любом месте
open() | открывает поток документа для записи
close() | закрывает поток документа для записи
write() | запись текста в документ
writeln() | write() + `'\n'`

## События

Событие | Описание
-|-
copy | Происходит, когда пользователь копирует что-то на сайте
cut | Происходит при процессе вырезания
paste | Происходит при вставке
fullscreenchange | Происходит при переключении полноэкранного режима
keydown | При нажатии клавиши
keyup | При отпускании клавиши
keypress | При нажатии на клавишу, которая соответствует символу
scroll | Происходит при прокручивании содержимого сайта
wheel | Происходит при вращении колесика мыши

#Web #JavaScript #HTML