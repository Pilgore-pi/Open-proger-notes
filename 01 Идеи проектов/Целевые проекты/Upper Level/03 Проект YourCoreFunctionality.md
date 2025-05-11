## Необходимо изучить

- YAML
- Git +


Общий проект будет состоять из 3 решений:

- Windows (.dll files)
- Linux (.so files)

При этом общая функциональность будет вынесена куда-нибудь, а ОС-зависимые решения будут ссылаться на нее 

1. Пользователь скачивает легковесный установщик (менеджер), который не содержит DLL-модулей
2. Пользователь сможет выбрать необходимый ему функционал (DLL-модули)
3. Менеджер приложения будет скачивать все выбранные модули
4. Программа будет динамически подключать библиотеки. Этот процесс будет генерировать конфигурационный файл `conf.json`, который будет хранить информацию о том, какие модули были установлены и доступны в текущем приложении
5. Модули должны собираться в определенном порядке

> Явным образом обозначить, какие сервисы используют интернет, какие сервисы требуют определенную ОС и т. д. То есть навешивать теги требований к сервисам

## CI/CD разработка

##### **Что такое CI/CD и зачем это нужно?**

**CI/CD** (Continuous Integration / Continuous Deployment) — это подход к разработке, который автоматизирует процесс сборки, тестирования и развертывания приложений. Основные цели CI/CD:

1. **Continuous Integration (CI)**: Автоматическая проверка и сборка кода при каждом изменении. Это позволяет быстро находить ошибки и проверять, что изменения не ломают существующий функционал.
2. **Continuous Deployment (CD)**: Автоматическое развертывание приложения на серверы или в облако после успешной сборки и тестирования.

Пример: Вы вносите изменения в код, отправляете их в репозиторий (например, на GitHub), и CI/CD система автоматически:

- Проверяет ваш код на ошибки.
- Собирает проект для всех целевых платформ.
- Запускает тесты.
- (Опционально) Разворачивает приложение на сервере или создает установочные файлы.

##### **Как настроить CI/CD для вашего проекта**

###### 1. **Выбор CI/CD инструмента**

Популярные инструменты для автоматизации:

- **Aspire**
- **GitHub Actions**: Если вы используете GitHub, это встроенный инструмент для автоматизации.
- **GitLab CI/CD**: Если ваш код хранится на GitLab.
- **Azure DevOps**: Подходит для проектов на .NET.
- **Jenkins**: Гибкий инструмент, который можно настроить для любых задач.
- **CircleCI** или **Travis CI**: Подходят для кроссплатформенных проектов.

###### 2. **Пример настройки GitHub Actions**

Если вы используете GitHub, настройка CI/CD выполняется через файл `.yml` в папке `.github/workflows`. Вот пример для сборки .NET проекта и Python модулей:

```yaml
name: Build and Test

on:
  push:
    branches:
      - main
  pull_request:
    branches:
      - main

jobs:
  build:
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [windows-latest, ubuntu-latest, macos-latest]

    steps:
    - name: Checkout code
      uses: actions/checkout@v2

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0' # Укажите версию .NET

    - name: Setup Python
      uses: actions/setup-python@v4
      with:
        python-version: '3.12' # Укажите версию Python

    - name: Install dependencies
      run: |
        dotnet restore
        pip install -r requirements.txt

    - name: Build .NET project
      run: dotnet build --configuration Release

    - name: Run tests
      run: |
        dotnet test
        pytest # Запуск тестов Python
```

Этот файл:

- Проверяет код при каждом изменении в ветке `main`.
- Устанавливает .NET и Python.
- Собирает проект и запускает тесты.

###### 3. **Сборка для разных ОС**

Если ваше приложение должно работать на Windows, Linux и macOS, настройте сборку для каждой ОС. В примере выше используется `matrix.os`, чтобы запускать сборку на всех платформах.

###### 4. **Автоматизация публикации**

После успешной сборки можно автоматически публиковать артефакты (например, установочные файлы или библиотеки):

- Для .NET: Публикация в NuGet.
- Для Python: Публикация в PyPI.
- Для приложений: Создание установочных файлов или Docker-образов.

Пример публикации артефактов:

