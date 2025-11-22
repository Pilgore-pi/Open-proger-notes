## ReactJS

Кроссплатформенный JavaScript фреймворк (библиотека), который предоставляет множество элементов управления и имеет возможность использовать JSX -- xml разметку прямо в коде JS.
```js
class HelloMessage extends React.Component {
  render() {
    return (
      <div>
        Привет, {this.props.name}
      </div>
    );
  }
}

root.render(<HelloMessage name="Саша" />);
```

Доступные платформы: web, Android, IOS

## React Native

Доступные платформы: web, Android, IOS, MacOS, Windows и UWP

#Web #JavaScript