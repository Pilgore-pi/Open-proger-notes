
Сначала рекомендуется ознакомиться с заметкой [Регулярные выражения](../../../../../Регулярные%20выражения/01%20Регулярные%20выражения.md)

Регулярные выражения в C# предоставляют мощный механизм для поиска, сопоставления и манипулирования текстом на основе шаблонов. Все классы для работы с регулярными выражениями находятся в пространстве имен `System.Text.RegularExpressions`

## Основные классы

### Класс `Regex`

Основной класс для работы с регулярными выражениями. Представляет неизменяемое регулярное выражение.

**Конструкторы:**

- `Regex(string pattern)` — создает экземпляр с указанным шаблоном
- `Regex(string pattern, RegexOptions options)` — создает экземпляр с шаблоном и опциями
- `Regex(string pattern, RegexOptions options, TimeSpan matchTimeout)` — добавляет таймаут выполнения

**Основные методы:**

| Метод | Возвращает | Описание |
| ----- | ---------- | -------- |
| `IsMatch(string input)` | `bool` | Проверяет, соответствует ли входная строка регулярному выражению |
| `Match(string input)` | `Match` | Ищет первое совпадение в строке |
| `Matches(string input)` | `MatchCollection` | Ищет все совпадения в строке |
| `Replace(string input, string replacement)` | `string` | Заменяет все совпадения на указанную строку |
| `Replace(string input, MatchEvaluator evaluator)` | `string` | Заменяет совпадения с использованием функции-оценщика |
| `Split(string input)` | `string[]` | Разделяет строку по совпадениям с шаблоном |
| `Escape(string str)` | `string` | Статический метод. Экранирует специальные символы регулярных выражений |
| `Unescape(string str)` | `string` | Статический метод. Убирает экранирование специальных символов |

**Основные свойства:**

| Свойство | Тип | Описание |
| -------- | --- | -------- |
| `Options` | `RegexOptions` | Возвращает опции, переданные в конструктор |
| `MatchTimeout` | `TimeSpan` | Возвращает интервал времени ожидания для операций сопоставления |
| `RightToLeft` | `bool` | Указывает, выполняется ли поиск справа налево |
| `CacheSize` | `int` | Статическое свойство. Размер кэша скомпилированных регулярных выражений |

### Класс `Match`

Представляет результаты одного совпадения регулярного выражения.

**Основные методы:**

| Метод | Возвращает | Описание |
| ----- | ---------- | -------- |
| `NextMatch()` | `Match` | Возвращает следующее совпадение |
| `Result(string replacement)` | `string` | Возвращает результат замены для текущего совпадения |

**Основные свойства:**

| Свойство | Тип | Описание |
| -------- | --- | -------- |
| `Success` | `bool` | Указывает, было ли совпадение успешным |
| `Value` | `string` | Возвращает совпавшую подстроку |
| `Index` | `int` | Позиция в исходной строке, где найдено совпадение |
| `Length` | `int` | Длина совпавшей подстроки |
| `Groups` | `GroupCollection` | Коллекция захваченных групп |
| `Captures` | `CaptureCollection` | Коллекция всех захватов |

### Класс `MatchCollection`

Представляет коллекцию результатов совпадений. Реализует `ICollection`, `IEnumerable`.

**Основные свойства:**

| Свойство | Тип | Описание |
| -------- | --- | -------- |
| `Count` | `int` | Количество совпадений в коллекции |
| `IsReadOnly` | `bool` | Всегда возвращает `true` |
| `this[int i]` | `Match` | Индексатор для доступа к совпадению по индексу |

### Класс `Group`

Представляет результаты захвата одной группы.

**Основные свойства:**

| Свойство | Тип | Описание |
| -------- | --- | -------- |
| `Success` | `bool` | Указывает, была ли группа захвачена |
| `Value` | `string` | Захваченная подстрока |
| `Index` | `int` | Позиция первого символа захваченной подстроки |
| `Length` | `int` | Длина захваченной подстроки |
| `Captures` | `CaptureCollection` | Все захваты, сделанные группой |
| `Name` | `string` | Имя группы захвата |

### Класс `GroupCollection`

