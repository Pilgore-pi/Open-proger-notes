```scala
try{
	//code
} catch {
	case e: Exception => "message"
} finally {
	//code
}
```

`try/catch/finally` как выражение, и Try-монода:

- `try/catch/finally` в Scala — выражение. Оно возвращает значение (тип блока `try`, `catch`'ей должен быть совместим).
- Блок `finally` выполняется в любом случае, но значение, возвращаемое из `finally`, игнорируется; не полагайся на возвращаемое из `finally`.
- Лучше использовать `scala.util.Try` для функционального обращения с исключениями (map/flatMap).

Примеры:

```scala
import scala.util.Try

def parseInt(s: String): Int = s.toInt

val safe: Try[Int] = Try(parseInt("123"))
val failed: Try[Int] = Try(parseInt("abc"))

safe.map(_ * 2)       // Success(246)
failed.recover { case _: NumberFormatException => 0 } // Success(0)
```

Пример try/catch как выражение:

```scala
val res: String = try {
    val n = "123".toInt
    s"number $n"
} catch {
    case e: NumberFormatException => "not a number"
} finally {
    println("done")
}
```

Замечания:

- Не стоит использовать `catch { case _: Throwable => ... }` без разбора — лучше обработать конкретные исключения
- Для ресурс-менеджмента лучше использовать `try-with-resources`-подобные абстракции (например, `Using` в Scala):

```scala
import scala.util.Using
import java.io._

Using.resource(new BufferedReader(new FileReader("file.txt"))) { br =>
    br.readLine()
}
```

#Scala