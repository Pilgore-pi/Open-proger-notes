
LINQ (Language-Integrated Query) — язык запросов к источнику данных. LINQ основан на языке SQL.

Истчниками данных могут выступать:

- объекты, реализующие `IEnumerable<T>`,
- наборы данных DataSet (см. Базы данных/Классы C-Sharp),
- документы XML

Фактически LINQ — это язык в языке.

Существует несколько разновидностей LINQ:

- LINQ to Objects — применяется для работы с `IEnumerable<T>`
- LINQ to Entities — используется в базах данных (технология Entity Framework)
- LINQ to XML — работа с файлами XML
- LINQ to DataSet — работа с объектом DataSet
- Parrallel LINQ (PLINQ) — выполнение параллельных запросов

## Выполнение запросов LINQ

Существует 2 способа выполнения отложенное (deferred) и немендленное (immidiate).
Отложенное выполнение запросов LINQ происходит при вызове следующих методов (полный список):

* AsEnumerable
* Cast
* Concat
* DefaultIfEmpty
* Distinct
* Except
* GroupBy
* GroupJoin
* Intersect
* Join
* OfType
* OrderBy
* OrderByDescending
* Range
* Repeat
* Reverse
* Select
* SelectMany
* Skip
* SkipWhile
* ThenBy
* ThenByDescending
* Union
* Where

Выполнение запроса происходит, при переборе через foreach. Поведение похоже на выполнение ленивой функции (`yield return`)

```cs
string[] people = { "Tom", "Sam", "Bob" };

//LINQ запрос
var selectedPeople = people.Where(s=>s.Length == 3).OrderBy(s=>s);
 
// выполнение LINQ-запроса
foreach (string s in selectedPeople)
    Console.WriteLine(s);
```

Запрос LINQ разбивается на 3 этапа:

1. Получение источника данных
2. Создание запроса
3. Выполнение запроса

### Немедленное выполнение запроса

С помощью ряда методов можно применить немедленное выполнение запроса. Эти методы возвращают атомарное значение, Array, List или Dictionary. Полный список методов:

* Aggregate
* All
* Any
* Average
* Contains
* Count
* ElementAt
* ElementAtOrDefault
* Empty
* First
* FirstOrDefault
* Last
* LastOrDefault
* LongCount
* Max
* Min
* SequenceEqual
* Single
* SingleOrDefault
* Sum
* ToArray
* ToDictionary
* ToList
* ToLookup

```cs
string[] people = { "Tom", "Sam", "Bob" };
// определение и выполнение LINQ-запроса
var count = people.Where(s=>s.Length == 3).OrderBy(s=>s).Count();
 
Console.WriteLine(count);   // 3 - до изменения коллекции
 
people[2] = "Mike";
Console.WriteLine(count);   // 3 - после изменения коллекции
```

Далее: [[02 LINQ to objects]]

#C-Sharp #C-Sharp/LINQ