Коллекция захваченных групп.

**Основные свойства:**

| Свойство | Тип | Описание |
| -------- | --- | -------- |
| `Count` | `int` | Количество групп в коллекции |
| `this[int groupnum]` | `Group` | Индексатор для доступа к группе по номеру |
| `this[string groupname]` | `Group` | Индексатор для доступа к группе по имени |

### Класс `Capture`

Представляет результаты одного успешного захвата подвыражения.

**Основные свойства:**

| Свойство | Тип | Описание |
| -------- | --- | -------- |
| `Value` | `string` | Захваченная подстрока |
| `Index` | `int` | Позиция в исходной строке |
| `Length` | `int` | Длина захваченной подстроки |

### Перечисление `RegexOptions`

Флаги для настройки поведения регулярных выражений.

| Значение | Описание |
| -------- | -------- |
| `None` | Без дополнительных опций (по умолчанию) |
| `IgnoreCase` | Игнорирование регистра при сопоставлении |
| `Multiline` | Многострочный режим (^ и $ соответствуют началу/концу строки) |
| `Singleline` | Однострочный режим (. соответствует любому символу, включая \n) |
| `ExplicitCapture` | Захватываются только явно именованные или нумерованные группы |
| `Compiled` | Компиляция регулярного выражения в сборку для повышения производительности |
| `IgnorePatternWhitespace` | Игнорирование пробелов в шаблоне и разрешение комментариев после # |
| `RightToLeft` | Поиск выполняется справа налево |
| `CultureInvariant` | Игнорирование культурных различий в языке |
| `NonBacktracking` | (.NET 7+) Использование движка без возвратов для повышения производительности |

## Примеры использования

```csharp
using System;
using System.Text.RegularExpressions;

class Program {
    static void Main() {
        // Создание экземпляра Regex
        Regex regex = new Regex(@"\d+", RegexOptions.IgnoreCase);
        
        // IsMatch - проверка соответствия
        string text1 = "Мне 25 лет";
        bool hasNumbers = regex.IsMatch(text1);
        Console.WriteLine($"Содержит числа: {hasNumbers}"); // True
        
        // Match - поиск первого совпадения
        Match match = regex.Match(text1);

        if (match.Success) {
            Console.WriteLine($"Найдено: {match.Value} на позиции {match.Index}");
            // Найдено: 25 на позиции 4
        }
        
        // Matches - поиск всех совпадений
        string text2 = "Телефон: 123-456-7890";
        MatchCollection matches = regex.Matches(text2);
        Console.WriteLine($"Найдено совпадений: {matches.Count}");
        
        foreach (Match m in matches) {
            Console.WriteLine($"  {m.Value} на позиции {m.Index}");
        }
        // Найдено совпадений: 3
        //   123 на позиции 9
        //   456 на позиции 13
        //   7890 на позиции 17
        
        // Replace - замена
        string text3 = "Цена: 100 рублей";
        string replaced = regex.Replace(text3, "XXX");
        Console.WriteLine(replaced); // Цена: XXX рублей
        
        // Replace с MatchEvaluator
        string doubled = regex.Replace(text3, m => (int.Parse(m.Value) * 2).ToString());
        Console.WriteLine(doubled); // Цена: 200 рублей
        
        // Split - разделение строки
        string text4 = "один1два2три3четыре";
        string[] parts = regex.Split(text4);
        Console.WriteLine(string.Join(" | ", parts)); 
        // один | два | три | четыре
        
        // Работа с группами
        Regex emailRegex = new Regex(@"(?<user>\w+)@(?<domain>\w+\.\w+)");
        Match emailMatch = emailRegex.Match("contact@example.com");
        
        if (emailMatch.Success){
            Console.WriteLine($"Пользователь: {emailMatch.Groups["user"].Value}");
            Console.WriteLine($"Домен: {emailMatch.Groups["domain"].Value}");
            // Пользователь: contact
            // Домен: example.com
        }
        
        // Статические методы
        string pattern = @"(a|b)*";
        string input = "Привет (a|b)* мир";
        
        // Escape - экранирование специальных символов
        string escaped = Regex.Escape(pattern);
        Console.WriteLine(escaped); // \(a\|b\)\*
        
        // Статический метод IsMatch без создания экземпляра
        bool isEmail = Regex.IsMatch("test@mail.ru", @"^\w+@\w+\.\w+$");
        Console.WriteLine($"Это email: {isEmail}"); // True
        
        // Работа с таймаутом
        try {
            Regex timeoutRegex = new Regex(@"(a+)+b", 
                RegexOptions.None, 
                TimeSpan.FromMilliseconds(100));
            
            // Эта строка может вызвать катастрофический возврат
            timeoutRegex.IsMatch("aaaaaaaaaaaaaaaaaaaaaaaac");
        }
        catch (RegexMatchTimeoutException ex) {
            Console.WriteLine($"Превышен таймаут: {ex.Message}");
        }
        
        // NextMatch - итерация по совпадениям
        Regex wordRegex = new Regex(@"\b\w+\b");
        Match wordMatch = wordRegex.Match("Hello world from C#");
        while (wordMatch.Success) {
            Console.WriteLine($"Слово: {wordMatch.Value}");
            wordMatch = wordMatch.NextMatch();
        }
        // Слово: Hello
        // Слово: world
        // Слово: from
        // Слово: C
        
        // Опция IgnorePatternWhitespace для читаемости
        Regex readableRegex = new Regex(@"
            (?<year>\d{4})  # Год (4 цифры)
            -               # Разделитель
            (?<month>\d{2}) # Месяц (2 цифры)
            -               # Разделитель
            (?<day>\d{2})   # День (2 цифры)
        ", RegexOptions.IgnorePatternWhitespace);
        
        Match dateMatch = readableRegex.Match("2024-01-15");
        if (dateMatch.Success) {
            Console.WriteLine($"Год: {dateMatch.Groups["year"].Value}");
            Console.WriteLine($"Месяц: {dateMatch.Groups["month"].Value}");
            Console.WriteLine($"День: {dateMatch.Groups["day"].Value}");
        }
    }
}
```

