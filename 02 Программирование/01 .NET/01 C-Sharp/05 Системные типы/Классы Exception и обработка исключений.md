
> **Исключение** — это непредвиденная ситуация, возникающая при ошибке выполнения программы. Ошибки могут возникать на 2 этапах:

- на этапе компиляции программы (compile time)
- на этапе выполнения программы (run time)

| Оператор    | Описание                                                                                                                              |
| :---------- | :------------------------------------------------------------------------------------------------------------------------------------ |
| **try**     | блок, внутри которого записывается код, потенциально вызывающий исключение                                                            |
| **catch**   | блок, перехватывающий и обрабатывающий исключения заданных типов                                                                      |
| **finally** | блок, который выполняется всегда после try/catch, независимо от того, было исключение или нет. Используется для освобождения ресурсов |
| **throw**   | оператор создания и "выбрасывания" исключения                                                                                         |

## Как работает обработка исключений

1. Код в блоке `try` пытается выполниться
2. Если внутри `try` возникает исключение (создается с помощью `throw` или автоматически CLR), выполнение блока прерывается
3. CLR проходит стек вызовов вверх и ищет первый подходящий `catch`, который может обработать исключение выбранного типа
4. Если `catch` найден, управление передается в него — там можно обработать ошибку
5. Если подходящего `catch` не найдено, программа аварийно завершается
6. После `try` и `catch` выполняется блок `finally` (если он есть)

```cs
try {
    
    // Код, который может вызвать исключение
    
} catch (SpecificExceptionType ex) {
    
    // Обработка конкретного типа исключения
    
} catch (Exception ex) {
    
    // Обработка всех остальных исключений
    
} finally {
    
    // Код, который выполнится в любом случае (например, освобождение ресурсов)
}
```

Пример простой обработки исключения

```cs
try {
    int a = 10;
    int b = 0;
    int c = a / b; // вызвать исключение DivideByZeroException
} catch (DivideByZeroException) {
    Console.WriteLine("Деление на ноль недопустимо");
} finally {
    Console.WriteLine("Этот блок выполнится всегда");
}
```

Можно создавать собственные классы исключений, наследуя от `Exception`:

```cs
public class MyCustomException : Exception {
    public MyCustomException(string message) : base(message) { }
}
```

> Важно перехватывать исключения **от более специфичных к более общим**, чтобы правильно обработать каждое из них

```cs
try {
    // Код
}
catch (ArgumentNullException ex) {
    Console.WriteLine("Пустой аргумент");
}
catch (ArgumentException ex) {
    Console.WriteLine("Некорректный аргумент");
}
catch (Exception ex) {
    Console.WriteLine("Общее исключение");
}
```

Ловить исключения необязательно при обработке ошибок:

```cs
try
{
    // Код
}
finally
{
    // Выполнится всегда
}
```

```cs
try
{
    // код
}
catch (Exception ex)
{
    // обработка или логирование
    throw; // повторный выброс существующего исключения
}

```

## Лучшие практики при работе с исключениями

- Следует перехватывать только те исключения, которые можете обработать.
- Не стоит заглушать исключения без логирования.
- Не следует использовать исключения для управления потоком программы.
- Преимущественно, следует использовать стандартные типы исключений, если они подходят.
- При необходимости очистки ресурсов, следует использовать конструкцию `using` для автоматического освобождения ресурсов, где это возможно (альтернатива finally).
- В catch-блоках следует указывать как можно более конкретные типы исключений.

## Тип Exception

| Конструктор                                    | Описание                                                                           |
| ---------------------------------------------- | ---------------------------------------------------------------------------------- |
| `Exception()`                                  |                                                                                    |
| `Exception(string message)`                    | При создании задает сообщение об ошибке                                            |
| `Exception(string message, Exception innerEx)` | Дополнительно задает ссылку на внутреннее исключение, вызвавшее текущее исключение |

