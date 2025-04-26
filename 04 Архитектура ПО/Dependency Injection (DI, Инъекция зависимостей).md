Это паттерн проектирования, который позволяет уменьшить связанность между компонентами системы, обеспечивая их независимость.

> Зависимость — это некий объект, от которого зависит текущий объект. К примеру объект `Компьютер` содержит следующие зависимости:

- `Процессор`
- `Видеокарта`
- `ОЗУ`
- `Жесткий диск`
- `Блок питания`
- `Материнская плата`

Далее рассмотрим пример без применения инъекции зависимостей и вместе с ней.

Без инъекций:

```csharp
public class Computer
{
    public Computer()
    {   // Все поля инициализированы явно (хардкод)
        _processor = new Processor(4);
        _graphicsCard = new GraphicsCard(2048, 6);
        _ram = new RAM(32);
        _rom = new ROM(1024);
        _powerUnit = new PowerUnit(700);
        _motherboard = new MotherBoard("Intel 234799712");
    }
    
    private Processor _processor;
    private GraphicsCard _graphicsCard;
    private RAM _ram;
    private ROM _rom;
    private PowerUnit _powerUnit;
    private MotherBoard _motherboard;
}
```

Создание инъекций:

```csharp
public class Computer
{
    // Инъекция зависимостей через использование параметров конструктора 
    public Computer(cpu, gpu, ram, rom, pu, mb)
    {
        _processor = cpu;
        _graphicsCard = gpu;
        _ram = ram;
        _rom = rom;
        _powerUnit = pu;
        _motherboard = mb;
    }
    
    private Processor _processor;
    private GraphicsCard _graphicsCard;
    private RAM _ram;
    private ROM _rom;
    private PowerUnit _powerUnit;
    private MotherBoard _motherboard;
}
```

Вместо того чтобы создавать зависимости внутри класса, они передаются извне, что упрощает тестирование и замену компонентов. DI открывает возможность использование подделок (fakes) при тестировании. То есть можно просто задать в качестве инъекции зависимости заглушку, которая не имеет значение на текущий этап тестирования

> Суть инъекции зависимостей заключается в вынесении зависимостей "наверх", во внешний код, чтобы соблюдался принцип единственной ответственности — каждый модуль отвечает только за одну функциональность

Пример

Имеется метод `sendData()`, который пересылает данные в хранилище или определенному пользователю. Метод выполняет различный алгоритм, в зависимости от передаваемых параметров

```csharp
public class DataSendingParameters
{
    public string? URL { get; set; }                // if server is used
    public string? DbConnectionString { get; set; } // if data base is used
    public string? Username { get; set; }
    public string? DescriptionMessage { get; set; }
}

void sendData(DataObject data, DataSendingParameters parameters)
{
    if (parameters.URL is not null){
        ApiSender.Send(URL, data);
    } else if (parameters.DbConnectionString is not null) {
        DbHandler connection = new(parameters.DbConnectionString);
        connection.CustomerData.Add(data);
    } else if (parameters.Username is not null) {
        HttpService.Send(data, parameters.Username, parameters.DescriptionMessage);
    }
}
```

Сделаем инъекцию зависимостей. Теперь класс `DataSendingParameters` не нужен

```csharp
void sendData(DataObject data)
{
    // static variable containing IStorage instance
    Storage.Receive(data);
}

/// ... ///

public interface IStorage {
    void Receive(DataObject data);
}

public class ApiSender : IStorage { ... }
public class DbHandler : IStorage { ... }
public class HttpService : IStorage { ... }

```

Теперь `sendData()` не зависит от того, куда и каким образом будут отосланы данные. Куда именно будет отправлено сообщение определяет значение статической переменной `Storage`, а реализация конкретной логики вынесена в сами классы, представляющие хранилища данных

## Преимущества инъекции зависимостей

- **Упрощение тестирования**: Позволяет легко подменять зависимости на моки или стабы при тестировании.
- **Снижение связанности**: Компоненты становятся менее зависимыми друг от друга, что упрощает их замену и модификацию.
- **Улучшение читаемости кода**: Ясно видно, какие зависимости необходимы классу, что делает код более понятным.

Существует 3 типа инъекций зависимости:

- Инъекция через конструктор (Constructor injection). Зависимости выносятся в конструктор типа, что делает этот тип независимым от реализации других компонентов.

- Инъекция через сеттер (Setter injection). Зависимости задаются не при инициализации экземпляра типа, а при вызове сеттера. В сеттер передаются внешние зависимости

- Инъекция через интерфейс (Interface injection). Зависимости, выполняющие одну роль, абстрагируются, реализуя единый интерфейс

## Inversion of control (IoC)

Паттерн проектирования, суть которого заключается в частичной передаче управления над приложением фреймворку или контейнеру. Под частичным управлением подразумевается перекладывание ответственности за инициализацию и проверку объектов на фреймворк, что позволяет повышает уровень абстракции над зависимостями.

Dependency Injection является реализацией этого паттерна

#Architecture #Patterns #Patterns/Dependency_injection