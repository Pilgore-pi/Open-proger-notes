# https://docs.umbraco.com/umbraco-cms/tutorials/getting-started-with-entity-framework-core

> **Entity Framework Core** -- это ORM-технология, основанная на экосистеме .NET, от компании Microsoft. Фреймворк является open-source проектом, который поддерживает несколько встроенных провайдеров для поддержки различных СУБД:

* MS SQL Server
* SQLite
* PostgreSQL

Также существуют сторонние провайдеры (например для MySQL). Полный список провайдеров можно увидеть [здесь]() (ссылка на заметку Провайдеры БД)

> ORM (object-relational mapping) -- это система сопоставления данных......

Entity Framework позволяет абстрагироваться от фундаментальных понятий БД, таких как таблицы, индексы, триггеры и так далее. Эти понятия заменяются объектами .NET, что позволяет обеспечить независимость кода логики приложения от используемой СУБД.

> Для взаимодействия с базой данных через **Entity Framework**, принято исполоьзовать язык **LINQ to entities**

> **Reverse engineering** — обратное проектирование (разработка). В контексте ORM это анализ структуры БД для автоматического создания классов

## Схема работы с Entity Framework Core

Данные, используемые ORM, можно разделить на несколько слоев:

- База данных SQL
- ООП модели SQL таблиц (.NET классы без бизнес-логики)
- Сервисы управления данными (программные модули)

### Начало работы

> Если ORM используется в проекте ASP.NET, то никаких доп. пакетов устанавливать не требуется. Если приложение создано на основе шаблона, который из коробки не содержит предустановленных пакетов EF Core, то необходимо их установить вручную через Nuget менеджер, согласно [провайдеру БД](), который используется в приложении


#### Контекст базы данных

Контекст базы данных, представляемый наследниками класса `DbContext` -- это основой API для взаимодействия с фактическими данными БД 

```csharp
using Microsoft.EntityFrameworkCore;

// Реализует IDisposable
public class ApplicationContext : DbContext
{
    // необязательная инициализация
    public DbSet<User> Users => Set<User>();
    
    // создание БД, если она не создана
    public ApplicationContext() => Database.EnsureCreated();
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Создание или использование файла .db по относительному пути
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
```

Контекст БД можно настраивать вручную, редактируя классы `DbContext`, или создавать миграции с помощью EF Core Tools. EF Core Tools предоставляет CLI через команды **`dotnet ef`** или API через PowerShell. IDE JetBrains Rider позволяет управлять миграциями через GUI (ПКМ по проекту, вкладка меню "Entity Framework Core").

EF Core Tools упрощает работу программиста, беря на себя поддержку контекстных классов `DbContext`, генерируя необходимый код.

* `bool EnsureCreated()` — создает БД или, в случае когда БД создана, но не имеет таблиц, создает таблицы в соответствии со схемой данных. Если какие-либо таблицы уже созданы, метод не оказывает никакого влияния. Возвращает `true`, если БД была создана с использованием этого метода, в противном случае — `false`

Применение `ApplicationContext`

```csharp
using var db = new ApplicationContext();

// создаем два объекта User
var tom   = new User { Name = "Tom"  , Age = 33 };
var alice = new User { Name = "Alice", Age = 26 };

// добавляем их в бд
db.Users.Add(tom);   // SQL: INSERT dfgmsldkfjg;hshfdg;
db.Users.Add(alice);
db.SaveChanges();    // фиксация транзакции
Console.WriteLine("Объекты успешно сохранены");

// получаем объекты из бд и выводим на консоль
var users = db.Users.ToList();
Console.WriteLine("Список объектов:");
foreach (User u in users)
    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
```

После этого будет сгенерирован код класса `<DB_FILE_NAME>Context : DbContext` и классы на основе таблиц SQL. Все эти классы являются `partial`. Программисту доступен минимально необходимый функционал этих классов.

#DB #Dotnet #Dotnet/EntityFramework
