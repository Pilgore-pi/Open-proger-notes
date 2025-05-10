
Написать тестовую прогу, которая будет вызывать функцию из DLL, написанной на Python и выводить тип возвращаемого значения и типы ожидаемых параметров.

Выяснить, нужна ли сериализация

# YourFIles

## General

#### **`[UI]`** [[Comfort explorer|Advanced efficient explorer]]
#### `[UI]` Metadata editor

- `[Model]` **FileDataSource**: general model for every file system entity
- `[Model]` **File : FileDataSource**: file in general
- `[Model]` **Directory : FileDataSource**: directory
- `[Model]` **Data : FileDataSource**: byte sequence
- `[Model]` **Metadata : Data**: dictionary \<string, byte array\>
- `[Model]` **Video : File**: video
- `[Model]` **RasterImage : File**: raster image
- `[Model]` **VectorImage : File**: vector image
- `[Model]` **Audio : File**: music

#### Commands

- `[Command]` **AssociateWith**: позволяет ассоциировать приложение или URL адрес с типом файла или с конкретным каталогом (в таком случае генерируется скрытый каталог `.yourcore` с метаданными каталога)
- `[Command]` **Compress**: архивация файла, нескольких файлов или каталога
- `[Command]` **Decompress**: разархивирование
- `[Command]` **Encrypt**: шифрование файла, нескольких файлов или каталога
- `[Command]` **Decrypt**: расшифровка по ключу
- `[Command]` **GetHash**: рассчет хэш-значения для источника данных по указанному алгоритму хэширования
- `[Command]` **FindByText(Encoding = UTF8, SearchOptions = FileNames)**: выполняет поиск заданного текста в названиях файла, в расширениях файлов, в метаданных файла или данных файла (`[Flags] enum DataSearchOptions`)
- `[Command]` **FindByBytes**: выполняет поиск заданной последовательности байт согласно настройкам поиска (`[Flags] enum DataSearchOptions`)
- `[Command]` **FindByAttributes**: Выполняет поиск файлов или каталогов по файловым атрибутам
- `[Command]` **Find(Predicate)**: выполняет поиск согласно условию. ==ПО УМОЛЧАНИЮ ПОИСК ВЫПОЛНЯЕТСЯ В ТЕКУЩЕМ КАТАЛОГЕ==
- `[Command]` **Compare(\_, \_)**: сравнивает 2 источника данных (File, {File inheritor}, byte array, Metadata) и выводит категории, которые совпадают и которые не совпадают
- `[Command]` **FileDataSource.Replace**: выполняет замену строкой или массивом байт в указанном наборе источников данных согласно параметрам замены (первое вхождение, последнее, все вхождения...)
- `[Command]` **~~FileDataSource.AsSingle\[ \* \]~~**: Формирует временный объект, представляющий объединение нескольких источников данных
- `[Command]` **FileDataSource.OpenAs(format)**: открывает файл так, будто он имеет другой формат, указанный в параметрах команды
- `[Command]` **FileDataSource.OpenWith**: стандартная функция "Открыть с помощью" 
- `[Command]` **AnalyzeActualFormat**: анализирует действительный тип файла по структуре его метаданных и данных независимо от указанного расширения
- `[Command]` **CopyTo**: копирует файл или директорию в указанное расположение
- `[Command]` **MoveTo**: перемещает файл
- `[Command]` **CreateShortcut**: создает ярлык
- `[Command]` **Serialize**: выполняет сериализацию источника(-ов) данных в указанный формат (JSON, XML, HTML, Binary, YAML, UTF16, UTF8...)
- `[Command]` **Deserialize**: создает файл или каталог, или набор файлов и каталогов согласно сериализованному массиву данных
- `[Command]` **Convert**: преобразует источник данных из одного формата в другой (огромный метод, который реализует логику всех возможных преобразований (условно всех))

Фактический синтаксис этих команд:

```
РУС: Найти по тексту (параша, UTF8) переместить в (Корзина)
Парсинг: найтипотексту(параша,UTF8).переместитьв(корзина)

ENG: Find by text (salmanella, ansi) move to (Recycle)
Финальный парсинг: findbytext(salmanella,ansi).moveto(recycle)
```

## Images


## Audio


## Video

#Идеи_проектов #Идеи_проектов/Upper_Level