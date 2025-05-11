Idea origin:

```
LINQ — очень удобный язык запросов для получения и редактирования коллекций данных. Я хотел бы расширить LINQ и использовать его для управления метаданными файлов и работать с древом файлов (вложенными каталогами) как с логическим деревом.

Объединить этот проект с интерактивным графом
```

----

Для команд `UPDATE` будут предопределенные операции для работы с данными.
Можно в явном виде задать новое значение данных в виде
- текста `"text"`, `'s'`, `'\u0061'` (все воспринимается как строка)
- числа, представляющего данные файла `0xFDC234F436...`, `0b0100001101`, `0d01065189`,
- задать, из какого файла будет браться новое значение

```sql
val path = "C:/MyFolder/example.txt" -- immutable
val insertIndex = 0

update <path1>
set Data = file(<path2>) as binary [startByteIndex=0:endByteIndex=endOfFile()]

update <path1> set Encoding = Encodings.UTF8

insert into <path1> at [endOfFile()] "appended text to the end of the file";

insert into <path1> at [startOfFile()] "text has been added to the start of the file";

select file(
    <path> as text
    [<line1=0>:<position=0>]
    [<line2=endOfFile()>:<position=endOfFile()>]
)

delete <path>

select (delete <path>); -- returns response status

select Name
from Disk_D.<path1> as A
full join
Disk_F<path2> as B
on A.Name = B.Name
as UniqueFiles;

create file(UniqueNames as UTF16) in <path> -- in current folder by default

select Name from (create file(UniqueNames, ".md") in <path>)

select * from -- lazy operation
(
    select * from <path1>.Metadata join <path2>.Metadata
    union -- or union all (it's more performant but creates duplicates)
    select * from <path1>.Data join <path3>.Data
);

open <path> -- , <path2>, <path3>
open <path> as text;
open <path> as binary
open <path> as image
open <path> as video
open <path> with explorer
open <path> with <exePath> -- Exception: No executable app found at <exePath>

-- file union
create select File from (<path1> + <path2>) 
```

Русский аналог

```sql
обновить <путь1>
задать Данные = файл(
    <путь2> как бинарный [первыйБайт=0:последнийБайт=конецФайла()]
)

обновить <путь1> задать Кодировка = Кодировки.UTF8

вставить <путь1> "Добавленный в конец файла текст"; -- вставить [после]

вставить перед <путь1> "текст, добавленный в начало файла";

выбрать файл(
    <путь> как текст
    [<строка1>:<позиция=0>] [<строка2>:<позиция=конецФайла()>]
)

удалить <путь>

выбрать (удалить <путь>); -- returns status response

выбрать Имя
из Disk_D.<путь1> как А полное соединение Disk_F.<путь2> как Б
по А.Имя = Б.Имя как УникальныеФайлы

создать файл(УникальныеФайлы как текст, ".txt") в <путь>
```

> Проверять, использует ли пользователь как кириллические операторы, так и латинские. Если пользователь смешивает языки, то выдавать ошибку: "Так не пойдет, используйте только один язык при написании скриптов". **НЕТ**, просто заставлять явно выбирать пользователя используемый естественный язык. Без разницы, какие языки будут использоваться в разных скриптах, так как они все будут транслироваться в C#

Каждая команда возвращает статус выполнения, который можно получить через `select`

- Конструктор Yoinq

> Названия для моего языка запросов: Exinq (explorer integrated query), Yinq, **Yoinq** (Your integrated query), Your-SQL, File-SQL, 

Интерфейс взаимодействия с объектом File:

| Property               | Свойство                | Значение                                    |
| ---------------------- | ----------------------- | ------------------------------------------- |
| `FullName`             | `ПолноеИмя`             | Полный путь к файлу                         |
| `NameWithoutExtention` | `ИмяБезРасширения`      |                                             |
| `Extention`            | `Расширение`            |                                             |
| `Name`                 | `Имя`                   | `=> NameWithoutExtention + '.' + Extention` |
| `ActualFormat?`        | `ДействительныйФормат?` | `string? GetActualFormat(File)`             |
|                        |                         |                                             |

Все Readonly свойства будут вызываться как методы

| Function       | Функция        | Значение                                                                                                |
| -------------- | -------------- | ------------------------------------------------------------------------------------------------------- |
| `Attributes()` | `Атрибуты()`   | `GetFileAttributes()`                                                                                   |
| `Size()`       | `Размер()`     | Суммирует размер всех принятых параметров (числа как результаты других вызовов `Size()`, строки, файлы) |
| `Merge()`      | `Объединить()` | Объединяет строки или файлы                                                                             |
| `Replace()`    | `Заменить()`   | Заменяет все найденные вхождения в строке или в файле                                                   |
| `IndexOf()`    | `Индекс()`     | Индекс первого вхождения в строке или файле                                                             |
| `File()`       | `Файл()`       | Выполняет чтение файла и возвращает объект File                                                         |

#Идеи_проектов #Идеи_проектов/Comfort_explorer