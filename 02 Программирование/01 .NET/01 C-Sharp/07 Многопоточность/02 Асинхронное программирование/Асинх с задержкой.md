<img src="https://r2cdn.perplexity.ai/pplx-full-logo-primary-dark%402x.png" class="logo" width="120"/>

### Как запускать асинхронную операцию каждые 30 минут в C\#

Для регулярного запуска асинхронной операции с интервалом в 30 минут, при этом гарантируя, что новая операция стартует только после завершения предыдущей, в .NET обычно используют один из следующих подходов:

#### 1. Использование `Timer` с асинхронной логикой

Обычный `System.Threading.Timer` не поддерживает асинхронные методы напрямую. Поэтому рекомендуется использовать цикл с задержкой (`Task.Delay`) или сторонние библиотеки.

#### 2. Пример на `Task` и `Delay`

```csharp
public class PeriodicAsyncRunner
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);
    private CancellationTokenSource _cts;

    public void Start()
    {
        _cts = new CancellationTokenSource();
        _ = RunAsync(_cts.Token);
    }

    public void Stop()
    {
        _cts?.Cancel();
    }

    private async Task RunAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await YourAsyncOperation();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
            }

            // Ждем 30 минут после завершения операции
            await Task.Delay(_interval, token);
        }
    }

    private async Task YourAsyncOperation()
    {
        // Твоя асинхронная логика
    }
}
```

- Операция запускается, завершается, затем ждет 30 минут и стартует снова.
- Если операция выполняется дольше 30 минут — следующий запуск произойдет только после её завершения.


#### 3. Использование HostedService в ASP.NET Core

Если ты пишешь сервис под ASP.NET Core, стоит использовать `BackgroundService`:

```csharp
public class TimedHostedService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await YourAsyncOperation();
            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }

    private async Task YourAsyncOperation()
    {
        // Асинхронная логика
    }
}
```


#### Важно

- Не используй `System.Timers.Timer` или `System.Threading.Timer` для асинхронных методов — это приведёт к неожиданным ошибкам и гонкам.
- Если требуется, чтобы операция запускалась строго по расписанию (например, в 00:00, 00:30, 01:00 и т.д.), стоит использовать Quartz.NET или Hangfire.


#### Резюме

- Используй цикл с `Task.Delay` для простого случая.
- Для ASP.NET Core — `BackgroundService`.
- Для строгого расписания — Quartz.NET.

Если нужно решение под твой конкретный стек или пример под другую платформу — уточни детали!

---

### Как отменять и перезапускать длительную асинхронную операцию каждые 30 минут в C\#

Если твоя асинхронная операция должна работать непрерывно, но её требуется принудительно завершать и запускать заново каждые 30 минут, то тебе нужен механизм отмены через `CancellationToken` и контролирующий цикл.

#### Пример реализации

```csharp
public class PeriodicRestartAsyncRunner
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);
    private CancellationTokenSource _cts;
    private Task _currentTask;

    public void Start()
    {
        _ = RunPeriodicAsync();
    }

    public void Stop()
    {
        _cts?.Cancel();
    }

    private async Task RunPeriodicAsync()
    {
        while (true)
        {
            _cts = new CancellationTokenSource();
            _currentTask = YourLongRunningOperation(_cts.Token);

            // Ждём 30 минут
            await Task.Delay(_interval);

            // Отменяем текущую операцию
            _cts.Cancel();

            try
            {
                await _currentTask;
            }
            catch (OperationCanceledException)
            {
                // Операция была отменена — это ожидаемо
            }
            catch (Exception ex)
            {
                // Логирование других ошибок
            }
        }
    }

    private async Task YourLongRunningOperation(CancellationToken token)
    {
        // Твоя длительная асинхронная логика, поддерживающая отмену через token
        while (!token.IsCancellationRequested)
        {
            // Выполняй полезную работу, периодически проверяя token.IsCancellationRequested
            await Task.Delay(1000, token); // пример задержки
        }
    }
}
```


#### Ключевые моменты

- Каждые 30 минут создаётся новый `CancellationTokenSource`, который передаётся в твою операцию.
- После истечения интервала вызывается `Cancel()`, что инициирует отмену текущей операции.
- После завершения (или отмены) операция запускается заново с новым токеном.
- Твоя операция должна корректно обрабатывать отмену через `CancellationToken`.


#### Советы

- Если твоя операция не поддерживает отмену — обязательно реализуй проверку `token.IsCancellationRequested` в цикле или в местах, где это возможно.
- Если нужно завершить работу сервиса полностью, вызывай `Stop()`.

