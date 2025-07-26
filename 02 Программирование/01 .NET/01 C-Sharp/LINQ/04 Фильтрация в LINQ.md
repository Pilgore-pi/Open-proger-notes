Фильтрация осуществляется оператором `where`, `Where()`.

```cs
var selectedPeople = people.Where(p => p.Age > 25);
```

Сложная фильтрация:

```cs
var selectedPeople = from person in people
                     from lang in person.Languages
                     where person.Age < 28
                     where lang == "english"
                     select person;
```

Или:

```cs
var selectedPeople = people.SelectMany
(
	u => u.Languages, //users
	(u, l) => new { Person = u, Lang = l }
).Where(u => u.Lang == "english" && u.Person.Age < 28)
    .Select(u => u.Person);
```

Метод `SelectMany()` в качестве первого параметра принимает последовательность, которую надо проецировать, а в качестве второго параметра - функцию преобразования, которая применяется к каждому элементу.

## Фильтрация по типу данных

`OfType()` позволяет отфильтровать данные коллекции по определенному типу:
```cs
var people = new List<Person>
{
    new Student("Tom"),
    new Person("Sam"),
    new Student("Bob"),
    new Employee("Mike")
};
 
var students = people.OfType<Student>();


record class Person(string Name);
record class Student(string Name) : Person(Name);
record class Employee(string Name) : Person(Name);
```

Далее: [[05 Сортировка]]

#C-Sharp #C-Sharp/LINQ