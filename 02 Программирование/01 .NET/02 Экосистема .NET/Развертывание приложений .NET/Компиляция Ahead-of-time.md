
Стандартная компиляция программы .NET происходит в 2 этапа:

```text
C# -> IL -> Assembly
```

1. Компиляция проекта C# в промежуточный IL-код
2. JIT-компиляция динамического кода во время выполнения в машинный код

AOT меняет механизм компиляции, оптимизируя его за счет дополнительных ограничений: **нативность** и **предсказуемость**

- **Нативность**. Нативное AOT-приложение может работать только в определенной ОС или ОС с совместимыми системными функциями. Даже в рамках одного семейства ОС (Windows, Linux) работоспособность программы зависит от версии ядра ОС

- **Предсказуемость**. В AOT-приложении недопустимо использование любого динамического кода, который должен компилироваться JIT-компилятором. По этой причине рефлексию и ключевое слово `dynamic` использовать нельзя

Механизм компиляции при AOT:

```text
C# -> Assembly
```

Проект C# напрямую компилируется в машинный код, а JIT-компиляция не задействуется. При этом выполняется обрезка неиспользуемого кода и удаление лишних зависимостей

Преимущества компиляции AOT:

- Файлы скомпилированной сборки занимают меньше места на диске из-за обрезки лишнего кода и зависимостей
- Запуск приложения выполняется в разы быстрее, так как пропускается этап с JIT-компиляцией
- Использовать режим AOT крайне просто — достаточно просто задать флаг в файле проекта для компиляции в AOT

Недостатки:

- **Нельзя использовать рефлексию**, что приводит к необходимости переработки проекта и замены кода с рефлексией
- **Ограничения библиотек**. Необходимо дополнительно анализировать все подключаемые сборки на использование рефлексии, что ограничивает разработчика в выборе библиотек
- **Обрезка неиспользуемого кода**. AOT выполняется в режиме обрезки неиспользуемого кода, что может нарушить работу таких библиотек, как System.Text.Json. По этой причине, приходится проводить дополнительный анализ кода и делать изменения в нем
- **Долгая компиляция**. Компиляция в режиме AOT занимает больше времени, чем стандартная компиляция
- **Проблемы с антивирусом**. Существует известная проблема со сборками AOT. Антивирус Microsoft Defender часто воспринимает программы скомпилированные в AOT как вредоносные, поэтому при запуске или скачивании файлов сборки антивирус может просто удалить эти файлы
- **Зависимость от платформы**. AOT приложение работает только в конретной версии ОС. Кроме того, выполнять AOT компиляцию следует выполнять только на целевой ОС, иначе приложение может быть не работоспособным

https://stackoverflow.com/questions/74580241/net-ahead-of-time-publishreadytorun-vs-publishaot-vs-runaotcompilation

RTR and AOT both precompile the assemblies in the project. But big difference, AOT must precompile everything, RTR still allows the just-in-time compiler to run to deal with code that could not correctly be precompiled


PublishAOT will not ship a JIT with your application. Many reflection APIs will fail at runtime. You will be unable to load .NET assemblies into your application at runtime, so your app won't be extensible in the traditional .NET ways.

PublishReadyToRun gives you some or most of the perf benefit of AOT without the added restrictions that PublishAOT brings

```xml
<PropertyGroup>
    <PublishAot>true</PublishAot>
</PropertyGroup>
```

AOT Blazor WebAssembly:

```xml
<PropertyGroup>
    <RunAOTCompilation>true</RunAOTCompilation>
</PropertyGroup>
```

> Кароче, не стоит использовать R2R и AOT, если в этом нет необходимости, так как даже не во всех случаях будет повышена производительность

## Ready to run compilation

УКАЗАТЬ ВЕРСИИ .NET, ПОДДЕРЖИВАЮЩИЕ R2R, AOT

https://learn.microsoft.com/en-us/dotnet/core/deploying/ready-to-run

> **`ReadyToRun`** (`R2R`) компиляция — это форма AOT компиляции

```cli
dotnet publish -c Release -r win-x64 -p:PublishReadyToRun=true
```

```xml
<PropertyGroup>
    <PublishReadyToRun>true</PublishReadyToRun>
</PropertyGroup>
```

To exclude specific assemblies from ReadyToRun processing, use the `<PublishReadyToRunExclude>` list

```xml
<ItemGroup>
    <PublishReadyToRunExclude Include="Contoso.Example.dll" />
</ItemGroup>
```

----