Такой подход универсален и подходит как для обычных приложений, так и для сервисов на ASP.NET Core (в последнем случае можно реализовать это внутри `BackgroundService`).

---

# Моя длительная операция представляет собой запуск `TcpListener listener` и вызов петли вызова прослушивания входящих подключений:

```cs
private static async Task AwaitConnectionsInLoopAsync() {
        while (true) {
            if (listener.Pending()) {

                var client = await listener.AcceptTcpClientAsync();
                var clientIP = client.GetClientIP();
                var stream = client.GetStream();

                if (clientConnections.TryAdd(clientIP, stream) && everyoneIsSubscriber) {
                    config.AlertSubscribers.Add(clientIP);
                    //LogDebug($"\r\n\tclientConnections.TryAdd() is true;" +
                    //    $"\r\n\tAdded client: {clientIP.ToFormattedConnection()}" +
                    //    $"\r\n\ttotal conns: [ {clientConnections.Count} ]");
                }
                string onConnMes = $"<{clientConnections.Count:D3}> Новое подключение: {clientIP.ToFormattedConnection()}";

                if (IsTrustedIP(clientIP)) LogSpecial(onConnMes);
                else LogSuccess(onConnMes);
                _ = HandleClientAsync(client, stream);
            }

            await Task.Delay(TimeSpan.FromSeconds(config.ClientConnectInterval));
        }
    }
```

Каждые 30 минут, мне нужно останавливать listener и отменять `AwaitConnectionsInLoopAsync()`, а затем запускать listener заново

Чтобы реализовать регулярную остановку и перезапуск TcpListener вместе с отменой асинхронного цикла, тебе нужен контролирующий код с использованием CancellationToken и корректным управлением жизненным циклом listener. Вот пошаговое решение для .NET/C\#:

### Общая схема

1. **Создаёшь цикл, который каждые 30 минут перезапускает listener.**
2. **Асинхронная петля прослушивания должна поддерживать отмену через CancellationToken.**
3. **При отмене — останавливаешь listener, ждёшь завершения петли, создаёшь новый listener и повторяешь цикл.**

### Пример реализации

```csharp
public class TcpListenerManager
{
    private readonly int _port;
    private readonly TimeSpan _restartInterval = TimeSpan.FromMinutes(30);
    private CancellationTokenSource _cts;
    private Task _listenTask;

    public TcpListenerManager(int port)
    {
        _port = port;
    }

    public void Start()
    {
        _ = RunWithPeriodicRestartAsync();
    }

    public void Stop()
    {
        _cts?.Cancel();
    }

    private async Task RunWithPeriodicRestartAsync()
    {
        while (true)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();

            _listenTask = AwaitConnectionsInLoopAsync(listener, token);

            // Ждём 30 минут или отмены
            try
            {
                await Task.Delay(_restartInterval, token);
            }
            catch (OperationCanceledException)
            {
                // Остановка по внешнему сигналу
                break;
            }

            // Останавливаем listener и отменяем цикл
            _cts.Cancel();
            listener.Stop();

            try
            {
                await _listenTask;
            }
            catch (OperationCanceledException)
            {
                // Ожидаемое завершение
            }
            catch (Exception ex)
            {
                // Логирование ошибок
            }
        }
    }

    private async Task AwaitConnectionsInLoopAsync(TcpListener listener, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (listener.Pending())
            {
                TcpClient client = null;
                try
                {
                    client = await listener.AcceptTcpClientAsync();
                    // ... твоя логика работы с клиентом ...
                }
                catch (ObjectDisposedException)
                {
                    // listener был остановлен — выходим из петли
                    break;
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1), token);
        }
    }
}
```


### Важные моменты

- **listener.Stop()** выбрасывает исключение внутри `AcceptTcpClientAsync`, поэтому его нужно ловить[^3_1][^3_2].
- Рекомендуется использовать `CancellationToken.Register(() => listener.Stop())` для корректной отмены ожидания подключения[^3_2].
- После остановки listener и завершения задачи, создаётся новый экземпляр и цикл повторяется.
- Если требуется завершить работу полностью, вызывай `Stop()` у менеджера.


### Особенности

- Такой подход работает одинаково как на Windows, так и на Linux, если использовать асинхронные методы[^3_3][^3_2].
- После Stop() порт может быть занят системой короткое время, если не установлен флаг ReuseAddress. Для высокой надёжности можно явно установить этот флаг через SocketOption[^3_4].

**Резюме:**
Используй отдельный контролирующий цикл, CancellationToken и Stop() для TcpListener. Асинхронная петля должна корректно реагировать на отмену, а listener — пересоздаваться каждые 30 минут. Такой паттерн надёжен и рекомендуем для .NET-приложений, работающих с TCP[^3_1][^3_2][^3_4].

