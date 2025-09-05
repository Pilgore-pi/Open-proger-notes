
Тип ***char*** хранит 2 байта информации, представляющих один символ в кодировке Unicode. Это позволяет разрабатывать многоязыковые приложения .NET

## Литералы

* `'h' 'л' ' ' '9'` -- обычные символы
* `\u0100` -- символ Юникода в шестнадцатеричной системе
* `\x78` -- ASCII символ, представленный в шестнадцатеричной системе счисления

### Escape-последовательности

Это сочетания символов, где первым символом всегда выступает обратный слеш `'\'`, а затем еще 1 буква или набор цифр

| Последовательность | Описание                                                            |
| ------------------ | ------------------------------------------------------------------- |
| `\0`               | Нулевой символ (U+0000)                                             |
| `\a`               | Звуковой сигнал                                                     |
| `\b`               |                                                                     |
| `\n`               | Перевод на новую строку                                             |
| `\r`               | Возврат курсора                                                     |
| `\t`               | Горизонтальная табуляция (обычная)                                  |
| `\v`               | Вертикальная табуляция                                              |
| `\'`               | Одинарная кавычка                                                   |
| `\"`               | Двойная кавычка                                                     |
| `\\`               | Обратная косая черта                                                |
| `\x##`             | Литерал `char` в кодировке ASCII                                      |
| `\u####`           | Литерал `char` представленный в виде 2 байтов в порядке little endian |
| `\U########`       | Строка, представляющая собой одиночный или суррогатный символ       |

## Структура [System.Char](https://learn.microsoft.com/en-us/dotnet/api/system.char?view=net-8.0#definition)

| Поле | Значение | Описание |
| :--- | :------- | :------- |
| `MinValue` | `(char)0x00`, `'\0'` | Пустой символ, минимальное значение |
| `MaxValue` | `(char)0xFFFF` | Максимальное значение |

- Структура не содержит свойств

| Метод экземпляра | Возвращает | Описание |
| :--------------- | :--------- | :------- |
| `CompareTo()` |  | Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified object |

- `System.Char` содержит всего один метод экземпляра, которого нет в `System.Object`

