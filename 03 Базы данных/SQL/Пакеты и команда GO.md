В предыдущих случаях сначала создавалась база данных, а затем в эту БД добавлялась таблица с помощью отдельных команд SQL. Но можно сразу совместить в одном скрипте несколько команд. В этом случае отдельные наборы команд называются **пакетами** (batch).

Каждый пакет состоит из одного или нескольких SQL-выражений, которые выполняются как оно целое. В качестве сигнала завершения пакета и выполнения его выражений служит команда **GO**.

Смысл разделения SQL-выражений на пакеты состоит в том, что одни выражения должны успешно выполниться до запуска других выражений. Например, при добавлении таблиц мы должны бы уверены, что была создана база данных, в которой мы собираемся создать таблицы.

Например:
```sql
CREATE DATABASE internetstore;
GO -- по завершении создания БД начинается следующий пакет
 
USE internetstore;
 
CREATE TABLE Customer
(
    ID INT PRIMARY KEY IDENTITY,
    Age INT DEFAULT 18, 
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Email VARCHAR(30) UNIQUE,
    Phone VARCHAR(20) UNIQUE
);
 
CREATE TABLE Order
(
    ID INT PRIMARY KEY IDENTITY,
    CustomerID INT,
    CreatedAt DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers (ID)
	    ON DELETE CASCADE
);
```

#DB #SQL #SQL/DDL