
Паттерн **Factory Method (Фабричный метод)** — это порождающий шаблон проектирования, который определяет интерфейс для создания объектов, но делегирует решение о том, какой конкретно объект создавать, подклассам. Это позволяет сделать систему более гибкой и расширяемой, так как добавление новых типов объектов не требует изменения существующего кода, а лишь создания новых подклассов с нужной логикой создания объектов[^1_1][^1_3][^1_5]

### Когда использовать Factory Method?

- Когда заранее неизвестно, объекты каких классов нужно создавать.

- Когда нужно делегировать создание объектов подклассам.

- Чтобы избежать жесткой привязки к конкретным классам и упростить расширение системы новыми типами объектов[^1_1][^1_3][^1_5].

### Пример реализации Factory Method на C\#

```csharp

// Абстрактный продукт
public abstract class Transport {
    public abstract void Deliver();
}

// Конкретные продукты
public class Truck : Transport {
    public override void Deliver() {
        Console.WriteLine("Доставка грузовиком");
    }
}

public class Ship : Transport {
    public override void Deliver() {
        Console.WriteLine("Доставка кораблем");
    }
}

// Абстрактный создатель
public abstract class Logistics {

    // Фабричный метод
    public abstract Transport CreateTransport();

    public void PlanDelivery() {
    
        // Используем фабричный метод для получения объекта
        Transport transport = CreateTransport();
        transport.Deliver();
    }
}

// Конкретные создатели
public class RoadLogistics : Logistics {
    public override Transport CreateTransport() {
        return new Truck();
    }
}

public class SeaLogistics : Logistics {
    public override Transport CreateTransport() {
        return new Ship();
    }
}

// Использование
class Program {
    static void Main() {

        Logistics logistics = new RoadLogistics();
        logistics.PlanDelivery();  // Выведет: Доставка грузовиком

        logistics = new SeaLogistics();
        logistics.PlanDelivery();  // Выведет: Доставка кораблем
    }
}
```

В этом примере класс `Logistics` определяет фабричный метод `CreateTransport()`, но конкретные подклассы `RoadLogistics` и `SeaLogistics` решают, какой объект создавать — `Truck` или `Ship`. Клиентский код работает с объектами через общий интерфейс `Transport` и не зависит от конкретных классов[^1_1][^1_5][^1_9].

### Кратко о преимуществах паттерна Factory Method:

- Избавляет от жесткой привязки к конкретным классам.

- Позволяет добавлять новые типы продуктов, не меняя существующий код.

- Делегирует создание объектов подклассам, что улучшает расширяемость и поддержку кода[^1_1][^1_3][^1_5].

<div style="text-align: center">⁂</div>

  
[^1_1]: https://metanit.com/sharp/patterns/2.1.php
[^1_2]: https://vertex-academy.com/tutorials/ru/pattern-factory-java/
[^1_3]: https://habr.com/ru/articles/556512/
[^1_4]: https://logrocon.ru/news/design_patterns4
[^1_5]: https://refactoring.guru/ru/design-patterns/factory-method
[^1_6]: https://javarush.com/groups/posts/3996-kofe-breyk-189-pattern-proektirovanija-factory-na-primere-metodih-equal-i-hashcode-v-java
[^1_7]: https://javarush.com/groups/posts/2370-pattern-proektirovanija-factory
[^1_8]: https://django.fun/articles/python/pattern-fabrika-i-ego-realizaciya-v-python/
[^1_9]: https://seregaukr.gitbooks.io/design-patterns/23-factory-method.html
[^1_10]: https://sky.pro/wiki/java/factory-vs-factory-method-vs-abstract-factory-raznitsa-i-primery-java/
[^1_11]: https://academy.mediasoft.team/article/porozhdayushie-patterny-proektirovaniya-dlya-kakikh-zadach-nuzhny-vidy-i-primery-realizacii/
[^1_12]: https://ru.wikipedia.org/wiki/Фабричный_%D0%BC%D0%B5%D1%82%D0%BE%D0%B4_(%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F)

#Architecture #Patterns/Factory