Полная документация по [CanvasRenderingContext2D](https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D#gradients_and_patterns)

Координатная система обычная. Ось Y растет вниз и начинается с левого верхнего угла.

```html
<canvas id="mainCanvas"></canvas>
```

```js
// Получение холста
canvas = document.getElementById("mainCanvas");

// Получение контекста (CanvasRenderingContext2D)
context = mainCanvas.getContext('2D');
```

## Свойства CanvasRenderingContext2D

* lineWidth (default 1.0)
* lineCap — тип концов линии (butt (default), round, square)
* lineJoin — Defines the type of corners where two lines meet (значения: round, bevel, mitter(default))

__
* font — стиль текста (по умолчанию: `"10px sans-serif"`)
* textAlign — Possible values: `start` (default), `end`, `left`, `right`, `center`
* letterSpacing — by default: `0px`
* wordSpacing
* fontStretch — Possible values: `ultra-condensed`, `extra-condensed`, `condensed`, `semi-condensed`, `normal` (default), `semi-expanded`, `expanded`, `extra-expanded`, `ultra-expanded`
* textRendering — Possible values: `auto` (default), `optimizeSpeed`, `optimizeLegibility`, `geometricPrecision`

__
* fillStyle — стиль для заполнения фигуры. Стиль: строка, представляющее собой стиль CSS
* strokeStyle — стиль обводки



## Методы

Создание и завершение пути:

* `beginPath()` — начало пути
* `closePath()` — окончание пути
* `moveTo(x, y)` — переход в точку
* `lineTo(x, y)` — линия до указанной точки
* `bezierCurveTo()` — кубическая кривая безье
* `quadraticCurveTo()` — квадратичная кривая безье
* `arc()` — дуга
* `arcTo()`
* `ellipse()` — эллиптическая дуга
* `rect(x, y, width, height)` — задает прямоугольник. (x, y) — левый верхний угол
* `roundRect(x, y, width, height, radii)` — прямоугольник с настраиваемыми углами. `radii` — массив чисел, который указывает радиусы для каждого угла. Формат записи при положительных ширине и высоте прямоугольника:
	- `all-corners`
	- `[all-corners]`
	- `[top-left-and-bottom-right, top-right-and-bottom-left]`
	- `[top-left, top-right-and-bottom-left, bottom-right]`
	- `[top-left, top-right, bottom-right, bottom-left]`

Рисование путей:

* `fill([path2D, rule])` — Fills the current sub-paths with the current fill style. Rules: `"nonzero"` by default & `"evenodd"`
* `stroke()` — Заполняет текущие подпути стилем `stroke`
* `clearRect(x, y, width, height)` — 
* `fillRect(x, y, width, height)` — 
* `strokeRect(x, y, width, height)` — 

#Web #JavaScript