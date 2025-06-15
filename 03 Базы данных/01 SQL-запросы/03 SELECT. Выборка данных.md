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

Продолжение: [[ORDER BY. Сортировка]]

#DB #SQL #SQL/DML