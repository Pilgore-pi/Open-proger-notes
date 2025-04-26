Разработать систему визуального программирования, основанную на расстановке узлов и созданию связей между ними.

>Программа, разработанная с помощью ComponentScript далее будет называться *исполняющим графом*

>*Компонентой* будет называться любой объект графа, кроме связей между вершинами, а именно: узлы, графы, подграфы, объединения графов

Разрабатываемая система должна очень хорошо поддерживать модульность исполняющих графов. Другими словами, каждый компонент итоговой программы должен являться самостоятельной и независимой маленькой программой, которая также может состоять из таких же независимых подпрограмм.

## Узел графа

Узел графа должен представлять собой логически единую операцию. Узел может представлять, как простой вызов функции, так и выполнение полноценной программы, но которую нелогично было бы делить на составляющие, так как эти составляющие тесно зависят друг от друга.

Узел графа может представлять собой следующие сущности

- **Скрипт** **\[ЯП\] \[Синх/Асинх\]** ((КРУГ))
- **Условный узел (скрипт)** **\[ЯП\] \[Синх/Асинх\]** ((Сильно скругленный квадрат))
- **Циклический узел (скрипт)** **\[ЯП\] \[Синх/Асинх\]** ((Скругленный шестигранник)) // добавить проверки ("вы не указали циклическе операторы", "Цикл бесконечный", "Стек переполнен")
- **Запланированный скрипт**, уловный или циклический узел **\[ЯП\] \[Синх/Асинх\]**
- **Форматированный текст** ((Слегка скругленный прямоугольник))
- **Модуль (DLL)** - подключаемые библиотеки ((Присоединенная фигура к верхней границе узла))
- **Сетевой запрос** (HTTP, REST, SOAP...) ((Треугольник))

Асинхронные скрипты должны обводиться пунктиром

**Используемые языки**: C#, Python, PowerShell, Batch, Bash

Для конечного пользователя должна быть определена палитра инструментов кода, которая будет содержать шаблоны для быстрого написания кода. Кроме того, палитра инструментов позволит не писать код вручную, что уменьшит вероятность ошибок
Палитра команд должна содержать в себе элементы, которые пользователь сможет перетаскивать в узел.

Полная структура палитры инструментов:

- **Узлы**. Параметры: \[Язык программирования\], \[Синх/Асинх\], \[Запланированный\]
    - Script
    - Conditional script (открывающееся окно с настройками, "Запомнить выбор" `if else`, `switch case`)
    - Cyclic script (открывающееся окно с настройками, "Запомнить выбор" `for`, `while`...)
    - Import module (открывающееся окно...)
    - Text
    - Network request (connection, sending and listening incoming responces)
- Переменные
    - int
        - byte
        - short
        - long
    - double
        - float
    - text (string)
        - char
    - Перечисления
        - Языки
        - Алгоритмы шифрования
        - Алгоритмы хеширования
        - Тип ОС (`Windows`, `Linux`, `MacOS`, `Android`, `IOS`)
- Функции
    - `RunPowerShellScript(script)`
    - `RunPythonScript(script)`
    - `Logger.Log()`
    - Data
        - `Encode(EncodingAlgorithms algorithm, byte[] data)`
        - `Decode`
        - `Hash`
        - `SendRestRequest`
        - `SendSoapRequest`
        - `SendHttpRequest`
        - `GetSizeInBytes(data)` (data is a file or a variable)
    - Inputs
        - `GetCharacter(UIMode ui)` // `UIMode{  }`
        - `GetTextLine()`
        - `GetText()`
        - `GetVoice()`
        - `GetIntegerNumber(positive = false)`
        - `GetPositiveNumber()`
        - `GetFloatingNumber()`
        - `GetDate()`
        - `GetTime()`
        - `GetDateTime()`
    - Files
        - `Files.Create`
        - `Files.Delete (1 или множество файлов)`
        - `Files.Copy`
        - `Files.GetMetadata`
        - `Files.Write`
        - `Files.ReadCharacter()`
        - `Files.ReadLine()`
        - `Files.ReadAllText()`
        - `Files.ReadAllBytes()`
        - `Files.OpenWithDefaultApp(params filePaths)`
        - `Files.OpenWithSpecificApp(appPath, params filePaths)`
        - `Files.OpenExplorer`
        - Image editor
            - `ChangeImageSize()`
            - `ImageToGrayScale()`
            - `ChangeColorDepth()`
            - `LoadImage(fileName)`
            - `Red`
            - `Green`
            - `Blue`
            - `Opacity`
            - `Hue`
            - `Saturation`
            - `Brightness`
            - `Pixels.Foreach(x => x.Color += "#3FBC")`
            - `Width`
            - `Height`
            - 
        - Text editor
    - Operational system
        - `GetOsType()`
        - `GetVersion() : string`
        - `GetComputerInfo() : JSON`
        - `ShowNotification()` (WINDOWS)
        - Registry (WINDOWS)
            - lalala
            - автозагрузка
            - параметры системы
    - Mouse
        - `GetCurrentPosition()` (default)
        - `GetCurrentPixelColor() (ToRgb(), ToHsl(), ToHex())`
        - `Move(x, y)`
        - Left button
            - `LeftClick()` (default)
            - `LeftPress()`
            - `LeftRelease()`
        - Right button
            - `RightClick()` (default)
            - `RightPress()`
            - `RightRelease()`
        - Wheel
            - `ScrollWheel()` 
            - `WheelClick()` (default)
            - `WheelPress()`
            - `WheelRelease()`
    - Screen
        - `GetResolution()` (like 16:9)
        - `GetWidth()`
        - `GetHeight()`
        - `GetPixelColor(x,y)`
        - `ApplyEffect(ScreenEffects.Negative)`
    - API
        - Text and speech
            - `TranslateText(Languages fromLang, Languages toLang)`
            - `VoiceToText(audioFile)`
            - `VoiceToText()`
            - `TranslateVoice(Languages fromLang, Languages toLang)`
        - Weather
            - `GetTodaysWeather(location)`
            - `GetThisWeeksWeather(location)`
            - `GetWeatherOfDay(Date day)`
        - FileConverter
            - `ConvertFile(fromExt, toExt)`
    - Network
        - `GetConnectionSpeed() : string`
        - `GetConnectionInfo() : JSON`
        - `SendHttpRequest()`
        - `SendRestRequest()`
        - `SendSoapRequest()`
        - `SendTcpRequest()`
