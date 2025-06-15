Вопрос: Зачем явно указывать в запросах SQL, что конкретный атрибут является внешним ключом?

Синтаксис установки внешнего ключа на уровне столбца:
```sql
my_col <type> [FOREIGN KEY] REFERENCES main_table (main_table_col)
	[ON DELETE CASCADE | NO ACTION]
	[ON UPDATE CASCADE | NO ACTION]
```
На уровне таблицы:
```sql
FOREIGN KEY (col1, col2, ..., colN) REFERENCES main_table
(main_col1, main_col2, ... main_colN)
[ON DELETE CASCADE | NO ACTION]
[ON UPDATE CASCADE | NO ACTION]
```

Пример.
```sql
CREATE TABLE Customer
(
    ID INT PRIMARY KEY IDENTITY,
    Email VARCHAR(30) UNIQUE,
    Phone VARCHAR(20) UNIQUE
);
CREATE TABLE Order
(
    ID INT PRIMARY KEY IDENTITY,
    CustomerID INT,
    CreatedAt Date,
    CONSTRAINT FK_Order_To_Customer FOREIGN KEY (CustomerID)
	    REFERENCES Customer (ID)
);
```
С помощью оператора CONSTRAINT можно задать имя для ограничения внешнего ключа. Обычно это имя начинается с префикса "FK_"

## ON DELETE & ON UPDATE

Данные команды позволяют установить действия, которые выполнятся при удалении или изменении связанной строки из главной таблицы.
* **CASCADE** — если главная строка удаляется или изменяется, зависимая строка делает тоже самое.
* **NO ACTION** — ничего не произойдет при изменениях в главной таблице.
* **SET NULL** — при удалении связанной строки из главной таблицы устанавливает для столбца внешнего ключа значение NULL. При этом важно, чтобы внешний ключ поддерживал зн-е NULL.
* **SET DEFAULT** — при удалении связанной строки из главной таблицы устанавливает для столбца внешнего ключа значение по умолчанию по атрибуту DEFAULT или NULL, если DEFAULT не определен.

По умолчанию нельзя удалить строку главной таблицы, если на нее ссылаются зависимые таблицы, пока мы не удалим связанные строки из зависимой таблицы. Можно убрать это ограничение с помощью действия CASCADE.

Продолжение: [[ALTER TABLE. Изменение таблицы]]

#DB #SQL #SQL/DDL