Возможно, в какой-то момент понадобится изменить таблицу: добавить или удалить столбцы, изменить тип и ограничения столбцов.

Для изменения таблиц используется команда ALTER TABLE
Синтаксис:
```sql
ALTER TABLE my_table [WITH CHECK | WITH NOCHECK]
	ADD col <type> [col_attributes] |
	DROP COLUMN col |
	ALTER COLUMN col <type> [NULL | NOT NULL] |
	ADD [CONSTRAINT] определение_ограничения |
	DROP [CONSTRAINT] constraint_name
```
* Команды ADD & DROP могут принимать несколько параметров

Например:
```sql
ALTER TABLE Customer
ADD Address NVARCHAR(50) NULL;
```
Но если в таблице уже есть данные, нельзя будет добавить NOT NULL столбец, если для него не определено значение по умолчанию:
```sql
ALTER TABLE Customer
ADD Address NVARCHAR(50) NOT NULL DEFAULT 'Неизвестно';
```

Добавление ограничения CHECK:
```sql
ALTER TABLE Customer
ADD CHECK (Age > 21);
```
Если в таблице существуют данные не соответствующие новому ограничению, то произойдет ошибка. Чтобы в любом случае добавить ограничение нужно использовать параметр WITH NOCHECK, так как по умолчанию используется WITH CHECK.

Добавление внешнего ключа как ограничения:
```sql
ALTER TABLE Order
ADD FOREIGN KEY(CustomerID) REFERENCES Customer(ID);
-- Именованное ограничение:
ADD CONSTRAINT FK_Orders_To_Customers FOREIGN KEY(CustomerID) REFERENCES Customer(ID);
```

>Внимание! Если в таблице есть внешние ключи ссылающиеся на атрибут в другой таблице, то при изменинии или удалении этого атрибута возникнет ошибка, так как на него ссылаются внешние ключи. Чтобы убрать это ограничение, нужно убрать внешние ключи, провести необходимые изменения и затем заново установить внешние ключи.

## Удаление столбцов

```sql
ALTER TABLE Customer
DROP COLUMN Address
```

## Удаление ограничений

Чтобы удалить ограничение, нужно знать его имя. Если имя было установлено по умолчанию средой Visual Studio, то его можно посмотреть в обозревателе серверов. Ограничения ключей будут хранится в папке Keys.
```sql
ALTER TABLE Orders
DROP FK_Orders_To_Customers;
```

## Переименование столбца

SQL-запрос, который не будет работать в MS SQL Server:
```sql
ALTER TABLE my_table RENAME COLUMN col_name TO new_col_name;
```
Переименование с помощью функций (MS SQL)\*:
```sql
EXEC sp_rename 'dbo.my_table.my_col_name', 'my_new_col_name',
'COLUMN'
```

## Изменение типа столбца

```sql
ALTER TABLE [my_table]
ALTER COLUMN my_col DECIMAL (5, 2) NOT NULL -- необязательно NOT NULL
```

## Удаление данных таблицы

Оператор TRUNCATE удаляет все данные из таблицы
```sql
TRUNCATE TABLE my_table
```

Продолжение: [[Пакеты и команда GO]]

#DB #SQL #SQL/DDL