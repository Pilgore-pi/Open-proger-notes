XML документ объявляет строка:

```
<?xml version="1.0" encoding="utf-8" ?>
```

XML-документ должен иметь один единственный корневой элемент, внутрь которого помещаются все остальные элементы.

Каждый элемент определяется с помощью открывающего и закрывающего тегов, например, `<person>` и `</person>`, внутри которых помещается значение или содержимое элементов. Также элемент может иметь сокращенное объявление: `<person />`

>Язык XML чувствителен к регистру

Элемент может иметь вложенные элементы и атрибуты.

XML-теги могут быть любыми, это не регламентировано. Например, можно поставить в соответствие некоторым объектам в стиле ООП XML теги и атрибуты, которые будут аналогами свойств.

ООП:
```csharp
class Person
{
  public string Name { get;}
  public int Age { get; set; }
  public string Company { get; set; }
  public Person(string name, int age, string company)
  {
    Name = name;
    Age = age;
    Company = company;
  }
}
```

XML:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<people>
  <person name="Tom">
    <company>Microsoft</company>
    <age>37</age>
  </person>
  <person name="Bob">
    <company>Google</company>
    <age>41</age>
  </person>
</people>
```

## System.Xml

#### Парсинг XML документа и вывод данных

https://metanit.com/sharp/tutorial/16.2.php

Допустим, у нас есть XML документ следующего вида:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Patterns>
  <LastUsedPath>C:/someFolder</LastUsedPath>

  <Pattern>
    <Name>Russian words</Name>
    <Description>finds all the russian words</Description>
    <Regex>[a-я]+</Regex>
  </Pattern>

  <!--Запись в виде атрибутов-->
  <Pattern Name="English words"
           Description="finds all the english words"
           Regex="[a-z]+">
  </Pattern>
</Patterns>
```

```csharp
// XML документ
XmlDocument xmlDoc = new();
// загрузка содержимого документа
xmlDoc.Load("file.xml");
// корневой элемент дерева XML
XmlElement? xmlRoot = xmlDoc.DocumentElement;
```

XmlNode — узел XML дерева. Этот класс реализует `IEnumerable`. От XmlNode наследуются классы `XmlElement`, `XmlDocument`, `XmlAttribute`

>Атрибуты в XML могут быть записаны как свойства: `property="value"` и как вложенные узлы: `<property>value</property>`

```csharp
// вывод имен всех подузлов самого верхнего уровня
foreach(XmlNode node in xmlRoot) //ИЛИ xmlRoot.ChildNodes
  Console.WriteLine($"{node.Name}:");
  /* LastUsedPath
   * Pattern
   * Pattern
   */

// вывод узлов второго уровня и их внутреннего текста
foreach(XmlNode child in node)
  Console.WriteLine($"\t{child.Name} / {child.InnerText}");

// Вывод всех имен и значений атрибутов данного узла
if (tag.Attributes is null) throw new Exception("Null");
foreach (XmlAttribute attr in tag.Attributes)
  Console.WriteLine($"\t{attr.Name} / {attr.Value}");
```

Итоговый вывод: (ПРОБЛЕМА, разный уровень тегов, как вывести LastUsedPath правильно?)

```
xmlRoot:

LastUsedPath / C:/someFolder / #0:
        #text / C:/someFolder
Pattern / Russian wordsfinds all the russian words[a-я]+ / #1:
        Name / Russian words
        Description / finds all the russian words
        Regex / [a-я]+
Pattern /  / #2:
        Name / English words
        Description / finds all the english words
        Regex / [a-z]+
Press any key to continue...
```

В данном случае свойства `InnerText` & `Value` у **атрибутов** будут одинаковы. У тегов, содержащих текст `Value` равно `null`.

>Свойство `InnerText` конкатенирует все внутренние значения тегов.

```csharp

```

#### Редактирование XML документа

Далее: [[XPath]]

#C-Sharp #C-Sharp/XML