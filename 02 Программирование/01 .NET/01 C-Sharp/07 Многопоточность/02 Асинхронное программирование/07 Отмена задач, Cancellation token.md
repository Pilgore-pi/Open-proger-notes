https://metanit.com/sharp/tutorial/12.5.php

# WRITTEN WITH Claude Sonnet 4.5

Асинхронные операции могут выполняться длительное время, и часто возникает необходимость их досрочной отмены: пользователь закрыл окно, превышено время ожидания, изменились условия выполнения и т.д. Для этого в .NET предусмотрен механизм кооперативной отмены задач через **`CancellationToken`**.

> **Кооперативная отмена** означает, что задача сама проверяет, была ли запрошена отмена, и сама решает, как на это реагировать. Принудительно прервать выполнение задачи извне нельзя — это может привести к непредсказуемым последствиям (незакрытые ресурсы, поврежденные данные и т.д.)

Механизм отмены основан на двух ключевых типах из пространства имен **`System.Threading`**:

- **`CancellationTokenSource`** — источник токена отмены, управляет процессом отмены
- **`CancellationToken`** — токен отмены, передается в методы для проверки состояния отмены

## Класс `CancellationTokenSource`

**`CancellationTokenSource`** — это объект, который создает и управляет токенами отмены. Он позволяет инициировать отмену одной или нескольких связанных операций.

### Конструкторы

```csharp
CancellationTokenSource();
CancellationTokenSource(int millisecondsDelay); // автоотмена через указанное время
CancellationTokenSource(TimeSpan delay);
```

### Основные методы

| Метод                                                        | Описание                                                   |
| ------------------------------------------------------------ | ---------------------------------------------------------- |
| `Cancel()`                                                   | Инициирует запрос на отмену операций                       |
| `Cancel(bool throwOnFirstException)`                         | Отмена с возможностью выброса исключения при первой ошибке |
| `CancelAfter(int millisecondsDelay)`                         | Планирует отмену через указанное время                     |
| `CancelAfter(TimeSpan delay)`                                | Планирует отмену через указанный промежуток времени        |
| `Dispose()`                                                  | Освобождает ресурсы, используемые источником токена        |
| `CreateLinkedTokenSource(params CancellationToken[] tokens)` | Создает связанный источник токенов (статический метод)     |

### Основные свойства

| Свойство                  | Описание                                                  |
| ------------------------- | --------------------------------------------------------- |
| `Token`                   | Получает `CancellationToken`, связанный с этим источником |
| `IsCancellationRequested` | Указывает, была ли запрошена отмена                       |

## Структура `CancellationToken`

**`CancellationToken`** — это структура (value type), которая передается в асинхронные методы для проверки состояния отмены. Она не содержит методов для инициации отмены — только для проверки и реагирования на нее.

### Основные методы

| Метод                                             | Описание                                                             |
| ------------------------------------------------- | -------------------------------------------------------------------- |
| `ThrowIfCancellationRequested()`                  | Выбрасывает `OperationCanceledException`, если была запрошена отмена |
| `Register(Action callback)`                       | Регистрирует делегат, который будет вызван при отмене                |
| `Register(Action<object> callback, object state)` | Регистрирует делегат с передачей состояния                           |

### Основные свойства

| Свойство                  | Описание                                                                   |
| ------------------------- | -------------------------------------------------------------------------- |
| `IsCancellationRequested` | Возвращает `true`, если была запрошена отмена                              |
| `CanBeCanceled`           | Указывает, может ли токен быть отменен                                     |
| `WaitHandle`              | Получает `WaitHandle` для ожидания отмены                                  |
| `None`                    | Статическое свойство, возвращающее токен, который никогда не будет отменен |

## Базовые паттерны использования

### Паттерн 1: Проверка через `IsCancellationRequested`

```csharp
async Task ProcessDataAsync(CancellationToken token) {
    for (int i = 0; i < 1000; i++) {
        // Проверка запроса на отмену
        if (token.IsCancellationRequested) {
            Console.WriteLine("Операция отменена");
            return; // или break, или другая логика
        }
        
        await Task.Delay(100);
        // Выполнение полезной работы
        Console.WriteLine($"Обработка элемента {i}");
    }
}

// Использование
var cts = new CancellationTokenSource();
var task = ProcessDataAsync(cts.Token);

// Отмена через 2 секунды
await Task.Delay(2000);
cts.Cancel();

await task; // Дожидаемся завершения задачи
cts.Dispose(); // Освобождаем ресурсы
```

> Этот паттерн дает максимальный контроль над логикой отмены, но требует явных проверок в коде

### Паттерн 2: Выброс исключения через `ThrowIfCancellationRequested()`

