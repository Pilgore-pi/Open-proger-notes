Это класс, предоставляющий информацию о региональных стандартах. В языках с неуправляемым кодом это называется `locale`

`CultureInfo` включает в себя информацию о:

1. Названии регионального стандарта
2. Системы записи
3. Используемом календаре
4. Порядке сортировки строк
5. Форматировании дат и чисел

При запуске приложения каждый поток в .NET определяет 2 объекта: `CultureInfo.CurrentCulture` & `CultureInfo.CurrentUICulture`

**Далее понятие "язык и региональные параметры" будет обозначаться словом "культура"**

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

## Структура типа System.Globalization.CultureInfo

| Свойство                        | Тип                  | Описание                                                                                                                                                                                                                                                                |
| ------------------------------- | -------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Calendar`                      | `Calendar`           | Возвращает календарь, используемый по умолчанию для культуры                                                                                                                                                                                                            |
| `DateTimeFormat`                | `DateTimeFormatInfo` | Информация о формате данных текущей культуры                                                                                                                                                                                                                            |
| `CurrentUICulture`              | `CultureInfo`        | Текущая культура пользовательского интерфейса, используемая диспетчером ресурсов для поиска ресурсов, связанных с конкретным языком и региональными параметрами, во время выполнения                                                                                    |
| `CurrentCulture`                | `CultureInfo`        |                                                                                                                                                                                                                                                                         |
| `CultureTypes`                  | `CultureTypes`       | Флаги типов текущей культуры                                                                                                                                                                                                                                            |
| `CompareInfo`                   | `CompareInfo`        | Информация о способе сравнения строк в данном языке и региональных параметрах                                                                                                                                                                                           |
| `DisplayName`                   | `string`             | Полное локализованное имя культуры                                                                                                                                                                                                                                      |
| `DefaultThreadCurrentCulture`   | `CultureInfo?`       | Культура, используемая по умолчанию для потоков в текущем домене приложения                                                                                                                                                                                             |
| `DefaultThreadCurrentUICulture` | `CultureInfo?`       | Культура пользовательского интерфейса, используемая по умолчанию для потоков в текущем домене приложения                                                                                                                                                                |
| `EnglishName`                   | `string`             | Имя языка и региональных параметров в формате `languagefull [country/regionfull]` на английском языке                                                                                                                                                                   |
| `IetfLanguageTag`               | `string`             | **Не рекомендуется**. Получает идентификацию языка по стандарту RFC 4646. Вместо этого следует использовать [CultureInfo.Name](https://learn.microsoft.com/ru-ru/dotnet/api/system.globalization.cultureinfo.name?view=net-8.0) свойство . Теги и имена IETF идентичны. |
| `InstalledUICulture`            | `CultureInfo`        | Культура операционной системы                                                                                                                                                                                                                                           |
| `static InvariantCulture`       | `CultureInfo`        | Возвращает инвариантную (нейтральную) культуру                                                                                                                                                                                                                          |
| `IsNeutralLanguage`             | `bool`               | Показывает, является ли культура нейтральной                                                                                                                                                                                                                            |
| `IsReadOnly`                    | `bool`               | Флаг "только для чтения" для текущей культуры                                                                                                                                                                                                                           |
| `KeyboardLayoutId`              | `int`                | Активный идентификатор языка ввода                                                                                                                                                                                                                                      |
| `LCID`                          | `int`                | Возвращает идентификатор LCID текущей культуры                                                                                                                                                                                                                          |
| `Name`                          | `string`             | Имя культуры в формате: `languagecode2-country/regioncode2`                                                                                                                                                                                                             |
| `NativeName`                    | `string`             | Имя культуры, состоящей из языка, страны или региона и дополнительного набора символов, которые свойственны для этого языка                                                                                                                                             |
| `NumberFormat`                  | `NumberFormatInfo`   | Информация о числовом формате культуры                                                                                                                                                                                                                                  |
| `OptionalCalendars`             | `Calendar[]`         | Список календарей, которые могут использоваться в данной культуре                                                                                                                                                                                                       |
| `Parent`                        | `CultureInfo`        | Родительская культура                                                                                                                                                                                                                                                   |
| `TextInfo`                      | `TextInfo`           | Информация о системе письма, связанная с текущей культурой                                                                                                                                                                                                              |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |
| ``                              | ``                   |                                                                                                                                                                                                                                                                         |









| Метод экземпляра                | Возвращает    | Описание                                                                                                                                                              |
| ------------------------------- | ------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ClearCachedData()`             | `void`        | Обновляет кэш с информацией о культуре                                                                                                                                |
| `Clone()`                       | `object`      | Создает копию текущей культуры                                                                                                                                        |
| `GetConsoleFallbackUICulture()` | `CultureInfo` | Альтернативная культура, используемая для чтения и отображения текста в окне консоли                                                                                  |
| `GetFormat(Type?)`              | `object?`     | Параметр может принимать только 2 значения: `typeof(NumberFormatInfo)`, `typeof(DateTimeFormatInfo)`. Возвращает данные о формате чисел или даты для текущей культуры |
| `ToString()`                    | `string`      | Возвращает строку, содержащую имя текущего объекта `CultureInfo` в формате `languagecode2-country/regioncode2`                                                        |

[Перечисление](https://learn.microsoft.com/ru-ru/dotnet/api/system.globalization.culturetypes?view=net-8.0) `CultureTypes`:

```cs
[System.Flags]
public enum CultureTypes {
    NeutralCultures        = 1, // Культуры, связанные
                                // с языком, но не с определенным регионом
    
    SpecificCultures       = 2, // Культуры, присущие региону
    
    InstalledWin32Cultures = 4, // Этот элемент устарел. Все культуры,
                                // установленные в ОС Windows
    
    AllCultures            = 7, // Все культуры, распознаваемые .NET
    
    UserCustomCulture      = 8, // Этот элемент устарел.
                                // Пользовательские культуры
    
    ReplacementCultures    = 16,// Этот элемент устарел. Пользовательская культура,
                                // замещающая культуру платформы .NET Framework
    
    WindowsOnlyCultures    = 32,// Этот элемент считается нерекомендуемым и игнорируется
    
    FrameworkCultures      = 64,// Этот элемент считается нерекомендуемым.
                                // При использовании этого значения с GetCultures(CultureTypes)
                                // будут возвращены нейтральные и конкретные культуры,
                                // поставляемые с платформой .NET Framework 2.0
}
```


| Статический метод                         | Возвращает      | Описание                                                                                                                                                                                |
| ----------------------------------------- | --------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `CreateSpecificCulture(string)`           | `CultureInfo`   | Создает объект `CultureInfo` на основе кодов ISO (например `uz-Latn-UZ` — узбекский)                                                                                                    |
| `GetCultureInfo()`                        | `CultureInfo`   | Получает кешированную культуру только для чтения                                                                                                                                        |
| `GetCultureInfoByIetfLanguageTag(string)` | `CultureInfo`   | **Не рекомендуется**. Служит для получения объекта `CultureInfo`, доступного только для чтения, который имеет языковые характеристики, указываемые определенным языковым тегом RFC 4646 |
| `GetCultures(CultureTypes)`               | `CultureInfo[]` | Возвращает список поддерживаемых языков и региональных параметров, отфильтрованный по заданным значениям параметра `CultureTypes`                                                       |
| `ReadOnly(CultureInfo)`                   | `CultureInfo`   | Возвращает оболочку над `CultureInfo` только для чтения                                                                                                                                 |

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
