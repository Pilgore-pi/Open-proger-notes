## Открытие, создание и закрытие Excel документа

Создание COM-объекта:

```bsl
Попытка
    Excel = Новый COMОбъект("Excel.Application"); 
Исключение
    Сообщить(ОписаниеОшибки() + "Возможно программа Exсel не установлена на данном компьютере!");
    Возврат Ложь;
КонецПопытки;
```

```bsl
//Создание книги
Книга = Excel.WorkBooks.Add();

//Открытие существующей книги  
Книга = Excel.WorkBooks.Open(ПутьКФайлу);

//Выбор рабочего листа по номеру 
sheet = WorkBook.WorkSheets(НомерЛиста);

//Выбор рабочего листа по имени 
sheet = WorkBook.WorkSheets(ИмяЛиста);   

//Сохранение книги
Попытка
	Книга.SaveAs(ПутьКФайлу);
	Книга.Close();
	Excel.Quit();	
Исключение
	Книга.Close();
	Excel.Quit();
КонецПопытки;
```

## Редактирование Excel документа

```bsl
ОбщегоНазначения.СообщитьПользователю(
    "Количество листов: " + Книга.Sheets.Count
);

currentSheet = WorkBook.WorkSheets(0);

currentSheet.Cells(i, j).Value = "2";
```

Выделение диапазона колонок:

```bsl
Excel.Columns("A:H").Select();
```

Установка ширины колонок:

```bsl
sheet.Columns(НомерКолонки).ColumnWidth = Ширина;
```

Настройка шрифта для всех ячеек:

```bsl
//Размер шрифта
sheet.Cells.Font.Size = 12;

//Тип шрифта                        
sheet.Cells.Font.Name = "Calibri";

//1 — жирный шрифт, 0 — обычный.
sheet.Cells.Font.Bold = 1;

//1 — наклонный шрифт, 0 — обычный.	
sheet.Cells.Font.Italic = 1;

//2 — подчеркнутый, 1 — нет.	
sheet.Cells.Font.Underline = 1;
```

```bsl
// Отключение/включение режима показа предупреждений

ExcelApp.DisplayAlerts = False; // отключение
ExcelApp.DisplayAlerts = True;  // включение

// Установка свойства ячейки "переносить по словам"

ТекущийЛист.Cells(i, j).WrapText = True;
```

Горизонтальное выравнивание ячеек в листе:

```bsl
sheet.Cells(i, j).HorizontalAlignment = -4130; // По центру

sheet.Cells(i, j).VerticalAlignment = -4130;
```

Значения выравнивания по ширине:

| Режим выравнивания  | Константа в Excel               | Значение в ISBL |
| ------------------- | ------------------------------- | --------------- |
| По центру           | `xlHAlignCenter`                | `-4108`         |
| По ширине           | `xlHAlignJustify`               | `-4130`         |
| По левому краю      | `xlHAlignLeft`                  | `-4131`         |
| По правому краю     | `xlHAlignRight`                 | `-4152`         |
| По центру выделения | `xlHAlignCenterAcrossSelection` | `7`             |
| Распределенное      | `xlHAlignDistributed`           | `-4117`         |
| С заполнением       | `xlHAlignFill`                  | `5`             |
| По значению         | `xlHAlignGeneral`               | `1`             |

Значения выравнивания по высоте:

| Режим выравнивания | Константа в Excel     | Значение в ISBL |
| ------------------ | --------------------- | --------------- |
| По нижнему краю    | `xlVAlignBottom`      | `-4107`         |
| По центру          | `xlVAlignCenter`      | `-4108`         |
| Распределенное     | `xlVAlignDistributed` | `-4117`         |
| По высоте          | `xlVAlignJustify`     | `-4130`         |
| По верхнему краю   | `xlVAlignTop`         | `-4160`         |

Формулы:

```bsl
// Назначение формулы в английском синтаксисе
sheet.Cells(i, j).Formula = "SUM(A1:A10)";

// Назначение формулы в русском синтаксисе
sheet.Cells(i, j).FormulaLocal = "Сумм(A1:A10)";
```

#1С #API #COM #MS_Office/Excel #1С/Интеграция