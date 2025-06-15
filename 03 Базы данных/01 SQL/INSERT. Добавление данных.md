Добавление данных в таблицу осуществляется командой INSERT.

Синтаксис:
```sql
INSERT [INTO] table_name [(<attributes>)]
VALUES (<new_values>)[, (<new_values>)]
```

* `<attributes>` — должны быть перечислены все поля таблицы через запятую, у которых нет значения по умолчанию.
* VALUES — можно добавлять неограниченное количество записей.

Пример:

Пусть имеется база данных:
```sql
CREATE DATABASE productdb;
GO
USE productdb;
CREATE TABLE Product
(
    ID INT IDENTITY PRIMARY KEY,
    ProductName NVARCHAR(30) NOT NULL,
    Manufacturer NVARCHAR(20) NOT NULL,
    ProductCount INT DEFAULT 0,
    Price MONEY NOT NULL
)
```
Добавление 1-й записи:
```sql
INSERT Product VALUES ('iPhone 7', 'Apple', 5, 52000)
```
Значения после VALUES добавляются по порядку

Если опустить список атрибутов для добавления значений, то по умолчанию будут выбраны все атрибуты, кроме атрибутов с автоинкрементом, а порядок будет определяться порядком добавления атрибутов при создании таблицы.

Можно добавлять значения в другом порядке при указанном кортеже атрибутов:
```sql
INSERT INTO Product (ProductName, Price, Manufacturer) 
VALUES ('iPhone 6S', 41000, 'Apple')
```

* Для неуказанных столбцов будет добавляться значение по умолчанию, если задан атрибут DEFAULT, или значение NULL. При этом неуказанные столбцы должны допускать значение NULL или иметь атрибут DEFAULT.

Если все столбцы имеют атрибут DEFAULT, определяющий значение по умолчанию, или допускают значение NULL, то можно для всех столбцов вставить значения по умолчанию:
```sql
INSERT INTO Product DEFAULT VALUES
```

Продолжение: [[SELECT. Выборка данных]]

#DB #SQL #SQL/DML