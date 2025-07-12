> **Roslyn** — это современная [открытая](https://github.com/dotnet/roslyn) реализация компиляторов C\# и Visual Basic а также API предоставляющий инструменты анализа кода

Существует статья от ведущего архитектора C# по Roslyn, переведенная на русский язык, в которой можно узнать историю создания платформы Roslyn

## Что такое Roslyn?

- Roslyn — это не просто компилятор, а полноценная **платформа Compiler-as-a-Service**.

- Компиляторы C# и VB.NET в Roslyn написаны на самих этих языках (C# компилятор на C#), в отличие от старых компиляторов, написанных на C++[1](https://ru.wikipedia.org/wiki/Roslyn)[5](https://xn--90aia9aifhdb2cxbdg.xn--p1ai/blogs/roslyn-features-visual-studio).

- Roslyn предоставляет **API для лексического, синтаксического и семантического анализа кода**, что позволяет программно работать с исходным кодом, создавать инструменты рефакторинга, статического анализа и генерации кода[2](https://habr.com/ru/companies/veeam/articles/648775/)[3](https://itvdn.com/ru/blog/article/compiler-roslyn)[4](https://habr.com/ru/companies/pvs-studio/articles/301204/)[8](https://learn.microsoft.com/ru-ru/dotnet/csharp/roslyn-sdk/).


## Чем Roslyn отличается от "обычного" компилятора C#?

|Особенность|Старый компилятор C# (до Roslyn)|Roslyn (новый компилятор)|
|---|---|---|
|Язык реализации|C++|C#|
|Открытость исходного кода|Закрытый|Открытый, доступен на GitHub|
|Доступность API|Отсутствует, компиляция — "чёрный ящик"|Полноценный API для анализа и генерации кода|
|Возможность динамической компиляции|Нет|Есть, можно компилировать и запускать код на лету|
|Интеграция с IDE|Ограниченная|Глубокая интеграция (IntelliSense, рефакторинг, анализ)|
|Поддержка создания инструментов|Очень ограничена|Позволяет создавать расширения, анализаторы, генераторы кода|
|Поддержка интерактивного режима|Нет|Есть (C# Interactive)|

## Что даёт Roslyn разработчикам?

- Возможность **создавать собственные анализаторы кода и рефакторинги**, которые интегрируются в Visual Studio[4](https://habr.com/ru/companies/pvs-studio/articles/301204/)[5](https://xn--90aia9aifhdb2cxbdg.xn--p1ai/blogs/roslyn-features-visual-studio).
    
- Доступ к структуре кода в виде дерева синтаксиса (Syntax Tree) и семантики, что облегчает метапрограммирование и генерацию кода[3](https://itvdn.com/ru/blog/article/compiler-roslyn)[8](https://learn.microsoft.com/ru-ru/dotnet/csharp/roslyn-sdk/).
    
- Возможность **динамически компилировать и выполнять C# код** из приложений (например, скрипты, плагины)[5](https://xn--90aia9aifhdb2cxbdg.xn--p1ai/blogs/roslyn-features-visual-studio).
    
- Улучшенная поддержка инструментов разработки: подсветка ошибок, предупреждений, подсказки, автоматические исправления[5](https://xn--90aia9aifhdb2cxbdg.xn--p1ai/blogs/roslyn-features-visual-studio).
    

## История и развитие

- Roslyn был анонсирован в 2010 году и стал частью Visual Studio начиная с 2013-2015 годов[1](https://ru.wikipedia.org/wiki/Roslyn)[5](https://xn--90aia9aifhdb2cxbdg.xn--p1ai/blogs/roslyn-features-visual-studio).
    
- В 2014 году Microsoft открыла исходники Roslyn и сделала проект открытым на GitHub[1](https://ru.wikipedia.org/wiki/Roslyn)[6](https://learn.microsoft.com/ru-ru/archive/msdn-magazine/2014/november/the-working-programmer-rise-of-roslyn).
    
- Roslyn используется не только в Visual Studio, но и в других IDE и инструментах, например, Xamarin Studio[1](https://ru.wikipedia.org/wiki/Roslyn).
    

**Итог:**  
Roslyn — это современная, открытая и расширяемая платформа компилятора C# и VB.NET, которая значительно расширяет возможности традиционного компилятора, предоставляя API для полного доступа к процессу компиляции, анализа и генерации кода. Это делает Roslyn ключевым инструментом для создания интеллектуальных средств разработки и динамического исполнения кода в экосистеме .NET[1](https://ru.wikipedia.org/wiki/Roslyn)[2](https://habr.com/ru/companies/veeam/articles/648775/)[3](https://itvdn.com/ru/blog/article/compiler-roslyn)[4](https://habr.com/ru/companies/pvs-studio/articles/301204/)[5](https://xn--90aia9aifhdb2cxbdg.xn--p1ai/blogs/roslyn-features-visual-studio).