| Статический метод | Возвращает | Описание |
| :---- | :--------- | :------- |
| `GetTypeCode()` | `TypeCode` | Тип кода символа |
| `Parse(string)` |  | Преобразует строку в символ |
| `ToLower(char)` |  | Преобразование в нижний регистр с учетом региональных параметров. Например латинская буква `i` в верхнем регистре будет `I`, но, например, в турецком языке в верхнем регистре добавляется надстрочная точка |
| `ToLowerInvariant(char)` |  | Преобразование в нижний регистр без учета региональных стандартов |
| `ToUpper(char)` |  | Преобразование в верхний регистр с учетом региональных параметров |
| `ToUpperInvariant(char)` |  | Преобразование в верхний регистр без учета региональных стандартов |
| `ConvertFromUtf32(int)` |  | возвращает 1 или 2 символа в виде строки, представляющих символ кодировки UTF-32 |
| `ConvertToUtf32(char, char)` |  | возвращает код символа UTF-32, указанного с помощью [суррогатной пары](obsidian://open?vault=IT%20Notes&file=C-Sharp%2F%D0%A1%D0%B8%D0%BC%D0%B2%D0%BE%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5%20%D1%82%D0%B8%D0%BF%D1%8B%2F%D0%A1%D1%83%D1%80%D1%80%D0%BE%D0%B3%D0%B0%D1%82%D0%BD%D1%8B%D0%B5%20%D0%BF%D0%B0%D1%80%D1%8B) |
| `GetUnicodeCategory(char)` |  | возвращает категорию указанного символа в системе Unicode. Категория представляет собой перечисление UnicodeCategory (29 категорий): `UppercaseLetter`, `LowercaseLetter`, [Surrogate](obsidian://open?vault=IT%20Notes&file=C-Sharp%2F%D0%A1%D0%B8%D0%BC%D0%B2%D0%BE%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5%20%D1%82%D0%B8%D0%BF%D1%8B%2F%D0%A1%D1%83%D1%80%D1%80%D0%BE%D0%B3%D0%B0%D1%82%D0%BD%D1%8B%D0%B5%20%D0%BF%D0%B0%D1%80%D1%8B), `LineSeparator`, `DecimalDIgitNumber` и другие |
| `GetNumericValue()` |  | конвертирует символ в число типа `double`, если конвертация невозможно возвращает -1. Конвертируются также дроби и другие символы, обозначающие числа |
| `IsWhiteSpace(char)` |  | Indicates whether the specified Unicode character is categorized as white space |
| `IsAscii(char)` |  | Returns true if c is an ASCII character (`[ U+0000..U+007F ]`) |
| `IsAsciiDigit(char)` |  | Indicates whether a character is categorized as an ASCII digit |
| `IsAsciiHexDigit(char)` |  | Indicates whether a character is categorized as an ASCII hexademical digit |
| `IsAsciiHexDigitLower(char)` |  | Indicates whether a character is categorized as an ASCII lower-case hexademical digit |
| `IsAsciiHexDigitUpper(char)` |  | Indicates whether a character is categorized as an ASCII upper-case hexademical digit |
| `IsAsciiLetter(char)` |  | Indicates whether a character is categorized as an ASCII letter |
| `IsAsciiLetterLower(char)` |  | Indicates whether a character is categorized as a lowercase ASCII letter |
| `IsAsciiLetterOrDigit(char)` |  | Indicates whether a character is categorized as an ASCII letter or digit |
| `IsAsciiLetterUpper(char)` |  | Indicates whether a character is categorized as an uppercase ASCII letter |
| `IsBetween(char, char, char)` |  | Indicates whether a character is within the specified inclusive range |
| `IsControl()` |  | Indicates whether the specified Unicode character is categorized as a control character |
| `IsDigit(char)` |  | Indicates whether the specified Unicode character is categorized as a decimal digit |
| `IsHighSurrogate(char)` |  | Indicates whether the specified char object is a high surrogate |
| `IsLetter(char)` |  | Indicates whether the specified Unicode character is categorized as a Unicode letter |
| `IsLetterOrDigit(char)` |  | Indicates whether the specified Unicode character is categorized as a letter or a decimal digit |
| `IsLower(char)` |  | Indicates whether the specified Unicode character is categorized as a lowercase letter |
| `IsLowSurrogate(char)` |  | Indicates whether the specified char object is a low surrogate |
| `IsNumber(char)` |  | Indicates whether the specified Unicode character is categorized as a number |
| `IsPunctuation(char)` |  | Indicates whether the specified Unicode character is categorized as a punctuation mark |
| `IsSeparator(char)` |  | Indicates whether the specified Unicode character is categorized as a separator character |
| `IsSurrogate(char)` |  | Indicates whether the specified character has a surrogate code unit |
| `IsSurrogatePair(char, char)` |  | Indicates whether the two specified char objects form a surrogate pair |
| `IsSymbol(char)` |  | Indicates whether the specified Unicode character is categorized as a symbol character |
| `IsUpper(char)` |  | Indicates whether the specified Unicode character is categorized as an uppercase letter |
| `TryParse(string, char)` |  | Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed |

Минимальное значение типа char -- `'\0'`, максимальное -- `'\uffff'`.
Используя escape-последовательности без кавычек (`\u####`) можно именовать сущности кода и заменять любые другие нелитеральные символы соответствующими escape-последовательностями:

```csharp
void \u01001() { } //Ā1
"12345".To\u0043harArray(); //"12345".ToCharArray();
```

- Не всякий символ исходного кода можно заменить escape-последовательностью

- Такая возможность подменять символы их кодами Unicode, предоставляет удобный механизм обфускации кода*

## Конвертация типа char

1) С помощью приведения типа — наиболее производительный (не вызываются методы)
2) С помощью класса `Convert`
3) Через упаковку в интерфейс `IConvertible`

[[String]], [[_CultureInfo]]

#C-Sharp #Text_types
