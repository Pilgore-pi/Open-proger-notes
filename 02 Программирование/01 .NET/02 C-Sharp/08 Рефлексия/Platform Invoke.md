
https://www.youtube.com/watch?v=KYq2WF3otxc

> `P/Invoke` — это вызов функции из готовой сборки. Для совершения вызова, необходимо знать название функции и передаваемые параметры.

> Для работы с `P/Invoke` нужно добавить `using System.Runtime.InteropServices`, и сами функции, так как они будут вызываться за пределами платформы .NET, то их нужно описать с помощью класса `DllImportAttribute`

Но класс так же может быть использован для описания дополнительных характеристик, например, можно с помощью атрибута CharSet = CharSet.Unicode явно указать какая кодировка используется для работы функции в приложении

Ключевые `P/Invoke` библиотеки:

[Win32 API](https://learn.microsoft.com/ru-ru/windows/win32/) состоит из следующих библиотек:

- `user32.dll` https://learn.microsoft.com/ru-ru/windows/win32/api/winuser/
- `gdi32.dll`
- `kernel32.dll`

### Функционал библиотеки `user32.dll`

https://learn.microsoft.com/ru-ru/windows/win32/api/winuser/nf-winuser-mouse_event?redirectedfrom=MSDN

```csharp
[DllImport("user32.dll")]
static extern void Mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
	
	Возрват: отсутствует
	Действие: Позволяет программно управлять событиями мышью
	Параметры (C++ типы):
	
		[in] DWORD dwFlags // Определяет события, которые будут выполняться
						   // при вызове функции. Является аналогом [Flags] enum
			Возможные состояния:
				MOUSEEVENTF_MOVE       0x0001  // Курсор был сдвинут
				MOUSEEVENTF_LEFTDOWN   0x0002  // ЛКМ нажата
				MOUSEEVENTF_LEFTUP     0x0004  // ЛКМ отпущена
				MOUSEEVENTF_RIGHTDOWN  0x0008  // ПКМ нажата
				MOUSEEVENTF_RIGHTUP    0x0010  // ПКМ отпущена
				MOUSEEVENTF_MIDDLEDOWN 0x0020  // СКМ нажата
				MOUSEEVENTF_MIDDLEUP   0x0040  // СКМ отпущена
				MOUSEEVENTF_XDOWN      0x0080  // Доп. кнопка мыши нажата
				MOUSEEVENTF_XUP        0x0100  // Доп. кнопка мыши отпущена
				MOUSEEVENTF_WHEEL      0x0800  // Колесико прокручено на dwData единиц
				MOUSEEVENTF_HWHEEL     0x1000  // Колесико наклонено
				MOUSEEVENTF_ABSOLUTE   0x8000  // 
				
		[in] DWORD dx, dy // нормализованные абсолютные координаты курсора
		
		[in] DWORD dwData — Если задано событие:
				MOUSEEVENTF_WHEEL  // - WHEEL_DELTA, единицы прокрутки колесика
								   //   Одна прокрутка колесика = +-120 единицам
				MOUSEEVENTF_HWHEEL // - Положительное dwData означает, что колесико
								   //   наклонено вправо, Отрицательное - влево
				MOUSEEVENTF_XDOWN или MOUSEEVENTF_XUP
					// dwData содержит флаги,
					// соответсвтующие нажатым доп. кнопкам:
					XBUTTON1 0x0001
					XBUTTON2 0x0002 
				Если перечисленные флаги dwFlags не заданы, то dwData = 0
		
		[in] ULONG_PTR dwExtraInfo // Доп. инфа, привязанная к событию.
		                           // Для получения доп. инфы, требуется
		                           // вызывать функцию GetMessageExtraInfo
```

```csharp
[DllImport("user32.dll")]
static extern bool SetCursorPos(int x, int y);

	Возрват: отсутствует
	Действие: Мгновенно перемещает курсор в указанную позицию. Если позиция курсора
		вне пределов экрана, то курсор будет перемещен в крайнюю точку экрана
	Параметры:
		int x — Позиция по оси X
		int y — Позиция по оси Y (направление сверху вниз)
```

```csharp
[DllImport("user32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool GetCursorPos(out POINT lpPoint);

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;
    public POINT(int x, int y){ this.X = x; this.Y = y; }
}
```

```csharp
[DllImport("user32.dll")]
static extern int MessageBox(IntPtr hWnd, string text, string caption, int options);
Возврат:
Действие: открывает окно с сообщением
Параметры:
	hWnd    // 
	text    // текст сообщения
	caption // текст заголовка
	options // 
```

### Функционал библиотеки `win32.dll`

## Генератор кода PInvoke `win32metadata`

Существует Nuget-пакет, который позволяет автоматически генерировать объявление внешних функций и сопоставлять C++ типы с .Net типами. Для работы пакета требуется включение небезопасного кода.

Для установки проекции необходимо:

- Добавить ссылку на `Microsoft.Windows.SDK.Win32Metadata` от `nuget.org`;

- Поместить файл `NativeMethods.txt` в корневую директорию проекта вместе со списком функций `Win32`, которые запланированы для проекта.

После этого проекция C# / Win32 начнёт генерировать обёртки P/Invoke для запрашиваемых функций и всех их зависимостей

```powershell
dotnet add package Microsoft.Windows.CsWin32 --prerelease
```

Для проектов ниже версий .NET Core 2.1 и .NET 5 требуется установить эти пакеты:

```powershell
dotnet add package System.Memory
dotnet add package System.Runtime.CompilerServices.Unsafe
```

#C-Sharp #API #OS/Windows