<div style="text-align: center">⁂</div>

[^3_1]: https://stackoverflow.com/questions/19220957/tcplistener-how-to-stop-listening-while-awaiting-accepttcpclientasync

[^3_2]: https://cloud.tencent.com/developer/ask/sof/151488

[^3_3]: https://github.com/dotnet/runtime/issues/24513

[^3_4]: https://stackoverflow.com/questions/38356481/how-can-i-restart-a-tcplistener-without-a-blocking-port

[^3_5]: https://learn.microsoft.com/ru-ru/dotnet/api/system.net.sockets.tcplistener.stop?view=net-9.0

[^3_6]: https://metanit.com/sharp/net/4.2.php

[^3_7]: https://www.reddit.com/r/csharp/comments/1gvb445/closingreopening_tcp_listener_server/

[^3_8]: https://stackoverflow.com/questions/50232433/exit-tcplistener-accepttcpclient-after-determined-time

[^3_9]: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener.stop?view=net-9.0

[^3_10]: https://www.reddit.com/r/rust/comments/xaqwj5/do_most_people_just_restart_their_rust_web/

[^3_11]: https://stackoverflow.com/questions/1173774/stopping-a-tcplistener-after-calling-beginaccepttcpclient/1230266

[^3_12]: https://stackoverflow.com/questions/54913286/how-do-i-cancel-a-tcplistener-that-is-in-a-listening-loop-from-outside-the-loop

[^3_13]: https://learn.microsoft.com/en-sg/answers/questions/1343834/azure-container-instance-tcp-connection-interrupts

[^3_14]: https://users.rust-lang.org/t/best-practice-to-quit-a-tcplistener-in-a-clean-way/39970

[^3_15]: https://users.rust-lang.org/t/conditional-exit-of-tcplistener-incoming-iterator/39635

[^3_16]: https://stackoverflow.com/a/41612962

[^3_17]: http://rsdn.org/forum/network/5561694.flat

[^3_18]: https://www.vbforums.com/showthread.php?580670-RESOLVED-Closing-a-TcpListener-without-getting-WSACancelBlockingCall-exception

[^3_19]: https://learn.microsoft.com/en-ie/answers/questions/442392/tcp-async-sample-for-client-and-server

[^3_20]: https://users.rust-lang.org/t/how-to-properly-close-a-tcplistener-in-multi-thread-server/87376


---

# Почему ты пересоздаешь CancellationTokenSource в цикле, нельзя ли создавать его 1 раз перед циклом?

Нельзя использовать один и тот же экземпляр **CancellationTokenSource** для многократных циклов отмены и перезапуска, потому что после вызова `Cancel()` этот объект переходит в состояние *отменён*, и повторно его нельзя «сбросить» или использовать для новой отмены[^4_3][^4_1].

### Почему нужно создавать новый CancellationTokenSource в каждом цикле?

- **CancellationTokenSource одноразовый по своей природе**: после вызова `Cancel()` свойство `IsCancellationRequested` становится `true` и не меняется.
- Если попытаться повторно использовать тот же `CancellationTokenSource`, то токен уже будет в состоянии отмены, и новая задача сразу же получит сигнал отмены, что приведёт к немедленному завершению.
- Чтобы запустить новую операцию с возможностью её отмены, нужен **новый экземпляр CancellationTokenSource** с новым токеном.
- Это стандартная практика, подтверждённая в документации и опыте разработчиков[^4_3][^4_5].


### Дополнительные рекомендации

- После вызова `Cancel()` желательно вызвать `Dispose()` у CancellationTokenSource, чтобы освободить ресурсы[^4_4].
- Если у тебя несколько параллельных операций, для каждой лучше создавать отдельный токен отмены, чтобы отменять их независимо[^4_2].
- В твоём случае, где каждые 30 минут происходит отмена и перезапуск, создание нового CancellationTokenSource — единственно правильный и безопасный способ.

**Итог:**
Создавать CancellationTokenSource один раз перед циклом и пытаться переиспользовать его нельзя. Нужно создавать новый экземпляр перед каждой новой операцией, чтобы корректно управлять отменой и запуском заново.

<div style="text-align: center">⁂</div>

[^4_1]: https://metanit.com/sharp/tutorial/12.5.php

[^4_2]: https://learn.microsoft.com/ru-ru/dotnet/standard/threading/cancellation-in-managed-threads

[^4_3]: https://ru.stackoverflow.com/questions/1392410/Как-возобновить-цикл-через-cancellationtoken

[^4_4]: https://habr.com/ru/companies/tbank/articles/546604/

