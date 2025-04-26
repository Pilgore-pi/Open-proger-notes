До версии C#8.0 значение "**null**" могли принимать только ссылочные типы. В новых версиях добавлена концепция ссылочных nullable-типов и **"nullable aware context"** -- nullable-контекст в котором можно использовать ссылочные nullable-типы.

Объявление nullable-переменных
```cs
int? number = null;
StringBuilder? builder;

// Или
Nullable<int> number;
Nullable<StringBuilder> builder;
```

***Зачем нужны nullable-типы?***
* для статического анализа кода, который позволяет минимизировать вероятность возникновения NullReferenceException.
* для явного указания, может ли тип допустить значение null или нет.
* для создания возможности значимым типам принимать значение null

***Когда объявлять ссылочный тип как nullable?*** -- Когда данный ссылочный тип может принимать значение null и эта ситуация учитываются в коде. Если ссылочный тип не предполагает то, что он будет иметь значение null, следует не объявлять его как nullable.

Nullable-контекст позволяет компилятору определить на этапе компиляции какие переменные имеют значение null и создать предупреждение в среде разработки.
```cs
// Нет предупреждения
string name = null;
PrintUpper(name);  // NullReferenceException
 
void PrintUpper(string text)
{
    Console.WriteLine(text.ToUpper());
}
```

```cs
// Предупреждение
string? name = null;
PrintUpper(name);

void PrintUpper(string? text)
{
    Console.WriteLine(->text<-.ToUpper());
}
```

Nullable-типы следует использовать только в nullable-контексте, который можно объявить локально с помощью директивы \#nullable:
```cs
#nullable enable
\\ code
#nullable disable
```
И глобально через конфигурационный файл приложения:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
	  <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```
Для этого нужно, нажав на проект, выбрать "изменить файл проекта".

## Оператор ! (null-forgiving operator)
Данный оператор влияет только на статический анализ nullable-типов. Оператор "!" позволяет указать, что переменная ссылочного типа не равна **null**:
```cs
string? name = null;
 
PrintUpper(name!);
 
void PrintUpper(string text)
{
    if(text == null) Console.WriteLine("null");
    // Нет предупреждения
    else Console.WriteLine(text.ToUpper());
}
```
Здесь мы явно говорим, что **name != null**, хоть оно и равно null, так как мы и так проверяем значение на **null**. Это делается для того, чтобы компилятор не создавал предупреждения.

Далее: [[10 _Структура Nullable]]

#C-Sharp #OOP #C-Sharp/Nullable