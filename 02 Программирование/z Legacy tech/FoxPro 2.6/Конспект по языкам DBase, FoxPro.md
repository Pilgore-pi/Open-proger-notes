
Источник: Perplexity (AI)

В данном конспекте рассмотрены основные характеристики и особенности языка программирования dBase, который является одним из первых языков для работы с базами данных и сыграл значительную роль в развитии систем управления базами данных.

## Классификация языка dBase

dBase представляет собой семейство широко распространённых систем управления базами данных, а также язык программирования, используемый в этих системах. По своей классификации dBase относится к императивным и декларативным языкам программирования.

### Общие характеристики

- **Год появления**: 1979
- **Разработчик**: Cecil Wayne Ratliff
- **Расширение файлов**: .dbf
- **Последняя версия**: dBASE® 2019.1 (выпущена в 2019 году)
- **Платформы**: Microsoft DOS, Microsoft Windows


### Назначение языка

dBase изначально создавался как язык разработки приложений и интегрированная навигационная система управления базами данных. Компания Ashton-Tate называла его "реляционной" системой, хотя строго говоря, он не соответствовал критериям, определенным реляционной моделью доктора Эдгара Ф. Кодда.

### Архитектура и особенности исполнения

В dBase использовалась архитектура интерпретатора среды выполнения, позволявшая пользователям выполнять команды, вводя их непосредственно в командной строке "dot prompt". Программные скрипты (текстовые файлы с расширениями .PRG) запускались в интерпретаторе с помощью команды DO.

Одно из главных преимуществ dBase заключалось в простоте написания и тестирования программ - даже человек, не имеющий опыта программирования, мог разрабатывать приложения.

## Пример использования переменных

В dBase переменные активно используются при работе с данными, в том числе при формировании запросов к базе данных.

### Объявление и использование переменных в SQL-запросах

Переменные могут содержать как определенные значения для условий раздела WHERE, так и часть текста запроса или весь запрос целиком.

#### Способ 1: Использование значения переменной

```sql
SELECT * FROM TABLE WHERE ID = :VarID
```

В этом примере `:VarID` - это обращение к переменной с именем VarID. Если значение VarID равно 152, то запрос будет эквивалентен:

```sql
SELECT * FROM TABLE WHERE ID = 152
```


#### Способ 2: Использование переменной как текстовой подстановки

Переменная может хранить текст SQL-запроса или его часть и использоваться в качестве макроподстановки с синтаксисом `"%<VarName>%"`.

Например, если переменная VarID содержит текст "WHERE D.ID = 152", то запрос:

```sql
SELECT * FROM TABLE %VarID%
```

будет эквивалентен:

```sql
SELECT * FROM TABLE WHERE D.ID = 152
```


## Условные операторы и циклы

В dBase, как и в большинстве языков программирования, поддерживаются различные управляющие конструкции для организации логики программы.

### Циклы

Хотя в предоставленных результатах нет полной информации о циклах конкретно в dBase, можно предположить, что они имеют структуру, схожую с другими языками для работы с базами данных. Типичные конструкции циклов в подобных языках включают:

- Цикл `DO WHILE ... ENDDO`
- Цикл `FOR ... ENDFOR`
- Цикл `DO ... UNTIL`

Примеры использования циклов в dBase могут выглядеть следующим образом:

```basic
* Цикл FOR
FOR i = 1 TO 10
  ? "Значение i:", i
ENDFOR

* Цикл WHILE
nCount = 1
DO WHILE nCount <= 5
  ? "Итерация:", nCount
  nCount = nCount + 1
ENDDO
```


### Условные операторы

Типичные условные конструкции в dBase:

```basic
* Простое условие IF
IF nValue > 10
  ? "Значение больше 10"
ENDIF

* Условие IF-ELSE
IF lCondition
  ? "Условие истинно"
ELSE
  ? "Условие ложно"
ENDIF

* Множественное условие
DO CASE
  CASE nDay = 1
    ? "Понедельник"
  CASE nDay = 2
    ? "Вторник"
  OTHERWISE
    ? "Другой день"
ENDCASE
```


## Процедуры и функции

В dBase для работы с данными предоставлены многочисленные процедурные команды и функции.

### Функции для работы с данными

- **Функции навигации по записям**: USE (открытие файла), SKIP (перемещение между записями), GO TOP, GO BOTTOM, GO RECNO (переход к записи по номеру)
- **Функции управления значениями полей**: REPLACE (замена значения), STORE (сохранение значения)
- **Функции для работы со строками**: STR() (преобразование числа в строку), SUBSTR() (извлечение подстроки)
- **Функции для работы с числами и датами**: CHECK_DATE(), CHECKNMBR()