```csharp
async Task ProcessDataAsync(CancellationToken token) {
    try {
        for (int i = 0; i < 1000; i++) {
            // Выбросит OperationCanceledException при отмене
            token.ThrowIfCancellationRequested();
            
            await Task.Delay(100, token); // token можно передать в Task.Delay
            Console.WriteLine($"Обработка элемента {i}");
        }
    } catch (OperationCanceledException) {
        Console.WriteLine("Операция была отменена");
        throw; // Перебрасываем исключение дальше
    }
}

// Использование
var cts = new CancellationTokenSource();

try {
    var task = ProcessDataAsync(cts.Token);
    await Task.Delay(2000);
    cts.Cancel();
    await task;
    
} catch (OperationCanceledException) {
    Console.WriteLine("Задача отменена на верхнем уровне");
} finally {
    cts.Dispose();
}
```

> **`OperationCanceledException`** — специальное исключение для отмены операций. Оно не считается ошибкой и обрабатывается инфраструктурой .NET особым образом

### Паттерн 3: Автоматическая отмена по таймауту

```csharp
async Task DownloadFileAsync(string url, CancellationToken token) {
    using var client = new HttpClient();
    // Передаем токен в асинхронные методы библиотек
    var response = await client.GetAsync(url, token);
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Загружено {content.Length} байт");
}

// Автоматическая отмена через 5 секунд
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

try {
    await DownloadFileAsync("https://example.com/largefile", cts.Token);
} catch (OperationCanceledException) {
    Console.WriteLine("Превышено время ожидания");
} catch (HttpRequestException ex) {
    Console.WriteLine($"Ошибка загрузки: {ex.Message}");
}
```

> Многие асинхронные методы стандартных библиотек .NET принимают **`CancellationToken`** в качестве параметра и автоматически поддерживают отмену

## Продвинутые сценарии

### Связывание нескольких токенов

Иногда нужно отменить операцию при срабатывании любого из нескольких условий:

```csharp
async Task ComplexOperationAsync(CancellationToken userToken) {
    // Создаем токен с таймаутом
    using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    
    // Связываем пользовательский токен и токен таймаута
    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
        userToken, 
        timeoutCts.Token
    );
    
    try {
        // Операция отменится либо по запросу пользователя, либо по таймауту
        await LongRunningOperationAsync(linkedCts.Token);
    } catch (OperationCanceledException) {
        if (userToken.IsCancellationRequested)
            Console.WriteLine("Отменено пользователем");
        else if (timeoutCts.Token.IsCancellationRequested)
            Console.WriteLine("Превышен таймаут");
    }
}
```

### Регистрация callback-ов при отмене

```csharp
async Task ProcessWithCleanupAsync(CancellationToken token) {
    var resource = new SomeResource();
    
    // Регистрируем действие, которое выполнится при отмене
    using var registration = token.Register(() => {
        Console.WriteLine("Выполняется очистка ресурсов...");
        resource.Dispose();
    });
    
    try {
        for (int i = 0; i < 100; i++) {
            token.ThrowIfCancellationRequested();
            await Task.Delay(100);
            await resource.ProcessAsync();
        }
    } finally {
        // Если операция завершилась нормально, очищаем ресурсы здесь
        if (!token.IsCancellationRequested)
            resource.Dispose();
    }
}
```

> Callback, зарегистрированный через **`Register()`**, выполняется синхронно в потоке, вызвавшем **`Cancel()`**. Для длительных операций очистки лучше использовать асинхронные варианты или запускать отдельную задачу

### Отмена параллельных задач

```csharp
async Task ProcessMultipleItemsAsync(List<string> items, CancellationToken token) {
    var tasks = items.Select(item => ProcessItemAsync(item, token)).ToArray();
    
    try {
        await Task.WhenAll(tasks);
    } catch (OperationCanceledException) {
        Console.WriteLine("Одна или несколько задач были отменены");
        // Task.WhenAll выбросит исключение, даже если отменена только одна задача
    }
}

async Task ProcessItemAsync(string item, CancellationToken token) {
    await Task.Delay(Random.Shared.Next(1000, 5000), token);
    token.ThrowIfCancellationRequested();
    Console.WriteLine($"Обработан: {item}");
}

// Использование
var cts = new CancellationTokenSource();
List<string> items = ["item1", "item2", "item3", "item4", "item5"];

var task = ProcessMultipleItemsAsync(items, cts.Token);

// Отменяем через 3 секунды
await Task.Delay(3000);
cts.Cancel();

try {
    await task;
} catch (OperationCanceledException) {
    Console.WriteLine("Обработка массива отменена");
}
```

## Статусы задач при отмене

При отмене задачи ее свойство **`Status`** принимает значение **`TaskStatus.Canceled`**:

```csharp
var cts = new CancellationTokenSource();
var task = Task.Run(async () => {
    await Task.Delay(5000, cts.Token);
}, cts.Token);

await Task.Delay(1000);
cts.Cancel();

try {
    await task;
} catch (OperationCanceledException) {
    Console.WriteLine($"Статус задачи: {task.Status}"); // Canceled
    Console.WriteLine($"IsCanceled: {task.IsCanceled}"); // True
    Console.WriteLine($"IsFaulted: {task.IsFaulted}");   // False
}
```

> Важно передавать токен не только внутрь метода, но и в **`Task.Run()`**, чтобы задача корректно перешла в состояние **`Canceled`**

## Лучшие практики

### 1. Всегда освобождайте ресурсы