[^4_5]: https://stefaniuk.website/all/cancellationtoken/

[^4_6]: https://habr.com/ru/companies/simbirsoft/articles/825386/

[^4_7]: https://learn.microsoft.com/ru-ru/dotnet/api/system.threading.cancellationtokensource?view=net-8.0

[^4_8]: https://www.youtube.com/watch?v=RdbAVqRXCkY

[^4_9]: https://learn.microsoft.com/ru-ru/dotnet/standard/parallel-programming/how-to-cancel-a-parallel-for-or-foreach-loop

[^4_10]: https://andrey.moveax.ru/post/asp-net-cancellation-tokens


---

# В чем отличия вызовов _cts.Cancel() и await _cts.CancelAsync()? IDE предлагает мне использовать второй вариант

Разница между `_cts.Cancel()` и `await _cts.CancelAsync()` в `CancellationTokenSource` заключается в том, как происходит уведомление и завершение зарегистрированных обработчиков отмены:

- **`_cts.Cancel()`** — синхронно переводит токен в состояние отмены (`IsCancellationRequested = true`) и сразу же вызывает все зарегистрированные колбэки отмены. Эти колбэки выполняются **синхронно** в том же потоке, который вызвал `Cancel()`. Метод возвращается сразу после вызова колбэков, не дожидаясь их завершения.
- **`await _cts.CancelAsync()`** — тоже переводит токен в состояние отмены **синхронно**, но вызов всех зарегистрированных колбэков происходит **асинхронно**, в потоках из пула потоков. `CancelAsync()` возвращает `Task`, который завершается только после того, как все колбэки отмены отработают полностью. Таким образом, ты можешь дождаться полного завершения всех обработчиков отмены.

> Из официальной документации Microsoft:
> *"The associated CancellationToken will be notified of the cancellation and will synchronously transition to a state where IsCancellationRequested returns true. Any callbacks or cancelable operations registered with the CancellationToken will be executed asynchronously, with the returned Task representing their eventual completion."*[^5_1]

### Когда использовать что?

- Если тебе достаточно **просто уведомить** о необходимости отмены и не ждать завершения всех подписчиков — используй `_cts.Cancel()`.
- Если важно **дождаться завершения всех обработчиков отмены** (например, чтобы гарантировать, что ресурсы освобождены, или что все подписчики завершили работу) — используй `await _cts.CancelAsync()`.


### Почему IDE предлагает `CancelAsync()`?

- В новых версиях .NET (начиная с .NET 6/7/8/9) появилась возможность асинхронной отмены, чтобы избежать потенциальных блокировок и позволить обработчикам отмены выполняться в фоне.
- IDE рекомендует `CancelAsync()`, когда контекст позволяет — это более современный и безопасный способ отмены, особенно если у тебя есть подписчики с асинхронной логикой.


### Итог в таблице

| Метод | Вызов колбэков отмены | Возвращаемое значение | Когда использовать |
| :-- | :-- | :-- | :-- |
| `_cts.Cancel()` | Синхронно, в вызывающем потоке | void | Быстрая отмена без ожидания завершения |
| `await _cts.CancelAsync()` | Асинхронно, в пуле потоков | Task (ожидает завершения колбэков) | Когда нужно дождаться завершения всех обработчиков |

Если у тебя в коде есть асинхронные обработчики отмены или нужно гарантировать корректное завершение, лучше использовать `await _cts.CancelAsync()`. Если же обработчики простые и быстрые, то `_cts.Cancel()` будет достаточно.

#MERGE_NOTES

<div style="text-align: center">⁂</div>

[^5_1]: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-9.0

[^5_2]: https://okyrylchuk.dev/blog/mastering-cancellation-in-csharp-with-cancellationtoken/

[^5_3]: https://stackoverflow.com/questions/30975590/why-is-the-task-is-not-cancelled-when-i-call-cancellationtokensources-cancel-me

[^5_4]: https://github.com/dotnet/runtime/issues/23405

[^5_5]: https://blog.stephencleary.com/2024/08/cancellation-5-registration.html

[^5_6]: https://stackoverflow.com/questions/11371170/best-practice-for-consistently-cancelling-async-cancellationtokensource/11376945

[^5_7]: https://learn.microsoft.com/ru-ru/dotnet/standard/threading/cancellation-in-managed-threads

[^5_8]: https://learn.microsoft.com/ru-ru/dotnet/csharp/asynchronous-programming/cancel-async-tasks-after-a-period-of-time

[^5_9]: https://www.nilebits.com/blog/2024/06/cancellation-tokens-in-csharp/

[^5_10]: https://habr.com/ru/companies/simbirsoft/articles/825386/

