
## Операторы сравнения

Оператор | Описание
-- | --
`<` | меньше
`>` | больше
`<=` | меньше или равен
`>=` | больше или равен
`=` | равно
`<>` или `!=` | не равно

https://www.postgresql.org/docs/9.5/functions-comparison.html

## Математические операторы

Оператор | Описание | Пример | Результат
-- | -- | -- | --
+ | addition | 2 + 3 | 5
- | subtraction | 2 - 3 | -1
* | multiplication | 2 * 3 | 6
/ | division (integer division truncates the result) | 4 / 2 | 2
% | modulo (remainder) | 5 % 4 | 1
^ | exponentiation (associates left to right) | 2.0 ^ 3.0 | 8
\|/ | square root | \|/ 25.0 | 5
\|\|/ | cube root | \|\|/ 27.0 | 3
! | factorial (deprecated, use factorial() instead) | 5 ! | 120
!! | factorial as a prefix operator (deprecated, use factorial() instead) | !! 5 | 120
@ | absolute value | @ -5.0 | 5
& | bitwise AND | 91 & 15 | 11
\| | bitwise OR | 32 \| 3 | 35
\# | bitwise XOR | 17 # 5 | 20
~ | bitwise NOT | ~1 | -2
<< | bitwise shift left | 1 << 4 | 16
>> | bitwise shift right | 8 >> 2 | 2

## Математические функции

