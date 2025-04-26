## Основные элементы PowerShell

### 1. Атрибут `[Parameter(Mandatory=$false)]`

Используется для объявления **необязательных параметров** в функциях:
- `Mandatory=$false` указывает, что параметр можно не передавать ([1][5])
- По умолчанию для параметров используется `Mandatory=$false`
- Пример с параметром по умолчанию:
  ```powershell
  param(
    [Parameter(Mandatory=$false)]
    [string]$Name = "Гость"
  )
  ```

### 2. Атрибут `[CmdletBinding()]`

Добавляет функции возможности командлетов:
- Включает поддержку общих параметров: `-Verbose`, `-ErrorAction` и др.
- Позволяет использовать `$PSCmdlet` для управления выполнением
- Пример использования:

```powershell
[CmdletBinding(SupportsShouldProcess=$true)]
param()
```

### 3. Специальные символы

Для форматирования текста используйте:
- **Перевод строки**: `` `n``
  Пример: ``"Первая строка`nВторая строка"``
- **Табуляция**: `` `t``
  Пример: ``"Имя:`tАлексей"``
- Комбинация: `` `r`n`` — возврат каретки + новая строка

---

## Синтаксис командлетов и функций

### Общий синтаксис командлетов

Формат: `Глагол-Существительное` с параметрами:

```powershell
Командлет -Параметр1 Значение -Параметр2 @{Хэш=...}
```

Пример:

```powershell
Get-Process -Name "pwsh" | Stop-Process -Force
```

### Синтаксис объявления функций

```powershell
function <Verb>-<Noun> {
  [CmdletBinding()]
  param(
    [Parameter(Position=0)]
    [string]$Param1,
    
    [Parameter(Mandatory)]
    [int]$Param2
  )
  
  begin { Инициализация }
  process { Обработка каждого элемента }
  end { Завершение }
}
```

Ключевые элементы:

- `param()` — блок параметров
- `begin{}` — выполняется один раз перед обработкой
- `process{}` — выполняется для каждого элемента ввода
- `end{}` — постобработка

### Вызов функций

```powershell
# По имени параметра
Get-UserInfo -UserName "Admin" -Age 30

# Позиционно (если задано Position=0)
Get-UserInfo "Admin" 30

# Через конвейер
"Admin" | Get-UserInfo -Age 30
```

---

## Пример комплексной функции

```powershell
[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Mandatory=$false, Position=0)]
    [string]$Message = "Приветствие",
    
    [Parameter(Mandatory)]
    [ValidateRange(1,10)]
    [int]$RepeatCount
)

begin {
    $header = "Сообщение:`t$Message`nПовторы:`t$RepeatCount`n"
}

process {
    if ($PSCmdlet.ShouldProcess($Message)) {
        1..$RepeatCount | ForEach-Object {
            "$_`: $Message`nДата: $(Get-Date)`n"
        }
    }
}

end {
    Write-Host $header -ForegroundColor Cyan
}
```

Вызов:

```powershell
.\Greeting.ps1 -Message "Тест" -RepeatCount 3 -Verbose
```

#OS #OS/Windows #CMD #PowerShell