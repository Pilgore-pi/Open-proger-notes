
```csharp

```

## События "перед тестом" и "после теста"

В NUnit действие "перед каждым тестом" помечается атрибутом `[SetUp]`, а действие "после каждого теста" атрибутом `[Teardown]`. В xUnit атрибуты использовать не нужно, используются существующие средства C\#

```csharp
using Xunit;
using Xunit.Abstractions;

namespace Example;

public class ExampleServiceTests : IDisposable {
    
    public ExampleServiceTests(ITestOutputHelper output) {
        _output = output;
        _output.WriteLine()
    }
    
}
```

#Testing #Testing/Unit_testing