### Пример пользовательской процедуры

```basic
* Процедура для расчета суммы двух чисел
PROCEDURE AddNumbers
  PARAMETERS nNum1, nNum2
  nResult = nNum1 + nNum2
  ? "Результат сложения:", nResult
RETURN

* Вызов процедуры
AddNumbers(5, 10)
```


### Пример пользовательской функции

```basic
* Функция для вычисления среднего арифметического
FUNCTION CalcAverage
  PARAMETERS nNum1, nNum2, nNum3
  nAvg = (nNum1 + nNum2 + nNum3) / 3
RETURN nAvg

* Использование функции
nResult = CalcAverage(10, 20, 30)
? "Среднее значение:", nResult
```


## Специфические особенности языка

dBase имеет ряд особенностей, которые делают его уникальным языком программирования:

### Архитектурные особенности

1. Интерпретируемый язык с командной строкой "dot prompt", позволяющей выполнять команды напрямую
2. Программы хранятся в текстовых файлах с расширением .PRG и исполняются с помощью команды DO
3. Сильная ориентация на работу с базами данных и файлами формата .dbf

### Исторические особенности

1. dBase оказал огромное влияние на множество языков и систем, включая Clipper, FoxBASE+, FoxPro, Visual FoxPro, Harbour и другие
2. Изначально dBase был создан для CP/M, позже появились версии для Apple II, Apple Macintosh, UNIX, VMS и IBM PC под DOS
3. Формат данных dBase не был закрытым, что привело к появлению множества клонов и диалектов языка, собирательно именуемых xBase

### Практические особенности

1. Простота изучения и использования, доступность даже для людей без опыта программирования
2. Сочетание возможностей языка программирования и системы управления базами данных в одном продукте
3. Ограниченная портируемость под Microsoft Windows, что привело к появлению более сильных конкурентов в этой нише - Paradox, Clipper, FoxPro и Microsoft Access

## Заключение

dBase представляет собой важную веху в истории развития систем управления базами данных и языков программирования. Несмотря на то, что современные СУБД предлагают более широкий функционал и лучшую интеграцию с современными платформами, многие концепции, впервые реализованные в dBase, нашли свое отражение в современных языках и системах программирования для работы с базами данных.

Простота использования, интуитивно понятный синтаксис и мощные возможности для работы с данными сделали dBase популярным языком для разработки бизнес-приложений в 1980-х и начале 1990-х годов, а его влияние на последующие разработки в области баз данных трудно переоценить.

<div style="text-align: center">⁂</div>

[^1_1]: https://ru.wikipedia.org/wiki/DBase

[^1_2]: https://basegroup.ru/community/knowledge/variables-import-export-database

[^1_3]: https://plsql.ru/plsql/loops/

[^1_4]: https://reddatabase.ru/media/documentation/RedDatabase-3.0.15-FBJava_Guide-ru.pdf

[^1_5]: https://studbooks.net/2091095/informatika/dbase_naznachenie_raznovidnosti_dostoinstva_nedostatki_osobennosti_interfeys_programmy

[^1_6]: https://learn.microsoft.com/ru-ru/sql/mdx/comments-mdx-syntax?view=sql-server-ver16

[^1_7]: https://ru.ruwiki.ru/wiki/DBase

[^1_8]: https://www.tflexcad.ru/help/cad/16/functions_for_external_db.htm

[^1_9]: https://it.wikireading.ru/20062

[^1_10]: https://learn.microsoft.com/ru-ru/sql/t-sql/language-elements/comment-transact-sql?view=sql-server-ver16

[^1_11]: https://dbasedev.ru/arangodb/aql/fundamentals/syntax/

[^1_12]: https://ya.ru/neurum/c/nauka-i-obrazovanie/q/kakie_osnovnye_pravila_oformleniya_kommentariev_126da282

[^1_13]: https://www.php.net/manual/ru/regexp.reference.comments.php

[^1_14]: https://www.youtube.com/watch?v=Z0TPjKr4EQo

[^1_15]: https://postgrespro.ru/docs/postgresql/9.4/sql-comment

[^1_16]: https://citforum.ru/internet/php3/php3_15.shtml

[^1_17]: http://bd-subd.ru/oracle/dbase-ms-sql-oracle.htm

[^1_18]: https://www.garshin.ru/it/dbms/rdb/dbf.html

[^1_19]: https://dbasedev.ru/arangodb/aql/operations/collect/