| Свойство         | Тип           | Доступ к свойству | Описание                                                                                                                                                                                                    |
| ---------------- | ------------- | ----------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Source`         | `string?`     | `{ get; set; }`   | Имя приложения или объекта, вызвавшего исключение                                                                                                                                                           |
| `Message`        | `string`      | `{ get; }`        | Поясняющее сообщение об ошибке или пустая строка                                                                                                                                                            |
| `StackTrace`     | `string?`     | `{ get; }`        | Строка, описывающая стек вызовов при возникновении ошибки. Если отслеживание стека невозможно, то возвращает `null`                                                                                         |
| `Data`           | `IDictionary` | `{ get; set; }`   | Словарь с доп. информацией, которую поместил в исключение программист. По умолчанию словарь пуст. Интерфейс очень похож на `Dictionary<T,Key>`                                                              |
| `TargetSite`     | `MethodBase?` | `{ get; }`        | Возвращает информацию о методе, вызвавшем исключение через рефлексию                                                                                                                                        |
| `InnerException` | `Exception?`  | `{ get; }`        | Объект, описывающий ошибку, вызвавшую текущее исключение                                                                                                                                                    |
| `HelpLink`       | `string?`     | `{ get; set; }`   | Ссылка на источник с документацией — Uniform Resource Name (URN) или Uniform Resource Locator (URL)                                                                                                         |
| `HResult`        | `int`         | `{ get; set; }`   | Числовой код ошибки или состояния, связанный с исключением, который облегчает взаимодействие между .NET и COM/WinAPI, а также позволяет программистам анализировать и обрабатывать ошибки по числовым кодам |

Кратко о структуре HRESULT:

- 1 бит — код серьёзности (0 — успех, 1 — ошибка)
- 4 бита — зарезервированы 
- 11 бит — код устройства (facility code)
- 16 бит — код ошибки (error code) ([1](http://www.delphikingdom.com/asp/viewitem.asp?catalogid=1112)) ([2](https://pvs-studio.ru/ru/docs/warnings/v716/))

| Метод        | Возвращает | Описание                                                                                                                                                                                      |
| ------------ | ---------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ToString()` | `string`   | Возвращает [текстовое представление](https://learn.microsoft.com/en-us/dotnet/api/system.exception.tostring?view=net-8.0#system-exception-tostring) объекта исключения (Message + StackTrace) |

## [Список](https://mikevallotton.wordpress.com/2009/07/08/net-exceptions-all-of-them/) встроенных типов исключений

| Сборка | Исключение | Описание |
| :-- | :-- | :-- |
| (`mscorlib.dll`) `System` | `AccessViolationException` | Возникает при попытке записи или чтения защищенной области памяти |
| (`mscorlib.dll`) `System` | `AppDomainUnloadedException` | Возникает при попытке обращения к невыгруженному домену приложения |
| (`mscorlib.dll`) `System` | `ApplicationException` | Возникает при нефатальных ошибках приложения |
| (`mscorlib.dll`) `System` | `ArgumentException` | Один из аргументов метода некорректен |
| (`mscorlib.dll`) `System` | `ArgumentNullException` | Один из аргументов является `null` и это является недопустимым значением для метода |
| (`mscorlib.dll`) `System` | `ArgumentOutOfRangeException` |  |
| (`mscorlib.dll`) `System` | `ArithmeticException` |  |
| (`mscorlib.dll`) `System` | `ArrayTypeMismatchException` |  |
| (`mscorlib.dll`) `System` | `BadImageFormatException` |  |
| (`mscorlib.dll`) `System` | `CannotUnloadAppDomainException` |  |
| (`mscorlib.dll`) `System` | `ContextMarshalException` |  |
| (`mscorlib.dll`) `System` | `DataMisalignedException` |  |
| (`mscorlib.dll`) `System` | `DivideByZeroException` |  |
| (`mscorlib.dll`) `System` | `DllNotFoundException` |  |
| (`mscorlib.dll`) `System` | `DuplicateWaitObjectException` |  |
| (`mscorlib.dll`) `System` | `EntryPointNotFoundException` |  |
| (`mscorlib.dll`) `System` | `ExecutionEngineException` |  |
| (`mscorlib.dll`) `System` | `FieldAccessException` |  |
| (`mscorlib.dll`) `System` | `FormatException` |  |
| (`mscorlib.dll`) `System` | `IndexOutOfRangeException` |  |
| (`mscorlib.dll`) `System` | `InsufficientMemoryException` |  |
| (`mscorlib.dll`) `System` | `InvalidCastException` |  |
| (`mscorlib.dll`) `System` | `InvalidProgramException` |  |
| (`mscorlib.dll`) `System` | `MemberAccessException` |  |
| (`mscorlib.dll`) `System` | `MethodAccessException` |  |
| (`mscorlib.dll`) `System` | `MissingFieldException` |  |
| (`mscorlib.dll`) `System` | `MissingMemberException` |  |
| (`mscorlib.dll`) `System` | `MissingMethodException` |  |
| (`mscorlib.dll`) `System` | `MulticastNotSupportedException` |  |
| (`mscorlib.dll`) `System` | `NotFiniteNumberException` |  |
| (`mscorlib.dll`) `System` | `NotImplementedException` | Метод не реализован |
| (`mscorlib.dll`) `System` | `NotSupportedException` |  |
| (`mscorlib.dll`) `System` | `NullReferenceException` | Возникает при попытке разименования нулевой ссылки |
| (`mscorlib.dll`) `System` | `ObjectDisposedException` | Возникает при попытке обращения к удаленному объекту |
| (`mscorlib.dll`) `System` | `OperationCanceledException` |  |
| (`mscorlib.dll`) `System` | `OutOfMemoryException` |  |
| (`mscorlib.dll`) `System` | `OverflowException` |  |
| (`mscorlib.dll`) `System` | `PlatformNotSupportedException` |  |
| (`mscorlib.dll`) `System` | `RankException` |  |
| (`mscorlib.dll`) `System` | `StackOverflowException` |  |
| (`mscorlib.dll`) `System` | `SystemException` | базовый класс для системных исключений |
| (`mscorlib.dll`) `System` | `TimeoutException` | Время выделенное на операцию вышло |
| (`mscorlib.dll`) `System` | `TypeInitializationException` |  |
| (`mscorlib.dll`) `System` | `TypeLoadException` |  |
| (`mscorlib.dll`) `System` | `TypeUnloadedException` |  |
| (`mscorlib.dll`) `System` | `UnauthorizedAccessException` |  |
| (`mscorlib.dll`) `System.Collections.Generic` | `KeyNotFoundException` | Указанный ключ словаря не найден |
| (`mscorlib.dll`) `System.IO` | `DirectoryNotFoundException` |  |
| (`mscorlib.dll`) `System.IO` | `DriveNotFoundException` |  |
| (`mscorlib.dll`) `System.IO` | `EndOfStreamException` |  |
| (`mscorlib.dll`) `System.IO` | `FileLoadException` |  |
| (`mscorlib.dll`) `System.IO` | `FileNotFoundException` |  |
| (`mscorlib.dll`) `System.IO` | `IOException` |  |
| (`mscorlib.dll`) `System.IO` | `PathTooLongException` | Путь к файлу превышает допустимую длину, определенную в системе |
| (`mscorlib.dll`) `System.IO.IsolatedStorage` | `` |  |
| (`mscorlib.dll`) `System.Reflection` | `AmbiguousMatchException` |  |
| (`mscorlib.dll`) `System.Reflection` | `CustomAttributeFormatException` |  |
| (`mscorlib.dll`) `System.Reflection` | `InvalidFilterCriteriaException` |  |
| (`mscorlib.dll`) `System.Reflection` | `MetadataException` |  |
| (`mscorlib.dll`) `System.Reflection` | `ReflectionTypeLoadException` |  |
| (`mscorlib.dll`) `System.Reflection` | `TargetException` |  |
| (`mscorlib.dll`) `System.Reflection` | `TargetInvocationException` |  |
| (`mscorlib.dll`) `System.Reflection` | `TargetParameterCountException` |  |
| (`mscorlib.dll`) `System.Resources` | `MissingManifestResourceException` |  |
| (`mscorlib.dll`) `System.Resources` | `MissingSatelliteAssemblyException` |  |
| (`mscorlib.dll`) `System.Runtime.CompilerServices` | `RuntimeWrappedException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `COMException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `ExternalException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `InvalidComObjectException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `InvalidOleVariantTypeException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `MarshalDirectiveException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `SafeArrayRankMismatchException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `SafeArrayTypeMismatchException` |  |
| (`mscorlib.dll`) `System.Runtime.InteropServices` | `SEHException` |  |
| (`mscorlib.dll`) `System.Runtime.Remoting` | `RemotingException` |  |
| (`mscorlib.dll`) `System.Runtime.Remoting` | `RemotingTimeoutException` |  |
| (`mscorlib.dll`) `System.Runtime.Remoting` | `ServerException` |  |
| (`mscorlib.dll`) `System.Runtime.Serialization` | `SerializationException` |  |
| (`mscorlib.dll`) `System.Security` | `HostProtectionException` |  |
| (`mscorlib.dll`) `System.Security` | `SecurityException` |  |
| (`mscorlib.dll`) `System.Security` | `VerificationException` |  |
| (`mscorlib.dll`) `System.Security` | `XmlSyntaxException` |  |
| (`mscorlib.dll`) `System.Security.AccessControl` | `PrivilegeNotHeldException` |  |
| (`mscorlib.dll`) `System.Security.Cryptography` | `CryptographicException` |  |
| (`mscorlib.dll`) `System.Security.Cryptography` | `CryptographicUnexpectedOperationException` |  |
| (`mscorlib.dll`) `System.Security.Policy` | `PolicyException` |  |
| (`mscorlib.dll`) `System.Security.Principal` | `IdentityNotMappedException` |  |
| (`mscorlib.dll`) `System.Text` | `DecoderFallbackException` |  |
| (`mscorlib.dll`) `System.Text` | `EncoderFallbackException` |  |
| (`mscorlib.dll`) `System.Threading` | `AbandonedMutexException` |  |
| (`mscorlib.dll`) `System.Threading` | `SynchronizationLockException` |  |
| (`mscorlib.dll`) `System.Threading` | `ThreadAbortException` |  |
| (`mscorlib.dll`) `System.Threading` | `ThreadInterruptedException` |  |
| (`mscorlib.dll`) `System.Threading` | `ThreadStartException` |  |
| (`mscorlib.dll`) `System.Threading` | `ThreadStateException` |  |
| (`mscorlib.dll`) `System.Threading` | `WaitHandleCannotBeOpenedException` |  |
| (`System.AddIn.dll`) `Microsoft.Contracts` | `Contract+AssertionException` |  |
| (`System.AddIn.dll`) `Microsoft.Contracts` | `Contract+AssumptionException` |  |
| (`System.AddIn.dll`) `Microsoft.Contracts` | `Contract+InvariantException` |  |
| (`System.AddIn.dll`) `Microsoft.Contracts` | `Contract+PostconditionException` |  |
| (`System.AddIn.dll`) `Microsoft.Contracts` | `Contract+PreconditionException` |  |
| (`System.AddIn.dll`) `System.AddIn.Hosting` | `AddInBaseInAddInFolderException` |  |
| (`System.AddIn.dll`) `System.AddIn.Hosting` | `AddInSegmentDirectoryNotFoundException` |  |
| (`System.AddIn.dll`) `System.AddIn.Hosting` | `InvalidPipelineStoreException` |  |
| (`System.AddIn.dll`) `System.AddIn.MiniReflection` | `GenericsNotImplementedException` |  |
| (`System.Configuration.dll`) `System.Configuration` | `ConfigurationErrorsException` |  |
| (`System.Configuration.dll`) `System.Configuration.Provider` | `ProviderException` |  |
| (`System.Configuration.Install.dll`) `System.Configuration.Install` | `InstallException` |  |
| (`System.Data.dll`) | `ModuleLoadException` |  |
| (`System.Data.dll`) | `ModuleLoadExceptionHandlerException` |  |
| (`System.Data.dll`) `Microsoft.SqlServer.Server` | `InvalidUdtException` |  |
| (`System.Data.dll`) `System.Data` | `ConstraintException` |  |
| (`System.Data.dll`) `System.Data` | `DataException` |  |
| (`System.Data.dll`) `System.Data` | `DBConcurrencyException` |  |
| (`System.Data.dll`) `System.Data` | `DeletedRowInaccessibleException` |  |
| (`System.Data.dll`) `System.Data` | `DuplicateNameException` |  |
| (`System.Data.dll`) `System.Data` | `EvaluateException` |  |
| (`System.Data.dll`) `System.Data` | `InRowChangingEventException` |  |
| (`System.Data.dll`) `System.Data` | `InvalidConstraintException` |  |
| (`System.Data.dll`) `System.Data` | `InvalidExpressionException` |  |
| (`System.Data.dll`) `System.Data` | `MissingPrimaryKeyException` |  |
| (`System.Data.dll`) `System.Data` | `NoNullAllowedException` |  |
| (`System.Data.dll`) `System.Data` | `OperationAbortedException` |  |
| (`System.Data.dll`) `System.Data` | `ReadOnlyException` |  |
| (`System.Data.dll`) `System.Data` | `RowNotInTableException` |  |
| (`System.Data.dll`) `System.Data` | `StrongTypingException` |  |
| (`System.Data.dll`) `System.Data` | `SyntaxErrorException` |  |
| (`System.Data.dll`) `System.Data` | `TypedDataSetGeneratorException` |  |
| (`System.Data.dll`) `System.Data` | `VersionNotFoundException` |  |
| (`System.Data.dll`) `System.Data.Common` | `DbException` |  |
| (`System.Data.dll`) `System.Data.Odbc` | `OdbcException` |  |
| (`System.Data.dll`) `System.Data.OleDb` | `OleDbException` |  |
| (`System.Data.dll`) `System.Data.SqlClient` | `SqlException` |  |
| (`System.Data.dll`) `System.Data.SqlTypes` | `SqlAlreadyFilledException` |  |
| (`System.Data.dll`) `System.Data` | `` |  |
| (`System.Data.dll`) `System.Data` | `` |  |
| (`System.Data.dll`) `System.Data` | `` |  |
| (`System.Data.dll`) `System.Data` | `` |  |

#Dotnet #C-Sharp #C-Sharp/Exceptions
