
Класс **`Parallel`** упрощает параллельное управление кодом и является частью **TPL.**

## ==В новых версиях .NET рекомендуется использовать асинхронные аналоги этих методов==

#### `void Parallel.Invoke()`

Позволяет вызвать ряд делегатов **`Action[]`** параллельно.

```csharp
Parallel.Invoke(
  Print,
  () => {
    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
    Thread.Sleep(3000);
  },
  () => Square(5)
);

// методы Print & Square
```

#### `Parallel.For(int, int, Action)`

Цикл с блоком **`Action`** в диапазоне `[start, end)`. Каждая итерация выполняется параллельно.

#### `Parallel.ForEach<TSource>()`

Параллельный цикл по элементам **`IEnumerable<T>`**.

`ForEach<T>(IEnumerable<T>, Action<T>)`

```csharp
ParallelLoopResult result = Parallel.ForEach<int>(
  new List<int>() { 1, 3, 5, 8 },
  Square
);
 
void Square(int n)
{
  Console.WriteLine($"Выполняется задача {Task.CurrentId}");
  Console.WriteLine($"Квадрат числа {n} равен {n * n}");
  Thread.Sleep(2000);
}
```

Параллельные циклы возвращают **`ParallelLoopResult`**, у которго есть свойство **`IsCompleted`** и **`LowestBreakIteration`** — индекс, на котором произошло прерывание работы.

```csharp
ParallelLoopResult result = Parallel.For(1, 10, Square);
if (!result.IsCompleted)
  Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");
 
void Square(int n, ParallelLoopState pls)
{
  // условный выход из параллельного цикла
  if (n == 5) pls.Break();
  Console.WriteLine($"Квадрат числа {n} равен {n * n}");
  Thread.Sleep(2000);
}
```

> Все описанные функции являются синхронными

Далее: [[Отмена задач, Cancellation token]]

#C-Sharp #C-Sharp/Threads #C-Sharp/Threads/Sync