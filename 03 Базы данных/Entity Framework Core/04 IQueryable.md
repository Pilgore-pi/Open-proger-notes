
> Интерфейс `IQueryable` предназначен для получения данных из удаленной БД

**Определение:**

```
public interface IQueryable : System.Collections.IEnumerable
```

[Исходный код](https://github.com/dotnet/runtime/blob/9d5a6a9aa463d6d10b0b0ba6d5982cc82f363dc3/src/libraries/System.Linq.Expressions/src/System/Linq/Expressions/ExpressionVisitor.cs)

- `IEnumerable<T>.MoveNext()` позиционирует указатель записи только на следующий элемент
- `IEnumerable<T>`

#DB #Dotnet #Dotnet/EntityFramework