- События
    - Inputs
        - `GotInput` params: data parsing type, data value
    - Operational system
        - EverySecond (kinda bad)
        - EveryMinute
        - EveryHour
    - Files
    - 
- Глобальные параметры : `JSON`
    - User interface language (Language RUS/ENG/CHI)
    - Show hidden tools
    - Logging (true/false)
    - CodeStyle
        - If else: full syntax; no braces; single line;
        - for: full syntax; no braces; single line;
        - foreach: full syntax; no braces; single line;
    - Import settings
    - Export settings

Панель инструментов будет представлять собой иерархию однотипных элементов. Каждый элемент может быть помечен флагом `hidden = false`

Все скрипты узлов будут с помощью рефлексии вставляться в финальный скрипт. Каждый узел (опционально) может заключаться в свою область видимости, чтобы не было конфликтов объявляемых переменных:

```csharp
using ...
namespace ComponentScript;

public static class Main
{
    public static ExecuteGraph(Component component)
    {
        // <REFLECTION_INJECTION_1>
        {
            int a, b;
            a = 8;
            b = 8 * a;
            Files.Write("myFile.txt", $"a = {a}\nb = {b}")
        }
        // </REFLECTION_INJECTION_1>
        
        // <REFLECTION_INJECTION_2>
        {
            Mouse.Move(100, 500);
            Mouse.LeftClick();
            Logger.Log(Mouse.GetCurrentPixelColor().ToHex())
        }
        // </REFLECTION_INJECTION_2>
    }
}
```

Если пользователь перетаскивает группу инструментов, а не конечный элемент (инструмент), то вместо группы вставляется функция (переменная и т.д.) по умолчанию

Палитра инструментов предоставляет богатый функционал для работы в системе. Функционал сгруппирован по тематике. Функционал должен быть реализован в виде нескольких логически полноценных и независимых подключаемых библиотек DLL

> Смежные узлы, содержащие скрипты на одном и том же языке могут быть оптимизированы путем объединения в один скрипт. Скрипты на разных языках, выполняющиеся друг за другом не будут оптимизированы. В таком случае для каждого узла будет сгенерирован EXE-файл, в который могут быть переданы аргументы, полученные в предыдущих скриптах.

Граф, состоящий из разных языков, будет представлять несколько исполняющих файлов, выполняемых по цепочке.

> А вообще, спросить у GPT, какие варианты он может предложить

### User interface

Пользователь обладает следующим функционалом:

- Сборка исполняющего графа
- Сборка и выполнение исполняющего графа
- Генерация Portable exe-файла
- Создание компонент любых видов (узел, подграф, граф, объединение графов)
- Удаление компонент
- Редактирование компонент
- Создание связей компонент (как от узла к узлу, так и от графа к графу). Если связи 2 и более, то над связью отображается условие (`if x > 0`, `if x = 0`, `else`)
- Установка цветов и комментариев для компонент
- Узлы можно двигать мышкой
- Холст можно масштабировать

При выпуске приложения, нужно анализировать зависимости и уведомлять программиста-пользователя о необходимых программных компонентах, позволяющих работать программе. Например, при использовании скриптов Python, необходимо установить Python, при использовании PowerShell, программа будет работать только для Windows. Предоставить возможность автоматической установки языка Python

#Идеи_проектов #Идеи_проектов/Component_script