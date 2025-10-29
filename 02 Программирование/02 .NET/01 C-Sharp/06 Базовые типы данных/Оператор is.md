возвращает true, если левый операнд можно преобразовать к правому операнду.

```cs
class Animal {...}
class Cat : Animal{...}

Cat notADog = new Cat();
Console.WriteLine(notADog is Cat); //true
Console.WriteLine(notADog is Animal); //true
Console.WriteLine(notADog is object); //true
Console.WriteLine(notADog is string); //false
```

Можно использовать так называемый **шаблон объявления**, то есть после выражения is объявить переменную, которая будет являтся преобразованной к указанному типу.

```cs
//Допустим в Cat есть сокрытый метод, переопределяющий метод Cry()
Cat котейка = new Cat();
if(котейка is Animal животное)
{
	животное.Cry(); //А-А-А
}
else котейка.Cry(); //Meow
```

Оператор is not возвращает инвертированное булевое значение.
```cs
if(myObject is not null) Console.WriteLine(myObject.GetHashCode());
```

```csharp
5 is int // true
5 is double // false

object obj = new Cat();
obj is Cat // true
obj is Animal // true
```

#C-Sharp #C-Sharp/Type_casting