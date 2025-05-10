[Source](https://forum.foxclub.ru/read.php?29,655624,655683,view_spoiler=655683:1#bbcode_spoiler_anchor_655683_1)

```xbase
*****************************************************************************************************************************
* С подачи forum.foxclub.ru
* Вызов без (или с пустым) URL-параметром - Определяет наличие подключения к интеренету. Возвращает непусто, если подключение есть
* Вызов с непустым URL-параметром - пингует URL, вовзращает непусто, если URL пингуется. Отсутствующие адреса прингуются с большим таймаутом...
* URL задавать как например: "http://ya.ru", "http://123.456.789.012", "http://MyCompName.domen.ru"
* Возвращает C-вид подключения, если подключение есть. Однако в MSDN:
* "Вы не можете полагаться только на тот факт, что если ф-ция InternetGetConnectedState возвращает TRUE, это означает наличие активного соединения с Интернет.
*    InternetGetConnectedState не может определить, функционирует ли соединение с Интернет без обращения к серверу..." 
* См. www.delphimaster.ru Также посмотри:
*    InternetCheckConnection(S URL, I флаги, I рез.=0) IN winnet.dll  См. msdn.microsoft.com , www.nestor.minsk.by
*    InternetAttemptConnect() - заставляет установить соединение с интернетом
*    GetSystemMetrics(INTEGER SM_NETWORK = 63) IN user32.dll, проверять бит 0 (у меня всегда возвращает "соединено")
*    getHostByName()  - URL --> IP
*    getHostByAddr()  - IP --> URL
*    GetAddrInfoW     - список протоколов компа, с IP адресами
*    IcmpSendEcho     - на ней работает Ping, есть управление таймаутом. msdn.microsoft.com
*    InetIsOffline    - в URL.dll/Shell msdn.microsoft.com
* См. www.64soft.net
* Ссылки на пинговалки по icmp: forum.foxclub.ru
*****************************************************************************************************************************
FUNCTION IsInternetConnected
LPARAMETERS m.parURL
PRIVATE ALL LIKE ?
m.p = PCOUNT()
m.r = .NULL.  && возвращаемое C-значение - пусто, если НЕТ подключения к интернет, если что-то есть, то текст... но это совсем не есть гарантия доступности сайтов...

***********************************************************************************************************************************************************************
IF m.p<1 .OR. ISNULL(m.parURL) .OR. EMPTY(m.parURL)  && мода "Определяет наличие подключения к интеренету"
***********************************************************************************************************************************************************************

   * сначала попробуем вот такую ф-ию WinSock (в WinInet). Если такой нет, то попробуем GetSystemMetrics(SM_NETWORK=63)
   IF ISNULL(m.r)  && не нашлась предыдущая ф-ия (DLLка)
      EXTERNAL ARRAY InternetGetConnectedState
      IF IsDECLAREs("WinInet: InternetGetConnectedState(I@,I)")  && возвращаемые флаги, мода. Вот эта ф-ия возвращает правильный (ожидаемый) результат!
         m.f = 0   && сюда будут записаны флаги, возвращаемые DLLкой
         IF NOT InternetGetConnectedState(@f, 0)=0  && интернет подключен
            m.r = bitGETWORDNUM("DialUp,LAN,Proxy,ModemBusy,RAS,OffLine,Configured", m.f, ",")  && ModemBusy и все что после него - можно бы не возвращать...
         ELSE  && интернет не подключен
            m.r = ""
         ENDIF
      ENDIF
   ENDIF
   
   IF .F. && ISNULL(m.r)  && не нашлась предыдущая ф-ия (DLLка)
      EXTERNAL ARRAY InetIsOffline
      IF IsDECLAREs("URL: InetIsOffline(I)")
         * Returns TRUE if the local system is not currently connected to the Internet. Returns FALSE if the local system is connected to the Internet
         * or if no attempt has yet been made to connect to the Internet.
         * Реально всегда говорит что "интернет есть", только когда кабель отключен - думает секунд 30...
         * " функция эта выдает false не только, когда комп подключен к интернету, но и когда ЕЩЕ НЕ БЫЛО ПОПЫТОК подключения (or if no attempt has yet been made to connect to the Internet). 
         m.f = InetIsOffline(0)  && вызывать только с 0
         m.r = IIF(m.f=0, "есть", "")
      ENDIF
   ENDIF

   IF .F. && ISNULL(m.r)  && не нашлась предыдущая ф-ия (DLLка)
      * попробуем более штатную ф-ию, (в WIN32API) ...Правда, она все время возвращает "подключено", даже если сетевой кабель выдернуть...
      EXTERNAL ARRAY GetSystemMetrics
      IF IsDECLAREs("GetSystemMetrics(I)")  && мода
         * " The least significant bit is set if a network is present; otherwise, it is cleared. The other bits are reserved for future use.
         m.f = GetSystemMetrics(63)  && SM_NETWORK, возвращает бит0=1, если есть подключение
         m.r = bitGETWORDNUM("?", m.f, ",")
      ENDIF
   ENDIF

***********************************************************************************************************************************************************************
ELSE  && мода "пинговать URL". Вернуть непусто, если пингуется. ВНИМАНИЕ! Вот это: www.nestor.minsk.by, nestor.minsk.by - пингуется, www.nestor.minsk.by - так нет
***********************************************************************************************************************************************************************

   IF ISNULL(m.r)  && не нашлась предыдущая ф-ия (DLLка)
      EXTERNAL ARRAY InternetCheckConnection
      IF IsDECLAREs("WinInet: InternetCheckConnection(S,I,I)")  && URL, флаги, резервный
         m.s = ALLTRIM(m.parURL)  && может, тут UNICODE-версию надо исп. ..., т.к., например, http://президент.рф - не пингуется
         * (2)=FLAG_ICC_FORCE_CONNECTION=1:
         *     если (1)=URL не NULL, то пингует этот URL,
         *     если (1)=NULL, то "пингуется ближайший сервер, информация о котором присутствует во внутренней базе данных ОС"
         * Также оказалось, что если (2)=0, ТО НЕ ПИНГУЕТ ВООБЩЕ...
         IF NOT InternetCheckConnection(m.s, 1, 0)=0
            m.r = "Ping"
         ENDIF
      ENDIF
   ENDIF

***********************************************************************************************************************************************************************
ENDIF
***********************************************************************************************************************************************************************

RETURN IIF(ISNULL(m.r), "", m.r)
```
