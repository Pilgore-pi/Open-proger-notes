```csharp
public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
{
    return source.Select((item, index) => (item, index));
}
```

```csharp
foreach (var (item, index) in collection.WithIndex())
{
    DoSomething(item, index);
}

// Или

foreach (var (item, index) in collection.Select((x, i) => (x, i)))
{
    DoSomething(item, index);
}
```

> **Предупреждение**: такой подход имеет меньшую производительность, по сравнению с обычным циклом for и foreach

#C-Sharp #Utils #C-Sharp/Cycles