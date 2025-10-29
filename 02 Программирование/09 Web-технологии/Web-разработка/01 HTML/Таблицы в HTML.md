[Источник](https://developer.mozilla.org/ru/docs/Learn/CSS/Building_blocks/Styling_tables)

`table` — таблица (`border="1"` -- ширина границ таблицы)
`caption` — название таблицы
`thead` — заголовок таблицы (\~наименования столбов)
`td` — table data (1 столбец), наименьшая единица таблицы
`tr` — table row, строка, хранящая ячейки `td` или `th`
`th` — table header
`tfoot` — 
`tbody` — 

Атрибуты таблицы (`table`):

* `width`
* `background`
* `color` — цвет текста
* `border-spacing` — расстояние между ячейками
* `padding` — отступы вокруг текста

Атрибуты `td (table data)`:

* width — ширина в пикселях
* `colspan & rowspan` — определяют количество занимаемых строк и столбцов

Атрибуты `th (table header)`:

* `scope` — 
* `colspan` — 

Ширина таблицы наследуется от обертывающего ее тега.

```html
<table>
  <caption>
    A summary of the UK's most famous punk bands
  </caption>
  <thead>
    <tr>
      <th scope="col">Band</th>
      <th scope="col">Year formed</th>
      <th scope="col">No. of Albums</th>
      <th scope="col">Most famous song</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Buzzcocks</th>
      <td>1976</td>
      <td>9</td>
      <td>Ever fallen in love (with someone you shouldn't have)</td>
    </tr>
    <tr>
      <th scope="row">The Clash</th>
      <td>1976</td>
      <td>6</td>
      <td>London Calling</td>
    </tr>

    ... some rows removed for brevity

    <tr>
      <th scope="row">The Stranglers</th>
      <td>1974</td>
      <td>17</td>
      <td>No More Heroes</td>
    </tr>
  </tbody>
  <tfoot>
    <tr>
      <th scope="row" colspan="2">Total albums</th>
      <td colspan="2">77</td>
    </tr>
  </tfoot>
</table>
```

Результат:

<table>
  <caption>
    A summary of the UK's most famous punk bands
  </caption>
  <thead>
    <tr>
      <th scope="col">Band</th>
      <th scope="col">Year formed</th>
      <th scope="col">No. of Albums</th>
      <th scope="col">Most famous song</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Buzzcocks</th>
      <td>1976</td>
      <td>9</td>
      <td>Ever fallen in love (with someone you shouldn't've)</td>
    </tr>
    <tr>
      <th scope="row">The Clash</th>
      <td>1976</td>
      <td>6</td>
      <td>London Calling</td>
    </tr>
    ... some rows removed for brevity
    <tr>
      <th scope="row">The Stranglers</th>
      <td>1974</td>
      <td>17</td>
      <td>No More Heroes</td>
    </tr>
  </tbody>
  <tfoot>
    <tr>
      <th scope="row" colspan="2">Total albums</th>
      <td colspan="2">77</td>
    </tr>
  </tfoot>
</table>

Истчники: https://htmlacademy.ru/blog/html/table-in-html

#Web #HTML