Представляет собой сетку, каждая ячейка которой может содержать UIElement. Так как определение объекта Grid в коде XAML очень простое, ниже приведен только код C#.

Легкое добавление строк и столбцов:

```xml
<Grid ColumnDefinitions="100,1.5*,4*" RowDefinitions="Auto,Auto,Auto"></Grid>
```

По умолчанию столбец и строка имеют размер "`*`"

## Добавление элемента в ячейку

```cs
Grid grid = new Grid();
//Создание столбцов и колонок, если это необходимо
grid.RowDefinitions.Add(new RowDefinition() { MinHeight = 20 });
grid.RowDefinitions.Add(new RowDefinition() { MaxHeight = 100 });
grid.ColumnDefinitions.Add(new ColumnDefinition());
grid.ColumnDefinitions.Add(new ColumnDefinition());

grid.Children.Add(myUIElement);
Grid.SetColumn(myUIElement, 1);
Grid.SetRow(myUIElement, 0);
```

Ширина объекта ColumnDefinition и высота RowDefinition — это самостоятельные структуры, которые нужно задавать через конструктор. В коде XAML можно задать абсолютное значение в пикселях и дюймах, можно задать относительное значение в звездах или с помощью auto. Чтобы это было возможно в коде C# эти свойства были реализованы в виде структуры GridLength.

Задание размеров столбцов и строк
```cs
grid.ColumnDefinitions.Add(new ColumnDefinition()
   { Width = new GridLength(2.0, GridUnitType.Star) });
grid.RowDefinitions.Add(new RowDefinition()
   { Height = new GridLength(2.0, GridUnitType.Star) });
```

## GridLength

Конструкторы:
1. GridLength(double pixels) — устанавливает длину в пикселях
2. GridLength(double value, GridUnitType type) — устанавливает длину в указанных единицах.

Определение GridUnitType:
```cs
public enum GridUnitType
{
	//Размер определяется свойствами размера для объекта содержимого.
	Auto,
	//Значение выражено в пикселях.
	Pixel,
	//Значение выражено в виде пропорционально изменяющегося
	//доступного пространства.
	Star
}
```

#Desktop #Desktop/WPF