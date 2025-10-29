<img src="https://r2cdn.perplexity.ai/pplx-full-logo-primary-dark%402x.png" style="height:64px;margin-right:32px"/>

Класс `DataTable` в C\# представляет в памяти табличные данные в виде строк и столбцов, наподобие таблиц в реляционных базах данных. Он очень полезен для хранения, обработки и временного хранения структурированных данных

## Обзор

### Когда полезен DataTable

- Для работы с данными в памяти без постоянного подключения к базе
- Для представления табличных данных в приложениях (например, при работе с `DataGridView`)
- Для выполнения сортировки, фильтрации и группировки данных
- При обмене данными между слоями приложения в табличном формате
- Для сериализации и экспорта данных в XML, JSON и другие форматы
- При обработке результатов запросов ADO.NET (`SqlDataAdapter` и т.д.)

### Основные возможности

- Определение схемы таблицы через `DataColumn` (названия столбцов, типы)
- Хранение коллекции `DataRow` — строк с данными
- Методы добавления, удаления и обновления строк
- Поддержка ограничений целостности, ключей и связей между таблицами через `DataRelation`
- Возможность сортировки и фильтрации через `DataView`
- Интеграция с ADO.NET для работы с базами данных

### Основные альтернативы DataTable

| Вариант | Описание | Применение |
| :-- | :-- | :-- |
| Классы моделей (POCO) | Свои классы с обычными свойствами | Удобно для ООП подхода, типобезопасности, работы с ORM |
| `List<T>`, `Dictionary<K, V>` | Коллекции объектов | Используются для хранения наборов данных без табличной схемы |
| `DataSet` | Коллекция связанных `DataTable` | Для работы с несколькими таблицами и связями |
| ORM (EF Core, Dapper) | Работа с данными через объектно-реляционное отображение | При работе с базами данных и сложной логикой |
| `Array`, `Span<T>`, `Memory<T>` | Низкоуровневые структуры для хранения данных | Для высокопроизводительных сценариев |

`DataTable` — мощный инструмент для табличных данных в .NET, особенно если нужны функциональность базы данных без прямого подключения. Альтернативы лучше подходят, когда нужны более типобезопасные, объектно-ориентированные или производительные решения

## Операции над DataTable

### Создание DataTable и заполнение значениями

```cs
// Создание таблицы с 2 столбцами и добавление строк
DataTable table = new("ExampleTable");

// Определение колонок
table.Columns.Add("Id", typeof(int));
table.Columns.Add("Name", typeof(string));

// Добавление строк через NewRow и Rows.Add
for (int i = 0; i < 3; i++) {
    var row = table.NewRow();
    row["Id"] = i;
    row["Name"] = $"Name {i}";
    table.Rows.Add(row);
}
```

### Изменение и удаление строк

```cs
// Изменение значения в первой строке
table.Rows[0]["Name"] = "Updated Name";

// Удаление последней строки
table.Rows[^1].Delete();

// После удаления нужно принять изменения
table.AcceptChanges();
```

### Чтение данных из DataTable

```cs
foreach (DataRow row in table.Rows) {
    int id = (int)row["Id"];
    string name = (string)row["Name"];
    Console.WriteLine($"Id: {id}, Name: {name}");
}
```

### Пример использования с DataSet и связями между таблицами

```cs
DataSet ds = new DataSet();

// Создание родительской таблицы
DataTable parentTable = new DataTable("Parents");
parentTable.Columns.Add("Id", typeof(int));
parentTable.Columns.Add("ParentName", typeof(string));
parentTable.PrimaryKey = new DataColumn[] { parentTable.Columns["Id"] };
ds.Tables.Add(parentTable);

// Создание дочерней таблицы
DataTable childTable = new DataTable("Children");
childTable.Columns.Add("ChildId", typeof(int));
childTable.Columns.Add("ChildName", typeof(string));
childTable.Columns.Add("ParentId", typeof(int));
ds.Tables.Add(childTable);

// Добавление связи
ds.Relations.Add(
    "ParentChild",
    parentTable.Columns["Id"],
    childTable.Columns["ParentId"]
);

// Добавление данных в таблицы
parentTable.Rows.Add(1, "Parent1");
parentTable.Rows.Add(2, "Parent2");

childTable.Rows.Add(10, "Child1", 1);
childTable.Rows.Add(11, "Child2", 1);
childTable.Rows.Add(20, "Child3", 2);
```

Источники:[^1][^4][^5][^6][^7][^8][^9]
<span style="display:none">[^10][^2][^3]</span>

<div align="center">⁂</div>

[^1]: https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-9.0

[^2]: https://dev.to/bytehide/datatable-in-c-usage-and-examples-40f2

[^3]: https://stackoverflow.com/questions/18746064/using-reflection-to-create-a-datatable-from-a-class

[^4]: https://code-maze.com/csharp-datatable-class/

[^5]: https://bizcoder.com/datatable-in-c-sharp-c/

[^6]: https://guttitech.com/phpfusion/articles.php?article_id=3910

[^7]: https://www.c-sharpcorner.com/UploadFile/mahesh/datatable-in-C-Sharp/

[^8]: https://ironpdf.com/blog/net-help/csharp-datatable-tutorial/

[^9]: https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-data-datatable

[^10]: https://exceptionnotfound.net/mapping-datatables-and-datarows-to-objects-in-csharp-and-net-using-reflection/

#C-Sharp
