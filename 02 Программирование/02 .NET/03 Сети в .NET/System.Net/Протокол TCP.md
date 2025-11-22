
Из официальной [документации](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/sockets/tcp-classes):

> Настоятельно рекомендуется использовать класс `Socket` для продвинутых пользователей, вместо `TcpClient` и `TcpListener`

Классы `TcpClient` и `TcpListener` — это обертки над `Socket`, предоставляющие упрощенное API, скрывающее детали механизма передачи данных

Протокол TCP устанавливает соединение с удаленным эндпоинтом и, затем, использует это соединение для отправки и чтения пакетов данных. TCP отвечает за то, чтобы пакеты данных доходили до эндпоинта и располагались в правильном порядке при получении

> **Эндпоинт (end point)** — это комбинация адреса в сети и порта, которая однозначно идентифицирует ресурс в сети

Абстрактный класс `EndPoint` описывает само понятие эндпоинта. От этого класса наследуется несколько конкретизированных типов эндпоинтов:

- `System.Net.IPEndPoint` ([docs](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipendpoint?view=net-9.0)): Комбинация IP-адреса и порта
- `System.Net.DnsEndPoint` ([docs](https://learn.microsoft.com/en-us/dotnet/api/system.net.dnsendpoint?view=net-9.0)): Комбинация URL-адреса или IP-адреса и порта
- `System.Net.Sockets.UnixDomainSocketEndPoint` ([docs](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.unixdomainsocketendpoint?view=net-9.0)): используется для создания и представления локальных Unix Domain Socket (UDS) соединений, которые обеспечивают межпроцессное взаимодействие на уровне операционной системы без использования сетевых протоколов. Представляет собой путь к специальному файлу, через который процессы внутри одной машины могут обмениваться данными

Экземпляр `IPEndPoint` создается с указанием IP-адреса и соответствующего порта

#C-Sharp #Network