ЧТО ТАКОЕ ReadyToRun компиляция

## [Habr](https://habr.com/ru/companies/timeweb/articles/815209/)

https://habr.com/ru/articles/922944/

https://www.reddit.com/r/csharp/comments/1bxitto/eli5_what_is_aot_and_what_kind_of_apps_can_it/?tl=ru

На платформе .NET 7 впервые была представлена новая модель развертывания: опережающая нативная компиляция. Когда приложение .NET компилируется нативно по методу AOT, оно превращается в автономный нативный исполняемый файл, оснащённый собственной минимальной средой исполнения для управления выполнением кода.

Время выполнения весьма небольшое и в .NET 8 можно создавать автономные приложения на C# размером менее 1 МБ. Для сравнения: размер нативного приложения AOT Hello World на C# ближе к размеру аналогичного приложения в Rust, чем в Golang, при этом вшестеро меньше аналогичного приложения на Java.

Кроме того, впервые в истории программы .NET распространяются в формате файла, отличном от того, что определён в ECMA-335 (т. е. в виде инструкций и метаданных для виртуальной машины), а именно, распространяются в виде нативного кода (формат файла PE/ELF/Mach-O) с нативными структурами данных, точно как, например, в С++. Это означает, что ни один из инструментов реверс-инжиниринга для .NET, созданных за последние 20 лет, не работает с нативной опережающей компиляцией.

К сожалению, из-за этих двух аспектов (компактность и сложность реверс-инжиниринга) нативная AOT-компиляция популярна среди авторов вредоносного ПО, о чем свидетельствуют, например, эти статьи:

https://malware.news/t/analysis-of-ms-sql-server-proxyjacking-cases/72766
https://jfrog.com/blog/impala-stealer-malicious-nuget-package-payload/
https://labs.withsecure.com/content/dam/labs/docs/WithSecure_Research_DUCKTAIL_Returns.pdf

<img src="https://r2cdn.perplexity.ai/pplx-full-logo-primary-dark%402x.png" class="logo" width="120"/>

# Расскажи мне про механизм Ahead-of-time. Какие преимущества и недостатки у этого механизма

Механизм Ahead-of-Time (AOT) — это способ компиляции программного кода в машинный код заранее, до запуска приложения, в отличие от Just-in-Time (JIT), который компилирует код во время выполнения программы[^1_1][^1_2].

## Как работает AOT

