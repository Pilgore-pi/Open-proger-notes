
Установка модуля:
```
pip install moderngl
```

если `moderngl_window` не найден:

```
pip install moderngl_window
```

![[Shaders_схема.png]]

Это Python библиотека, которая позволяет компилировать файлы glsl. Как минимум должно быть 2 файла:
1) вершинный шейдер
2) фрагментный шейдер

Пример построения кардиоиды.

Параметрические уравнения кардиоиды:

$$
\begin{cases}
x = 2r\ cos(t) - r\ cos(2t) \\
y = 2r\ sin(t) - r\ sin(2t)
\end{cases}
$$
Вертексный шейдер (vertex_shader.glsl):
```cs
#version 430 // версия GLSL (совпадает с в-ей OpenGL)

// координаты вершин экранной плоскости
// (передаются из буфера вершин)
in vec3 in_position;

void main(){
	// предопределенная переменная
	gl_Position = vec4(in_position, 1);
}
```

Вершинный шейдер применяется к каждой вершине по отдельности.

![[Py_Shaders.png]]

Фрагментный шейдер (fragment_shader.glsl):
```cs
#version 430

// итоговый цвет пикселя на экране
out vec4 fragColor;

uniform vec2 resolution;
uniform float time;

vec2 rotate2D(vec2 uv, float a) {
	float s = sin(a);
	float c = cos(a);
	return mat2(c, -s, s, c) * uv;
}

// свой ГСЧ (в OpenGL его нет)
vec2 hash12(float t) {
	float x = fract(sin(t * 3453.329));
	float y = fract(sin((t + x) * 8532.732));
	return vec2(x, y);
}

void main() {
	// нормализованный координатный вектор с центром по
	// середине экрана
	vec2 uv = (gl_FragCoord.xy - 0.5 * resolution.xy) /
	resolution.y;
	vec3 col = vec3(0.0);

	uv = rotate2D(uv, 3.14 / 2.0);

	float r = 0.17;
	for (float i=0.0; i < 60.0; i++) {
		float factor = (sin(time) * 0.5 + 0.5) + 0.3;
		i += factor;

		float a = i / 3;
		float dx = 2 * r * cos(a) - r * cos(2 * a);
		float dy = 2 * r * sin(a) - r * sin(2 * a);

		col += 0.013 * factor /
		length(uv-vec2(dx + 0.1, dy) - 0.02 * hash12(i));
	}
	col *= sin(vec3(0.2, 0.8, 0.9) * time) * 0.15 + 0.25;

	fragColor = vec4(col, 1.0);
}
```

>Единственная задача фрагментного шейдера — вычислить цвет пикселя.

Python программа:
```python
import moderngl_window as mglw

class App(mglw.WindowConfig): # наследование от WindowConfig
	window_size = 1600, 900
	resource_dir = 'shaders'
	vsync = False

	def __init__(self, **kwargs):
		super().__init__(**kwargs)
		# создание плоскости с 4 вершинами, которая будет
		# выступать в роли экрана
		self.quad = mglw.geometry.quad_fs()
		# шейдерная программа
		self.prog = self.load_program(
								vertex_shader='vertex_shader.glsl',
								fragment_shader='fragment_shader.glsl')
		self.set_uniform('resolution', self.window_size)

	def set_uniform(self, u_name, u_value):
		try:
			self.prog[u_name] = u_value
		except KeyError:
			print(f'uniform: {u_name} - not used in shader')

	# происходит каждый кадр, очищается буфер кадра
	# и перерисовывается
	def render(self, time, frame_time):
		self.ctx.clear()
		self.set_uniform('time', time)
		self.quad.render(self.prog)


if __name__ == '__main__':
	mglw.run_window_config(App)
```

Вызов `super()` автоматически создаст:
* self.ctx — контекст OpenGL
* self.wnd — экземпляр окна
* self.timer — таймер

## Класс WindowConfig

Представляет собой интерфейс настраиваемого окна для отображения графики.

* Некоторые свойства и методы определены в BaseWindow — базовой версии окна.

"Статические" свойства:
* window_size — 2 числа  
* resource_dir — каталог с ресурсами, в котором должны находиться файлы шейдеров
* vsync = True

Методы:
* load_texture_2d()
* load_texture_cube()
* load_program() — loads a shader program
* render() — рендер кадра (вызывается каждый кадр)
* resize() — должно быть вызвано при изменении размеров окна

События (обработчики):
* close() — при закрытии окна
* key_event()
* mouse_position_event()
* mouse_drag_event()
* mouse_press_event()
* mouse_release_event()
* mouse_scroll_event()

## Библиотека moderngl_window

Методы:
* run_window_config(WindowConfig) запускает окно (реализующее интерфейс WindowConfig)

#Shaders #Python