```yaml
- name: Publish artifacts
    uses: actions/upload-artifact@v3
    with:
        name: build-output
        path: bin/Release
```

## Структура проекта

==**PowerShell** доступен на Linux & MacOS==

```
YourCoreFunctionality/
│
├── YourCoreApi/              # Главный проект (точка входа) (Console)
│   ├── YourCoreApi.csproj    
│   ├── conf.json             # содержит информацию о доступных (установленных) модулях
│   └── Program.cs            # Terminal API
│
├── Universal/                # Универсальные модули, общие для всех платформ
│   ├── Core/                 # Общая логика, независимая от платформы
│   ├── DotnetModules/        # Универсальные модули
│   ├── PythonModules/        # Универсальные Python-модули
│   ├── Tests/                
│   └── Build/                # Cкрипты сборки универсальных модулей под Windows/Linux
│       ├── build_win.ps1
│       └── build_lin.sh
│
├── Windows/             DLLs # Каталог для модулей и логики, специфичных для Windows
│   ├── Core/                 # Общая логика для Windows
│   ├── DotnetModules/        # Модули для Windows
│   ├── PythonModules/        # Python-модули для Windows
│   ├── Tests/                # Тесты для Windows
│   └── Build/                # Скрипты сборки модулей для Windows
│       └── build.ps1
│
├── Linux/                    # Каталог для модулей и логики, специфичных для Linux
│   ├── Core/                 # Общая логика для Linux
│   ├── DotnetModules/        # Модули для Linux
│   ├── PythonModules/        # Python-модули для Linux
│   ├── Tests/                # Тесты для Linux
│   └── Build/                # Скрипты сборки модулей для Linux
│       └── build_lin.sh
│
├── .github/                  # Конфигурация GitHub Actions
│   └── workflows/
│       └── build.yml
│
├── Documentation/
│   └── README.md             # Документация
│
└── LICENSE                   # Лицензия
```

В файле проекта `.csproj` можно определить символы препроцессора:

```xml
<PropertyGroup Condition="'$(RuntimeIdentifier)' == 'win-x64'">
    <DefineConstants>WINDOWS</DefineConstants>
</PropertyGroup>

<PropertyGroup Condition="'$(RuntimeIdentifier)' == 'linux-x64'">
    <DefineConstants>LINUX</DefineConstants>
</PropertyGroup>

<PropertyGroup Condition="'$(RuntimeIdentifier)' == 'osx-x64'">
    <DefineConstants>MACOS</DefineConstants>
</PropertyGroup>
```

```csharp
public class PlatformSpecificService
{
    public void DoSomething()
    {
#if WINDOWS
        Console.WriteLine("Running on Windows");
#elif LINUX
        Console.WriteLine("Running on Linux");
#elif MACOS
        Console.WriteLine("Running on macOS");
#else
        Console.WriteLine("Unknown platform");
#endif
    }
}
```

Однако, не следует применять такой подход, в случае значительных различий в реализации модулей, так как придется тащить версии проекта для всех платформ

Dependency injection подход для разных ОС:

```csharp
public interface IPlatformSpecificService
{
    void DoSomething();
}

public class WindowsService : IPlatformSpecificService
{
    public void DoSomething() => Console.WriteLine("Windows implementation");
}

public class LinuxService : IPlatformSpecificService
{
    public void DoSomething() => Console.WriteLine("Linux implementation");
}

public class MacOSService : IPlatformSpecificService
{
    public void DoSomething() => Console.WriteLine("macOS implementation");
}

// Внедрение зависимости
public class PlatformServiceFactory
{
    public static IPlatformSpecificService CreateService()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return new WindowsService();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return new LinuxService();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return new MacOSService();
            
        throw new PlatformNotSupportedException("Unsupported OS");
    }
}
```

## DLL модули

Архитектура модульного приложения состоит из независымих модулей, которые компилируются в DLL-файлы. Между DLL-файлами создаются необходимые зависимости, в случае необходимости наличия одной DLL для работы другой DLL.

**Компиляция:** должна происходить поэтапно, согласно построенным зависимостям (от корня — независимого модуля к веткам — все более и более зависимым модулям)

Архитектура решения:

> Каждый каждый элемент представляет собой DLL

> Родительские модули зависят от вложенных в них модулей

