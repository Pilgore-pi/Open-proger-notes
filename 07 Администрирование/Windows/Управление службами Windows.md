[[Менеджер служб|Менеджер служб]] можно открыть, запустив утилиту `services.msc`

Batch:

```batch
setlocal

set SERVICE_NAME=MyService
set EXE_PATH="%LOCALAPPDATA%\MyService\MyService.exe"

xcopy /Y /E "%~dp0*" "%LOCALAPPDATA%\MyService\"

sc create %SERVICE_NAME% binPath= %EXE_PATH% start= auto
```

PowerShell:

```powershell
@ServiceName = "YourService"
@BinPath = "//Main/folder"
sc create @ServiceName binPath= @BinPath start= auto
```



#OS/Windows/Services