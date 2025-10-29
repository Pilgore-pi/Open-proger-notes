# Паттерн Matching в C++

> **Паттерн Matching** — это механизм для проверки структуры данных и извлечения значений из неё на основе заданных шаблонов

Паттерн Matching позволяет писать более выразительный и безопасный код при работе с вариативными типами, опциональными значениями и сложными структурами данных.

## Основные концепции

Паттерн Matching в C++ основан на нескольких ключевых компонентах:

- **Структурированные привязки (Structured Bindings)** — разложение объекта на составные части
- **std::variant** — тип, который может содержать одно из нескольких возможных значений
- **std::optional** — тип, который может содержать значение или быть пустым
- **if constexpr** — условная компиляция на основе типов

```cpp
// Простой пример структурированной привязки
std::pair<int, std::string> data = {42, "Hello"};
auto [number, text] = data;

std::cout << number << " " << text; // 42 Hello
```

## Структурированные привязки (C++17)

Структурированные привязки позволяют разложить объект на его компоненты и присвоить их отдельным переменным:

```cpp
// Разложение пары
std::pair<int, double> pair = {10, 3.14};
auto [first, second] = pair;
std::cout << first << " " << second; // 10 3.14

// Разложение кортежа
std::tuple<int, std::string, bool> tuple = {5, "test", true};
auto [num, str, flag] = tuple;

// Разложение структуры
struct Point {
    int x;
    int y;
};

Point p = {10, 20};
auto [px, py] = p;
std::cout << px << " " << py; // 10 20

// Разложение массива
int arr[3] = {1, 2, 3};
auto [a, b, c] = arr;

// Разложение с игнорированием значений
auto [first_val, _, third_val] = tuple;
```

## Работа с std::variant

`std::variant` позволяет хранить одно из нескольких возможных типов. Паттерн Matching используется для безопасного извлечения значения:

```cpp
#include <variant>

// Определение варианта, который может содержать int, double или std::string
std::variant<int, double, std::string> value;

// Присвоение значения
value = 42;
value = 3.14;
value = std::string("Hello");

// Проверка типа и извлечение значения
if (std::holds_alternative<int>(value)) {
    int num = std::get<int>(value);
    std::cout << "Integer: " << num;
}

if (std::holds_alternative<std::string>(value)) {
    std::string str = std::get<std::string>(value);
    std::cout << "String: " << str;
}
```

### Использование std::visit для паттерн Matching

`std::visit` — это функция, которая применяет функцию-обработчик к значению варианта. Это более элегантный способ работы с вариантами:

```cpp
std::variant<int, double, std::string> value = 42;

// Использование лямбды с перегрузками (C++17)
auto result = std::visit([](auto&& arg) {
    using T = std::decay_t<decltype(arg)>;
    
    if constexpr (std::is_same_v<T, int>) {
        std::cout << "Integer: " << arg;
        return arg * 2;
    } else if constexpr (std::is_same_v<T, double>) {
        std::cout << "Double: " << arg;
        return static_cast<int>(arg);
    } else if constexpr (std::is_same_v<T, std::string>) {
        std::cout << "String: " << arg;
        return 0;
    }
}, value);
```

### Перегруженные лямбды (C++20)

В C++20 можно использовать перегруженные лямбды для более чистого кода:

```cpp
// Вспомогательный шаблон для перегрузки лямбд
template<class... Ts> struct overload : Ts... { using Ts::operator()...; };
template<class... Ts> overload(Ts...) -> overload<Ts...>;

std::variant<int, double, std::string> value = "Hello";

std::visit(overload{
    [](int i) { std::cout << "Integer: " << i; },
    [](double d) { std::cout << "Double: " << d; },
    [](const std::string& s) { std::cout << "String: " << s; }
}, value);
```

## Работа с std::optional

`std::optional` представляет значение, которое может быть или не быть. Паттерн Matching используется для безопасной работы с опциональными значениями:

```cpp
#include <optional>

std::optional<int> maybeValue = 42;

// Проверка наличия значения
if (maybeValue.has_value()) {
    std::cout << "Value: " << maybeValue.value();
}

// Альтернативный синтаксис
if (maybeValue) {
    std::cout << "Value: " << *maybeValue;
}

// Структурированная привязка (C++17)
if (auto value = maybeValue) {
    std::cout << "Value: " << *value;
}

// Использование value_or для значения по умолчанию
int result = maybeValue.value_or(0);
std::cout << result; // 42
```

### Цепочка опциональных операций

```cpp
std::optional<int> getValue() {
    return 42;
}

std::optional<int> processValue(int x) {
    if (x > 0) return x * 2;
    return std::nullopt;
}

// Цепочка операций
auto result = getValue()
    .and_then(processValue)
    .transform([](int x) { return x + 10; })
    .value_or(0);

std::cout << result; // 94
```

