
## Global usings (глобальные директивы using)

- Это стандартные директивы `using`, которые благодаря ключевому слову `global` применяются **ко всем файлам в проекте**.
    
- Обычно пишутся в одном отдельном файле, например, `GlobalUsings.cs`, и выглядят так:

```cs
global using System;
global using System.Collections.Generic;
```

- Это значит, что в любом файле проекта можно использовать типы из этих пространств имён без необходимости снова писать `using System;` и т.п.
    
- Глобальные `using` появились в C# 10 (и .NET 6).

#Dotnet #C-Sharp #Dotnet/Applications