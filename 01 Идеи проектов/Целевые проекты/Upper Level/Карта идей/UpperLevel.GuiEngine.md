

- HTML
- Markdown
- Mathjax (Latex)  
## Constructors  
  
- SQL query to the set of sources (\*1) (user specifies sources)  
- HTTP(S) request  
- Message manager (user writes a message and sends it anywhere he wants: telegram, email, http, gpt API...)  
- Message box  
  - Type: warning, error, info, success  
  - Background color  
  - Foreground color and font  
  - Rich text content  
  
(\*1) Source types:  
- File  
- String (JSON, XML, CSV, DBF, YAML, EXCEL, ...)  
- Database connection  
  
## Inputs  
  
- Read number  
- Read number range  
- Read string (bool formatted, StringWrapper wrapper)  
- Read character  
- Read character range  
- Read one of given options  
- Read date \[time\] (enum DateTimeFormat format)  
- Read date \[time\] range (enum DateTimeFormat format)  
  
## Converters  
  
- Object1 to Object2  
  
```text  
|-----------------------------------------------------|  
| <Source object type>   |  <Converted object type>   |  
|-----------------------------------------------------|  
|                        |                            |  
|                        |                            |  
|  <body of the source>  |      <converted body>      |  
|                        |                            |  
|                        |                            |  
|-----------------------------------------------------|  
|                    [ Convert ]                      |  
|-----------------------------------------------------|  
```  
  
## Displayers  
  
- TreeViewer. Accepts a serialized string and displays it as tree  
- TableViewer. Accepts table data and provides tools to edit the table  
  
## Templates  
  
- AuthorizationForm  
- RegistrationForm  
- ContainerForm. User can choose an Avalonia's UserControl to insert into one of the predefined cells. Form should support dragging  
-

#Идеи_проектов #Идеи_проектов/Upper_Level/GUI-Engine