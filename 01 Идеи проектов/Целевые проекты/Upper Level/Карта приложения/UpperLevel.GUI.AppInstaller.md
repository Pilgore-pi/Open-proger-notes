
Файловая структура итоговой сборки установщика

```text
setup.exe
data.dat
```

`setup.exe` определяет используемые модули и целевые платформы (все, Windows, Linux, MacOS)
`data.dat` содержит:

- Все скомпилированные модули в виде отдельных динамических библиотек
- `UpperLevel.exe` -- один исполняемый файл, выполняющий роль точки входа в приложение
- Ресурсы приложения (файлы словарей и др.)

```text
resources <каталог>
UpperLevel.exe
activities.dll
os.dll
text.dll
net.dll
net.communiction.dll
...
```

