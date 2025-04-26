Пусть имеется таблица Ketchup с атрибутами: Title, SPEEED, Color, Volume
Выполним запрос, нажав на значок БД в обозревателе серверов:
```sql
INSERT INTO [Ketchup] (Title, SPEEED, Color, Volume)
VALUES
('Heinz', 100, 'brown', 0.25),
('Blouburt', 20, 'red', 1),
('Mayo', 200, 'white', 20)
```

Получим таблицу

![[MS_SQL_05.png]]

## Создание запроса в коде C\#

Создавать запросы позволяет класс SqlCommand.
```cs
private void ButtonClicked()
{
	SqlCommand command = new SqlCommand(
	"INSERT INTO Ketchup
	(Title, SPEEED, Color, Volume) VALUES
	('Heinz', 100, 'brown', 0.25),
	('Blouburt', 20, 'red', 1),
	('Mayo', 200, 'white', 20)",
	sqlCon); //обязательно передаем объект строки подключения
	
	//выполнение с возвращением количества задействованых строк
	command.ExecuteNonQuery();
}
```

## Создание запроса в интерфейсе программы

Допустим, у нас в программе реализованы текстовые поля для каждого атрибута и кнопка "Insert", которая должна добавлять новую запись в таблицу на основе введенных значений атрибутов.
Примерно так:
![[MS_SQL_07.png]]
Можно напрямую, через интерполяцию строк, добавлять значения текстовых полей в SqlCommand, но существует более удобный способ:
```cs
private void ButtonClicked()
{
	SqlCommand command = new SqlCommand(
	"INSERT INTO [Ketchup]
	(Title, SPEEED, Color, Volume) VALUES
	(@Title, @SPEEED, @Color, @Volume)",
	sqlCon); //обязательно передаем объект строки подключения
	
	//указание параметров в текстовом виде для вставки в код SQL
	comand.Parameters.AddWithValue("Title", textBox1.Text);
	comand.Parameters.AddWithValue("SPEEED", textBox2.Text);
	comand.Parameters.AddWithValue("Color", textBox3.Text);
	comand.Parameters.AddWithValue("Volume", textBox4.Text);
	
	command.ExecuteNonQuery();
}
```

[[Использование готовой БД]]
[Источник](https://www.youtube.com/playlist?list=PLH3y3SWteZd1oBVuI7mIGsk5psiCb4teB)

#DB #SQL/MSSQL