Для изменения уже имеющихся строк в таблице применяется команда UPDATE
```sql
UPDATE my_table
SET col1 = val1, col2 = val2, ... colN = valN
[FROM <выборка> AS псевдоним_выборки]
[WHERE <condition>]
```

Например:
```sql
UPDATE Product SET Price = Price + 5000
```

Замена значения Manufacturer "Apple" на "Apple Inc." в первых 2х строках:
```sql
UPDATE Product SET Manufacturer = 'Apple Inc.'
FROM
(
	SELECT TOP 2 Manufacturer FROM Product
	WHERE Manufacturer = 'Apple'
)
AS Selected WHERE Selected.ID = Product.ID
```

Продолжение: [[DELETE. Удаление данных]]

#DB #SQL #SQL/DML