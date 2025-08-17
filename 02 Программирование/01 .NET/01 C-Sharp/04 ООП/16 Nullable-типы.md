До версии C#8.0 значение "**null**" могли принимать только ссылочные типы. В новых версиях добавлена концепция ссылочных nullable-типов и **"nullable aware context"** — nullable-контекст в котором можно использовать ссылочные nullable-типы.

Объявление nullable-переменных

```cs
int? number = null;
StringBuilder? builder;

// Или
Nullable<int> number;
Nullable<StringBuilder> builder;
```

***Зачем нужны nullable-типы?***
* Для статического анализа кода, который позволяет минимизировать вероятность возникновения `NullReferenceException`.
* Для улучшения правил целостности логики программы. При использовании nullable-конекста необходимо явно указывать, может ли тип допустить значение `null` или нет.
* Для возможности значимым типам принимать значение `null`

***Когда объявлять ссылочный тип как nullable?*** — Когда данный ссылочный тип может принимать значение `null` и эта ситуация учитываются в коде. Если ссылочный тип не предполагает то, что он будет иметь значение `null`, то не следует объявлять его как nullable.

Nullable-контекст позволяет компилятору на этапе компиляции программы определить, какие переменные допускают значение null и, на основе этого, создать соответствующие предупреждения, которые отображаются в любой IDE и в консоли, если сборка проекта выполняется через консоль

```cs
// Нет предупреждения
string name = null;
PrintUpper(name);
 
void PrintUpper(string text)
{
    Console.WriteLine(text.ToUpper()); // NullReferenceException
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

Nullable-массивы:

```cs
int?[] nullableElements; // Массив не допускает null, элементы допускают
int[]? nullableArray;    // Массив допускает null, элементы не допускают
int?[]? allNullable;     // Массив и элементы допускают null
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

Данный оператор — аннотация "не допускающий null", влияет только на статический анализ nullable-типов. Оператор "`!`" позволяет указать, что переменная ссылочного типа не равна **null**:

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

Далее: [[17 Структура Nullable]]

#C-Sharp #OOP #C-Sharp/Nullable