[^1_20]: https://www.schoolsw3.com/python/python_while_loops.php

[^1_21]: https://www.opennet.ru/docs/RUS/php42/f/ref.dbase.html

[^1_22]: https://hist.bsu.by/images/stories/files/uch_materialy/do/muz/1/IT_Buzun-2/M2_L4_V3.pdf

[^1_23]: https://www.vsavm.by/knigi/kniga3/960.html

[^1_24]: https://www.php.net/manual/ru/function.dbase-numrecords.php

[^1_25]: https://ivan-shamaev.ru/t-sql-fundamentals-and-examples/

[^1_26]: https://www.kansoftware.ru/?did=21

[^1_27]: https://pro.arcgis.com/ru/pro-app/latest/tool-reference/conversion/an-overview-of-the-to-dbase-toolset.htm

[^1_28]: https://ru.stackoverflow.com/questions/312850/Правильный-синтаксис-при-работе-с-dbf-visual-fox-pro

[^1_29]: https://help.fsight.ru/ru/mergedProjects/uietl/desktop/03_etlobjects/03_outputs/dbase/uietl_outputs_dbase.htm

---

## Объяснение кода на FoxPro

Данный код представляет собой типичный стартовый скрипт для приложения FoxPro, который выполняет настройку среды разработки и запускает главное меню. Рассмотрим основные части:

### Настройка окружения

```sql
CLOSE ALL          && Закрытие всех открытых файлов
CLEAR ALL          && Очистка памяти от переменных
SET DELETE ON      && Скрытие записей, помеченных на удаление [^2_4][^2_8]
SET TALK OFF       && Отключение системных сообщений [^2_4][^2_7]
SET DATE TO GERMAN && Формат даты DD.MM.YYYY [^2_4]
SET EXACT OFF      && Нестрогое сравнение строк
SET EXCLUSIVE OFF  && Разрешение совместного доступа к таблицам [^2_4]
SET REPROCESS TO AUTOMATIC  && Автоматическая обработка блокировок [^2_7]
```


### Инициализация переменных

```
m.add = .F.       && Флаг добавления записи
m.edit = .F.      && Флаг редактирования записи
store .f. to _exit  && Флаг выхода из приложения
```


### Настройка путей

```sql
SET PATH TO \u10\prgs;\screens;\reports;\menus;\dbfs  
&& Поиск файлов в указанных каталогах [^2_1][^2_4]
```


### Главный цикл приложения

```basic
DO WHILE ! _exit
    DO u10.mpr       && Запуск скомпилированного меню [^2_1]
    READ VALID myhandler()  && Ожидание ввода с обработчиком [^2_4]
ENDDO
```


### Обработчик событий

```1C
FUNCTION myhandler
IF LASTKEY() = 45  && Проверка нажатия Alt+X (ASCII 45) [^2_4][^2_7]
    _exit = .T.
ENDIF
RETURN _exit
```


### Завершение работы

```sql
SET SYSMENU TO DEFA  && Восстановление системного меню
MODIFY PROJECT u10.PJX NOWA  && Открытие проекта
```

**Особенности реализации:**

1. Используется паттерн "бесконечный цикл" с флагом выхода
2. Меню реализовано через скомпилированный MPR-файл
3. Обработка клавиатурных сочетаний через LASTKEY()
4. Настройка многопользовательского режима через SET EXCLUSIVE OFF
5. Автоматическое восстановление блокировок через SET REPROCESS

**Рекомендации по модификации:**

- Добавить обработку ошибок с помощью ON ERROR
- Реализовать логирование действий
- Использовать более современные методы обработки событий
- Добавить проверку существования файлов меню и проекта

#Legacy #FoxPro
<div style="text-align: center">⁂</div>

[^2_1]: https://foxdos.narod.ru/Prog_1.htm

[^2_2]: https://foxclub.ru/knowledgebase/osnovy-yazyka-visual-foxpro/

[^2_3]: http://dit.isuct.ru/IVT/BOOKS/DBMS/DBMS16/subd/foxpro7/glava19.html

[^2_4]: https://kpfu.ru/docs/F619977767/kosterina_rabotavfo_pro_51.pdf

[^2_5]: https://kpfu.ru/sistema-upravleniya-bazami-dannyh-visual-foxpro_383849.html

[^2_6]: https://ru.wikipedia.org/wiki/FoxPro

[^2_7]: https://help.foxclub.ru/html/6dcddc3f-9944-4ad8-be2f-003610af616a.htm

[^2_8]: https://ru.wikipedia.org/wiki/Visual_FoxPro

