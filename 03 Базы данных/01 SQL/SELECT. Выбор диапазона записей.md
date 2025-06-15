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

Продолжение: [[WHERE. Фильтрация]]

#DB #SQL #SQL/DML