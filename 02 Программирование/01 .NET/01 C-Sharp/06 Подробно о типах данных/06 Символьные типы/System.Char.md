
Тип ***char*** хранит 2 байта информации, представляющих один символ в кодировке Unicode. Это позволяет разрабатывать многоязыковые приложения .NET

## Литералы

* `'h' 'л' ' ' '9'` -- обычные символы
* `\u0100` -- символ Юникода в шестнадцатеричной системе
* `\x78` -- ASCII символ, представленный в шестнадцатеричной системе счисления

## Структура System.Char

| Метод | Возвращает | Описание |
| :- | :- | :- |
CompareTo(Char) |  | Compares this instance to a specified Char object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified Char object.
CompareTo(Object) |  | Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified Object.
ConvertFromUtf32(Int32) |  | Converts the specified Unicode code point into a UTF-16 encoded string.
ConvertToUtf32(Char, Char) |  | Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.
ConvertToUtf32(String, Int32) |  | Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a Unicode code point.
Equals(Char) |  | Returns a value that indicates whether this instance is equal to the specified Char object.
Equals(Object) |  | Returns a value that indicates whether this instance is equal to a specified object.
GetHashCode() |  | Returns the hash code for this instance.
GetNumericValue(Char) |  | Converts the specified numeric Unicode character to a double-precision floating point number.
GetNumericValue(String, Int32) |  | Converts the numeric Unicode character at the specified position in a specified string to a double-precision floating point number.
GetTypeCode() |  | Returns the TypeCode for value type Char.
GetUnicodeCategory(Char) |  | Categorizes a specified Unicode character into a group identified by one of the UnicodeCategory values.
GetUnicodeCategory(String, Int32) |  | Categorizes the character at the specified position in a specified string into a group identified by one of the UnicodeCategory values.
IsAscii(Char) |  | Returns true if c is an ASCII character (`[ U+0000..U+007F ]`).
IsAsciiDigit(Char) |  | Indicates whether a character is categorized as an ASCII digit.
IsAsciiHexDigit(Char) |  | Indicates whether a character is categorized as an ASCII hexademical digit.
IsAsciiHexDigitLower(Char) |  | Indicates whether a character is categorized as an ASCII lower-case hexademical digit.
IsAsciiHexDigitUpper(Char) |  | Indicates whether a character is categorized as an ASCII upper-case hexademical digit.
IsAsciiLetter(Char) |  | Indicates whether a character is categorized as an ASCII letter.
IsAsciiLetterLower(Char) |  | Indicates whether a character is categorized as a lowercase ASCII letter.
IsAsciiLetterOrDigit(Char) |  | Indicates whether a character is categorized as an ASCII letter or digit.
IsAsciiLetterUpper(Char) |  | Indicates whether a character is categorized as an uppercase ASCII letter.
IsBetween(Char, Char, Char) |  | Indicates whether a character is within the specified inclusive range.
IsControl(Char) |  | Indicates whether the specified Unicode character is categorized as a control character.
IsControl(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a control character.
IsDigit(Char) |  | Indicates whether the specified Unicode character is categorized as a decimal digit.
IsDigit(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.
IsHighSurrogate(Char) |  | Indicates whether the specified Char object is a high surrogate.
IsHighSurrogate(String, Int32) |  | Indicates whether the Char object at the specified position in a string is a high surrogate.
IsLetter(Char) |  | Indicates whether the specified Unicode character is categorized as a Unicode letter.
IsLetter(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a Unicode letter.
IsLetterOrDigit(Char) |  | Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.
IsLetterOrDigit(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a letter or a decimal digit.
IsLower(Char) |  | Indicates whether the specified Unicode character is categorized as a lowercase letter.
IsLower(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a lowercase letter.
IsLowSurrogate(Char) |  | Indicates whether the specified Char object is a low surrogate.
IsLowSurrogate(String, Int32) |  | Indicates whether the Char object at the specified position in a string is a low surrogate.
IsNumber(Char) |  | Indicates whether the specified Unicode character is categorized as a number.
IsNumber(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a number.
IsPunctuation(Char) |  | Indicates whether the specified Unicode character is categorized as a punctuation mark.
IsPunctuation(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a punctuation mark.
IsSeparator(Char) |  | Indicates whether the specified Unicode character is categorized as a separator character.
IsSeparator(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a separator character.
IsSurrogate(Char) |  | Indicates whether the specified character has a surrogate code unit.
IsSurrogate(String, Int32) |  | Indicates whether the character at the specified position in a specified string has a surrogate code unit.
IsSurrogatePair(Char, Char) |  | Indicates whether the two specified Char objects form a surrogate pair.
IsSurrogatePair(String, Int32) |  | Indicates whether two adjacent Char objects at a specified position in a string form a surrogate pair.
IsSymbol(Char) |  | Indicates whether the specified Unicode character is categorized as a symbol character.
IsSymbol(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as a symbol character.
IsUpper(Char) |  | Indicates whether the specified Unicode character is categorized as an uppercase letter.
IsUpper(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as an uppercase letter.
IsWhiteSpace(Char) |  | Indicates whether the specified Unicode character is categorized as white space.
IsWhiteSpace(String, Int32) |  | Indicates whether the character at the specified position in a specified string is categorized as white space.
Parse(String) |  | Converts the value of the specified string to its equivalent Unicode character.
ToLower(Char, CultureInfo) |  | Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.
ToLower(Char) |  | Converts the value of a Unicode character to its lowercase equivalent.
ToLowerInvariant(Char) |  | Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.
ToString() |  | Converts the value of this instance to its equivalent string representation.
ToString(Char) |  | Converts the specified Unicode character to its equivalent string representation.
ToString(IFormatProvider) |  | Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.
ToUpper(Char, CultureInfo) |  | Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.
ToUpper(Char) |  | Converts the value of a Unicode character to its uppercase equivalent.
ToUpperInvariant(Char) |  | Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.
TryParse(String, Char) |  | Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.

Минимальное значение типа char -- `'\0'`, максимальное -- `'\uffff'`.
Используя escape-последовательности без кавычек (`\u####`) можно именовать сущности кода и заменять любые другие нелитеральные символы соответствующими escape-последовательностями:

```csharp
void \u01001() { } //Ā1
"12345".To\u0043harArray(); //"12345".ToCharArray();
```

- Не всякий символ исходного кода можно заменить escape-последовательностью

- Такая возможность подменять символы их кодами Unicode, предоставляет удобный механизм обфускации кода*

## Статические методы

Char имеет статический метод `char.GetUnicodeCategory(char c)`, который возвращает категорию указанного символа в системе Unicode. Категория представляет собой перечисление UnicodeCategory (29 категорий): `UppercaseLetter`, `LowercaseLetter`, [Surrogate](obsidian://open?vault=IT%20Notes&file=C-Sharp%2F%D0%A1%D0%B8%D0%BC%D0%B2%D0%BE%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5%20%D1%82%D0%B8%D0%BF%D1%8B%2F%D0%A1%D1%83%D1%80%D1%80%D0%BE%D0%B3%D0%B0%D1%82%D0%BD%D1%8B%D0%B5%20%D0%BF%D0%B0%D1%80%D1%8B), `LineSeparator`, `DecimalDIgitNumber` и другие.

Полезные статические методы:
* IsLetter
* IsDigit
* IsWhiteSpace
* IsUpper
* IsLower
* IsPunctuation
* IsSurrogate
* IsHighSurrogate
* IsLowSurrogate
* IsNumber

* `ConvertFromUtf32(int utf32)` -- возвращает 1 или 2 символа в виде строки, представляющих символ кодировки UTF-32
* `ConvertToUtf32(char highSurrogate, char lowSurrogate)` -- возвращает код символа UTF-32, указанного с помощью [суррогатной пары](obsidian://open?vault=IT%20Notes&file=C-Sharp%2F%D0%A1%D0%B8%D0%BC%D0%B2%D0%BE%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5%20%D1%82%D0%B8%D0%BF%D1%8B%2F%D0%A1%D1%83%D1%80%D1%80%D0%BE%D0%B3%D0%B0%D1%82%D0%BD%D1%8B%D0%B5%20%D0%BF%D0%B0%D1%80%D1%8B).

* `GetNumericValue(char c)` -- конвертирует символ в число типа double, если конвертация невозможно возвращает -1. Конвертируются также дроби и другие символы, обозначающие числа.

Изменение регистра символа без учета региональных стандартов:
* `ToLowerInvariant()`
* `ToUpperInvariant()`

С учетом региональных стандартов (Culture):
* `ToLower(char c, [Culture])`
* `ToUpper(char c, [Culture])`

Например латинская буква `i` в верхнем регистре будет `I`, но, например, в турецком языке в верхнем регистре добавляется надстрочная точка.

## Конвертация типа char

1) С помощью приведения типа -- наиболее производительный (не вызываются методы)
2) С помощью Convert
3) Через упаковку в интерфейс IConvertible

### Escape-последовательности

Это сочетания символов, где первым символом всегда выступает черта `'\'`, а затем еще 1 буква или набор цифр.

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
| `\x##`             | Литерал char в кодировке ASCII                                      |
| `\u####`           | Литерал char представленный в виде 2 байтов в порядке little endian |
| `\U########`       | Строка, представляющая собой одиночный или суррогатный символ       |

[[String]], [[_CultureInfo]]

#C-Sharp #Text_types
