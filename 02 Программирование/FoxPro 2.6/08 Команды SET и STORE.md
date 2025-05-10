## Команда `STORE`

Инициализирует одну или несколько переменных заданным значением

```foxpro
STORE <значение> TO <переменные>

store '' to str1, str2, str3, str4
store 2 to vari
store DATE()  TO gdDate
store 50      TO gnNumeric
store 'Hello' TO gcCharacter
store .T.     TO glLogical
store $19.99  TO gyCurrency

declare myArray(2,2)
store 10 to myArray  && все элементы = 10
```

## Команда `SET`

Команда `SET` задает значения глобальных параметров FoxPro.

```foxpro
SET <ПАРАМЕТР> ON|OFF
SET <ПАРАМЕТР> TO <значение>

set autosave on
set border to single
set clear off
set cursor on
set deleted off
set device to screen
```

Команда без параметров открывает окно `View`

```foxpro
SET
```

### Функция `SET`

Возвращает значение параметра, установленного через команду SET. Не все `SET ...` команды поддерживают функцию `SET()`, но таких команд мало

```foxpro
SET(<параметр>[, 1])
```

#FoxPro
