Все действия будут выполняться в IDE Rider. В целом, нет больших отличий от Visual Studio

## Создание проекта с тестами

`New project...` > `Unit test` > `Type = xUnit` > `Create`

По умолчанию будут предустановлены 4 пакета Nuget (информация актуальна на 01.08.2025):

```
coverlet.collector/6.0.2
C:\Users\andre\.nuget\packages\microsoft.net.test.sdk\17.11.1
```

```csharp

```

## События "перед тестом" и "после теста"

В NUnit действие "перед каждым тестом" помечается атрибутом `[SetUp]`, а действие "после каждого теста" атрибутом `[Teardown]`. В xUnit атрибуты использовать не нужно, используются существующие средства C\#

```csharp
using Xunit;
using Xunit.Abstractions;

namespace Example;

public class ExampleServiceTests : IDisposable {
    
    private readonly ExampleService _sut = new();
    
    private readonly ITestOutputHelper _output;
    
    public ExampleServiceTests(ITestOutputHelper output) {
        _output = output;
        _output.WriteLine("Before test");
    }
    
    [Fact] public void Guid_IsNotEmpty(){
        var id = _sut.Id;
        Assert.NotEqual(Guid.Empty, id);
        _output.WriteLine(id.ToString());
    }
    
    //...
    
    public void Dispose() {
        _output.WriteLine("After test");
    }
}
```

Асинхронный вариант:

```csharp
public class ExampleServiceTests : IAsyncLifetime {
    
    private readonly ExampleService _sut = new();
    
    private readonly ITestOutputHelper _output;
    
    public async Task InitializeAsync(ITestOutputHelper output) {
        _output = output;
        _output.WriteLine("Before test");
    }
    
    [Fact] public void Guid_IsNotEmpty(){
        var id = _sut.Id;
        Assert.NotEqual(Guid.Empty, id);
        _output.WriteLine(id.ToString());
    }
    
    //...
    
    public async Task DisposeAsync() {
        _output.WriteLine("After test");
    }
    
}
```

#Testing #Testing/Unit_testing