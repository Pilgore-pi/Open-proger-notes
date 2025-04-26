Напиши простое приложение в Android Studio на языке Kotlin. Приложение представлять собой простой калькулятор, оно должно содержать 2 поля для ввода чисел, поле с результатом вычислений, несколько кнопок с различными операциями (сложение, вычитание, умножение, деление). Нужно, чтобы использовалось 2 элемента Activity

---

Чтобы создать простое приложение-калькулятор на Kotlin в Android Studio, выполните следующие шаги. Предполагается, что вы уже знакомы с Android Studio и имеете установленный и настроенный Kotlin плагин.

1. Создайте новый проект в Android Studio.

2. Определите два `Activity`: `MainActivity` для ввода чисел и выбора операции, а `ResultActivity` для отображения результата.

3. В `activity_main.xml` добавьте элементы интерфейса:
```xml
<LinearLayout
    xmlns:android="http://schemas.android.com/apk/res/android"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:orientation="vertical"
    android:padding="16dp">

    <EditText
        android:id="@+id/number1EditText"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:inputType="numberDecimal"
        android:hint="Enter number 1"/>

    <EditText
        android:id="@+id/number2EditText"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:inputType="numberDecimal"
        android:hint="Enter number 2"/>

    <LinearLayout
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:orientation="horizontal"
        android:gravity="center">

        <Button
            android:id="@+id/addButton"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="+"/>

        <Button
            android:id="@+id/subtractButton"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="-"/>

        <Button
            android:id="@+id/multiplyButton"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="*"/>

        <Button
            android:id="@+id/divideButton"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="/"/>

    </LinearLayout>
</LinearLayout>
```

4. Создайте `activity_result.xml` для отображения результата:
```xml
<LinearLayout
    xmlns:android="http://schemas.android.com/apk/res/android"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:orientation="vertical"
    android:gravity="center"
    android:padding="16dp">

    <TextView
        android:id="@+id/resultTextView"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:textSize="24sp"
        android:text="Result"/>

</LinearLayout>
```

5. В `MainActivity.kt` добавьте логику калькулятора:

```kotlin
import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.EditText
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {

    private lateinit var number1EditText: EditText
    private lateinit var number2EditText: EditText

    enum class Operation {
        ADD, SUBTRACT, MULTIPLY, DIVIDE
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        number1EditText = findViewById(R.id.number1EditText)
        number2EditText = findViewById(R.id.number2EditText)

        val addButton: View = findViewById(R.id.addButton)
        val subtractButton: View = findViewById(R.id.subtractButton)
        val multiplyButton: View = findViewById(R.id.multiplyButton)
        val divideButton: View = findViewById(R.id.divideButton)

        addButton.setOnClickListener { calculateAndNavigate(Operation.ADD) }
        subtractButton.setOnClickListener { calculateAndNavigate(Operation.SUBTRACT) }
        multiplyButton.setOnClickListener { calculateAndNavigate(Operation.MULTIPLY) }
        divideButton.setOnClickListener { calculateAndNavigate(Operation.DIVIDE) }
    }

    private fun calculateAndNavigate(operation: Operation) {
        val number1 = number1EditText.text.toString().toDoubleOrNull()
        val number2 = number2EditText.text.toString().toDoubleOrNull()

        if (number1 == null || number2 == null) {
            // Show error or request valid numbers
            return
        }

        val result = when (operation) {
            Operation.ADD -> number1 + number2
            Operation.SUBTRACT -> number1 - number2
            Operation.MULTIPLY -> number1 * number2
            Operation.DIVIDE -> if (number2 != 0.0) number1 / number2 else Double.NaN
        }

        val intent = Intent(this, ResultActivity::class.java).apply {
            putExtra("result", result)
        }
        startActivity(intent)
    }
}
```

6. Создайте `ResultActivity.kt` для отображения результата:
```kotlin
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class ResultActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_result)

        val resultTextView: TextView = findViewById(R.id.resultTextView)
        val result = intent.getDoubleExtra("result", 0.0)
        resultTextView.text = result.toString()
    }
}
```

7. Не забудьте добавить `ResultActivity` в `AndroidManifest.xml`:
```xml
<activity android:name=".ResultActivity"/>
```

8. Запустите ваше приложение.

Обратите внимание, что это очень простой пример, и для полноценного приложения понадобится обработка ошибок, валидация ввода и улучшенный пользовательский интерфейс.

---

Обязательно ли создавать `ResultActivity.kt` или можно обойтись XML фалом?

---

В Android приложениях `Activity` является основным компонентом, который представляет собой один экран с пользовательским интерфейсом. Каждый экран в вашем приложении, как правило, связан с определённым классом `Activity`, который управляет жизненным циклом представления и взаимодействует с пользователем.

XML-файлы в Android используются для определения макета пользовательского интерфейса (layout), но они не могут обрабатывать действия пользователя или другую бизнес-логику. Для этого необходим класс `Activity`, который ссылается на эти XML-файлы через метод `setContentView(R.layout.your_layout_xml)`, после чего он может взаимодействовать с элементами макета, управлять их состоянием и реагировать на события пользовательского интерфейса.

В вашем случае для калькулятора вам нужен класс `ResultActivity.kt` для следующих целей:

