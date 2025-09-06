Это класс, предоставляющий информацию о региональных стандартах. В языках с неуправляемым кодом это называется `locale`

`CultureInfo` включает в себя информацию о:

1. Названии регионального стандарта
2. Системы записи
3. Используемом календаре
4. Порядке сортировки строк
5. Форматировании дат и чисел

При запуске приложения каждый поток в .NET определяет 2 объекта: `CultureInfo.CurrentCulture` & `CultureInfo.CurrentUICulture`

Каждая культура описывается в формате `RFC 4646`:

```text
culture-subculture
```

Где `culture` представляет код *ISO 639* в нижнем регистре, ассоциированный с языком.
А `subculture` представляет код *ISO 3166* в верхнем регистре, ассоциированный со страной или регионом. Код *ISO 3166* можно не указывать, если не важны региональные особенности культуры

Создание:

```csharp
// По кодам ISO
var cult1 = new CultureInfo("en-UA");
var cult2 = new CultureInfo("en");

// По коду LSID
CultureInfo enUs = new CultureInfo(1033); //en-US
```

Можно задавать культуры по специальному идентификатору **LCID** (LoCale ID)

Свойство `Parent` является ссылкой на культуру, на которой основана данная культура, например, британская культура и американская культура унаследованы от нейтральной английской культуры

Код:

```csharp
CultureInfo enGb = new CultureInfo("en-GB");  
CultureInfo enUs = new CultureInfo("en-US");  
Console.WriteLine(enGb.DisplayName);  
Console.WriteLine(enUs.DisplayName);  
Console.WriteLine(enGb.Parent.DisplayName);  
Console.WriteLine(enUs.Parent.DisplayName);
```

Вывод:

```text
English (United Kingdom)  
English (United States)  
English  
English
```

## Структура типа CultureInfo

| Метод экземпляра                          | Возвращает      | Описание                                                                                                                                                              |
| ----------------------------------------- | --------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ClearCachedData()`                       | ``              |                                                                                                                                                                       |
| `Clone()`                                 | ``              |                                                                                                                                                                       |
| `CreateSpecificCulture(string)`           | ``              |                                                                                                                                                                       |
| `GetConsoleFallbackUICulture()`           | ``              |                                                                                                                                                                       |
| `GetCultureInfo()`                        | ``              |                                                                                                                                                                       |
| `GetCultureInfoByIetfLanguageTag(string)` | ``              |                                                                                                                                                                       |
| `GetCultures(CultureTypes)`               | `CultureInfo[]` | Возвращает список поддерживаемых языков и региональных параметров, отфильтрованный по заданным значениям параметра `CultureTypes`                                     |
| `GetFormat(Type?)`                        | `object?`       | Параметр может принимать только 2 значения: `typeof(NumberFormatInfo)`, `typeof(DateTimeFormatInfo)`. Возвращает данные о формате чисел или даты для текущей культуры |
| `ToString()`                              | `string`        | Возвращает строку, содержащую имя текущего объекта `CultureInfo` в формате `languagecode2-country/regioncode2`                                                        |



| Статический метод                         | Возвращает    | Описание                                                |
| ----------------------------------------- | ------------- | ------------------------------------------------------- |
| `ClearCachedData()`                       | ``            |                                                         |
| `Clone()`                                 | ``            |                                                         |
| `CreateSpecificCulture(string)`           | ``            |                                                         |
| `GetConsoleFallbackUICulture()`           | ``            |                                                         |
| `GetCultureInfo()`                        | ``            |                                                         |
| `GetCultureInfoByIetfLanguageTag(string)` | ``            |                                                         |
| `GetCultures(CultureTypes)`               | ``            |                                                         |
| `ReadOnly(CultureInfo)`                   | `CultureInfo` | Возвращает оболочку над `CultureInfo` только для чтения |

## Список культур

| ISO        | LCID  | Регион                                  |
| ---------- | ----- | --------------------------------------- |
| __         | 127   | Инвариантный язык (страна)              |
| en         | 9     | английский                              |
| en-US      | 1033  | английский (США)                        |
| en-GB      | 2057  | английский (Великобритания)             |
| es         | 10    | испанский                               |
| es-ES      | 3082  | испанский (Испания)                     |
| es-MX      | 2058  | испанский (Мексика)                     |
| it         | 16    | итальянский                             |
| it-IT      | 1040  | итальянский (Италия)                    |
| de         | 7     | немецкий                                |
| de-DE      | 1031  | немецкий (Германия)                     |
| de-AT      | 3079  | немецкий (Австрия)                      |
| fr         | 12    | французский                             |
| fr-FR      | 1036  | французский (Франция)                   |
| ru         | 25    | русский                                 |
| ru-RU      | 1049  | русский (Россия)                        |
| ru-UA      | 4096  | русский (Украина)                       |
| ru-BY      | 4096  | русский (Беларусь)                      |
| uk         | 34    | украинский                              |
| uk-UA      | 1058  | украинский (Украина)                    |
| be         | 35    | белорусский                             |
| be-BY      | 1059  | белорусский (Беларусь)                  |
| pl         | 21    | польский                                |
| pl-PL      | 1045  | польский (Польша)                       |
| hy         | 43    | армянский                               |
| ka-GE      | 1079  | грузинский (Грузия)                     |
| kk         | 63    | казахский                               |
| kk-KZ      | 1087  | казахский (Казахстан)                   |
| ja         | 17    | японский                                |
| ja-JP      | 1041  | японский (Япония)                       |
| zh         | 30724 | китайский                               |
| zh-Hans-CN | 4096  | китайский (упрощенная китайская, Китай) |

Этот код выводит полный список культур:

```csharp
CultureInfo[] specificCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
foreach (CultureInfo ci in specificCultures) {
    Console.WriteLine(ci.Name + " / " + ci.LCID + " -- " + ci.DisplayName);
}
Console.WriteLine("Total: " + specificCultures.Length + '\n');

CultureInfo[] neutralCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
foreach (CultureInfo ci in neutralCultures) {
    Console.WriteLine(ci.Name + " / " + ci.LCID + " -- " + ci.DisplayName);
}
Console.WriteLine("Total: " + neutralCultures.Length);
```

Продолжение:
https://csharp.net-tutorials.com/working-with-culture-and-regions/the-cultureinfo-class/

#MERGE_NOTES

#C-Sharp #Char_types
