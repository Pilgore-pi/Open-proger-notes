BenchmarkDotNet — open-source фреймворк, который позволяет измерять время выполнения кода и количество выделяемой памяти.

Чтобы осуществить замеры, нужно создать специальный класс, в котором будут находиться измеряемые методы, содержащие нужный код.

```csharp
[MemoryDiagnoser]  
public class Benchmarks  
{  
	int NumberOfItems = 100000;
	
	[Benchmark]  
	public string ConcatStringsUsingStringBuilder()  
	{  
		var sb = new StringBuilder();  
		for (int i = 0; i < NumberOfItems; i++)  
		{  
			sb.Append("Hello World!" + i);  
		}  
		return sb.ToString();  
	}
	
	[Benchmark]  
	public string ConcatStringsUsingGenericList()  
	{  
		var list = new List<string>(NumberOfItems);  
		for (int i = 0; i < NumberOfItems; i++)  
		{  
			list.Add("Hello World!" + i);  
		}  
		return list.ToString();  
	}  
}
```

Далее в методе `Main()` нужно вызвать BenchmarkRunner:
```csharp
static void Main(string[] args)  
{  
   var summary = BenchmarkRunner.Run<MemoryBenchmarkerDemo>();  
}
```

>Программу необходимо запускать в профиле Release при использовании данного фрэймворка

>Проект должен быть типа "Консоль", так как результаты измерений выводяться в консоль.

>Класс для тестирования производительности должен быть открытым.

#Performance #Performance/Benchmark