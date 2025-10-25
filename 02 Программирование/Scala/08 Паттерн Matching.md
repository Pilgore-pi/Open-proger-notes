Назад: [[07 Ленивые вычисления]]

```scala
x match {
	case <sample1> => <expr1>
	case <sample2> => <expr2>
}
```

```scala
val x = {1 + 2} match {
	case 3 => println("matches 3")
	case _ => println("doesn't match 3")
}

x // matches 3
```

`match` проверяет, соответствует ли выражение ***`x`*** какому-либо образцу.
`match` возвращает то выражение, образец которого первым подошел.

## Типы образцов

1) Константа  (**`case 1`**)
2) Подстановка (**`case _`**)
3) Переменная (**`case z`**) — совпадает всегда и принимает значение входного выражения (которое может использоваться в `<exprN>`)
4) Проверка типа (**`case num: Int`**) — срабатывает, если часть входного выражения имеет тип Int (для кортежей например)
5) Кортеж (**`case (2, z)`**) — выполняется, если количество элементов кортежа образца совпадает с кол-вом эл-ов входного выражения и выполняются все вложенные образцы
6) Защита образца (**`case (x, y) if x > 0 && y > 0`**)
7) Именование образцов (**`case identifier@(x, y)`**)
8) Список (**`case 4 :: tail`**)
9) Конструктор класса (**`case Point(0, y)`**) — проверяет тип, затем вложенные образцы

Полезные приёмы и замечания:
- Порядок `case` имеет значение
- Если трейты или классы помечены `sealed`, компилятор может проверить покрытие всех случаев
- Проверка типа `case x: List[Int]` может быть неточной из-за "стирания типа" — компилятор предупредит о небезопасном касте

Примеры:

Match на `Option`:

```scala
val maybe: Option[Int] = Some(5)

maybe match {
  case Some(v) => println(s"got $v")
  case None => println("empty")
}
```

Match на списки и varargs:

```scala
List(1,2,3) match {
  case Nil => println("empty")
  case head :: tail => println(s"head=$head tail=$tail")
  case first :: second :: rest => println("two or more")
}
```

Использование именования образцов (`@`) и `guard`:

```scala
val pair = (3, -1)
pair match {
  case x @ (a, b) if a > 0 && b > 0 => println(s"$x both positive")
  case (a, b) if a > 0 => println("first positive")
  case _ => println("other")
}
```

Экстракторы (`unapply`):
- Классы и объекты могут определять `unapply` для пользовательских образцов.
- case class автоматически содержит `unapply`.

Пример объекта-экстрактора:

```scala
object Even {
  def unapply(n: Int): Option[Int] =
    if (n % 2 == 0) Some(n) else None
}

42 match {
  case Even(n) => println(s"$n is even")
  case _ => println("odd")
}
```

Далее: [[02 Программирование/Scala/09 Функции]]

#Scala