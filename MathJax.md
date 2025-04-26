>==Last file update 10.04.24==

Формулы начинаются и заканчиваются на знак: `$` или `$$`
[Полный перечень](https://math.meta.stackexchange.com/questions/5020/mathjax-basic-tutorial-and-quick-reference)

[Определение команды MathJax для нарисованного символа](http://detexify.kirelabs.org/classify.html)

| Пример                                | Синтаксис                                        |
| ------------------------------------- | ------------------------------------------------ |
| $a, b,\ c$                            | `$a, b,\ c$`, принудительное отображение пробела |
| $a\quad b$                            | `$a \quad b$` (tab)                              |
| $\text{my text}$                      | `$\text{my text}$`                               |
| $\forall$                             | `$\forall$`                                      |
| $\exists$                             | `$\exists$`                                      |
| $\nexists$                            | `$\nexists$`                                     |
| $\varnothing$                         | `$\varnothing$`                                  |
| $\emptyset$                           | `$\emptyset$`                                    |
| $\approx$                             | `$\approx$` приблизительное равенство            |
| $\sim$                                | `$\sim$` пропорциональная зависимость            |
| $\equiv$                              | `$\equiv$` эквивалентность                       |
| $\le$                                 | `$\le$`                                          |
| $\ge$                                 | `$\ge$`                                          |
| $\in$                                 | `$\in$`                                          |
| $\notin$                              | `$\notin$`                                       |
| $\cup$                                | `$\cup$`                                         |
| $\cap$                                | `$\cap$`                                         |
| $\subset$                             | `$\subset$`                                      |
| $\subseteq$                           | `$\subseteq$`                                    |
| $\subsetneq$                          | `$\subsetneq$`                                   |
| $\supset$                             | `$\supset$`                                      |
| $X_{11}$                              | `$X_{11}$` индекс                                |
| $X^{22}$                              | `$X^{22}$ ` степень                              |
| $\bigcup\limits_{i=1}^{\infty} F_{i}$ | `$\bigcup\limits_{i=1}^{\infty} F_{i}$`          |
| $\bigcap\limits_{i=1}^{\infty} F_{i}$ | `$\bigcap\limits_{i=1}^{\infty} F_{i}$`          |
| $\land$                               | `$\land$`                                        |
| $\lor$                                | `$\lor$`                                         |
| $\lnot$                               | `$\lnot$`                                        |
| $\vdash$                              | `$\vdash$`                                       |
| $\to$                                 | `$\to$` (rightarrow)                             |
| $\gets$                               | `$\gets$` (leftarrow)                            |
| $\Rightarrow$                         | `$\Rightarrow$`                                  |
| $\Leftarrow$                          | `$\Leftarrow$`                                   |
| $\dots$                               | `$\dots$`                                        |
| $\times$                              | `$\times$`                                       |
| $\lim_{x\to0}$                        | `$\lim (lim_{x\to 0})$`                          |
| $f\prime (x)$                         | `$f\prime (x)$`                                  |
| $\partial x$                          | `$\partial x$`                                   |
| $\int$                                | `$\int$`                                         |
| $\oint$                               | `$\oint$`                                        |
| $\sum$                                | `$\sum$`                                         |
| $1\over2$                             | `$1 \over 2$`                                    |
| $\frac{111}{222}$                     | `$\frac{111}{222}$`                              |
| $\pm$                                 | `$\pm$`                                          |
| $\cdot$                               | `$\cdot$`                                        |
| $\bullet$                             | `$\bullet$`                                      |
| $C^{\circ}$                           | `$C^{\circ}$`                                    |
| $\overline{xyz}$                      | `$\overline{xyz}$`                               |
| $\hat x,\ \hat{xyz}$                  | `$\hat x,\ \hat{xyz}$`                           |
| $\widehat x,\ \widehat{xyz}$          | `$\widehat x,\ \widehat{xyz}$`                   |
| $\vec{my\_vector}$                    | `$\vec{my\_vector}$`                             |
| $\overrightarrow {my\_vector}$        | `$\overrightarrow {my\_vector}$`                 |
| $\sqrt{1024}$                         | `$\sqrt{1024}$`                                  |
| `$$formula$$`                         | центрированная формула                           |

$$\sum_{i=1}^{10}c_i$$ — `$$\sum_{i=1}^{10}c_i$$` (для лучшего отображения нужно центрировать формулу)
$$\int_a^bf(x)dx$$
— `$$\int_a^bf(x)dx$$`


$\mathbb R, \mathbb C, \mathbb D, \mathbb A$ — `$\mathbb R$`

$$\sigma = \sqrt{ \frac{1}{N} \sum_{i=1}^N (x_i -\mu)^2}$$
$$x = {-b \pm \sqrt{b^2-4ac} \over 2a + 0}$$
— `$$x = {-b \pm \sqrt{b^2-4ac} \over 2a + 0}$$`

Скобки на несколько строк:

$$f(n) =
\begin{cases}
n/2,  & \text{if $n$ is even} \\
3n+1, & \text{if $n$ is odd}
\end{cases}$$
```mathjax
f(n) =
\begin{cases}
n/2,  & \text{if $n$ is even} \\
3n+1, & \text{if $n$ is odd}
\end{cases}
```

### Греческий алфавит
![[Greek_alphabet.png]]

| Символ | Синтаксис |
| ------ | --------- |
|$\Psi$ | `$\Psi$`
|$\psi$ | `$\psi$`
|$\alpha$ | `$\alpha$`
|$\omega$ | `$\omega$`
|$\Omega$ | `$\Omega$`
|$\epsilon$ | `$\epsilon$`
|$\varepsilon$ | `$\varepsilon$`
|$\phi$ | `$\phi$`
|$\varphi$ | `$\varphi$`

>Любой символ юникода: $\unicode{x21af}$ — `$\unicode{x21af}$`

## Наложение символов

второй символ накладывается на первый
$\rlap{\,\times}\bigcirc$ — `$\rlap{\,\times}\bigcirc$`, где `\,` — 1 отступ слева

$\rlap{\,\,\,\times}\bigcirc$ — `$\rlap{\,\,\,\times}\bigcirc$` — 3 отступа

$\mathbin{\rlap{\,\times}\bigcirc}$ — иногда можно оборачивать в дополнительный оператор mathbin, для корректного отображения.

## Матрицы
$$
\begin{matrix}
1 & x & x^2 \\
1 & y & y^2 \\
1 & z & z^2 \\
\end{matrix}
$$
```
$$
\begin{matrix}
1 & x & x^2 \\
1 & y & y^2 \\
1 & z & z^2 \\
\end{matrix}
$$
```

1) pmatrix — матрица в скобках $(\quad)$
2) bmatrix — матрица в скобках $[\quad]$
3) Bmatrix — матрица в скобках $\{\quad\}$
4) vmatrix — матрица в скобках $|\quad|$
5) Vmatrix — матрица в скобках $||\quad||$

**Многоточия в матрицах**:

$\cdots$ — `$\cdots$`
$\ddots$ — `$\ddots$`
$\vdots$ — `$\vdots$`

#Latex #Mathjax