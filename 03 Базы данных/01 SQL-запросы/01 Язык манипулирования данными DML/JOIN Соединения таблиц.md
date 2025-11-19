# КАРТИНКИ И ТАБЛИЦЫ С ПРИМЕРАМИ

Оператор JOIN создает выборку, которая представляет собой соединение таблиц. Соединение происходит по совпадающему атрибуту. Соединение бывает разных видов (AND, OR, XOR...)

Синтаксис:
```sql
select * from table_1 join table_2
on table_1.attrubute = table_2.attrubute
[where table_1.attribute is 'something']
```

Множественное совпадение атрибутов:

```sql
select * from table_1 join table_2
on table_1.attrubute1 = table_2.attrubute1 and
table_1.attribute2 = table_2.attribute2 [and ...]
```

Соединение нескольких таблиц:

```sql
select table_1.attr1, table_2.attr4, table_3.attr1 from
table_1 join table_2 on table_1.attr2 = table_2.attr3
join table_3 on table_2.attr4 = table_3.attr1
```

обобщить:

```sql
select [Session].ID, [Session].UserLogin, [Session].CreationDate, [Session].TestAuthorLogin
from [Session] join [Question] join [User_answer]
on [Question].ID = [User_answer].QuestionID
on [Session].ID = [User_answer].SessionID
where [Session].IsFinished = 1 and [Session].UserLogin = 'Ura8'
group by [Session].ID, [Session].UserLogin, [Session].CreationDate, [Session].TestAuthorLogin
```

## Типы соединений

| тип         | описание              |
| ----------- | -------------------------------------------------- |
| Inner join  | Множество записей, которые встречаются в обоих таблицах |
| Left join   | Множество всех записей левой таблицы и множество записей, встречающихся в обеих таблицах |
| Outher join | выводит все записи из обеих таблиц. Другими словами Left join объединяется с Right join. Записей может быть больше, чем в двух таблицах вместе |
| Cross join  | Декартово произведение записей из двух таблиц (каждый с каждым) |

### Cross join

```sql
select * from table_1 cross join table_2
```

### Outer join

```sql

```

#DB #SQL