## Генерируемые регулярные выражения (Source Generators)

Начиная с .NET 7, появилась возможность генерировать регулярные выражения во время компиляции с помощью Source Generators. Это позволяет избежать накладных расходов на компиляцию регулярных выражений в runtime и получить лучшую производительность.

### Атрибут `[GeneratedRegex]`

Атрибут `[GeneratedRegex]` используется для генерации оптимизированного кода регулярного выражения на этапе компиляции с помощью Roslyn Source Generators.

**Преимущества:**

- Компиляция происходит во время сборки, а не в runtime
- Нет накладных расходов на парсинг и компиляцию регулярного выражения при запуске
- Лучшая производительность по сравнению с `RegexOptions.Compiled`
- Меньший размер сборки
- Поддержка Ahead-of-Time (AOT) компиляции
- Возможность анализа регулярного выражения на этапе компиляции

**Синтаксис:**

```csharp
[GeneratedRegex(string pattern)]
[GeneratedRegex(string pattern, RegexOptions options)]
[GeneratedRegex(string pattern, RegexOptions options, int matchTimeoutMilliseconds)]
[GeneratedRegex(string pattern, RegexOptions options, int matchTimeoutMilliseconds, string cultureName)]
```

### Примеры использования генерируемых регулярных выражений

```csharp
using System;
using System.Text.RegularExpressions;

partial class RegexExamples {
    // Простое регулярное выражение
    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
    
    // С опциями
    [GeneratedRegex(@"[a-z]+", RegexOptions.IgnoreCase)]
    private static partial Regex LettersRegex();
    
    // С таймаутом (в миллисекундах)
    [GeneratedRegex(@"\b\w+\b", RegexOptions.None, 1000)]
    private static partial Regex WordsRegex();
    
    // С культурой
    [GeneratedRegex(@"[а-я]+", RegexOptions.IgnoreCase, 1000, "ru-RU")]
    private static partial Regex RussianWordsRegex();
    
    // Сложное выражение для email
    [GeneratedRegex(@"^[\w\.-]+@[\w\.-]+\.\w+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();
    
    // Для телефонных номеров
    [GeneratedRegex(@"^\+?[1-9]\d{0,3}[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$")]
    private static partial Regex PhoneRegex();
    
    // С именованными группами
    [GeneratedRegex(@"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})")]
    private static partial Regex DateRegex();
    
    static void Main() {
        // Использование сгенерированного регулярного выражения
        string text = "У меня есть 42 яблока и 17 апельсинов";
        
        // IsMatch
        bool hasNumbers = NumberRegex().IsMatch(text);
        Console.WriteLine($"Содержит числа: {hasNumbers}"); // True
        
        // Matches
        MatchCollection matches = NumberRegex().Matches(text);
        Console.WriteLine($"Найдено чисел: {matches.Count}");
        foreach (Match match in matches) {
            Console.WriteLine($"  Число: {match.Value}");
        }
        // Найдено чисел: 2
        //   Число: 42
        //   Число: 17
        
        // Replace
        string replaced = NumberRegex().Replace(text, "N");
        Console.WriteLine(replaced); 
        // У меня есть N яблока и N апельсинов
        
        // Проверка email
        string email1 = "user@example.com";
        string email2 = "invalid.email";
        Console.WriteLine($"{email1} валиден: {EmailRegex().IsMatch(email1)}"); // True
        Console.WriteLine($"{email2} валиден: {EmailRegex().IsMatch(email2)}"); // False
        
        // Проверка телефона
        string phone1 = "+7-999-123-45-67";
        string phone2 = "123";
        Console.WriteLine($"{phone1} валиден: {PhoneRegex().IsMatch(phone1)}"); // True
        Console.WriteLine($"{phone2} валиден: {PhoneRegex().IsMatch(phone2)}"); // False
        
        // Работа с группами
        string dateStr = "2024-01-15";
        Match dateMatch = DateRegex().Match(dateStr);
        if (dateMatch.Success) {
            Console.WriteLine($"Год: {dateMatch.Groups["year"].Value}");
            Console.WriteLine($"Месяц: {dateMatch.Groups["month"].Value}");
            Console.WriteLine($"День: {dateMatch.Groups["day"].Value}");
        }
        // Год: 2024
        // Месяц: 01
        // День: 15
        
        // Split
        string sentence = "one1two2three3four";
        string[] parts = NumberRegex().Split(sentence);
        Console.WriteLine(string.Join(" | ", parts));
        // one | two | three | four
        
        // Работа с русским текстом
        string russianText = "Привет МИР";
        MatchCollection russianMatches = RussianWordsRegex().Matches(russianText);
        foreach (Match match in russianMatches) {
            Console.WriteLine($"Слово: {match.Value}");
        }
        // Слово: Привет
        // Слово: МИР
    }
}
```

