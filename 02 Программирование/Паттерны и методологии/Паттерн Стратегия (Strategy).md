
> **Паттерн Стратегия** — это поведенченский паттерн проектирования, который определяет семейство схожих алгоритмов и помещает каждый из них в собственный класс, после чего алгоритмы можно взаимозаменять прямо во время исполнения программы

> Паттерн стратегия построен на типе связи **композиция**

> Контекст стратегии — это базовый класс (или интерфейс), представляющий стратегию как таковую. От базового класса наследуются конктетные стратегии

Решим проблему. Пусть у нас имеется 3 различных алгоритма выполнения одного и того же действия. Необходимо реализовать логику обработки такого поведения наиболее рациональным 

### Простой подход

Можно определить всю логику в единственном методе и передавать параметр, который будет определять, какой алгоритм нужно в данном случае выполнить 

```csharp
enum MovementKind {
    Walk,
    Drive,
    Scooter
}

class Human {
    public void StartMovement(MovementKind movement){
        switch(movement){
            case MovementKind.Walk:    Console.WriteLine("Walking"); break;
            case MovementKind.Drive:   Console.WriteLine("Driving"); break;
            default:                   Console.WriteLine("On scooter"); break;
        }
    }
    //...
}

Human person = new Human();
person.StartMovement(MovementKind.Walk);
person.StartMovement(MovementKind.Drive);
```

**Преимущества:**

- Краткость кода

**Недостатки:**

- Отсутствующая гибкость, нарушение **принципа открытости-закрытости**. При добавлении нового типа передвижения, нужно будет добавлять новое поле перечисления и редактировать метод `StartMovement()`, добавляя новую ветку логики

### Разделение логики в семействе класса

Рассмотрим пример структуру программы, основанную на **наследовании** и **полиморфизме**

```csharp
// Человек, который может передвигаться
interface IHuman {
    void Move();
}

class WalkHuman : IHuman {
    public override Move() { /*Передвижение пешком*/ }
}

class DriveHuman : IHuman {
    public override Move() { /*Передвижение на машине*/ }
}

class ScooterHuman : IHuman {
    public override Move() { /*Передвижение на самокате*/ }
}

// Полиморфный вызов метода Move()
static void StartMovement(IHuman human) => human.Move();

IHuman human = new DriveHuman();
StartMovement(human);
human = new ScooterHuman();
StartMovement(human);
StartMovement(new WalkHuman());
```

**Преимущества:**

- Улучшенная гибкость. Теперь не требуется вмешиваться в реализацию метода `StartMovement()`

**Недостатки:**

- Усложнение читаемости кода
- Небольшое ухудшение производительности программы, из-за объявления нескольких наследуемых классов
- При изменении способа передвижения необходимо создавать объект заново, что требует повторять все операции, необходимые для инициализации объекта
- По этой же причине теряется производительность

Учитывая все недостатки, этот подход еще хуже предыдущего, где вся логика определена в непосредственно в методе

### Разделение алгоритмов на стратегии в ООП стиле

```csharp
interface IMoveStrategy {
    void Move();
}

class WalkStrategy : IMoveStrategy {
    public override void StartMovement() { Console.WriteLine("Walking"); }
}

class DriveStrategy : IMoveStrategy {
    public override void StartMovement() { Console.WriteLine("Driving"); }
}

class ScooterStrategy : IMoveStrategy {
    public override void StartMovement() { Console.WriteLine("On scooter"); }
}

class Human {
    public IMoveStrategy MovementStrategy { get; set; }
    public Move() => MovementStrategy.StartMovement();
}


Human person = new Human();

person.MovementStrategy = new DriveStategy();
person.Move();

person.MovementStrategy = new ScooterStategy();
person.Move();

person.MovementStrategy = WalkStrategy();
person.Move();
```

**Преимущества:**

- Абсолютная гибкость и простота добавления новой стратегии (необходимо добавить еще один класс)
- Код, отвечающий за выполнения логики движения, хорошо читается
- Метод `StartMovement()` стал очень простым, благодаря делегированию логики другим методам

**Недостатки:**

- Код, в целом усложнен, особенно в части объявления классов стратегий

### Разделение алгоритмов на стратегии в функциональном стиле

Тот же паттерн Стратегия, но с использованием функционального подхода

```csharp
class Human {
    public Action MovementStrategy { get; set; }
    public void Move() => MovementStrategy();
}

Human person = new Human();

person.MovementStrategy = () => Console.WriteLine("Walking");
person.Move();

person.MovementStrategy = () => Console.WriteLine("Driving");
person.Move();
```

**Преимущества:**

- Значительно меньше кода, что помогает быстрее в нем разобраться
- Все вышеописанные преимущества ООП стиля сохраняются

**Недостатки:**

- Лямбда-выражения менее читаемы


#Patterns