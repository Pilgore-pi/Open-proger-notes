Для получения данных используется команда SELECT.
Упрощенный синтаксис:

```sql
SELECT col1, col2, ... FROM my_table [WHERE <expression>]
```

Выбор всех столбцов:

```sql
SELECT * FROM my_table
```

SELECT позволяет:
* Считать столбцы
* Считать строки
* Считать данные нескольких таблиц
* Вернуть результат выражения (функции, математического выр-ия)

Возврат вычисляемого выражения:

```sql
SELECT ProductName + ' (' + Manufacturer + ')', Price,
Price * ProductCount FROM Product
```

![[MS_SQL_11.png]]

С помощью оператора AS можно изменить название выходного столбца или определить его псевдоним:

```sql
SELECT
ProductName + ' (' + Manufacturer + ')' AS ModelName, 
Price,  
Price * ProductCount AS TotalSum
FROM Products
```

Ключевое слово AS можно пропускать в тревиальных ситуациях, но это понижает читабельность.

## DISTINCT

DISTINCT возвращает уникальные значения из выборки

```sql
SELECT DISTINCT(col_name) FROM table_name
--ИЛИ
SELECT DISTINCT col_name FROM table_name
```

## SELECT INTO

Выражение SELECT INTO позволяет выбрать из одной таблицы некоторые данные в другую таблицу, при этом вторая таблица создается автоматически.

```sql
SELECT ProductName + ' (' + Manufacturer + ')' AS ModelName, Price
INTO ProductSummary
FROM Product
 
SELECT * FROM ProductSummary
```

После выполнения этой команды в базе данных будет создана еще одна таблица ProductSummary, которая будет иметь два столбца ModelName и Price, а данные для этих столбцов будут взяты из таблицы Products:

![[MS_SQL_14.png]]

Можно заполнить таблицу выборкой из другой таблицы:

```sql
INSERT INTO ProductSummary SELECT
ProductName + ' (' + Manufacturer + ')' AS ModelName, Price
FROM Product
```

## Order by - сортировка данных

Оператор ORDER BY позволяет отсортировать извлекаемые значения по определенному столбцу:

```sql
SELECT * FROM Product ORDER BY ProductName
```

Сортировка по текстовому столбцу `ProductName`:

![[MS_SQL_10.png]]

Сортировку также можно проводить по псевдониму столбца, который определяется с помощью оператора `AS`:

```sql
SELECT ProductName, ProductCount * Price AS TotalSum
FROM Product ORDER BY TotalSum
```

![[MS_SQL_13.png]]

По умолчанию сортировка по возрастанию (ASC). Сортировка по убыванию (DESC):

```sql
...ORDER BY ProductName DESC
```

Если необходимо отсортировать сразу по нескольким столбцам, то все они перечисляются после оператора ORDER BY:

```sql
SELECT ProductName, Price, Manufacturer
FROM Product ORDER BY Manufacturer, ProductName
```
В этом случае сначала строки сортируются по столбцу Manufacturer по возрастанию. Затем если есть две строки, в которых столбец Manufacturer имеет одинаковое значение, то они сортируются по столбцу ProductName также по возрастанию. Но опять же с помощью ASC и DESC можно отдельно для разных столбцов определить сортировку по возрастанию и убыванию:
```sql
SELECT ProductName, Price, Manufacturer
FROM Product ORDER BY Manufacturer ASC, ProductName DESC
```

![[MS_SQL_12.png]]

Критерием сортировки может быть выражение:

```sql
...ORDER BY ProductCount * Price
```

Оператор TOP позволяет выбрать определенное количество строк из начала таблицы:
```sql
SELECT TOP 4 ProductName FROM Product
```
-- Выбор первых 4 строк

Дополнительный оператор PERCENT позволяет выбрать процентное количество строк из таблицы. Например, выберем 75% строк:
```sql
SELECT TOP 75 PERCENT ProductName FROM Product
```

## OFFSET & FETCH

Данные операторы могут идти только после ORDER BY.

```SQL
ORDER BY <expression> 
    OFFSET row_skips ROW | ROWS
    [FETCH FIRST | NEXT количество_извлекаемых_строк ROW | ROWS ONLY]
```

Выборка отсортированной последовательности, начиная с 3 строки:

```sql
SELECT * FROM Product ORDER BY ID OFFSET 2 ROWS;
```

FETCH позволяет **получить** определенное количество строк:

```sql
SELECT * FROM Product ORDER BY ID 
    OFFSET 2 ROWS
    FETCH NEXT 3 ROWS ONLY;
```


#DB #SQL #SQL/DML