## Паттерн Matching с пользовательскими типами

Можно создавать собственные типы, которые поддерживают паттерн Matching через структурированные привязки:

```cpp
struct User {
    std::string name;
    int age;
    std::string email;
};

User user = {"John", 30, "john@example.com"};

// Разложение пользовательского типа
auto [name, age, email] = user;
std::cout << name << " " << age << " " << email;

// Использование в условиях
if (auto [n, a, e] = user; a >= 18) {
    std::cout << n << " is an adult";
}
```

## Вложенный паттерн Matching

Можно комбинировать несколько уровней паттерн Matching:

```cpp
struct Address {
    std::string city;
    std::string street;
};

struct Person {
    std::string name;
    Address address;
};

Person person = {"Alice", {"New York", "5th Avenue"}};

// Вложенное разложение
auto [name, [city, street]] = person;
std::cout << name << " lives in " << city << " on " << street;
```

## Паттерн Matching с вариантами и опциональными значениями

Комбинирование `std::variant` и `std::optional` для сложного паттерн Matching:

```cpp
using Result = std::variant<int, std::string, std::monostate>;

std::optional<Result> processData(int input) {
    if (input > 0) return Result(input * 2);
    if (input < 0) return Result(std::string("Negative"));
    return std::nullopt;
}

// Использование
if (auto result = processData(5)) {
    std::visit(overload{
        [](int i) { std::cout << "Result: " << i; },
        [](const std::string& s) { std::cout << "Result: " << s; },
        [](std::monostate) { std::cout << "Empty"; }
    }, *result);
}
```

## Паттерн Matching в циклах

Структурированные привязки удобны при итерации по контейнерам:

```cpp
std::vector<std::pair<std::string, int>> data = {
    {"Alice", 25},
    {"Bob", 30},
    {"Charlie", 35}
};

// Разложение в цикле
for (auto [name, age] : data) {
    std::cout << name << " is " << age << " years old\n";
}

// Разложение с индексом (C++20)
for (auto [index, value] : data | std::views::enumerate) {
    std::cout << index << ": " << value.first << "\n";
}
```

## Паттерн Matching с диапазонами (C++20)

Использование паттерн Matching с диапазонами для фильтрации и трансформации данных:

```cpp
#include <ranges>

std::vector<std::pair<int, std::string>> items = {
    {1, "apple"},
    {2, "banana"},
    {3, "cherry"}
};

// Фильтрация и трансформация
auto filtered = items 
    | std::views::filter([](auto [id, name]) { return id > 1; })
    | std::views::transform([](auto [id, name]) { 
        return std::make_pair(id * 10, name); 
    });

for (auto [id, name] : filtered) {
    std::cout << id << ": " << name << "\n";
}
```

## Обработка ошибок с паттерн Matching

Типичный паттерн для обработки результатов операций:

```cpp
template<typename T, typename E>
class Result {
public:
    std::variant<T, E> data;
    
    bool isOk() const { return std::holds_alternative<T>(data); }
    bool isErr() const { return std::holds_alternative<E>(data); }
};

Result<int, std::string> divide(int a, int b) {
    if (b == 0) return {std::variant<int, std::string>(std::string("Division by zero"))};
    return {std::variant<int, std::string>(a / b)};
}

// Использование
auto result = divide(10, 2);

std::visit(overload{
    [](int value) { std::cout << "Success: " << value; },
    [](const std::string& error) { std::cout << "Error: " << error; }
}, result.data);
```

## Сравнение с другими подходами

```cpp
// Без паттерн Matching (старый стиль)
std::pair<int, std::string> data = {42, "Hello"};
int first = data.first;
std::string second = data.second;

// С паттерн Matching (новый стиль)
auto [first, second] = data;

// Без паттерн Matching с вариантом
std::variant<int, std::string> value = 42;
if (std::holds_alternative<int>(value)) {
    int num = std::get<int>(value);
    std::cout << num;
}

// С паттерн Matching (перегруженные лямбды)
std::visit(overload{
    [](int i) { std::cout << i; },
    [](const std::string& s) { std::cout << s; }
}, value);
```

## Ограничения и особенности

> Паттерн Matching в C++ работает на уровне типов и структур данных, но не предоставляет полнофункциональное сопоставление с образцом, как в функциональных языках программирования

Основные ограничения:

- Структурированные привязки работают только с типами, которые имеют открытые члены данных или специализированные шаблоны
- `std::visit` требует явного обработчика для каждого типа в варианте
- Вложенный паттерн Matching может быть сложным в использовании
- Отсутствует встроенная поддержка сопоставления по значениям (как в Rust или Scala)

Несмотря на ограничения, паттерн Matching в C++ значительно улучшает безопасность типов и читаемость кода при работе с вариативными и опциональными типами

#C-Plus-Plus #GENERATED
