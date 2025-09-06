[[04 Проектирование UpperLevel.Files]]

FileManager (объединение файлов):

пункт в контекстном меню ПРИ ВЫДЕЛЕНИИ НЕСКОЛЬКИХ ФАЙЛОВ "как единый файл" ("As single file"):

- Как ZIP
- Как RAR
- Как PDF
- Как DOCX
- Как ODT
- Как XLSX
- Как HTML (с вставками данных файлов в кодировке base64)
- Как CombinedObject (самописный формат)

аналогично пункт "Для каждого выполнить" ("For each file execute")

## `File.IsSpecificFormat(byte[] array)`  
  
Create somewhat similar to library that consists of FileValidators.  
File validators check if the specified byte array matches given file type  
  
For example, some `byte[]` collection represents CSV text file. For this case, we translate byte array to string and call `string.AsCsv()` method  
  
So, file validators analyze the file structure to determine, if the file matches specified format  
  
## `File.GuessFormat(byte[] array)`  
  
This API method analyzes a file and tries to determine its format  
  
The logic gotta be hella complex

#Идеи_проектов #Идеи_проектов/Upper_Level/Os