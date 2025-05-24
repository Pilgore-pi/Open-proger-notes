## Программа GenScrn.prg

> **GenScrn.prg** -- это модуль, который преобразует данные конструктора формы, генерируя итоговый код в одном модуле, где обрабатывается вся логика и данные формы

Каждый раз при компиляции, сборке проекта и генерации форм FoxPro обращается к модулю GenScrn, чтобы сгенерировать файл `.SPR` на основе файла `.SCX`

## Окна WINDOW

```foxpro
(VISUAL foxpro)
DEFINE WINDOW WindowName1 FROM nRow1, nColumn1 TO nRow2, nColumn2 
 | AT nRow3, nColumn3 SIZE nRow4, nColumn4 
 [IN [WINDOW] WindowName2 | IN SCREEN | IN DESKTOP [NAME ObjectName]
 [FONT cFontName [, nFontSize [, nFontCharSet]]] [STYLE cFontStyle] 
 [FOOTER cFooterText] [TITLE cTitleText] [HALFHEIGHT] 
 [DOUBLE | PANEL | NONE | SYSTEM | cBorderString] 
 [CLOSE | NOCLOSE] [FLOAT | NOFLOAT] [GROW | NOGROW] [MDI | NOMDI]
 [MINIMIZE | NOMINIMIZE] [ZOOM | NOZOOM] [ICON FILE FileName1]
 [FILL cFillCharacter | FILL FILE FileName2] 
 [COLOR SCHEME nSchemeNumber | COLOR ColorPairList]]
```

### Цвета в окнах

```foxpro
COLOR <FOREGROUND> / <BACKGROUND>
```

Цвет | Код (передний план) | Код (задний план)
-|-|-
Пустой | X | X
Черный | N | N
Белый | W+ | W*
Серый | W | W
Темно-серый | N+ | N*
Красный | R+ | R*
Темно-красный | R | R
Зеленый | G+ | G*
Темно-зеленый | G | G
Синий | B+ | B*
Темно-синий | B | B
Желтый | GR+ | GR*
Темно-желтый | GR | GR
Пурпурный | RB+ | RB*
Темно-пурпурный | RB | RB

## Окно `BROWSE`

Команда BROWSE предоставляет гибкие возможности по отображению и редактированию текущей выборки

Синтаксис команды:

```foxpro
** <Выбор_данных>
** Например select myTable / use myTable

BROWSE;
    [FIELDS <поля>]
    [FOR <условие>]                      **   При наличии ОТКРЫТОГО индекса для таблицы,
                                         ** автоматически применяется оптимизация фильтрации Rushmore.

    [REST]                               **   Продолжает фильтрацию с последней позиции курсора
                                         ** (если до этого был использован BROWSE), по умолчанию,
                                         ** перебор записей происходит с самого начала.
    [KEY <выражение1> [, <выражение2>]]
    [FREEZE <поле>]                      **   Указание единственного редактируемого поля в выборке,
                                         ** остальные поля read-only
    [LAST]                               **   Сохранение конфигурации в файле FOXUSER.DBF...
    [PREFERENCE]                         **   Анаолг LAST...

**** Разделение окна ****    
    [LOCK <число_полей>]
    [PARTITION <число_полей>]
    [LPARTITION]
    [LEDIT/REDIT]
    [NOLINK]

    [NOAPPEND]                           ** Запрет создания новых записей
    [NOCLEAR]                            ** При закрытии окна, его ресурсы не будут автоматически очищены
    [NODELETE]                           ** Запрет пометок на удаление
    [NOEDIT / NOMODIFY]                  ** Запрет редактирования (пометка на удаление и дополнение доступны)
    [NOLGRID / NORGRID] ** (управление правыми или левыми вертикальными линиями таблицы)
    [NOMENU]                             **   Подавляет вызов системного BROWSE-меню.
                                         ** При этом неявно активируются NOAPPEND и NODELETE
    [NOOPTIMIZE]                         **   Отключение оптимизации Rushmore
    [NORMAL]
    [NOWAIT]                             ** Окно BROWSE не блокирует выполнение основной программы
    
    [TIMEOUT <секунды>]                  ** Время ожидания ввода данных (по умол. неограничено)
    [TITLE <заголовок_окна>]
    [{WINDOW <окно>} / {IN [WINDOW] <окно>} / {IN SCREEN}]

**** Контроль редактирования записей ****
    [WHEN <условие>]                     **   Для каждой записи определяет по условию, доступна ли
                                         ** запись для редактирования
    [VALID [F:] <УСЛОВИЕ> [ERROR <>]]    **   Анализ условия для записи, когда запсь измененяется
                                         ** или происходит переход к след. записи. Если <условие>
                                         ** истинно, то выход из записи разрешен, иначе запрещен
                                         ** :F - проверка не только вводимых, но и существующих записей

**** Параметры графического интерфейса ****
    [{COLOR SCHEME <>} / {COLOR <список_цветных_пар>}]
    [FONT <font_name>[, <font_size> [STYLE '<код_стиля>']]] ** (B: жирный, )
```

Подчиненная команда FIELDS

```foxpro
BROWSE FIELDS;
    [:R]                                  ** Поле только для чтения (read-only)
    [:<числовое_значение_ширины_колонки>] ** Ширина колонки в символах
    [:W = <условие>]                      ** Контроль входа в поле (WHEN)
    [:V = <условие> [:F] [:E = <текст>]]  ** Контроль выход из поля (VALID)
    [:P = <форматная_строка>]             ** Формат отображения (через форматную строку или Picture)
    [:H = <текст_заголовка_поля>]
    [:B = <выраж1>, <выраж2> [:F]]        ** Указание границ чисел и дат. Не допускаются ПФ... 
```

> Кроме окна BROWSE похожий функционал предоставляет окно CHANGE, которое лучше подходит для больших таблиц

### Цветовые схемы

```foxpro
color scheme 1
```

## Объект `WINDOW`

### Определение окна

```foxpro
СТРАНИЦА 228

DEFINE WINDOW <window_name>
    FROM <Y1, X1> TO <Y2,X2> / AT <Y1>, <X1> SIZE <row_count>, <col_count>
    [TITLE <>]
    [FOOTER <>]
    [SYSTEM / DOUBLE / PANEL / NONE / <>] && Тип границы окна
    [CLOSE]
    [FLOAT]
    [GROW]
    [SHADOW]
    [ZOOM]
    [FILL <>]
    [MINIMIZE]
    [COLOR SCHEME <схема> / COLOR <color_pair_list>]
```

## Окно READ

```foxpro
READ;
 | [CYCLE]
 | [ACTIVATE expL1]
 | [DEACTIVATE expL2]
 | [MODAL]
 | [WITH window title list]
 | [SHOW expL3]
 | [VALID expL4 / expN1]
 | [WHEN expL5]
 | [OBJECT expN2]
 | [TIMEOUT expN3]
 | [SAVE]
 | [NOMOUSE]
 | [LOCK / NOLOCK]
 | [COLOR <color_pair_list> / COLOR SCHEME <схема>]
```
