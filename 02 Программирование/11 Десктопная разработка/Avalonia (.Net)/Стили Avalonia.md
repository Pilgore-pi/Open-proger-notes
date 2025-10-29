
Стили в Avalonia представляют собой смесь стилей CSS и WPF/UWP. Они определяются в свойстве Window.Styles. Для стиля нужно указывать селектор "Selector", то есть к какой группе элементов должен применяться стиль.

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
  <Window.Styles>
    <Style Selector="TextBlock.h1">
      <Setter Property="FontSize" Value="24"/>
      <Setter Property="FontWeight" Value="Bold"/>
    </Style>
  </Window.Styles>

  <TextBlock Classes="h1">I'm a Heading!</TextBlock>
</Window>
```

Стили не будут работать, если их добавить в словарь ресурсов элемента управления или приложения. Это связано с тем, что в Avalonia порядок определения стилей важен, в отличие от WPF, а словарь ресурсов не является упорядоченным.

Приоритеты применения стилей:
1. Наивысший:
2. Высокий: локально заданное значение
3. Средний: последний стиль
4. Низкий: первый стиль

Стили можно импортировать из XAML-файла с помощью класса `StyleInclude`:

```xml
<Window.Styles>
  <StyleInclude Source="/CustomStyles.xaml" />
</Window.Styles>
```

>При запуске проекта Avalonia можно нажать F12, чтобы открыть просмотр логического и визуального дерева. Там содержится информация о компоновке и отображении стилей.

## Псевдоклассы

Как и в CSS контролы могут иметь псевдо классы, которые обозначаются через символ "`:`":
```xml
<StackPanel>
  <StackPanel.Styles>
    <Style Selector="Border:pointerover">
      <Setter Property="Background" Value="Red"/>
    </Style>
  </StackPanel.Styles>
  <Border>
  <TextBlock>I will have red background when hovered.</TextBlock>
  </Border>
</StackPanel>
```

Как видно из примера, стили можно определять для любого контрола. В примере для StackPanel все внутренние Border-ы приобретают указанный стиль.

Ниже приведен пример того, как можно установить стиль путем изменения шаблона элемента управления:
```xml
<StackPanel>
  <StackPanel.Styles>
    <Style Selector="Button:pressed /template/ ContentPresenter">
      <Setter Property="TextBlock.Foreground" Value="Red"/>
    </Style>
  </StackPanel.Styles>
  <Button>I will have red text when pressed.</Button>
</StackPanel>
```

### Список псевдоклассов

>Данный список не точный, информация была собрана с разных сайтов и форумов. Данные могут быть устаревшими

Button:
* `:pointerover` — при наведении,
* `:pressed` — при нажатии
* `:focus` — состояние фокуса,
* `:disabled` — `Enabled="False"`

CheckBox: 
* `:checked` — галочка стоит,
* `:indeterminate` — неопределенное состояние
* `:pointerover`
* `:pressed`
* `:disabled`
* `:unchecked`
* `:flyout-open`
* ? `:checked:pointerover`
* ? `:checked:pressed`
* ? `:checked:disabled`

TextBox:
* `:focus`
* `:focus-within`
* `:focus-visible`


- `:valid` — For input controls, when the input is valid.
- `:invalid` — For input controls, when the input is invalid.

## Селекторы

Селекторы используют синтаксис, основанный на CSS.

Примеры селекторов:

|Selector|Description|
|---|---|
|`Button`|Selects all `Button` controls|
|`Button.red`|Selects all `Button` controls with the `red` style class|
|`Button.red.large`|Selects all `Button` controls with the `red` and `large` style classes|
|`Button:focus`|Selects all `Button` controls with the `:focus` pseudoclass|
|`Button.red:focus`|Selects all `Button` controls with the `red` style class and the `:focus` pseudoclass|
|`Button#myButton`|Selects a `Button` control with a name of `myButton`|
|`StackPanel Button.foo`|Selects all `Button`s with the `foo` class that are descendants of a `StackPanel`|
|`StackPanel > Button.foo`|Selects all `Button`s with the `foo` class that are children of a `StackPanel`|
|`Button /template/ ContentPresenter`|Selects all ContentPresenter controls inside of Button's template|

Далее: [[Шаблоны Avalonia]]

#Desktop/Avalonia