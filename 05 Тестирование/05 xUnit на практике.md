
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
    
}
```

Асинхронный вариант:

```csharp
public class ExampleServiceTests : IAsyncLifetime {
    
    private readonly ExampleService _sut = new();
    
    private readonly ITestOutputHelper _output;
    
    public ExampleServiceTests(ITestOutputHelper output) {
        _output = output;
        _output.WriteLine("Before test");
    }
    
    [Fact] public void Guid_IsNotEmpty(){
        var id = _sut.Id;
    }
    
}
```

#Testing #Testing/Unit_testing