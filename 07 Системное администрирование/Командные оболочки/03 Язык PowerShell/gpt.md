# 4

Как запустить программу с заданными аргументами в PowerShell?

---

Для запуска программы с заданными аргументами в PowerShell можно использовать несколько методов. Вот основные из них:

## 1. Использование `Start-Process`

Командлет `Start-Process` позволяет запускать процессы с указанием аргументов.

### Синтаксис:
```powershell
Start-Process -FilePath "путь_к_программе" -ArgumentList "аргумент1", "аргумент2"
```

### Пример:
```powershell
Start-Process -FilePath "C:\Path\To\YourProgram.exe" -ArgumentList "-arg1 value1", "-arg2 value2"
```

## 2. Использование оператора вызова `&`
Если программа находится в текущем каталоге или вы хотите указать полный путь, можно использовать оператор вызова `&`.

### Синтаксис:
```powershell
& "путь_к_программе" "аргумент1" "аргумент2"
```

### Пример:
```powershell
& "C:\Path\To\YourProgram.exe" "-arg1 value1" "-arg2 value2"
```

## 3. Запуск из командной строки PowerShell
Если вы хотите запустить PowerShell скрипт с параметрами, используйте следующий синтаксис:
```powershell
powershell.exe -File "путь_к_скрипту.ps1" -ArgumentList "аргумент1", "аргумент2"
```

### Пример:
```powershell
powershell.exe -File "C:\Path\To\YourScript.ps1" -ArgumentList "value1", "value2"
```

## Примечания
- При использовании `Start-Process`, если аргументы содержат пробелы, их нужно заключать в кавычки.
- Оператор `&` не обрабатывает параметры, поэтому все аргументы должны быть переданы как отдельные строки.

Эти методы позволяют удобно запускать программы и скрипты с необходимыми параметрами в PowerShell.

Citations:
[1] https://learn.microsoft.com/ru-ru/powershell/module/microsoft.powershell.management/start-process?view=powershell-7.5
[2] https://www.osp.ru/winitpro/2014/09/13042591
[3] https://windowsnotes.ru/powershell-2/zapusk-programmy-iz-powershell/
[4] http://forum.oszone.net/nextoldesttothread-308862.html
[5] https://ru.stackoverflow.com/questions/1461230/%D0%97%D0%B0%D0%BF%D1%83%D1%81%D0%BA-%D1%81%D1%86%D0%B5%D0%BD%D0%B0%D1%80%D0%B8%D1%8F-powershell-%D0%B8%D0%B7-%D0%BF%D1%80%D0%BE%D0%B2%D0%BE%D0%B4%D0%BD%D0%B8%D0%BA%D0%B0-%D1%81-%D0%BF%D0%B0%D1%80%D0%B0%D0%BC%D0%B5%D1%82%D1%80%D0%B0%D0%BC%D0%B8
[6] https://learn.microsoft.com/ru-ru/windows/msix/psf/psf-launch-apps-with-parameters
[7] https://doprogerammer.blogspot.com/2015/09/powershell.html
[8] https://skillbox.ru/media/code/powershell-dlya-chaynikov-chto-eto-i-kak-s-nim-rabotat/

#MERGE_NOTES