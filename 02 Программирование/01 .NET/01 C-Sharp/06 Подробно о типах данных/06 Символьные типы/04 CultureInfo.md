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

## Структура типа CultureInfo

| Метод экземпляра                | Возвращает    | Описание                                                                                                                                                              |
| ------------------------------- | ------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ClearCachedData()`             | `void`        | Обновляет кэш с информацией о культуре                                                                                                                                |
| `Clone()`                       | `object`      | Создает копию текущей культуры                                                                                                                                        |
| `GetConsoleFallbackUICulture()` | `CultureInfo` | Альтернативная культура, используемая для чтения и отображения текста в окне консоли                                                                                  |
| `GetFormat(Type?)`              | `object?`     | Параметр может принимать только 2 значения: `typeof(NumberFormatInfo)`, `typeof(DateTimeFormatInfo)`. Возвращает данные о формате чисел или даты для текущей культуры |
| `ToString()`                    | `string`      | Возвращает строку, содержащую имя текущего объекта `CultureInfo` в формате `languagecode2-country/regioncode2`                                                        |



| Статический метод                         | Возвращает      | Описание                                                                                                                                                                                |
| ----------------------------------------- | --------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `CreateSpecificCulture(string)`           | `CultureInfo`   | Создает объект `CultureInfo` на основе кодов ISO (например `uz-Latn-UZ` — узбекский)                                                                                                    |
| `GetCultureInfo()`                        | `CultureInfo`   | Получает кешированную культуру только для чтения                                                                                                                                        |
| `GetCultureInfoByIetfLanguageTag(string)` | `CultureInfo`   | **Не рекомендуется**. Служит для получения объекта `CultureInfo`, доступного только для чтения, который имеет языковые характеристики, указываемые определенным языковым тегом RFC 4646 |
| `GetCultures(CultureTypes)`               | `CultureInfo[]` | Возвращает список поддерживаемых языков и региональных параметров, отфильтрованный по заданным значениям параметра `CultureTypes`                                                       |
| `ReadOnly(CultureInfo)`                   | `CultureInfo`   | Возвращает оболочку над `CultureInfo` только для чтения                                                                                                                                 |

[Перечисление](https://learn.microsoft.com/ru-ru/dotnet/api/system.globalization.culturetypes?view=net-8.0) `CultureTypes`:

```cs
[System.Flags]
public enum CultureTypes {
    NeutralCultures        = 1, // Культуры, связанные
                                // с языком, но не с определенным регионом
    
    SpecificCultures       = 2, // Языки и региональные параметры, присущие региону
    
    InstalledWin32Cultures = 4, // Этот элемент устарел. Все языки и региональные
                                // параметры, установленные в ОС Windows
    
    AllCultures            = 7, // Все языки и региональные параметры, распознаваемые .NET
    
    UserCustomCulture      = 8, // Этот элемент устарел.
                                // Пользовательские языки и региональные параметры
    
    ReplacementCultures    = 16,// Этот элемент устарел. Пользовательский язык
                                // и региональные параметры, замещающие язык
                                // и региональные параметры платформы .NET Framework
    
    WindowsOnlyCultures    = 32,// Этот элемент считается нерекомендуемым и игнорируется
    
    FrameworkCultures      = 64,// Этот элемент считается нерекомендуемым.
                                // При использовании этого значения с GetCultures(CultureTypes)
                                // будут возвращены нейтральные и конкретные языки
                                // и региональные параметры, поставляемые
                                // с платформой .NET Framework 2.0
    
}
```

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