AOT-компиляция происходит на этапе сборки или развертывания приложения. Исходный код (например, C\#, TypeScript, Java) преобразуется в оптимизированный машинный код или JavaScript (в случае Angular), который уже готов к исполнению без дополнительной компиляции в рантайме[^1_1][^1_2][^1_5]. Это снижает нагрузку на время запуска и уменьшает задержки при работе приложения.

## Преимущества AOT

- **Быстрый старт приложения:** Поскольку код уже скомпилирован, время запуска сокращается, нет необходимости ждать компиляции в рантайме[^1_2][^1_3][^1_5].
- **Лучшее быстродействие:** Отсутствие необходимости компилировать код во время выполнения позволяет приложению работать плавнее и быстрее[^1_5].
- **Меньший размер итогового кода:** AOT-компилятор может удалить неиспользуемый код (tree shaking), что уменьшает размер сборки и ускоряет загрузку[^1_2][^1_5].
- **Раннее обнаружение ошибок:** Ошибки в шаблонах и коде выявляются на этапе компиляции, а не во время выполнения, что повышает надежность приложения[^1_2][^1_5].
- **Совместимость с более старыми устройствами и браузерами:** AOT-код не требует сложной поддержки рантайма, что расширяет аудиторию пользователей[^1_5].
- **Оптимизации:** Компилятор имеет больше времени для выполнения сложных оптимизаций, чем JIT, который ограничен временем выполнения[^1_1][^1_4][^1_6].

## Недостатки AOT

- **Отсутствие динамической компиляции:** Невозможно динамически загружать или генерировать код во время выполнения, что ограничивает гибкость[^1_7].
- **Больший размер бинарника при компиляции для всех платформ:** Если приложение должно работать на разных устройствах, приходится либо создавать отдельные сборки, либо жертвовать оптимизацией под конкретную платформу[^1_1][^1_4].
- **Увеличенное время сборки:** Поскольку компиляция происходит заранее и может быть сложной, процесс сборки занимает больше времени[^1_4][^1_6].
- **Не всегда эффективно для небольших приложений:** Для маленьких проектов выигрыш в производительности может быть незначительным, а время сборки — неоправданно большим[^1_4].
- **Ограничения по функционалу:** Например, в .NET Native AOT отсутствует поддержка динамической загрузки сборок и генерации кода в рантайме[^1_7].

## Другие особенности

- Дизассеблирование выполнять для AOT программ гораздо тяжелее, чем для стандартных приложений .NET, так как весь код сразу компилируется в машинный код

## Итог

AOT — это технология, которая улучшает производительность и надежность приложений за счет предварительной компиляции кода в машинный формат. Она особенно полезна для крупных и производительных приложений, где важна скорость запуска и стабильность. Однако AOT ограничивает динамическую гибкость и увеличивает время сборки, что делает его менее удобным для быстрого прототипирования и небольших проектов. Выбор между AOT и JIT зависит от конкретных требований проекта и среды выполнения[^1_4][^1_5][^1_6].

Если говорить о .NET, то Native AOT позволяет создавать нативные исполняемые файлы с улучшенной производительностью, но при этом накладывает ограничения на динамические возможности приложения[^1_7].

<div style="text-align: center">⁂</div>

[^1_1]: https://www.intel.com/content/www/us/en/developer/articles/technical/ahead-of-time-compilation.html

[^1_2]: https://dev.to/this-is-angular/understanding-angular-ahead-of-time-aot-compilation-2m7p

[^1_3]: https://www.ibm.com/docs/en/sdk-java-technology/8?topic=reference-aot-compiler

[^1_4]: https://stackoverflow.com/questions/2106380/what-are-the-advantages-of-just-in-time-compilation-versus-ahead-of-time-compila

[^1_5]: https://www.monarch-innovation.com/aot-vs-jit-compiler-in-angular

[^1_6]: https://www.reddit.com/r/Compilers/comments/19ctf7p/aot_vs_jit_comilation/

[^1_7]: https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/

[^1_8]: https://en.wikipedia.org/wiki/Ahead-of-time_compilation

[^1_9]: https://abp.io/community/articles/understanding-angular-aot-vs-jit-compilations-0r0a0a3f

[^1_10]: https://bell-sw.com/blog/compilation-in-java-jit-vs-aot/

[^1_11]: https://sentry.engineering/blog/should-you-could-you-aot

[^1_12]: https://aminnez.com/programming-concepts/jit-vs-aot-compiler-pros-cons

[^1_13]: https://dev.to/bytehide/the-ultimate-guide-to-net-native-aot-benefits-and-examples-pg4

[^1_14]: https://www.mono-project.com/docs/advanced/aot/

[^1_15]: https://www.intel.com/content/www/us/en/docs/dpcpp-cpp-compiler/developer-guide-reference/2023-1/ahead-of-time-compilation.html

[^1_16]: https://dzone.com/articles/just-in-time-jit-compilation-advantages-disadvanta

[^1_17]: https://www.telerik.com/blogs/blazor-basics-exploring-blazor-webassembly-ahead-time-aot-compilation

[^1_18]: https://ru.wikipedia.org/wiki/AOT-компиляция

[^1_19]: https://langdev.stackexchange.com/questions/981/what-are-the-advantages-and-disadvantages-of-just-in-time-compilation

[^1_20]: https://stackoverflow.com/questions/46672752/what-reason-is-there-to-not-use-ahead-of-time-compilation

[^1_21]: https://docs.oracle.com/javame/config/cdc/cdc-opt-impl/ojmeec/1.1/custom/html/aot.htm

[^1_22]: https://angular.dev/tools/cli/aot-compiler


---

## Как развернуть AOT-приложение на практике (.NET Native AOT)

1. **Создай проект с поддержкой Native AOT**

Для ASP.NET Core с Native AOT можно использовать шаблон, например, минимальный API:

```bash
dotnet new webapiaot -o MyFirstAotWebApi
cd MyFirstAotWebApi
```

2. **Включи Native AOT в файле проекта (.csproj)**

Добавь в `<PropertyGroup>` следующую строку:

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
</PropertyGroup>
```

Это активирует Ahead-of-Time компиляцию при публикации приложения.

3. **Опубликуй приложение для нужной платформы**

Выполни команду публикации с указанием Runtime Identifier (RID), например, для Windows x64:

```bash
dotnet publish -r win-x64 -c Release
```

Или для Linux ARM64:

```bash
dotnet publish -r linux-arm64 -c Release
```

В результате в папке `publish` появится нативный исполняемый файл, который можно запускать без установленного .NET Runtime[^2_1][^2_3][^2_6].

4. **Требования**

- Visual Studio 2022 с установленной нагрузкой **Desktop development with C++** (для Windows).
- Для Linux — соответствующие системные зависимости.
- .NET 7 и выше поддерживают Native AOT, в .NET 8 добавлена расширенная поддержка, включая ASP.NET Core[^2_1][^2_6].

---

## Какие версии C\# и .NET поддерживают AOT

- **.NET 7** — поддержка Native AOT ограничена консольными приложениями.
- **.NET 8** — расширенная поддержка Native AOT, включая ASP.NET Core (минимальные API, gRPC, воркеры и др.)[^2_1][^2_2][^2_6].
- Версия C\# обычно соответствует версии .NET SDK, используемой в проекте (например, .NET 8 — C\# 11).
- Native AOT тесно интегрирован с современными версиями .NET (7 и 8), более старые версии не поддерживают этот механизм.

---

## Кратко о поддержке ASP.NET Core и ограничениях Native AOT в .NET 8

- Полностью поддерживаются: gRPC, JWT Authentication, CORS, HealthChecks, StaticFiles, WebSockets, минимальные API (частично)[^2_2][^2_4].
- Не поддерживаются: MVC, Blazor Server, SignalR, сессии, SPA, Entity Framework Core[^2_2][^2_4].

---

Таким образом, чтобы развернуть AOT-приложение, нужно создать проект с поддержкой Native AOT (например, через шаблон `webapiaot`), включить `<PublishAot>true</PublishAot>` в проекте и опубликовать под нужную платформу с помощью `dotnet publish -r <RID> -c Release`. Native AOT доступен начиная с .NET 7 (консольные приложения) и получил полноценную поддержку в .NET 8, включая ASP.NET Core минимальные API и другие сценарии[^2_1][^2_3][^2_6].

<div style="text-align: center">⁂</div>

[^2_1]: https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/

[^2_2]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/native-aot?view=aspnetcore-9.0

[^2_3]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/aot/native-aot-tutorial?view=aspnetcore-9.0

[^2_4]: https://blog.ndepend.com/net-native-aot-explained/

[^2_5]: https://dev.to/bytehide/the-ultimate-guide-to-net-native-aot-benefits-and-examples-pg4

[^2_6]: https://bitmiracle.com/pdf-library/howto/native-aot

[^2_7]: https://www.reddit.com/r/dotnet/comments/1d6lfjh/are_you_using_native_aot_what_is_your_experience/

[^2_8]: https://learn.microsoft.com/ru-ru/dotnet/core/deploying/native-aot/

[^2_9]: https://www.youtube.com/watch?v=-4QybRIm-Fc

[^2_10]: https://stackoverflow.com/questions/77482081/is-it-possible-to-use-net-aot-ahead-of-time-compilation-for-only-some-of-my-a

[^2_11]: https://stackoverflow.com/questions/78373985/how-to-publish-net-application-with-native-aot-without-visual-studio-on-windows

[^2_12]: https://microsoft.github.io/node-api-dotnet/features/dotnet-native-aot.html

[^2_13]: https://www.serverlessguru.com/blog/deploy-a-serverless-net-api-using-nativeaot

[^2_14]: https://visualstudiomagazine.com/articles/2024/08/27/net-community-toolkit-gets-native-aot-net-8-support.aspx


---

# Сравнительный анализ объема файлов .NET-приложений в различных режимах выпуска

**Ключевые выводы:**
Анализ трех основных стратегий публикации .NET-приложений демонстрирует существенные различия в размере итоговых файлов, требованиях к среде выполнения и производительности. Наибольший размер характерен для самодостаточных сборок (до 134 МБ для простых приложений), тогда как Native AOT позволяет сократить объем в 10-15 раз. Single File-режим занимает промежуточное положение, но требует установленной среды выполнения. Техники обрезки и сжатия дополнительно уменьшают размер на 40-60%, однако их применимость зависит от типа приложения и версии .NET SDK[^3_1][^3_5][^3_6].

---

## 1. Режим Single File (Framework-Dependent)

### 1.1 Основные характеристики

Сборка объединяет все управляемые библиотеки в один исполняемый файл, но требует наличия соответствующей версии .NET Runtime на целевой системе. Размер варьируется от 4 МБ для простых консольных приложений до 50 МБ для веб-API[^3_1][^3_2].

**Пример конфигурации .csproj:**

```xml
<PropertyGroup>
  <PublishSingleFile>true</PublishSingleFile>
  <SelfContained>false</SelfContained>
</PropertyGroup>
```


### 1.2 Факторы, влияющие на размер

- **Обрезка неиспользуемого кода (Trim):** Уменьшает размер на 30-40% за счет исключения неиспользуемых методов из BCL[^3_2][^3_5].
- **Сжатие (EnableCompressionInSingleFile):** В .NET 6+ снижает объем на 50% через zlib[^3_5].
- **Включение нативных библиотек (IncludeNativeLibrariesForSelfExtract):** Добавляет 5-15 МБ в зависимости от платформы[^3_5].

**Результаты для Hello World (.NET 8):**


| Конфигурация | Размер (МБ) |
| :-- | :-- |
| Базовый Single File | 4.2 |
| + Trim | 2.8 |
| + Trim + Compression | 1.4 |


---

## 2. Самодостаточный режим (Self-Contained)

### 2.1 Особенности реализации

Включает полную копию .NET Runtime и всех зависимостей, позволяя запускать приложение без предустановленной среды. Типичный размер для консольного приложения — 50-150 МБ[^3_1][^3_5].

**Пример публикации:**

```bash
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:SelfContained=true
```


### 2.2 Сравнение с другими подходами

- **Проигрывает в размере:** Даже минимальное приложение с включенным Runtime занимает 11 МБ против 4 МБ в framework-dependent режиме[^3_2][^3_5].
- **Преимущества автономности:** Не требует управления версиями .NET на целевых системах[^3_1][^3_6].
- **Оптимизации для .NET 8:**
    - **Объединение нативных библиотек:** Снижает дублирование кода между компонентами.
    - **Иерархическое сжатие:** Раздельное сжатие управляемых и нативных ресурсов[^3_5].

**Типичные размеры (WinForms, .NET 8):**


| Конфигурация | Размер (МБ) |
| :-- | :-- |
| Базовый Self-Contained | 134 |
| + Trim + Link | 87.9 |
| + Trim + Compression | 41.1 |


---

## 3. Режим Native AOT

### 3.1 Принципы работы

AOT-компиляция преобразует IL-код в нативный машинный код во время публикации, исключая JIT-компиляцию и значительную часть метаданных. Поддерживается с .NET 7, с расширением функциональности в .NET 8 для ASP.NET Core[^3_6][^3_7].

**Конфигурация проекта:**

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
  <StripSymbols>true</StripSymbols>
</PropertyGroup>
```


### 3.2 Размеры и оптимизации

- **Минимальные примеры:** Ручная оптимизация позволяет достичь 834 Б для Hello World[^3_4], но типичные шаблонные проекты занимают 3-5 МБ.
- **Сравнение с Self-Contained:**
    - ASP.NET Core Web API: 12 МБ против 110 МБ в Self-Contained[^3_7].
    - Консольное приложение: 1.8 МБ против 11 МБ[^3_2][^3_6].
- **Особенности .NET 8:**
    - Улучшенный tree shaking для System.Text.Json и других компонентов[^3_7].
    - Интеграция с AVX-512 инструкциями для векторных операций[^3_7].

---

## Сравнительная таблица характеристик

| Параметр | Single File | Self-Contained | Native AOT |
| :-- | :-- | :-- | :-- |
| **Средний размер** | 4-50 МБ | 50-150 МБ | 1.8-15 МБ |
| **Зависимость от .NET** | Да | Нет | Нет |
| **Время запуска** | 120-200 мс | 200-300 мс | 50-80 мс |
| **Потребление памяти** | 60-100 МБ | 80-150 МБ | 30-50 МБ |
| **Динамическая загрузка** | Полная | Полная | Ограничена |
| **Поддержка платформ** | Все | Целевая ОС | Целевая ОС |
| **Отладка** | Полная | Полная | Требует символов[^3_6] |


---

## Рекомендации по выбору стратегии

1. **Single File (Framework-Dependent):**
    - Для внутренних корпоративных приложений с контролируемой средой.
    - Когда критичен быстрый цикл разработки.
    - Пример: Микросервисы в Kubernetes с предустановленным .NET[^3_1][^3_5].
2. **Self-Contained:**
    - Распространение ПО конечным пользователям без админ-прав.
    - Кроссплатформенные решения с поддержкой множества ОС.
    - Пример: Десктопные приложения с установщиком[^3_5][^3_6].
3. **Native AOT:**
    - Cloud-native приложения с горизонтальным масштабированием.
    - Системы реального времени и IoT-устройства.
    - Пример: AWS Lambda-функции с жесткими требованиями к холодному старту[^3_6][^3_7].

---

## Заключение

Выбор режима публикации требует баланса между размером приложения, переносимостью и функциональными возможностями. Native AOT становится предпочтительным выбором для высоконагруженных систем, сокращая объем развертывания в 10 раз по сравнению с традиционными подходами. Однако его использование накладывает ограничения на динамические функции .NET, что делает необходимым тщательное проектирование архитектуры. Для legacy-систем и приложений с интенсивным использованием рефлексии более подходящими остаются Single File и Self-Contained режимы[^3_5][^3_6][^3_7].

<div style="text-align: center">⁂</div>

[^3_1]: https://learn.microsoft.com/en-us/dotnet/core/deploying/single-file/overview

[^3_2]: https://stackoverflow.com/questions/71575038/single-exe-file-is-too-large

[^3_3]: https://learn.microsoft.com/ru-ru/windows/uwp/get-started/create-a-hello-world-app-xaml-universal

[^3_4]: https://habr.com/ru/articles/748372/

[^3_5]: https://habr.com/ru/articles/561140/comments/

[^3_6]: https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/

[^3_7]: https://abp.io/community/articles/native-aot-compilation-in-.net-8-oq7qtwov

[^3_8]: https://devblogs.microsoft.com/dotnet/announcing-net-5-0-preview-8/

[^3_9]: https://blog.washi.dev/posts/tinysharp/

[^3_10]: https://learn.microsoft.com/ru-ru/dotnet/core/deploying/single-file/overview

[^3_11]: https://metanit.com/sharp/tutorial/24.1.php

[^3_12]: https://ru.stackoverflow.com/questions/1173878/Как-определить-что-сборка-является-single-file

[^3_13]: https://ru.stackoverflow.com/questions/1177077/Имеются-ли-зависимости-у-net-core

[^3_14]: https://github.com/dotnet/runtime/issues/83069

[^3_15]: https://www.awise.us/2021/06/05/smallest-dotnet.html

[^3_16]: https://habr.com/ru/articles/888538/

[^3_17]: https://www.reddit.com/r/dotnet/comments/qncfuq/nativeaot_net_7_plans/

[^3_18]: https://www.hanselman.com/blog/making-a-tiny-net-core-30-entirely-selfcontained-single-executable

[^3_19]: https://migeel.sk/blog/2023/09/15/reverse-engineering-natively-compiled-dotnet-apps/

[^3_20]: https://code.soundaranbu.com/trying-out-native-aot-in-net-7-preview-7/

[^3_21]: https://learn.microsoft.com/en-us/dotnet/core/deploying/

[^3_22]: https://github.com/dotnet/designs/blob/main/accepted/2020/single-file/design.md

[^3_23]: https://learn.microsoft.com/ru-ru/dotnet/core/deploying/

[^3_24]: https://www.reddit.com/r/csharp/comments/1en6a47/minimizing_selfcontained_publish_size/?tl=ru

[^3_25]: https://github.com/dotnet/runtime/issues/80165

[^3_26]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/native-aot?view=aspnetcore-9.0

[^3_27]: https://www.youtube.com/watch?v=2CDYjjgVhsI

[^3_28]: https://github.com/dotnet/runtime/issues/3569

[^3_29]: https://www.codemag.com/Article/2010092/.NET-5.0-Runtime-Highlights

[^3_30]: http://hueifeng.com/post/2020/8/26/net-5-preview8

[^3_31]: https://cloud.tencent.com/developer/article/1973964

[^3_32]: https://argosco.io/boost-net-8-performance-with-native-aot/net/

[^3_33]: https://visualstudiomagazine.com/articles/2022/04/15/net-7-preview-3.aspx

[^3_34]: https://www.reddit.com/r/csharp/comments/prqe5s/i_cant_create_single_exe_console_application_with/

[^3_35]: https://www.talkingdotnet.com/create-trimmed-self-contained-executable-in-net-core-3-0/

[^3_36]: https://blog.ndepend.com/net-5-0-app-trimming-and-potential-for-future-progress/

[^3_37]: https://github.com/dotnet/runtime/issues/36590

[^3_38]: https://learn.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli

[^3_39]: https://superuser.com/questions/1716891/you-must-install-net-desktop-runtime-6-0-4-x64-error

[^3_40]: https://kaki104.tistory.com/666

[^3_41]: https://github.com/AustinWise/SmallestDotnetHelloWorlds

#Dotnet #Dotnet/Applications/AOT
