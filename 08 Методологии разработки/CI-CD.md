
## **Что такое CI/CD и зачем это нужно?**

> **CI/CD** (Continuous Integration / Continuous Deployment) — это подход к разработке, который автоматизирует процесс сборки, тестирования и развертывания приложений. Основные цели CI/CD:

1. **Continuous Integration (CI)**: Автоматическая проверка и сборка кода при каждом изменении. Это позволяет быстро находить ошибки и проверять, что изменения не ломают существующий функционал.
2. **Continuous Deployment (CD)**: Автоматическое развертывание приложения на серверы или в облако после успешной сборки и тестирования.

Пример: Вы вносите изменения в код, отправляете их в репозиторий (например, на GitHub), и CI/CD система автоматически:

- Проверяет ваш код на ошибки.
- Собирает проект для всех целевых платформ.
- Запускает тесты.
- (Опционально) Разворачивает приложение на сервере или создает установочные файлы.

##### **Как настроить CI/CD для вашего проекта**

###### 1. **Выбор CI/CD инструмента**

Популярные инструменты для автоматизации:

- **Aspire**: проект Майкрософт для автоматизации разработки (предварительная версия)
- **GitHub Actions**: Если вы используете GitHub, это встроенный инструмент для автоматизации.
- **GitLab CI/CD**: Если ваш код хранится на GitLab.
- **Azure DevOps**: Подходит для проектов на .NET.
- **Jenkins**: Гибкий инструмент, который можно настроить для любых задач.
- **CircleCI** или **Travis CI**: Подходят для кроссплатформенных проектов.

###### 2. **Пример настройки GitHub Actions**

Если вы используете GitHub, настройка CI/CD выполняется через файл `.yml` в папке `.github/workflows`. Вот пример для сборки .NET проекта и Python модулей:

```yaml
name: Build and Test

on:
  push:
    branches:
      - main
  pull_request:
    branches:
      - main

jobs:
  build:
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [windows-latest, ubuntu-latest, macos-latest]

    steps:
    - name: Checkout code
      uses: actions/checkout@v2

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0' # Укажите версию .NET

    - name: Setup Python
      uses: actions/setup-python@v4
      with:
        python-version: '3.12' # Укажите версию Python

    - name: Install dependencies
      run: |
        dotnet restore
        pip install -r requirements.txt

    - name: Build .NET project
      run: dotnet build --configuration Release

    - name: Run tests
      run: |
        dotnet test
        pytest # Запуск тестов Python
```

Этот файл:

- Проверяет код при каждом изменении в ветке `main`.
- Устанавливает .NET и Python.
- Собирает проект и запускает тесты.

###### 3. **Сборка для разных ОС**

Если ваше приложение должно работать на Windows, Linux и macOS, настройте сборку для каждой ОС. В примере выше используется `matrix.os`, чтобы запускать сборку на всех платформах.

###### 4. **Автоматизация публикации**

После успешной сборки можно автоматически публиковать артефакты (например, установочные файлы или библиотеки):

- Для .NET: Публикация в NuGet.
- Для Python: Публикация в PyPI.
- Для приложений: Создание установочных файлов или Docker-образов.

Пример публикации артефактов:

```yaml
- name: Publish artifacts
    uses: actions/upload-artifact@v3
    with:
        name: build-output
        path: bin/Release
```

#Методологии_разработки #Методологии_разработки/CI_CD