Все перечисленные функции доступны в СУБД: [PostgreSQL](https://www.postgresql.org/docs/9.5/functions-math.html), MySQL, SQL Server, Oracle, SQLite

| Функция            | Описание                                                 |
| ------------------ | -------------------------------------------------------- |
| SUM(number)        | Суммирует значения по набору                             |
| MIN(number)        | Находит минимальное значение                             |
| MAX(number)        | Находит максимальное значение                            |
| AVG(number)        | Вычисляет среднее арифметическое                         |
| ABS(number)        | Модуль числа                                             |
| CEIL(number) / CEILING(number) | Округление вверх                             |
| FLOOR(number) | Округление вниз                                          |
| ROUND(number, decimal_places) | Округление до указанного количества знаков после запятой |
| TRUNCATE(number, decimal_places) | Усекает число разрядов числа без округления |
| POWER(x, y)        | Возведение в степень x^y                                 |
| SQUARE(number) | Возвращает квадрат числа |
| SQRT(number)             | Квадратный корень                                        |
| EXP(number)              | Возвращает e в степени x                                 |
| FACTORIAL(number)              | Возвращает факториал числа                               |
| LN(number)               | Натуральный логарифм                                     |
| LOG(number)              | Логарифм по основанию 10                                 |
| LOG(base, number)        | Логарифм по произвольному основанию                      |
| MOD(number, divisor) | Остаток от деления на divisor (делитель) |

### Тригонометрия

| Функция      | Описание |
| ------------ | -------- |
| PI()         | Возвращает число Пи |
| SIN(number)  | Синус угла (в радианах) |
| ASIN(number) | Арксинус |
| COS(number)  | Косинус угла |
| ACOS(number) | Арккосинус |
| TAN(number)  | Тангенс угла |
| ATAN(x)      | Арктангенс |
| ATAN2(y, x)  | Арктангенс y / x |
| COT(number)  | Котангенс |

Скрипт для тестирования:

```sql
SELECT
    ABS(-10)          AS AbsoluteValue,
    CEILING(4.2)      AS CeilingValue,
    FLOOR(4.8)        AS FloorValue,
    ROUND(3.14159, 2) AS RoundedValue,
    POWER(2, 3)       AS PowerValue,
    SQRT(25)          AS SquareRoot,
    LOG(10)           AS NaturalLog,
    PI()              AS PiValue;
```

### Случаные значения

Function | Return Type | Description
-- | -- | --
`random()` | `dp` | Случайное числов в диапазоне 0.0 <= x < 1.0
`setseed(dp)` | `void` | Задает ключ для генерации случайного значения (seed) для последовательных вызовов `random()`

## Работа с текстом

[Документация по PostgreSQL](https://www.postgresql.org/docs/9.5/functions-string.html)

Функция | Возвращаемый тип | Описание | Пример | Результат
------- | ---------------- | -------- | ------ | ---------
string \|\| string | text | Конкатенация (сложение) строк | `'Post'` \|\| `'greSQL'` | PostgreSQL
string \|\| non-string или non-string \|\| string | text | Конкатенация строки и нестрокового значения | 'Value: ' \|\| 42 | Value: 42
`bit_length(string)` | int | Количество бит в строке | bit_length('jose') | 32
`char_length(string)` или `character_length(string)` | int | Количество символов в строке | `char_length('jose')` | 4
`lower(string)` | text | Строка в нижнем регистре | `lower('TOM')` | tom
`octet_length(string)` | int | Количество байт, которое занимает строка | `octet_length('jose')` | 4
`overlay(string placing string from int [for int])` | text | Replace substring | `overlay('Txxxxas' placing 'hom' from 2 for 4)` | Thomas
`position(substring in string)` | int | Location of specified substring | `position('om' in 'Thomas')` | 3
`substring(string [from int] [for int])` | text | Extract substring | `substring('Thomas' from 2 for 3)` | hom
`substring(string from pattern)` | text | Extract substring matching POSIX regular expression. See Section 9.7 for more information on pattern matching. | substring('Thomas' from '...$') | mas
`substring(string from pattern for escape)` | text | Extract substring matching SQL regular expression. See Section 9.7 for more information on pattern matching. | `substring('Thomas' from '%#"o_a#"_' for '#')` | oma
`trim([leading / trailing / both] [characters] from string)` | text | Remove the longest string containing only characters from characters (a space by default) from the start, end, илиboth ends (both is the default) of string | trim(both 'xyz' from 'yxTomxx') | Tom
`trim([leading / trailing / both] [from] string [, characters])` | text | Нестандартный синтаксис для `trim()` | `trim(both from 'yxTomxx', 'xyz')` | Tom
`upper(string)` | `text` | Строка в верхнем регистре | `upper('tom')` | TOM

### Другие функции работы со строками

Функция | Возвращаемый тип | Описание | Пример | Результат
------- | ---------------- | -------- | ------ | ---------
`ascii(string)` | `int` | ASCII code of the first character of the argument. For UTF8 returns the Unicode code point of the character. For other multibyte encodings, the argument must be an ASCII character. | `ascii('x')` | 120
`btrim(string text [, characters text])` | `text` | Remove the longest string consisting only of characters in characters (a space by default) from the start and end of string | `btrim('xyxtrimyyx', 'xyz')` | trim
`chr(int)` | `text` | Character with the given code. For UTF8 the argument is treated as a Unicode code point. For other multibyte encodings the argument must designate an ASCII character. The NULL (0) character is not allowed because text data types cannot store such bytes. | `chr(65)` | A
`concat(str "any" [, str "any" [, ...] ])` | `text` | Concatenate the text representations of all the arguments. NULL arguments are ignored. | `concat('abcde', 2, NULL, 22)` | abcde222
`concat_ws(sep text, str "any" [, str "any" [, ...] ])` | `text` | Concatenate all but the first argument with separators. The first argument is used as the separator string. NULL arguments are ignored. | concat_ws(',', 'abcde', 2, NULL, 22) | abcde,2,22
`convert(string bytea, src_encoding name, dest_encoding name)` | `bytea` | Convert string to dest_encoding. The original encoding is specified by src_encoding. The string must be valid in this encoding. Conversions can be defined by CREATE CONVERSION. Also there are some predefined conversions. See Table 9-8 for available conversions. | `convert('text_in_utf8', 'UTF8', 'LATIN1')` | text_in_utf8 represented in Latin-1 encoding (ISO 8859-1)
convert_from(string bytea, src_encoding name) | `text` | Convert string to the database encoding. The original encoding is specified by src_encoding. The string must be valid in this encoding. | `convert_from('text_in_utf8', 'UTF8')` | text_in_utf8 represented in the current database encoding
`convert_to(string text, dest_encoding name)` | `bytea` | Convert string to dest_encoding. | `convert_to('some text', 'UTF8')` | some text represented in the UTF8 encoding
`decode(string text, format text)` | `bytea` | Decode binary data from textual representation in string. Options for format are same as in encode. | `decode('MTIzAAE=', 'base64')` | `\x3132330001`
`encode(data bytea, format text)` | `text` | Encode binary data into a textual representation. Supported formats are: base64, hex, escape. escape converts zero bytes and high-bit-set bytes to octal sequences (\nnn) and doubles backslashes. | encode('123\000\001', 'base64') | MTIzAAE=
`format(formatstr text [, formatarg "any" [, ...] ])` | `text` | Format arguments according to a format string. This function is similar to the C function sprintf. See Section 9.4.1. | format('Hello %s, %1$s', 'World') | Hello World, World
`initcap(string)` | `text` | Convert the first letter of each word to upper case and the rest to lower case. Words are sequences of alphanumeric characters separated by non-alphanumeric characters. | initcap('hi THOMAS') | Hi Thomas
`left(str text, n int)` | `text` | Возвращает первые n символов строки. Когда n отрицательное, возвращает все символы, кроме последних \|n\| символов | `left('abcde', 2)` | ab
`length(string)` | `int` | Number of characters in string | length('jose') | 4
`length(string bytea, encoding name)` | `int` | Number of characters in string in the given encoding. The string must be valid in this encoding. | `length('jose', 'UTF8')` | 4
`lpad(string text, length int [, fill text])` | `text` | Fill up the string to length length by prepending the characters fill (a space by default). If the string is already longer than length then it is truncated (on the right). | lpad('hi', 5, 'xy') | xyxhi
`ltrim(string text [, characters text])` | `text` | Remove the longest string containing only characters from characters (a space by default) from the start of string | `ltrim('zzzytest', 'xyz')` | test
`md5(string)` | `text` | Calculates the MD5 hash of string, returning the result in hexadecimal | `md5('abc')` | 900150983cd24fb0 d6963f7d28e17f72
`pg_client_encoding()` | name | Current client encoding name | `pg_client_encoding()` | SQL_ASCII
`quote_ident(string text)` | `text` | Return the given string suitably quoted to be used as an identifier in an SQL statement string. Quotes are added only if necessary (i.e., if the string contains non-identifier characters or would be case-folded). Embedded quotes are properly doubled. See also Example 40-1. | `quote_ident('Foo bar')` | "Foo bar"
`quote_literal(string text)` | `text` | Return the given string suitably quoted to be used as a string literal in an SQL statement string. Embedded single-quotes and backslashes are properly doubled. Note that quote_literal returns null on null input; if the argument might be null, quote_nullable is often more suitable. See also Example 40-1. | quote_literal(E'O\'Reilly') | 'O''Reilly'
`quote_literal(value anyelement)` | `text` | Coerce the given value to text and then quote it as a literal. Embedded single-quotes and backslashes are properly doubled. | `quote_literal(42.5)` | '42.5'
`quote_nullable(string text)` | `text` | Return the given string suitably quoted to be used as a string literal in an SQL statement string; or, if the argument is null, return NULL. Embedded single-quotes and backslashes are properly doubled. See also Example 40-1. | quote_nullable(NULL) | NULL
`quote_nullable(value anyelement)` | `text` | Coerce the given value to text and then quote it as a literal; or, if the argument is null, return NULL. Embedded single-quotes and backslashes are properly doubled. | quote_nullable(42.5) | '42.5'
`regexp_matches(string text, pattern text [, flags text])` | setof text[] | Return all captured substrings resulting from matching a POSIX regular expression against the string | regexp_matches('foobarbequebaz', '(bar)(beque)') | {bar,beque}
regexp_replace(string text, pattern text, replacement text [, flags text]) | text | Replace substring(s) matching a POSIX regular expression | regexp_replace('Thomas', '.[mN]a.', 'M') | ThM
`regexp_split_to_array(string text, pattern text [, flags text ])` | text[] | Split string using a POSIX regular expression as the delimiter | `regexp_split_to_array('hello world', '\s+')` | {hello,world}
`regexp_split_to_table(string text, pattern text [, flags text])` | setof text | Split string using a POSIX regular expression as the delimiter | `regexp_split_to_table('hello world', '\s+')` | hello <br/> world 
`repeat(string text, number int)` | `text` | Repeat string the specified number of times | repeat('Pg', 4) | PgPgPgPg
`replace(string text, from text, to text)` | `text` | Replace all occurrences in string of substring from with substring to | replace('abcdefabcdef', 'cd', 'XX') | abXXefabXXef
`reverse(str)` | `text` | Return reversed string. | `reverse('abcde')` | edcba
`right(str text, n int)` | text | Return last n characters in the string. When n is negative, return all but first |n| characters. | right('abcde', 2) | de
rpad(string text, length int [, fill text]) | text | Fill up the string to length length by appending the characters fill (a space by default). If the string is already longer than length then it is truncated. | rpad('hi', 5, 'xy') | hixyx
rtrim(string text [, characters text]) | text | Remove the longest string containing only characters from characters (a space by default) from the end of string | rtrim('testxxzx', 'xyz') | test
split_part(string text, delimiter text, field int) | text | Split string on delimiter and return the given field (counting from one) | split_part('abc~@~def~@~ghi', '~@~', 2) | def
strpos(string, substring) | int | Location of specified substring (same as position(substring in string), but note the reversed argument order) | strpos('high', 'ig') | 2
substr(string, from [, count]) | text | Extract substring (same as substring(string from from for count)) | substr('alphabet', 3, 2) | ph
to_ascii(string text [, encoding text]) | text | Convert string to ASCII from another encoding (only supports conversion from LATIN1, LATIN2, LATIN9, and WIN1250 encodings) | to_ascii('Karel') | Karel
to_hex(number int or bigint) | text | Convert number to its equivalent hexadecimal representation | to_hex(2147483647) | 7fffffff
translate(string text, from text, to text) | text | Any character in string that matches a character in the from set is replaced by the corresponding character in the to set. If from is longer than to, occurrences of the extra characters in from are removed. | translate('12345', '143', 'ax') | a2x5

----

## COUNT(col_name)

Функция возвращает количество записей (строк) таблицы. Запись функции с указанием столбца вернет количество записей конкретного столбца за исключением NULL записей.

COUNT(\*) Вернет количество всех записей в таблице.

## AVG(col_name)

функция возвращающая среднее значение столбца. Данная функция применима только для числовых столбцов.

## MIN(col_name)

Минимальное значение столбца

## MAX(col_name)

Максимальное значение столбца

## SUM( \[ALL | DISTINCT\] expression)

функция, возвращающая сумму значений столбца таблицы. Используется только для числовых столбцов.
DISTINCT -- подсчет только уникальных столбцов;
ALL -- по умолчанию -- всех.

## ROUND(expression, lenght)

Функция для округления числового выражения.
**_expression_** может быть столбцом, набором столбцов, или числовым выражением;
**_length_** -- точность округления (количество знаков после запятой)

## UCASE(col_name) / UPPER для MSSQL

Возвращает значение столбца или столбцов в верхнем регистре

## LCASE(col_name) / LOWER для MSSQL

Возвращает значение столбца или столбцов в нижнем регистре

## LEN(col_name)

Возвращает длину значения поля в записи. Конечные пробелы не учитываются

## MID(col_name, start\[, lenght\])

Возвращает подстроку текстового поля.
**_start_** -- начало подстроки в символах (начинается с 1)
**_length_** -- длина подстроки

## EXISTS(выборка)

Проверяет существует ли хотя бы одна запись в выборке, получаемой из подзапроса (возвращает true или false).
Можно получить выборку, которая сообщает о наличии заданной выборки:

```sql
SELECT EXISTS(SELECT Login FROM User WHERE Login = 'Bomber')
```

## LEN(string)

Возвращает длину любой строки.

# Функции даты и времени
| Функция                | Описание                                                                                                |
| ---------------------- | ------------------------------------------------------------------------------------------------------- |
| NOW()                  | (GETDATE() в MS SQL) Возвращает текущую дату и время                                                    |
| CURDATE()              | Текущая дата                                                                                            |
| CURTIME()              | Текущее время                                                                                           |
| DATEDIFF(date1, date2) | возвращает разницу в датах. Аргументом может быть строка ('2021-08-21') или функция возвращающая время. |
| int YEAR( *date* )     | возвращает год указанной даты типа date                                                                 |
| int MONTH( *date* )    | месяц указанной даты типа date                                                                          |
| int DAY( *date* )      | день указанной даты типа date                                                                           |

#DB #SQL