### Сравнение подходов

```csharp
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

partial class PerformanceComparison {
    // Генерируемое регулярное выражение
    [GeneratedRegex(@"\b\w+@\w+\.\w+\b")]
    private static partial Regex GeneratedEmailRegex();
    
    static void Main() {
        string text = "Контакты: user1@example.com, user2@test.org, user3@mail.ru";
        int iterations = 100000;
        
        // 1. Без кэширования (создание каждый раз)
        Stopwatch sw1 = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++) {
            Regex.Matches(text, @"\b\w+@\w+\.\w+\b");
        }
        sw1.Stop();
        Console.WriteLine($"Без кэширования: {sw1.ElapsedMilliseconds} мс");
        
        // 2. С кэшированием экземпляра
        Regex cachedRegex = new Regex(@"\b\w+@\w+\.\w+\b");
        Stopwatch sw2 = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++) {
            cachedRegex.Matches(text);
        }
        sw2.Stop();
        Console.WriteLine($"С кэшированием: {sw2.ElapsedMilliseconds} мс");
        
        // 3. С опцией Compiled
        Regex compiledRegex = new Regex(@"\b\w+@\w+\.\w+\b", RegexOptions.Compiled);
        Stopwatch sw3 = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++) {
            compiledRegex.Matches(text);
        }
        sw3.Stop();
        Console.WriteLine($"Compiled: {sw3.ElapsedMilliseconds} мс");
        
        // 4. Генерируемое регулярное выражение
        Stopwatch sw4 = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++) {
            GeneratedEmailRegex().Matches(text);
        }
        sw4.Stop();
        Console.WriteLine($"GeneratedRegex: {sw4.ElapsedMilliseconds} мс");
        
        // Пример вывода (результаты могут варьироваться):
        // Без кэширования: 2500 мс
        // С кэшированием: 450 мс
        // Compiled: 280 мс
        // GeneratedRegex: 250 мс
    }
}
```

