
> Интерфейс `IQueryable` предназначен для получения данных из удаленной БД

**Определение:**

```
public interface IQueryable        : System.Collections.IEnumerable
public interface IQueryable<out T> : System.Collections.Generic.IEnumerable<out T>, System.Linq.IQueryable
```

[Исходный код](https://github.com/dotnet/runtime/blob/9d5a6a9aa463d6d10b0b0ba6d5982cc82f363dc3/src/libraries/System.Linq.Expressions/src/System/Linq/Expressions/ExpressionVisitor.cs)

> При вызове методов LINQ создается запрос. Его непосредственное выполнение происходит, когда только, когда происходит обращение к результату этого запроса.

Нередко это происходит при переборе результата запроса в цикле `for` или при применении к нему ряда методов - `ToList()` или `ToArray()`, а также если запрос представляет скалярное значение, например, метод `Count()`

В процессе выполнения запросов **LINQ to Entities** мы может получать два объекта, которые предоставляют наборы данных: `IEnumerable` и `IQueryable`. Хоть `IQueryable` наследуется от `IEnumerable`, `IQueryable` имеет другую функциональность и несовместим, в общем случае, с `IEnumerable`

- `IEnumerable<T>.MoveNext()` позиционирует указатель записи только на следующий элемент
- `IEnumerable<T>`

### Особенности IEnumerable\<T\>

**Назначение**: Используется для работы с коллекциями данных в памяти. Это базовый интерфейс для всех коллекций, которые можно перебирать (например, массивы, списки, словари и т.д.)
   
- Выполняет **ленивую** загрузку данных (Lazy Loading).
- Работает с данными, которые уже загружены в память.
- Подходит для работы с небольшими объемами данных.
- Не поддерживает построение запросов на уровне источника данных (например, базы данных).
- Все операции выполняются в памяти, что может быть неэффективно для больших наборов данных.

**Пример использования**:

```csharp
using System.Collections.Generic;
using System.Linq;

class Program {
    static void Main()
    {
        
        IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Фильтрация данных
        IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);
        
        // Перебор элементов
        foreach (var number in evenNumbers){
            Console.WriteLine(number); // Вывод: 2, 4
        }
    }
}
```

### Особенности IQueryable\<T\>

**Назначение**: Используется для построения и выполнения запросов к удаленным источникам данных (например, базам данных, веб-сервисам и т.д.).

- Выполняет **отложенное выполнение** (Deferred Execution).
- Поддерживает построение запросов на уровне источника данных (например, SQL-запросы для базы данных).
- Подходит для работы с большими объемами данных, так как запросы выполняются на стороне источника данных.
- Позволяет оптимизировать запросы, минимизируя объем передаваемых данных.

**Пример использования**:
  
```cs
using System.Linq;
using System.Data.Entity;

class Program {
    static void Main()
    {
        using var context = new MyDbContext();
        
        // IQueryable позволяет построить запрос к базе данных
        IQueryable<User> usersQuery = context.Users.Where(u => u.Age > 18);
        
        // Запрос выполняется только при переборе данных
        foreach (var user in usersQuery) {
            Console.WriteLine(user.Name);
        }
    }
}

// Пример контекста базы данных
public class MyDbContext : DbContext {
    public DbSet<User> Users { get; set; }
}

public class User {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Основные различия между `IEnumerable` и `IQueryable`

#DB #Dotnet #Dotnet/EntityFramework