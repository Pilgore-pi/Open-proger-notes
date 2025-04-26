`Nullable<T>` имеет 2 свойства:
* Value -- значение nullable-типа
* HasValue -- содержит ли тип значение отличное от null

И метод `GetValueOrDefault()` возвращает значение nullable-переменной или стандартное значение для соответствующего типа данных, если не задано другого значения.

```cs
int? number = null;
Console.WriteLine(number.GetValueOrDefault());   // 0
Console.WriteLine(number.GetValueOrDefault(10)); // 10

number = 15;
Console.WriteLine(number.GetValueOrDefault());    // 15
Console.WriteLine(number.GetValueOrDefault(10));  // 15
```

>Если при вычислении значения один из операндов равен `null`, то все выражение вернет `null`:

```cs
int? x = null;
int? w = x + 7; // w == null
```

>В операциях сравнения, если хотя бы один из операндов равен `null`, то возвращается false

Далее: [to be continued](https://metanit.com/sharp/tutorial/3.26.php)

#C-Sharp #OOP #C-Sharp/Nullable