### Ограничения атрибута `[GeneratedRegex]`

- Требуется .NET 7 или выше
- Метод должен быть `partial` и `static`
- Класс должен быть `partial`
- Шаблон регулярного выражения должен быть константой времени компиляции
- Не поддерживается динамическое создание регулярных выражений

### Дополнительные примеры паттернов

```csharp
using System.Text.RegularExpressions;

partial class CommonPatterns {
    // URL
    [GeneratedRegex(@"https?://[\w\.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+")]
    public static partial Regex UrlRegex();
    
    // IPv4 адрес
    [GeneratedRegex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b")]
    public static partial Regex IPv4Regex();
    
    // Дата в формате DD.MM.YYYY или DD/MM/YYYY
    [GeneratedRegex(@"\b(?<day>\d{2})[./](?<month>\d{2})[./](?<year>\d{4})\b")]
    public static partial Regex DateDMYRegex();
    
    // Время в формате HH:MM или HH:MM:SS
    [GeneratedRegex(@"\b(?<hours>[0-2]?\d):(?<minutes>[0-5]\d)(?::(?<seconds>[0-5]\d))?\b")]
    public static partial Regex TimeRegex();
    
    // Hex цвет (#RGB или #RRGGBB)
    [GeneratedRegex(@"#(?:[0-9a-fA-F]{3}){1,2}\b", RegexOptions.IgnoreCase)]
    public static partial Regex HexColorRegex();
    
    // Номер кредитной карты (упрощенный)
    [GeneratedRegex(@"\b\d{4}[\s-]?\d{4}[\s-]?\d{4}[\s-]?\d{4}\b")]
    public static partial Regex CreditCardRegex();
    
    // HTML тег
    [GeneratedRegex(@"<(?<tag>\w+)(?:\s+[\w-]+(?:=""[^""]*"")?)* *(?:/>|>.*?</\k<tag>>)", 
        RegexOptions.Singleline)]
    public static partial Regex HtmlTagRegex();
    
    // Пароль (минимум 8 символов, буквы и цифры)
    [GeneratedRegex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
    public static partial Regex PasswordRegex();
}
```

## Полезные шаблоны регулярных выражений

| Шаблон | Описание |
| ------ | -------- |
| `\d` | Любая цифра (0-9) |
| `\D` | Любой символ, кроме цифры |
| `\w` | Буква, цифра или подчеркивание |
| `\W` | Любой символ, кроме буквы, цифры или подчеркивания |
| `\s` | Пробельный символ (пробел, табуляция, перевод строки) |
| `\S` | Любой непробельный символ |
| `.` | Любой символ, кроме перевода строки |
| `^` | Начало строки |
| `$` | Конец строки |
| `\b` | Граница слова |
| `\B` | Не граница слова |
| `*` | 0 или более повторений |
| `+` | 1 или более повторений |
| `?` | 0 или 1 повторение |
| `{n}` | Ровно n повторений |
| `{n,}` | n или более повторений |
| `{n,m}` | От n до m повторений |
| `[abc]` | Любой из символов a, b или c |
| `[^abc]` | Любой символ, кроме a, b или c |
| `[a-z]` | Любой символ в диапазоне от a до z |
| `(pattern)` | Группа захвата |
| `(?:pattern)` | Группа без захвата |
| `(?<name>pattern)` | Именованная группа захвата |
| `\|` | Логическое ИЛИ |
| `(?=pattern)` | Положительный просмотр вперед |
| `(?!pattern)` | Отрицательный просмотр вперед |
| `(?<=pattern)` | Положительный просмотр назад |
| `(?<!pattern)` | Отрицательный просмотр назад |

CLAUDE SONNET^^^

#C-Sharp #Regex #Char_types #GENERATED