1. Получение результата вычисления, переданного из `MainActivity` через `Intent`.
2. Обновление текста в `TextView`, чтобы отобразить полученный результат.

Если вы решите не использовать второе `Activity` для отображения результата, вы можете просто обновить `TextView` в `MainActivity`, чтобы показать результат там же, где пользователь вводит числа и выбирает операцию. В таком случае вам не потребуется `ResultActivity` и его XML-файл, но нужно будет добавить `TextView` в `activity_main.xml` и обновить его содержимое непосредственно из `MainActivity`.

---

Как добавить окно, где будет сохраняться история результатов вычислений? Как обработать логику в коде, как добавлять новые элементы интерфейса?

---

Чтобы добавить окно с историей результатов вычислений в ваше приложение-калькулятор на Android, вы можете использовать фрагмент (`Fragment`) или новое `Activity`. Ниже представлены шаги для добавления нового `Activity` с историей результатов.

1. Создайте новый класс `Activity` с именем `HistoryActivity` и связанный с ним XML-файл макета `activity_history.xml`. В этом XML-файле используйте `RecyclerView` для отображения списка результатов. В `activity_history.xml` добавьте следующее:

```xml
<androidx.recyclerview.widget.RecyclerView
    xmlns:android="http://schemas.android.com/apk/res/android"
    android:id="@+id/historyRecyclerView"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:padding="16dp"/>
```

2. Определите объект `data class` для хранения информации о каждой операции:

```kotlin
data class CalculationHistoryItem(val expression: String, val result: String)
```

3. Создайте адаптер для `RecyclerView`, который будет использовать `CalculationHistoryItem`:

```kotlin
class HistoryAdapter(private val items: List<CalculationHistoryItem>) :
    RecyclerView.Adapter<HistoryAdapter.ViewHolder>() {

    class ViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val expressionTextView: TextView = view.findViewById(R.id.expressionTextView)
        val resultTextView: TextView = view.findViewById(R.id.resultTextView)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_history, parent, false)
        return ViewHolder(view)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = items[position]
        holder.expressionTextView.text = item.expression
        holder.resultTextView.text = item.result
    }

    override fun getItemCount() = items.size
}
```

4. В `item_history.xml` (это макет для отдельного элемента истории), добавьте следующее:

```xml
<LinearLayout
    xmlns:android="http://schemas.android.com/apk/res/android"
    android:layout_width="match_parent"
    android:layout_height="wrap_content"
    android:orientation="horizontal"
    android:padding="8dp">

    <TextView
        android:id="@+id/expressionTextView"
        android:layout_width="0dp"
        android:layout_height="wrap_content"
        android:layout_weight="1"
        android:text="3 + 2"/>

    <TextView
        android:id="@+id/resultTextView"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:text="5"
        android:layout_marginStart="16dp"/>
</LinearLayout>
```

5. В `HistoryActivity.kt`, инициализируйте `RecyclerView` и установите `HistoryAdapter`:

```kotlin
class HistoryActivity : AppCompatActivity() {

    private val historyList = mutableListOf<CalculationHistoryItem>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_history)

        val historyRecyclerView: RecyclerView = findViewById(R.id.historyRecyclerView)
        historyRecyclerView.layoutManager = LinearLayoutManager(this)
        historyRecyclerView.adapter = HistoryAdapter(historyList)
    }

    // Метод для добавления новой записи в историю
    fun addToHistory(expression: String, result: String) {
        historyList.add(CalculationHistoryItem(expression, result))
        historyRecyclerView.adapter?.notifyItemInserted(historyList.size - 1)
    }
}
```

6. В вашем `MainActivity`, сохраняйте историю операций и результатов в список каждый раз, когда пользователь выполняет вычисление. Затем, когда пользователь хочет просмотреть историю, запускайте `HistoryActivity` и передавайте список истории через `Intent`.

```kotlin
// Там, где вы обрабатываете операцию и выводите результат
val expression = "$number1 $operationSymbol $number2"
val resultString = result.toString()
addToHistory(expression, resultString)

// Метод для добавления записи в историю
fun addToHistory(expression: String, result: String) {
    historyList.add(CalculationHistoryItem(expression, result))
}

// Запустить HistoryActivity и передать историю
val intent = Intent(this, HistoryActivity::class.java)
intent.putParcelableArrayListExtra("history", ArrayList(historyList))
startActivity(intent)
```

7. В `HistoryActivity`, извлеките переданный список истории из `Intent` и используйте его для инициализации адаптера:

```kotlin
override fun onCreate(savedInstanceState: Bundle?) {
    super.onCreate(savedInstanceState)
    setContentView(R.layout.activity_history)

    val historyItems = intent.getParcelableArrayListExtra<CalculationHistoryItem>("history")
    if (historyItems != null) {
        historyList.addAll(historyItems)
    }

    // Остальная часть кода остаётся той же
}
```

8. Добавьте `HistoryActivity` в `AndroidManifest.xml`:

```xml
<activity android:name=".HistoryActivity"/>
```

Вышеуказанный код является лишь примером и требует дополнительной работы для полноценной интеграции. Необходимо реализовать сохранение истории между сессиями, возможно, с использованием базы данных или `SharedPreferences`. Также следует позаботиться о корректной передаче объектов между `Activity`, возможно, используя сериализацию с помощью `Parcelable` или `Serializable`.

---

#GPT #Kotlin