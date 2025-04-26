Оператор ORDER BY позволяет отсортировать извлекаемые значения по определенному столбцу:
```sql
SELECT * FROM Product ORDER BY ProductName
```
Сортировка по текстовому столбцу ProductName:
![[MS_SQL_10.png]]
Сортировку также можно проводить по псевдониму столбца, который определяется с помощью оператора AS:
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

Продолжение: [[SELECT. Выбор диапазона записей]]

#DB #SQL #SQL/DML