```csharp
// ❌ Плохо
var cts = new CancellationTokenSource();
await SomeOperationAsync(cts.Token);
cts.Cancel();
// cts не освобожден

// ✅ Хорошо
using var cts = new CancellationTokenSource();
await SomeOperationAsync(cts.Token);
cts.Cancel();
// cts автоматически освободится
```

### 2. Не игнорируйте `OperationCanceledException`

```csharp
// ❌ Плохо
try {
    await OperationAsync(token);
} catch (Exception ex) { // Поглощает OperationCanceledException
    LogError(ex);
}

// ✅ Хорошо
try {
    await OperationAsync(token);
} catch (OperationCanceledException) {
    // Обрабатываем отмену отдельно
    Console.WriteLine("Операция отменена");
    throw; // Или не перебрасываем, в зависимости от логики
}catch (Exception ex)
{
    LogError(ex);
    throw;
}
```

### 3. Передавайте токен во все асинхронные вызовы

```csharp
// ❌ Плохо
async Task ProcessAsync(CancellationToken token)
{
    await Task.Delay(1000); // Токен не передан
    await File.ReadAllTextAsync("file.txt"); // Токен не передан
}

// ✅ Хорошо
async Task ProcessAsync(CancellationToken token)
{
    await Task.Delay(1000, token);
    await File.ReadAllTextAsync("file.txt", token);
}
```

### 4. Используйте `CancellationToken.None` для методов, не поддерживающих отмену

```csharp
async Task WrapperAsync(CancellationToken token = default)
{
    // Если токен не передан, используется default (эквивалентно CancellationToken.None)
    await SomeOperationAsync(token);
}

// Вызов без токена
await WrapperAsync(); // Использует CancellationToken.None
```

### 5. Проверяйте отмену в циклах и длительных операциях

```csharp
async Task ProcessLargeDatasetAsync(CancellationToken token)
{
    foreach (var item in largeDataset)
    {
        // Проверка в начале каждой итерации
        token.ThrowIfCancellationRequested();
        
        await ProcessItemAsync(item);
        
        // Или проверка в конце, если нужно завершить текущую итерацию
        if (token.IsCancellationRequested)
            break;
    }
}
```

## Особенности и подводные камни

> **`CancellationTokenSource.Cancel()`** — это синхронный метод. Все зарегистрированные callback-и выполняются в потоке, вызвавшем **`Cancel()`**. Если callback-и выполняются долго, это может заблокировать вызывающий поток

> Повторный вызов **`Cancel()`** на уже отмененном **`CancellationTokenSource`** безопасен и ничего не делает

> **`CancellationToken`** — это структура (value type), поэтому ее можно безопасно копировать и передавать по значению

> После вызова **`Dispose()`** на **`CancellationTokenSource`** нельзя использовать связанный с ним токен — это приведет к **`ObjectDisposedException`**

> Отмена — это кооперативный механизм. Если код не проверяет токен, отмена не произойдет. Нельзя принудительно остановить выполнение задачи

## Интеграция с UI (например, WPF/WinForms)

```csharp
// В UI-приложении
private CancellationTokenSource _cts;

private async void StartButton_Click(object sender, EventArgs e)
{
    _cts = new CancellationTokenSource();
    StartButton.Enabled = false;
    CancelButton.Enabled = true;
    
    try
    {
        await LongRunningOperationAsync(_cts.Token);
        MessageBox.Show("Операция завершена");
    }
    catch (OperationCanceledException)
    {
        MessageBox.Show("Операция отменена");
    }
    finally
    {
        StartButton.Enabled = true;
        CancelButton.Enabled = false;
        _cts?.Dispose();
        _cts = null;
    }
}

private void CancelButton_Click(object sender, EventArgs e)
{
    _cts?.Cancel();
}
```

> В UI-приложениях важно хранить ссылку на **`CancellationTokenSource`** на уровне класса, чтобы иметь возможность отменить операцию из обработчика события кнопки "Отмена"

## Сравнение подходов к отмене

| Подход | Преимущества | Недостатки | Когда использовать |
|--------|--------------|------------|-------------------|
| **`IsCancellationRequested`** | Полный контроль над логикой, можно выполнить очистку перед выходом | Требует явных проверок в коде | Когда нужна специфическая логика при отмене |
| **`ThrowIfCancellationRequested()`** | Простой код, автоматическое прерывание | Нужна обработка исключений | Стандартный сценарий, когда достаточно просто прервать выполнение |
| **Передача токена в библиотечные методы** | Не требует дополнительного кода | Зависит от поддержки в библиотеке | Всегда, когда метод принимает `CancellationToken` |
| **`Register()` callback** | Гарантированное выполнение при отмене | Callback выполняется синхронно | Для критичной очистки ресурсов |

Механизм **`CancellationToken`** — это стандартный и рекомендуемый способ отмены асинхронных операций в .NET. Правильное его использование делает код более отзывчивым, предсказуемым и удобным для пользователя.

Далее: [[Parallel LINQ]]

#C-Sharp #C-Sharp/Threads/Async #GENERATED
