
при выделении нескольких байт в любом контексте отображается числовая интерпретация и текстовая (ASCII, UTF8, UTF16), и где самое для обратного порядка байт

## UpperLevel.Data.Converters

- Json — XML (elementary)
- Json — CSV (elementary)
- XML — CSV (easy)
- XML — YAML (easy)
- Json — HTML (ok, multiple implementations)
- obsidian vault to HTML site (No JS, include CSS (optionally))


Serialization formats:

xml, json, bson, html, yaml, csv, csharp type, cpp type, python type (pickle), java type, php serialization format

generally i can deserialize from any format to C# objects and then serialize to desired format

draw a tree showing connections between formats that can be two way converted

Markup languages:

Markdown
HTML

Add opportunity to user him to be able to create his own text format by implementing ISerializationFormat

### Functions examples


bytes `|11111111|01010101|00001111|` to:
- int
- float
- decimal
- double
- \<Encoding\>

Parameters:
- byte order (Big endian, Little endian)
- bit order


#Идеи_проектов #Идеи_проектов/Upper_Level/Data