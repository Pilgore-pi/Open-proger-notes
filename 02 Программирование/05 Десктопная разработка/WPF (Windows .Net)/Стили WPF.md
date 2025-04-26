Стили позволяют определить набор некоторых свойств и их значений, которые потом могут применяться к элементам в xaml.

Стили позволяют создать визуальное единообразие для определенных элементов управления и снизить повторение кода.

Определение стиля:
```xml
<Window.Resources>
	<Style x:Key="ButtonStyle">
		<Setter Property="Button.Background" Value="Black"/>
		<Setter Property="Button.Foreground" Value="Yellow"/>
		<Setter Property="Button.BorderBrush" Value="Yellow"/>
	</Style>
</Window.Resources>
```
Стиль должны определяться в ресурсах окна или страницы и иметь ключ, по которому элементы управления смогут обратиться к стилю.

Можно указать дополнительное свойство для Style — "TargetType", которое определяет, для какого типа должен применяться стиль. Если "TargetType" определен, можно не указывать "x:Key" для стиля и не уточнять тип для сеттера (не Property="Button.Background", а Property="Background").

```xml
<Style TargetType="Button">
<!-- ...Или-->
<Style TargetType="{x:Type Button}">
<!-- ... -->
```

### Setter
Определение сеттера в XAML:
```xml
<Setter Property="Button.Background" Value="Black"/>
```
Property — свойство, к которому будет применяться сеттер
Type — тип, содержащий данное свойство

### EventSetter
Позволяет задавать обработчик событий для элементов управления
```xml
<EventSetter Event="Button.Click" Handler="Button_Click" />
```

Создание стиля в коде C#
```cs
Style buttonStyle = new Style();

buttonStyle.Setters.Add(new Setter {
	Property = Control.FontFamilyProperty,
	Value = new FontFamily("Calibri") });
	// ...
buttonStyle.Setters.Add(new EventSetter {
	Property = Button.Click,
	Handler = new RoutedEventHandler(Button_Clicked) });
	// ...
```

## Наследование стилей
В XAML для стиля можно установить свойство BasedOn, которое позволит унаследовать характеристики другого стиля.
```XML
<Style x:Key="ChildStyle" BasedOn="{StaticResource ParentStyle}">
```

Далее: [[+Триггеры]]

#Desktop #Desktop/WPF