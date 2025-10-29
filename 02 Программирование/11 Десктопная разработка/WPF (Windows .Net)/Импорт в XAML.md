
В коде XAML нового проекта можно увидеть как подключаются несколько пространств имен.

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="using:FractalTreeAvalonia.ViewModels"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="610"
        x:Class="FractalTreeAvalonia.Views.MainWindow"
        Icon="/Assets/avalonia-logo.ico"
        Title="FractalTreeAvalonia">
```

xmlns (XML namespace) указано первым свойством класса Window в примере. Далее, где пишется `xmlns:`, подключаются другие пространства имен и для них создается псевдоним.

* Префикс `x:` в разметке XAML говорит о том, что элемент находится в пространстве имен XAML.

#Desktop #Desktop/WPF