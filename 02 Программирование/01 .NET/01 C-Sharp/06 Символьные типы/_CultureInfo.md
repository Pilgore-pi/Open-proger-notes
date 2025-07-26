Это класс, предоставляющий информацию о региональных стандартах. В языках с неуправляемым кодом это называется `locale`.

CultureInfo включает в себя информацию о:
1. Названии регионального стандарта
2. Системы записи
3. Используемом календаре
4. Порядок сортировки строк
5. Форматирование для дат и чисел

При запуске приложения каждый поток в .Net определяет 2 объекта: CultureInfo.CurrentCulture & CultureInfo.CurrentUICulture.

Каждая культура описывается в формате `RFC 4646`:
```
culture-subculture

Где culture представляет код ISO 639 в нижнем регистре,
ассоциированный с языком.
А subculture представляет код ISO 3166 в верхнем регистре,
ассоциированный со страной или регионом
```

* Можно не указывать код ISO 3166, если не важны региональные особенности культуры.

Создание:
```csharp
var cult1 = new CultureInfo("en-UA");
var cult2 = new CultureInfo("en");
```

Свойство Parent является ссылкой на культуру, на которой основана данная культура, например, британская культура и американская культура унаследованы от нейтральной английской культуры.

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
```
English (United Kingdom)  
English (United States)  
English  
English
```

* Можно задавать культуры по LCID (LoCale ID). Ниже приведен список возможных культур и их LCID.

ISO | LCID | Регион
-|-|-
__ | 127 | Invariant Language (Invariant Country)
en | 9 | английский
en-US | 1033 | английский (США)
en-GB | 2057 | английский (Великобритания)
es | 10 | испанский
es-ES | 3082 | испанский (Испания)
es-MX | 2058 | испанский (Мексика)
it | 16 | итальянский
it-IT | 1040 | итальянский (Италия)
de | 7 | немецкий
de-DE | 1031 | немецкий (Германия)
de-AT | 3079 | немецкий (Австрия)
fr | 12 | французский
fr-FR | 1036 | французский (Франция)
ru | 25 | русский
ru-RU | 1049 | русский (Россия)
ru-UA | 4096 | русский (Украина)
ru-BY | 4096 | русский (Беларусь)
uk | 34 | украинский
uk-UA | 1058 | украинский (Украина)
be | 35 | белорусский
be-BY | 1059 | белорусский (Беларусь)
pl | 21 | польский
pl-PL | 1045 | польский (Польша)
hy | 43 | армянский
ka-GE | 1079 | грузинский (Грузия)
kk | 63 | казахский
kk-KZ | 1087 | казахский (Казахстан)
ja | 17 | японский
ja-JP | 1041 | японский (Япония)
zh | 30724 | китайский
zh-Hans-CN | 4096 | китайский (упрощенная китайская, Китай)

Этот код выводит полный список культур
```csharp
CultureInfo[] specificCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
foreach (CultureInfo ci in specificCultures)
    Console.WriteLine(ci.Name + " / " + ci.LCID + " -- " + ci.DisplayName);
Console.WriteLine("Total: " + specificCultures.Length);

Console.WriteLine();

CultureInfo[] neutralCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
foreach (CultureInfo ci in neutralCultures)
    Console.WriteLine(ci.Name + " / " + ci.LCID + " -- " + ci.DisplayName);
Console.WriteLine("Total: " + neutralCultures.Length);
```

Продолжение:
https://csharp.net-tutorials.com/working-with-culture-and-regions/the-cultureinfo-class/

#C-Sharp #Char_types