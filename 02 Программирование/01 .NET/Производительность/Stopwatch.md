Самый оптимальный способ измерения затраченного времени:
```csharp
long stamp = Stopwatch.GetTimestamp();
//code
long result = Stopwatch.GetElapsedTime(stamp)
```

Методы:
* static GetTimeStamp() (`.Net 7`) — возвращает текущее время в тиках. Чтобы узнать разницу во времени, нужно найти разницу между двумя метками.
* static GetElapsedTime() (`.Net 7`) — разница между текущим временем и указанной меткой. Или разница между 2 метками.
* static Stopwatch StartNew() — создает запущенный секундомер
* Start() — запуск секундомера
* Stop()
* Restart() — остановка, обнуление и запуск секундомера
* Reset() — обнуление секундомера без остановки

Когда секундомер запущен, его внутреннее поле, хранящее время, обновляется каждый тик.

>1 x nanosecond = 100 x DateTime.Tick;
>Stopwatch tick = 1 sec / Stopwatch.Frequency (обычно `10_000_000`, то есть 1 nanoseconds = 100 ticks)

Свойства:
* Elapsed — TimeSpan
* ElapsedMilliseconds — прошло миллисекунд
* ElapsedTicks — прошло тиков
* IsRunning

Создание экземпляра секундомера затрачивает ресурсы. Если измерений много, будет выделено много памяти. Существует более производительный ValueStopwatch, но метод GetTimeStamp() позволяет вообще не выделять памяти.

#Performance