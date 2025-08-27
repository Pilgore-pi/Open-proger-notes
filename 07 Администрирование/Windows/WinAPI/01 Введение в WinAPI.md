[Официальная документация](https://learn.microsoft.com/en-us/windows/win32/api/)

Основные системные DLL-файлы (например, kernel32.dll, user32.dll, gdi32.dll) располагаются в каталоге Windows\System и содержат функции API

Заголовочные файлы (`.h`) в WinAPI содержат объявления функций, констант и структур, соответствующих этим функциям из DLL. Они нужны для того, чтобы компилятор знал, какие функции доступны для вызова и как их правильно вызывать, но сами реализации функций находятся в DLL-файлах

Заголовочные файлы и динамические библиотеки соотносятся как "многие ко многим"

Заголовочный файл -- это интерфейс, определяющий, какие функции доступны для использования. Заголовочный файл может обращаться к разным DLL-файлам для загрузки реализации объявленных функций

Динамическая библиотека (DLL) -- это скомпилированная библиотека, содержащая нативные функции с реализацией

> WinAPI написан на языке `C`

WinAPI имеет очень богатый функционал, который покрывает все основные области работы в системе. Категории функионала WinAPI:

- Базовые функции ОС: управление файлами, потоками, процессами, памятью и т. д.
- Рабочий стол и пользовательский интерфейс
- Графика и гейминг
- Аудио и видео
- Доступ к хранилищам данным
- Устройства: управление устройствами компьютера с использованием драйверов
- Сеть и интернет
- Безопасность и идентификация: аутентификация, криптография, сертификация, защита в сети, родительский контроль, нативный фреймворк биометрии и т. д.
- Диагностика системы: отслеживание и логирование событий ОС; обработка ошибок
- Установка приложений: защита ресурсов приложений, фреймворк создания установочных приложений, лицензирование

| Заголовочный файл | Динамическая библиотека | Категория | Описание |
| :-- | :-- | :-- | :-- |
| `winuser.h` | `user32.dll` | Рабочий стол и пользовательский интерфейс | Управление пользовательским интерфейсом: окна, сообщения, обработка событий, меню, клавиатура, мышь |
| `winbase.h` | `kernel32.dll` | Базовые функции ОС | Базовые функции ОС: управление памятью, процессами, потоками, файлами, синхронизация |
| `wingdi.h` | `gdi32.dll` | Графика и гейминг | Графический вывод: рисование, работа с изображениями, шрифтами, вывод на экран и принтер |
| `securitybaseapi.h`, `aclapi.h`, `winsvc.h` | `advapi32.dll` | Безопасность и идентификация | Расширенные функции: работа с реестром, безопасностью, службами, журналами событий |
|  | `shell32.dll` | Рабочий стол и пользовательский интерфейс | Функции оболочки Windows: работа с рабочим столом, ярлыками, запуск программ, диалоги Проводника |
|  | `comdlg32.dll` | Рабочий стол и пользовательский интерфейс | Стандартные диалоговые окна: открытие/сохранение файлов, выбор цвета, шрифта, печать |
|  | `ws2_32.dll` | Сеть и интернет | Сетевые функции: работа с сокетами, протокол TCP/IP, сетевые соединения |
|  | `winmm.dll` | Аудио и видео | Мультимедиа: воспроизведение звука, MIDI, таймеры мультимедиа, управление устройствами |
|  | `ntdll.dll` |  | Низкоуровневые функции ядра NT: системные вызовы, поддержка работы ОС |
| `commctrl.h`, `commoncontrols.h` | `comctl32.dll` |  | Общие элементы управления: кнопки, списки, вкладки, прогресс-бары и другие стандартные UI-элементы |
| `dwmapi.h` | `dwmapi.dll` | Рабочий стол и пользовательский интерфейс | API управления окнами рабочего стола (Desktop Window Manager API) |
| `winver.h` | `Api-ms-win-core-version-l1-1-0.dll` | Рабочий стол и пользовательский интерфейс | Позволяет получать информацию о версии ОС и файлов |
| `winusb.h` | `winusb.dll` | Сеть и интернет | Управление USB соединениями |
| `bluetoothapis.h` | `bthprops.cpl` | Сеть и интернет |  |
| `DirectML.h` | `directml.dll` | Графика и гейминг | Direct Machine Learning (DirectML) is a low-level API for machine learning (ML). The API has a familiar (native C++, nano-COM) programming interface and workflow in the style of DirectX 12. You can integrate machine learning inferencing workloads into your game, engine, middleware, backend, or other application. DirectML is supported by all DirectX 12-compatible hardware |
| `gdipluspath.h` (`gdiplus.h`) | `gdiplus.dll` | Графика и гейминг | Windows GDI+ is a class-based API for C/C++ programmers. It enables applications to use graphics and formatted text on both the video display and the printer. Applications based on the Microsoft Win32 API do not access graphics hardware directly. Instead, GDI+ interacts with device drivers on behalf of applications. GDI+ is also supported by Microsoft Win64 |
| `wincodec.h`, `wincodecsdk.h` | `windowscodecs.dll` | Графика и гейминг | The Windows Imaging Component (WIC) is an extensible platform that provides low-level API for digital images.  WIC supports the standard web image formats, high dynamic range images, and raw camera data.  WIC also supports additional features such as: Built-in support for standard metadata formats; Extensible framework for image codecs, pixel formats, and metadata formats; Wide range of pixel format support; High-color support (including 30-bit extended range, 30-bit high precision, and 48-bit high precision and wide gamut pixel formats); Progressive image decoding |

#MERGE_NOTES
