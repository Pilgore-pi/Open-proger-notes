> **Python** — это высокоуровневый сценарный интерпретируемый язык с динамической сильной типизацией. Поддерживает процедурную, функциональную и объектно-ориентированную парадигму.

##### Связанные темы:

* [[_Введение в Python#Синтаксис|Синтаксис]]
* [[_Введение в Python#Система типов|Система типов]]
* [[_Введение в Python#Условные выражения|Условные вырженеия]]
* [[_Введение в Python#Циклы|Циклы]]
* [[_Введение в Python#Функции|Функции]]
* Классы
* Коллекции
* Исключения
* [[__Встроенные функции Python|Встроенные функции]]
* [[__Работа с файлами в Python|Работа с файлами]]

## Синтаксис

* Нет точек с запятой;
* Нет скобок
* Вложенность блоков кода (функций, условий) определяется горизонтальным отступом (отступ должен быть одинаковым)

**Комментарии:**

```python
#однострочный комментарий

"""
многострочный
комментарий
"""
```

## Система типов

Все типы подразделяются на:
- встроенные
- невстроенные

Каждый объект в Python имеет 3 атрибута:

1. идентификатор (`id`) – позволяет отличать объекты
2. значение
3. тип

Встроенные типы данных:

* `None` – неопределенное значение переменной
* Логическая переменная – `Boolean Type`
* Числа – Numeric Type (`int`, `float`, `complex`)
* Текст – Text Sequence Type (`str`)
* Списки – Sequence Type (`list`, `tuple`, `range`)
* Множества – Set Types (`set`, `frozenset`)
* Бинарные списки – Binary Sequnce Type (`bytes`, `byrearray`, `memoryview`)
* Словари – Mapping Types (`dict`)

>Любая переменная представляет собой ссылку

Чтобы получить id объекта, нужно вызвать функцию `id()`.
Чтобы получить тип объекта, нужно вызвать функцию `type()`.

Все типы в Python делятся на **изменяемые** и **неизменяемые**.

* **Изменяемые**: list, set, dict
* **Неизменяемые**: int, float, complex, bool, tuple, str, frozen set

### Литералы

**Числа:**

* Десятичная СС: `567, 154.4, .0001, 2e5, 2E5, 1_000_000`
* `0b11011011` – двоичное представление
* `0o322571` – восьмеричное
* `0xfa34c` – шестнадцатеричное

**Текст:**

```python
'My str'
"My str"
'My str is "THE_TEXT" '
" \"TEXT \" "
```
escape-последовательности: `\\`, `\n`, `\t`, `\v`
`\x` –
Префикс `b` –
Префикс `r` (raw) строка воспринимается буквально
https://stackoverflow.com/questions/20450531/python-operator-in-print-statement

Квантификатор | Описание
-|-
`'d'` | Signed integer decimal.  
`'i'` | Signed integer decimal.  
`'o'` | Signed octal value
`'u'` | Obsolete type – it is identical to 'd'
`'x'` | Signed hexadecimal (lowercase)
`'X'` | Signed hexadecimal (uppercase)
`'e' 'E'` | Floating point exponential format (lowercase)
`'f' 'F'` | Floating point decimal format.
`'g' 'G'` | Floating point format. Uses lowercase exponential format if exponent is less than -4 or not less than precision, decimal format otherwise
`'c'` | Single character (accepts integer or single character string).   
`'r'` | String (converts any Python object using `repr()`)
`'s'` | String (converts any Python object using `str()`)
`'%'` | No argument is converted, results in a `'%'` character in the result.

**Интерполяция:** `f"text is {text}"`

**Логические значения:** `True`, `False`

### Операции

| Операция | Описание                    |
| -------- | --------------------------- |
| `+`      | сложение, конкатенация      |
| `-`      | вычитание, унарный минус    |
| `*`      | умножение                   |
| `/`      | деление возвращающее дробь  |
| `//`     | деление нацело, без остатка |
| `**`     | возведение в степень        |
| `%`      | остаток от деления          |

`round(number)` – округление по общепринятым правилам

> Операторов инкремента и декремента не существует, как, например в C++ (`i++`, `i--`)

| Булевая операция | Описание                                              |
| ---------------- | ----------------------------------------------------- |
| `==`             | проверка на равенство                                 |
| `>`              |                                                       |
| `<`              |                                                       |
| `>=`             |                                                       |
| `<=`             |                                                       |
| `and`            | И                                                     |
| `or`             | ИЛИ                                                   |
| `not`            | НЕ                                                    |
| `obj1 is obj2`   | ссылаются ли 2 объекта на одно и то же место в памяти |

## Условные выражения

>Вложенные блочные конструкции должны иметь одинаковый отступ (n пробелов)

```python
if condition:
    body
[elif] condition:
    body
[else]:
    body
```

>`elif` = `else if`

>`1` воспринимается как `True`

**Тернарный условный оператор:**

```python
result = _first if condition else _second
```
## Циклы

**Цикл while**

>Цикл while достаточно медленный

```python
while condition:
    body
```

**Цикл for**

```python
for item in collection:
    body
```

Оператор `continue` переходит к следующей итерации цикла

Оператор `break` прерывает цикл

#### Оператор else в циклах

```python
for i in 'hello world':
if i == 'a':
    break
else:
    print('Буквы a в строке нет')
```

В данном примере блок `else` выполниться только, если цикл завершился без прерывания (без вызова оператора break)

## Функции

```python
[modifier] def funName():
    body
```

#### Модификаторы (nonlocal, global, etc.)

## Исключения

```python
try
    code
except
    code
```

```python
assert condition
```

#Python #MERGE_NOTES