Далее будет использоваться термин "спец. команда", обозначающий независимую программу, которая может быть вызвана из других программ и приложений.

### Latest version

- YourInstaller `{YourBuilder.exe, MVVM Avalonia app}`

- YourOS
    - Info
        - Devices
    - KeyboardAndMouse
    - Events (startup)
    - Clipboard
    - Windows (handles)
    - Screen (depends on YourOS.Info.Devices)
- YourCommunication
    - Telegram
    - Emails
    - TcpIP (TCP, HTTP, HTTPS?) (Depends on Cryptography)
    - FTP
    - LLM (GPT based AI)
    - Translation
        - SpeechSynthesizer
        - SpeechRecognizer
- YourFiles
    - Images (if not included use defaul assosiated app)
    - Audio (if not included use defaul assosiated app)
    - Videos (if not included use defaul assosiated app)
    - Converters {TextTo(svg, html, md, pdf, word), BinaryTo(image, video, ...)}
- YourTexting (uses YourOS.Clipboard, YourCommunication.\*, YourOS.ContextMenu)
    Ctrl + H (replace, append, prepend), Ctrl + F extended, Regex
    translation, snippets, character analyzer, GetCodes(text, format)
    IsSlang(word), ContainsSlang()
    Транслитерация
    Команда "Создать подсветку кода" (язык) => HTML/SVG
    Число строкой, дата строкой
    Сегодняшняя дата, время, поиск дат и времени в диапазоне, поиск чисел в диапазоне, поиск комплексных чисел
    Форматирование (syntax) (MD, XML, JSON, C#, etc.)
    - Syntax (Подсветка, Обфускация)
- YourDataAnalysis
- 

### Previous version

- YourKeyboardSimulator `C#` — симуляция нажатий клавиш клавиатуры
- YourMouseSimulator `C#` — симуляция нажатий кнопок мыши и колесика
- YourFileManager `C#` — общие возможности по управлению файлами и их анализа
    - YourAudioManager `C#` — редактирование тегов аудиофайлов и отслеживание частоты использования
    - YourImageEngine `C#` — API для управления характеристиками изображения (библиотека OpenCV)
    - YourStringEngine `C#` — API для поиска, замены и другого анализа текста, форматирование текста
- YourDataAnalyzer `C#` — API для анализа данных, загружаемых из различных источников
- YourWeather `Python` — получение информации о погоде за определенный период из разных источников
- YourTranslater `Python` — перевод слов и предложений с одного языка на другой (API)
- YourOS `C#` — общие возможности по взаимодействию с ОС, получение информации об устройствах и параметрах системы
    - YourOsEvents `C#` — Отслеживание событий операционной системы (при открытии, при завершении работы, при открытии проводника...)
    - YourOsStartup `C#` — API для управления автозагрузкой приложений
    - YourClipboard `C#` — API для взаимодействия с буфером обмена
    - YourScreen `C#` — настройка параметров экрана и цветовая коррекция экрана
- YourComponentScripts `C#` — позволяет создавать или использовать готовые спец. команды, которые могут быть вызваны
    - YourCSharp `C#` — компилятор Roslyn и средства поддержки кода C#, написание спец. команд на C#
    - YourPython `Python` — средства поддержки Python, написание спец. команд на Python
    - YourTerminal `C#` — поддержка написания спец. команд на языках Bash, PowerShell, Batch
    - YourContextMenu `C#` — позволяет добавлять новые пункты в контекстном меню, которые связаны со спец. командами
- YourGptApi `Python` — простой API для взаимодействия с ChatGPT
- YourMessaging `Python` (emails, telegram, discord, ftp) — пересылка сообщений в различных форматах
    - YourHttpClient `C#` — API для легкого и быстрого создания сервера для отправки и получения сетевых запросов
    - YourLogger `C#` — логирование информации в консоли, текстовом файле, отправка логов на почту, в мессенджер и на HTTP сервер
- YourHotkeys `C#` — API для связывания комбинации клавиш с выполняемой программой или спец. командой
- YourCriptography `C#` — средства для хэширования, шифрования, сжатия и расжатия файлов и сообщений



#Идеи_проектов #Идеи_проектов/Upper_Level