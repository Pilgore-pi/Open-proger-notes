```html
<p class="text">Здесь могла быть ваша реклама</p>
<button onClick="changeText();">Нажми чтобы изменить</button>
```


```js
const changeText = () => {
  document.getElementsByClassName('text')[0].textContent =
  "Кто сказал мяу?";
}
```

#Web #JavaScript #HTML