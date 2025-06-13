Чтобы связать ViewModel с View, нужно задать контекст данных (который создан по умолчанию в Avalonia). Это можно сделать в MainWindow.cs:
```csharp
public MainWindow(){
	DataContext = new MainWindowViewModel();
	InitializeComponent();
}
```

Чтобы среда разработки интерфейса могла использовать анализаторы и IntelliSence, необходимо подключить контекст данных. В Avalonia он подключен сразу.

```xml
<Window>
		...
		xmlns:d="http..."
		d:DataContext="{d:DesignInstance Type=local:MainWindowViewModel}"
		...
</Window>
```

В таком состоянии можно уже использовать привязку данных UI-элементов и открытых свойств ViewModel. Но такая связь не отслеживает изменения свойств, то есть все данные являются read-only.

Чтобы изменения были видны в UI, нужно, чтобы ViewModel реализовал интерфейс `INotifyPropertyChanged` и вызывал событие `PropertyChanged` в блоке `set` (здесь используется фреймворк ~MVVM Community toolkit).

Описанный способ уведомления UI об изменениях свойств слишком громоздкий, потому что при любом вызове сеттера произойдет вызов PropertyChanged, поэтому нужно добавлять проверку того, имеет ли свойство тоже значение, что и новое значение. Таким образом для каждого свойства придется определять:
* поле
* свойство
* проверку
* вызов события

Этих проблем можно избежать. Для этого нужно:
1) Сделать ViewModel наследником `ObservableObject` (который реализует `INotifyPropertyChanged`) ИЛИ добавить атрибут `ObservableObject`, чтобы не занимать единственную вакансию класса родителя.
2) Определить ***поле*** с атрибутом `[ObservableProperty]`. Таким образом будет происходить автогенерация нужного кода оберточного свойства в отдельном файле. Поэтому ViewModel теперь нужно пометить как `partial`.

Теперь свойство-компаньон для поля недоступно напрямую. Для добавления логики свойства есть 2 события:
1) On==*PropertyName*==Changing — срабатывает перед изменением свойства
2) On==*PropertyName*==Changed — срабатывает после изменения свойства

Для них соответственно есть 2 обработчика с таким же именем.
```csharp
// В коде ViewModel
partial void OnPropertyNameChanging(string? value){...}
partial void OnPropertyNameChanged(string? value){...}
```

* Может возникнуть ошибка при использовании частичных обработчиков событий. Чтобы это исправить, нужно в разметку проекта добавить следующий код:
```xml
<Target Name="TargerUnique_RemoveDuplicatedAnalyzers" BeforeTargets="CoreCompile">
	<ItemGroup>
		<FilteredAnalyzer Include="@(Analyzer->Distinct())" />
		<Analyzer Remove="@(Analyzer)" />
		<Analyzer Include="@(FilteredAnalyzer)" />
	</ItemGroup>
</Target>
```

Допустим имеются свойства: Имя, Фамилия, Отчество и ПолноеИмя. Ниже определены свойства, изменения которых отслеживаются. Read-only свойство FullName обновляется автоматически.
```csharp
[ObservableProperty]
[AlsoNotifyChangeFor(nameof(FullName))]
private string name;

[ObservableProperty]
[AlsoNotifyChangeFor(nameof(FullName))]
private string surname;

[ObservableProperty]
[AlsoNotifyChangeFor(nameof(FullName))]
private string lastName;

public string FullName => $"{Name} {Surname} {LastName}";
```

#### Интерактивность

Для обработки событий используются команды, реализующие "ICommand". В ViewModel определяются свойства ICommand, которые при создании в качестве параметра принимают обработчик событий.

```csharp
public interface ICommand
{
	event EventHandler? CanExecuteChanged;
	
	bool CanExecute (object? parameter);
	void Execute (object? parameter); 
}
```

Одна из основных команд — это RelayCommand, которая при создании принимает обработчик события `Action` и опционально предикат.

CanExecute должен определять, когда и как можно выполнить команду.
Execute — логика команды.

Можно не создавать явным образом свойство типа ICommand, а сделать все проще. Можно определить Action-метод, который будет обработчиком команды и опционально метод доступа к выполнению команды "CanExecute".
```csharp
private bool CanClick() => Property == null;

// Скобки не обязательны
[RelayCommand(CanExecute = nameof(CanClick))]
private void Click()
{
	Property = "new value";
}
```

```csharp
[ICommand]
private void Send(){...}
```

[56:00 - MVVM ViewModel ObservableObject](https://www.youtube.com/watch?v=uVIzK2snugk&ab_channel=KevinBost)

CanClick — это логика доступа к выполнению команды. Click можно также сделать асинхронным.

#Architecture #Patterns #Patterns/MVVM #Desktop/WPF #Desktop/Avalonia