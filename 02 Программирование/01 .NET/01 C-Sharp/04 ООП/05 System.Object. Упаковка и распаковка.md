
## System.Object

> System.Object (`object`) — самый базовый класс в системе типов CLR. От типа `object` наследуются абсолютно все существующие типы .NET, даже все структуры наследуются от `ValueType`, который наследуется от `object`

### Конструкторы

У `object` есть единственный конструктор без параметров: `Object()`

### Методы

Следующие методы есть у всех объектов .NET

| Метод | Возвращает | Описание |
| :-- | :-- | :-- |
| `virtual Equals(object?)` | `bool` | Проверяет, равен ли текущий объект и указанный объект |
| `static Equals(object?, object?)` | `bool` | Выполняет сравнение двух объектов |
| `ReferenceEquals(Object, Object)` |  |
| `virtual GetHashCode()` | `int` | Возвращает хэш-код текущего объекта |
| `GetType()` | `Type` | Возвращает тип текущего объекта, выводимый во время выполнения |
| `MemberwiseClone()` | `object` | Возвращает копию объекта |
| `ToString()` | `string` | Возвращает текстовое представление объекта |
| `Finalize()` | `void` |  |



## Упаковка и распаковка объектов

[Официальная документация](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing)

> **Упаковка** – это неявная операция преобразования значимого типа к `object` или к интерфейсу, который реализован данным значимым типом. При этом переменная копируется в специальный объект, который помещается в **управляемую кучу**

Упаковку можно делать явно, но это никогда не требуется

> **Распаковка** – это явное извлечение значимого типа из ссылочного объекта

> Распаковка обязана осуществляться в точно такой же тип данных, который был упакован. В случае несоответствия возникнет исключение `InvalidCastException`

> при попытке распаковать нулевую ссылку возникнет исключение `NullReferenceException`

```cs
int number = 228;

object o = number;         // неявная упаковка (boxing)
object o = (object)number; // явная упаковка (boxing)

int i = (int)o;            // распаковка (unboxing)
```

```cs
enum IntEnum { None }
enum ShortEnum : short { None }

object boxUnsigned = 700u;

int i = (int)boxUnsigned;       // ошибка времени выполнения

int i = (int)(uint)boxUnsigned; //нет ошибки

object boxInt = (int)42;
long unbox = (long)(IntEnum)boxInt;

object boxInt = (int)42;
long unbox = (long)(ShortEnum)boxInt; // ошибка времени выполнения
```

| Сценарий | Stack | Heap | |
| :-- | :-- | :-- | :-- |
| Переменная не упакована | int i = 123 |  | все данные на стеке |
| Переменная упакована | object o | int i = 123 | на стеке располагается ссылка на переменную, хранящуюся в куче |

> Распаковка не использует операторы явного и неявного преобразования и интерфейс `IConvertible`

Распаковка `int` из `enum`:

```cs
public enum EnumType { None }
...
object box = EnumType.None;
long unbox = (long)(int)box;
```

Распаковка `enum` из `enum`:

```cs
public enum EnumType { None }
public enum EnumType2 { None }
...
object box = EnumType.None;
long unbox = (long)(EnumType2)box;
```

### Производительность при упаковке и распаковке

Операции упаковки и распаковки являются дорогими с точки зрения производительности. При упаковке, в "куче" создается новый объект и в него заворачивается упаковываемый тип значения. Операция создания нового объекта может быть дольше до 20 раз, чем простое присваивание ссылки в переменную, а распаковка до 4 раз дольше

> Рекоммендуется избегать упаковки и распаковки, если это целесообразно

Если происходит преобразование ссылочных типов, то затрачивается меньше ресурсов, так как преобразование допускается только между совместимыми типами, то есть между типами, построенными по одному шаблону (интерфейс или класс)

Упаковка может неявно происходить в следующих случаях:

- При приведении структуры к объекту
- При приведении структуры к интерфейсу
- При вызове метода базового ссылочного типа у значимого типа (`5.ToString()`)
- В старых версиях .NET -- при конкатенации строк (в новых версиях упаковки не происходит)
- В старых версиях .NET -- при интерполяции строк

К примеру, в .NET Framework:

```cs
static void Method(string str, int num) {
    // вызывается метод String.Format()
    Console.WriteLine($"{str} {num}"); // упаковка num в object
}

static void Method(string str, int num) {
    // вызывается метод String.Concat()
    Console.WriteLine($"{str} {num.ToString()}"); // упаковки нет
}
```

#### Примеры упаковки

Упаковка в интерфейс

```cs
struct Point : IComparable<Point> {
    // ... 
    public int CompareTo(Point point) { .... }
}

static void ProcessComparableItems<T>(IComparable<T> lhs, IComparable<T> rhs) { ... }

static int Calculate(....) {

    var firstPoint = new Point(....);
    var secondPoint = new Point(....);
    ProcessComparableItems(firstPoint, secondPoint); // упаковка в IComparable<Point>
    // ...
}
```

Упаковка при вызове метода базового класса

```cs
var dateTime = new DateTime(....);
Type typeInfo = dateTime.GetType();
```

dateTime — переменная значимого типа (DateType). У dateTime вызывается метод GetType, определённый в типе System.Object. Чтобы выполнить такой вызов, объект dateTime придётся упаковать

### Nullable-типы

Операции упаковки и распаковки поддерживают [[Nullable-типы|16 Nullable-типы]]

Распаковка Nullable типа из обычного:

```cs
object box = (int)42;
long unbox = (long)(int?)box;
```

Распаковка обычного типа из Nullable:

```cs
object box = (int?)42;
long unbox = (long)(int)box;
```

Ключевое слово as работает с такими типами, значение null получается при любой неудачной распаковке

```cs
object box = 42L;
int? unbox = box as int?; // null
```

#C-Sharp #OOP #Performance #Memory
