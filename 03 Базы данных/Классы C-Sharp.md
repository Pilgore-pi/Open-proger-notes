## SqlConnection

Представляет подключение к базе данных SQL Server
Конструктор принимает текстовое представление строки подключения.

## ConfigurationManager

Статический класс предоставляет доступ к файлам конфигурации для клиентских приложений.
Данный класс имеет 2 свойства:
1. AppSettings — коллекция настроек приложения. Данному свойству соответствует тег "\<appSettings\>" в файле конфигурации.
2. ConnectionStrings — коллекция строк подключения указанных в конфигурационном файле. Данному свойству соответствует тег "\<connectionStrings\>"

## SqlCommand

Представляет собой объектно-ориентированное представление SQL-запроса, имени таблицы или хранимой процедуры.
Тип команды указывается свойством CommandType, которое принимает значения из перечисления CommandType:

Значение | Пример SQL-запроса
---|---
Text |string select = "SELECT ContactName FROM Customers"; SqlCommand cmd = new SqlCommand(select, cn);
StoredProcedure | SqlCommand cmd = new SqlCommand("CustOrderHist", conn); cmd.CommandType = CommandType.StoredProcedure; cmd.Parameters.AddWithValue("@CustomerID", "QUICK");
TableDirect |OleDbCommand cmd = new OleDbCommand("Categories", conn); cmd.CommandType = CommandType.TableDirect;
Конструкторы:
* SqlCommand()
* SqlCommand(string com)
* SqlCommand(string com, SqlConection con)
Свойство CommandText содержит текст команды

### Выполнение команд

Существует много способов издания оператора, в зависимости от того, какой возврат ожидается (если ожидается) от команды. Классы \<provider\>Command предлагают следующие методы выполнения:
* ExecuteNonQuery() — выполнение команды без вывода
* ExecuteReader() — выполняет команду и возвращает IDataReader
* ExecuteScalar() — выполняет команду и возвращает значение из первого столбца первой строки любого результирующего набора
* ExecuteXMLReader() — выполняет команду и возвращает объект XmlReader

Во многих Случаях бывает необходимо вернуть единственный результат из оператора SQL, такой как количество записей в заданной таблице или текущие дату и время на сервере. В таких ситуациях применяется метод ExecuteScalar()

## SqlDataReader
### Создание

Создание объекта производится только с помощью метода SqlCommand.ExecuteReader():
```cs
SqlDataReader dataReader = sqlCommand.ExecuteReader();
```
* Данный класс реализует интерфейс IEnumerable\<T\>

### Закрытие объекта

Необходимо вызывать метод Close по окончании использования объекта SqlDataReader, так как для этого объекта эксклюзивно выделяется все подключение к базе данных и оно не может быть занято другим dataReader. Кроме того никакие команды выполнить будет нельзя.

### Свойства

* Connection — SqlConnection текущего SqlDataReader
* FieldCount — число столбцов в текущей строке
* VisibleFieldCount — нескрытое число полей в строке
* IsClosed — закрыт ли текущий объект
* this\[int\] — значение столбца по его индексу (при его наличии)
* this\[string\] — значение столбца по его имени (при его наличии)

### Методы

* Close() — закрывает объект
* Read() — перемещает SqlDataReader к следующей запись
* GetBoolean(index) — возвращает логическое значение, преобразованное из поля указанного столбца
* GetDateTime(index) — возвращает столбец в виде объекта DateTime
* Get\<TYPE\>(index) — возвращает значение указанного столбца, преобразованного в укзанный тип
* GetSql\<TYPE\>(index) — возвращает столбец, преобразованный в один из типов Sql (Int32, Single, Guid)
* GetValue(index) — Возвращает значение заданного столбца в его исходном формате
* GetOrdinal(col_name) — возвращает порядковый номер столбца, если известно его имя

## SqlDataAdapter

Классы-адаптеры применяются для заполнения контейнера DataSet с помощью объектов DataTable, а также для отправки измененных DataTable обратно в БД.

Заполнение или обновление DataSet происходит с помощью метода Fill(DataSet). Fill открывает подключение или использует уже открытое для доступа к БД. После заполнения DataSet метод закрывает это подключение.
```cs
SqlDataAdapter dataAdapter = new SqlDataAdapter
(
	"SELECT * FROM Products",
	nsqlCon
);

DataSet dataSet = new DataSet();
dataAdapter.Fill(dataSet);
```
### Конструкторы:

* SqlDataAdapter()
* SqlDataAdapter(SqlCommand)
* SqlDataAdapter(selectCommand, SqlConnection)
* SqlDataAdapter(selectCommand, connectionString)

\*С заглавной буквы — классы, со строчной — объекты.
selectCommand — текст, представляющий sql-запрос. Этот параметр заполняет свойство SqlCommand SelectCommand класса SqlAdapter.

### Свойства:

* SelectCommand
* UpdateCommand
* InsertCommand
* DeleteCommand

### Методы:

* Fill(DataSet) — заполнение DataSet
* Fill(DataTable) — заполнение таблицы объекта DataSet
* 

## DataSet

Класс DataSet из пространства имен System.Data представляет собой контейнер для любого количества объектов DataTable, каждый из которых содержит коллекцию объектов DataRow и DataColumn. DataSet хранит как данные, так и реляционные отношения между ними.

Свойство Tables предоставляет доступ к коллекции **DataTableCollection**, которая содержит отдельные объекты DataTable.
Также существует важное свойство **DataRelationCollection**. Оно используется для программного представления отношений между таблицами.

Свойства:
Tables — коллекция таблиц. Таблицы можно перебирать по индексу или по названию

Методы:
Clear() — очищение DataSet
Clone() — полное копирование DataSet
Copy() — копирование только структуры и данных DataSet (без отношений)
Merge() — объединяет DataSet с другим

## Tables

Tables представляет собой таблицу базы данных. Данный класс содержит два важных свойства Columns & Rows — коллекции столбцов и строк.
Столбец — это атрибут (не само значение). Строка — это коллекция полей, содержащих данные, к которым можно обратится по индексу или названию столбца.

Tables содержит ссылку на обертывающий DataSet, так же как и DataColumn и DataRow содержат ссылку на обертывающий их Table.

Свойства:
* Columns — список столбцов
* Rows — список строк
* TableName — имя таблицы

Методы:
* Clear() — очищает данные
* Clone() — полностью копирует таблицу со всеми ограничениями
* Copy() — копирует структуру и данные DataTable
* Select() — получение массива всех строк в таблице

### DataColumn

Представляет собой один столбец объекта DataTable. Совокупность DataColumn из определенной DataTable представляет собой ее схему(метаданные).

Свойства:
* ColumnName — имя столбца
* Caption — псевдоним~
* DataType — задает или возвращает тип столбца
* bool AllowDBNull — допускается ли отсутствие значение
* DefaultValue — значение по умолчанию
* bool Unique — уникальность значений в столбце

### DataRow

Коллекция объектов DataRow представляет конкретные данные в таблице. И если на складе имеются 20 автомобилей, то для хранения информации о них нужно 20 объектов DataRow.

Свойства:
* this\[int\] — значение поля по номеру столбца
* this\[string\] — значение поля по названию столбца
* ItemArray — значение всех полей текущей строки в виде массива

Методы:
* Delete() — удаляет текущую строку
* IsNull(param) — проверяет содержит ли поле по указанному столбцу значение Null. param — индекс, название или экземпляр столбца.

#DB #SQL/MSSQL #C-Sharp