# DBF files to CSV, EXCEL

**from stackoverflow.com**

Do you have Microsoft Access? The import wizard in that can import directly from DBF files into an Access table.

Alternatively, download vrunfox9.zip. You might need the Visual FoxPro 9 runtimes installed too.

Run it and you will get a simulated Visual FoxPro command window.

Type:

```foxpro
use ?
```
and press enter, then locate your DBF and open it. You can then type:

```foxpro
browse
```

to view it directly. You can export it to CSV:

copy to myfile.csv type csv
or XLS:

```foxpro
copy to myfile.xls type xl5
```
