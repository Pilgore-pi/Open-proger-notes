
```python
import random

# Целое число из указанного диапазона
print random.randint(0,10)
#>> 2

# Число из диапазона с указанием шага
print random.randrange(10,20,2)
# >>14

# Дробное число из диапазона 0.0 - 1.0
print random.random()
#>> 0.537843941827

# Выбирает один элемент из списка
print random.choice('abcdef')
#>> b

# Перемешивает элементы
r = range(10)
random.shuffle(r)
print r
#>> [0, 4, 1, 6, 8, 3, 2, 5, 9, 7]

# Выбирает указанное кол-во элементов из списка
print random.sample(range(50),10)
#>> [22, 29, 30, 14, 16, 17, 32, 48, 2, 19] 
Ещё есть os.urandom(n), возвращает n случайных байт в виде строки:
>>> from os import urandom
>>> urandom(8)
'z\x7f\xd6\xd3%\xc1O\xb3'

```

#Python