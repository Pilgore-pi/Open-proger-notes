**Запись (Record)** — это неизменяемый класс. Записи автоматически предоставляют реализацию методов `Equals`, `GetHashCode` и `ToString`.

```csharp
public record Person
{
  // Можно устанавливать значения по умолчанию
  public string Name { get; init; }
  public Person(string name) => Name = name;
}
// Сокращенный синтаксис
public record PersonRecord(string FirstName, string LastName);

// Автоматический конструктор
var person = new PersonRecord("John", "Doe");
Console.WriteLine(person);
// Output: PersonRecord { FirstName = John, LastName = Doe }
```

Аксессором для задания значения свойства записи может быть как `init`, так и `set`. По умолчанию `init`.

**Запись структуры** — это неизменяемая структура по аналогии с обычной записью.

```csharp
public record struct Vector2D(float X, float Y);
public record struct Vector3D(float X, float Y, float Z);
public record struct Color(byte R, byte G, byte B);
```

>Сравнение записей происходит по их значениям, а не по их ссылкам

Записи поддерживают *деконструкцию* и *сопоставление с образцом*.

```csharp
Rectangle rect = new Rectangle(100, 200);

// Деконструкция
(int width, int height) = rect;
// Вывод: Width: 100, Height: 200
Console.WriteLine($"Width: {width}, Height: {height}");

// Сопоставление с образцом
string description = rect switch
{
    (0, 0) => "Пустой прямоугольник",
    (var w, var h) when w == h => "Квадрат",
    (var w, var h) => $"Прямоугольник: ширина {w}, высота {h}"
};
```

>В случае написания сокращенной формы, запись автоматически создает *конструктор* и *деконструктор*, а также неизменяемые свойства. Деконструктор — это функция с выходными параметрами, которая возвращает кортеж из свойств записи.

Явное создание деконструктора:

```csharp
public record Person
{
    public string Name { get; init; }
    public int Age { get; init; }
    public Person(string name, int age)
    {
        Name = name; Age = age;
    }
    // Зарезервированный идентификатор
    public void Deconstruct(out string name, out int age)
      => (name, age) = (Name, Age);
}
```

### Неизменяемые записи

Модификатор `readonly` позволяет явно указать на то, что состояние записи не может быть изменено даже ее же методами.

```csharp
public readonly record Person(string FirstName, string LastName)
{
    // Этот метод не может изменять состояние объекта
    public string GetFullName() => $"{FirstName} {LastName}";
}
```

### Инициализация с оператором `with`

Этот чудесный оператор позволяет инициализировать запись на основе другой записи или структуры.

```csharp
record Person(string Name);
var macho = new Person("Petus");
var looser = macho with { Name = "Invalid" };

// Копирование всех свойств лузера
var repeater = looser with { };
```

На месте `macho` может стоять только запись или структура.

**Наследование записей:**

```csharp
public record Person(string Name, int Age);
public record Employee(string Name, int Age, string Company)
  : Person(Name, Age);
```

#C-Sharp #OOP #C-Sharp/Records