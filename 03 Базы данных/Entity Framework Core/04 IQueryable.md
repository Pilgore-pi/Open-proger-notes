
> Интерфейс `IQueryable` предназначен для получения данных из удаленной БД

**Определение:**

```
public interface IQueryable        : System.Collections.IEnumerable
public interface IQueryable<out T> : System.Collections.Generic.IEnumerable<out T>, System.Linq.IQueryable
```

[Исходный код](https://github.com/dotnet/runtime/blob/9d5a6a9aa463d6d10b0b0ba6d5982cc82f363dc3/src/libraries/System.Linq.Expressions/src/System/Linq/Expressions/ExpressionVisitor.cs)

> При вызове методов LINQ создается запрос. Его непосредственное выполнение происходит, когда только, когда происходит обращение к результату этого запроса.

Нередко это происходит при переборе результата запроса в цикле `for` или при применении к нему ряда методов - `ToList()` или `ToArray()`, а также если запрос представляет скалярное значение, например, метод `Count()`

В процессе выполнения запросов **LINQ** to Entities мы может получать два объекта, которые предоставляют наборы данных: IEnumerable и IQueryable

- `IEnumerable<T>.MoveNext()` позиционирует указатель записи только на следующий элемент
- `IEnumerable<T>`

#DB #Dotnet #Dotnet/EntityFramework