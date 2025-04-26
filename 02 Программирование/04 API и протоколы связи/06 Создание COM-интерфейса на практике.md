## Обеспечение COM-интерфейса в приложении .Net

Средства работы с COM находятся в пространстве имен: **`System.Runtime.InteropServices`**

1. Создаем проект (КАКОЙ)

2. Создаем COM-интерфейс и присваиваем ему **GUID**. **GUID** нужно сгенерировать с помощью встроенного средства Visual Studio: `Средства > Создать GUID`

```csharp

```

3. Создаем COM-класс, реализующий созданный нами интерфейс

```csharp

```

4. Выполняем сборку проекта и копируем сгенерированные файлы `MyProgID.dll`, `MyProgID.pdb`, `MyProgID.tlb`

5. Регистрируем созданную сборку в реестре, выполнив скрипт от имени админа (файл `.bat`):

```batch
C:\Windows\Microsoft .Net\Framework\v4.0.30319\regasm.exe "PATH_TO_MyProgId.dll" /tlb /codebase pause
```

## Обеспечение COM-интерфейса в приложении 1С

[[01 COM в 1С#Тип `COMОбъект`|Как работать с COM в 1С можно узнать здесь]]

### Использование COM из 1С

```bsl
ГрафикПоставки = Справочники.ДокументыОтдела.ГрафикПоставки.Выгрузить();

запись = новый ЗаписьJSON();

ЗаписатьJSON(запись, ГрафикПоставки, НазначениеТипаXML.Явное);

dotNetObject = новый COMобъект("MyProgId");
dotNetObject.Data = jsonObject;
dotNetObject.OpenTableForm();
```

В данном случае, табличная часть, предварительно выгруженная в таблицу значений, была сериализована в JSON. После чего помещена в свойство внешнего объекта, которое должно использоваться для корректной работы внешнего приложения.

Это условный пример, свойства и методы COM-объекта могут быть какими-угодно.

----

## Регистрация C# приложения как COM-класса

Чтобы зарегистрировать приложение на C# как COM-класс, следуй этим шагам:

**** 1. Создание проекта

1. Открой Visual Studio и создай новый проект.
2. Выбери тип проекта **Class Library** (Библиотека классов).
3. Убедись, что целевая платформа — .NET Framework (например, 4.7 или выше).

**** 2. Настройка COM-взаимодействия

1. Открой свойства проекта (правый клик на проекте в обозревателе решений и выбери "Свойства").
2. Перейди на вкладку **Build** (Сборка) и поставь галочку на **Register for COM Interop** (Регистрировать для взаимодействия с COM).

**** 3. Определение интерфейсов и классов

Создай интерфейс и класс, которые будут доступны через COM:

```csharp
using System.Runtime.InteropServices;

namespace YourNamespace;

[Guid("EAA4976A-45C3-4BC5-BC0B-E474F4C3C83F")]
public interface IComClass {
    string GetMessage();
}

[Guid("7BD20046-DF8C-44A6-8F6B-687FAA26FA71"),
ClassInterface(ClassInterfaceType.None)]
public class ComClass : IComClass {
    
    public string GetMessage() {
        return "Hello from COM!";
    }
}

```

**** 4. Сборка проекта

1. Собери проект, чтобы получить .dll файл.

**** 5. Регистрация сборки с помощью Regasm

1. Открой командную строку с правами администратора.
2. Перейди в каталог, где находится твоя .dll:

```bash
cd путь\к\твоему\проекту\bin\Release
```

3. Выполни команду для регистрации:

```bash
C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe YourAssembly.dll /tlb:YourAssembly.tlb /codebase
```

- Параметр `/tlb` создаст библиотеку типов, а `/codebase` укажет путь к .dll.

**** 6. Использование COM-класса из других приложений

Теперь ты можешь использовать зарегистрированный COM-класс в других языках программирования, таких как VBScript, C++, Python и даже в 1С.

Пример использования в VBScript:

```vbscript
Set obj = CreateObject("YourNamespace.ComClass")
WScript.Echo obj.GetMessage()
```

Если возникнут проблемы с доступом или ошибками регистрации, убедись, что используешь правильную версию Regasm (32-битная или 64-битная) в зависимости от архитектуры твоего приложения.

Следуя этим шагам, ты сможешь успешно зарегистрировать свое C# приложение как COM-класс и использовать его в других приложениях!

Citations:
[1] https://learn.microsoft.com/ru-ru/dotnet/framework/tools/regasm-exe-assembly-registration-tool
[2] https://learn.microsoft.com/ru-ru/dotnet/csharp/advanced-topics/interop/example-com-class
[3] https://ru.stackoverflow.com/questions/812073/%D0%A0%D0%B5%D0%B3%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%86%D0%B8%D1%8F-com-%D0%BE%D0%B1%D1%8A%D0%B5%D0%BA%D1%82%D0%B0-%D0%B4%D0%BB%D1%8F-%D0%B8%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F-%D0%B5%D0%B3%D0%BE-%D0%B2-1%D0%A1
[4] https://learn.microsoft.com/ru-ru/visualstudio/get-started/csharp/tutorial-console?view=vs-2022

#API #COM #OS/Windows #1С