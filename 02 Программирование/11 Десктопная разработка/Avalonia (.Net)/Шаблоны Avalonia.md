
## Изменение стандартных шаблонов элементов управления

Список [шаблонов](https://github.com/AvaloniaUI/Avalonia/tree/master/src/Avalonia.Themes.Fluent/Controls) элементов управления для темы **`Fluent`**.

Для большинства контролов и их состояний (`:pressed`, `:pointerover`, `:checked`, ...) можно без лишних заморочек изменить свойства шаблонов, которые напрямую переопределить нельзя. Основная способ изменения свойств шаблонов:

```xml
<Style Selector="<Control>[.class] /template/ <Template control>">
    <Setter Property="<property>" Value="<value>"/>
</Style>
```

>? Что происходит, когда мы обращаемся к шаблону для изменения его свойств? Мы создаем новый шаблон или изменяем существующий?

Значение `<Template control>` нужно подбирать в зависимости от задаваемых свойств. Например для изменения рамки `CheckBox`, нужно обращаться к `Border`, а при изменении цвета галочки к `Path`. Чтобы понять какие контролы ответствнны за определение тех или иных свойств в шаблоне элемента, нужно посмотреть [определение](https://github.com/AvaloniaUI/Avalonia/tree/master/src/Avalonia.Themes.Fluent/Controls) этого контрола на GitHub.

https://github.com/AvaloniaUI/Avalonia/tree/554aaec5e5cc96c0b4318b6ed1fbf8159f442889/src/Avalonia.Themes.Default

>Некоторые стандартные стили Avalonia используют локальные значения в своих шаблонах вместо привязки шаблонов или сеттеров стилей. Это делает невозможным изменение таких свойств без переопределения всего шаблона.

## Определение пользовательских шаблонов

[пара примеров](https://github.com/AvaloniaUI/Avalonia/issues/2516)

[Как это делается в WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-8.0)

Настройка шаблонов в Avalonia очень схожа с их настройкой в WPF.

Изменение шаблона элемента может происходить следующим образом:

```xml
<Style Selector="SomeControl">
  <Setter Property="Template">
    <SomeTemplatedControl .../>
  </Setter>
</Style>
```

`TemplateBinding` связывает значение свойства в шаблоне элемента управления со значением другого свойства элемента управления-шаблона.

----

Определение своего шаблона (github [muscurd-i](https://github.com/vikkio88/muscurd-i/blob/main/Muscurdi/Assets/Styles/Main.xaml)):

>Почему часто можно встретить префикс PART_ в названии шаблона?

Изменение шаблона чекбокса. Это позволяет изменить цвет фона самого прямоугольника с галочкой, а не фон всего элемента управления.

```xml
<Style Selector="CheckBox.dark">
  <!--Other Setters...-->
  <Setter Property="Template">
    <Setter.Value>
      <ControlTemplate>
        <Grid x:Name="RootGrid" ColumnDefinitions="20,*">
          <Border x:Name="PART_Border"
                  Grid.ColumnSpan="2"
                  Background="{TemplateBinding Background}"
                  BorderBrush="{TemplateBinding BorderBrush}"
                  BorderThickness="{TemplateBinding BorderThickness}"
                  CornerRadius="{TemplateBinding CornerRadius}"/>

          <Grid VerticalAlignment="Top" Height="32">
            <Border x:Name="NormalRectangle"
                    BorderBrush="{DynamicResource CheckBoxCheckBackgroundStrokeUnchecked}"
                    Background="#22ffffff"
                    BorderThickness="{DynamicResource CheckBoxBorderThemeThickness}"
                    CornerRadius="{TemplateBinding CornerRadius}"
                    UseLayoutRounding="False"
                    Height="20"
                    Width="20" />

            <Viewbox UseLayoutRounding="False">
              <Panel>
                <Panel Height="16" Width="16" />
                <Path x:Name="CheckGlyph"
                      Opacity="0"
                      Fill="{DynamicResource CheckBoxCheckGlyphForegroundUnchecked}"
                      Stretch="Uniform"
                      VerticalAlignment="Center"
                      FlowDirection="LeftToRight" />
              </Panel>
            </Viewbox>
          </Grid>
          <ContentPresenter x:Name="ContentPresenter"
            ContentTemplate="{TemplateBinding ContentTemplate}"
            Content="{TemplateBinding Content}"
            Margin="{TemplateBinding Padding}"
            RecognizesAccessKey="True"
            HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
            VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
            TextWrapping="Wrap"
            Grid.Column="1" />
        </Grid>
      </ControlTemplate>
    </Setter.Value>
  </Setter>
</Style>
```

Изменение фона в состояниях `:checked` и `:indeterminate`

```xml
<Style Selector="CheckBox.dark:checked /template/ Border#NormalRectangle">
  <Setter Property="Background" Value="#22ffffff"/>
</Style>

<Style Selector="CheckBox.dark:indeterminate /template/ Border#NormalRectangle">
  <Setter Property="Background" Value="#22ffffff"/>
</Style>

<Style Selector="CheckBox.dark:pointerover /template/ Border#NormalRectangle">
  <Setter Property="Background" Value="#33ffffff"/>
</Style>
```

[TemplateBinding](https://docs.avaloniaui.net/docs/0.10.x/data-binding/binding-in-a-control-template) — это ссылка на свойство шаблона родительского элемента.



Краткий синтаксис `{TemplateBinding Something}` эквивалентен слеудющему:
```
{Binding RelativeSource={RelativeSource TemplatedParent}, Mode=OneWay}
```

Чтобы изменить режим `Mode`, нужно использовать полный синтаксис.

`TemplateBinding` может быть использована только для элемента `IStyledElement`

https://docs.avaloniaui.net/docs/styling/styles

https://docs.avaloniaui.net/docs/data-binding

#Desktop/Avalonia