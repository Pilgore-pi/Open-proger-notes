
https://docs.devolutions.net/server/kb/how-to-articles/winrm-trustedhostslist/
https://www.perplexity.ai/search/kak-v-json-zapisat-stroku-igno-80gMQRUxTC.mg0wTg9WE_g


Служба WinRM

`Enable-PSRemoting -Force` — Активация службы WinRM в системе
`Start-Service winrm` — Ручной запуск службы
`Get-Service winrm` — Получение статуса службы
`Restart-Service WinRM` — Перезапуск службы
`winrm quickconfig` — Получение конфигурации WinRM


Добавление правила в брандмауэр
```powershell
Enable-NetFirewallRule -Name "WINRM-HTTP-In-TCP"
```


Использование WinRM доступно только при использовании HTTPS протокола или при работе только с доверенными хостами (TrustedHosts)

Установка единственного доверенного хоста:

```powershell
Set-Item WSMan:\localhost\Client\TrustedHosts -Value "192.168.1.77" -Force
```

Добавление доверенного хоста к существующим

```powershell
Set-Item WSMan:\localhost\Client\TrustedHosts -Value 'machineC' -Concatenate
```

Установка нескольких доверенных хостов:

```powershell
Set-Item WSMan:\localhost\Client\TrustedHosts -Value "192.168.1.77,192.168.1.73,192.168.1.74,192.168.1.93,192.168.1.72" -Force
```

```powershell
Get-Item WSMan:\localhost\Client\TrustedHosts
```


## Команда NET GROUP / NET LOCAL GROUP

ВСЕ КОМАНДЫ CMD: https://ab57.ru/cmdlist.html
Подробная документация: https://ab57.ru/cmdlist/net.html

Команды `NET GROUP` и `NET LOCALGROUP` практически идентичны, но первая выполняется на контроллере домена и относится к объектам Active Directory, а вторая – к локальным рабочим группам компьютера.

`net localgroup` выводит полный список зарегестрированных групп для данного компьютера

`/DOMAIN` Выполняет операцию на контроллере домена в текущем домене. В противном случае операция будет выполнена на локальном компьютере.

NET SHARE разрешает использовать серверные ресурсы другим пользователям сети


## Ответ Perplexity R1

Чтобы решить проблему с ошибкой безопасности при удаленном выполнении скрипта через WinRM, выполните следующие шаги:

---

### 1. **Проверьте корректность TrustedHosts**
   - Убедитесь, что компьютеры добавлены в TrustedHosts **без пробелов** и с правильным синтаксисом:
```powershell
Get-Item WSMan:\localhost\Client\TrustedHosts
```
   
   - Если список не отображается, перезапустите WinRM:

```powershell
Restart-Service WinRM
```

   - **Важно**: TrustedHosts работает только с **NTLM-аутентификацией** в рабочих группах. Если используется IP-адрес, явно передавайте учетные данные.

---

### 2. **Используйте явные учетные данные**

   - Добавьте параметр `-Credential` в `Invoke-Command`, чтобы передать права администратора удаленного компьютера:

```powershell
$cred = Get-Credential
Invoke-Command -ComputerName $ComputerNames -Credential $cred -ScriptBlock { ... }
```

   - Убедитесь, что учетная запись:
     - Существует на **удаленных компьютерах** (локальная учетная запись или общий аккаунт).
     - Входит в группу **Administrators**.

---

### 3. Настройте LocalAccountTokenFilterPolicy

   - На удаленных компьютерах измените реестр, чтобы разрешить доступ локальным администраторам:

```powershell
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" /v LocalAccountTokenFilterPolicy /t REG_DWORD /d 1 /f
```

   - Перезапустите WinRM:
 
```powershell
Restart-Service WinRM
```

---

### 4. **Проверьте аутентификацию и порты**

   - Убедитесь, что на удаленных компьютерах:
    - Служба WinRM запущена:

```powershell
Get-Service WinRM | Start-Service
```

- Открыты порты **5985 (HTTP)** и **5986 (HTTPS)** в брандмауэре:

```powershell
Enable-NetFirewallRule -Name "WINRM-HTTP-In-TCP", "WINRM-HTTPS-In-TCP"
```

### 5. **Настройте HTTPS (рекомендуется для безопасности)**

- Создайте самозаверяющий сертификат:

```powershell
New-SelfSignedCertificate -DnsName "server.domain" -CertStoreLocation Cert:\LocalMachine\My
```

- Привяжите сертификат к WinRM:

```powershell
$thumbprint = "XXXXX"  # Замените на отпечаток сертификата
winrm create winrm/config/Listener?Address=*+Transport=HTTPS @{Hostname="localhost"; CertificateThumbprint=$thumbprint}
```

- Используйте HTTPS в скрипте:

```powershell
Invoke-Command -ComputerName $ComputerNames -UseSSL -ScriptBlock { ... }
```

---

### 6. **Дополнительные проверки**

   - Убедитесь, что на удаленных компьютерах:
     - Включена групповая политика **Allow Remote Shell Access** (если используется домен).
     - Время на локальном и удаленном компьютерах синхронизировано (расхождение ≤ 5 минут).
   - Проверьте подключение:

```powershell
Test-WSMan 192.168.1.77
```

---

### Пример исправленного вызова `Invoke-Command`

```powershell
$cred = Get-Credential
Invoke-Command -ComputerName $ComputerNames -Credential $cred -ScriptBlock {
    # Ваш код
} -ArgumentList $HtmlContent, $LogFilePath
```

Если ошибка сохраняется, проверьте журналы WinRM на удаленном компьютере:

```powershell
Get-WinEvent -LogName "Microsoft-Windows-WinRM/Operational"
```

Citations:
[1] https://winitpro.ru/index.php/2012/01/31/kak-aktivirovat-windows-remote-management-s-pomoshhyu-gruppovoj-politiki/
[2] https://winitpro.ru/index.php/2022/01/11/psremoting-udalennoe-podklyuchenie-workgroup/
[3] https://coolcode.ru/powershell-reshenie-problem-svyazyinnyih-s-udalyonnyim-podklyucheniem-about_remote_troubleshooting/
[4] https://docs.vmware.com/ru/VMware-Aria-Automation/8.14/Using-Automation-Orchestrator-Plugins/GUID-79518969-9B73-48E3-8B05-72C78179F555.html
[5] http://blog.vmpress.org/2009/11/winrm-windows-server-2008.html
[6] https://learn.microsoft.com/ru-ru/windows/win32/winrm/installation-and-configuration-for-windows-remote-management
[7] https://docs.vmware.com/ru/vRealize-Orchestrator/8.8/com.vmware.vrealize.orchestrator-use-plugins.doc/GUID-2F7DA33F-E427-4B22-8946-03793C05A097.html
[8] https://winitpro.ru/index.php/2021/08/04/winrm-powershell-remoting-cherez-https/
[9] https://learn.microsoft.com/ru-ru/troubleshoot/windows-client/system-management-components/configure-winrm-for-https
[10] https://learn.microsoft.com/ru-ru/powershell/module/microsoft.wsman.management/test-wsman?view=powershell-7.5
[11] https://habr.com/ru/companies/f_a_c_c_t/articles/762006/
[12] https://learn.microsoft.com/ru-ru/troubleshoot/windows-client/system-management-components/errors-when-you-run-winrm-commands
[13] https://smearg.wordpress.com/2011/04/29/%D0%BD%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B0-%D1%83%D0%B4%D0%B0%D0%BB%D1%91%D0%BD%D0%BD%D0%BE%D0%B3%D0%BE-%D1%83%D0%BF%D1%80%D0%B0%D0%B2%D0%BB%D0%B5%D0%BD%D0%B8%D1%8F-%D0%B2-powershell/
[14] https://docs.etecs.ru/scanner/docs/administration/connection_setup/WinRM_setup/
[15] https://cqr.company/ru/wiki/protocols/windows-remote-management-winrm/
[16] https://sysadmins.ru/topic498367-10.html
[17] https://docs.redcheck.ru/articles/
[18] https://windowsnotes.ru/powershell-2/nastrojka-udalennogo-vzaimodejstviya-v-powershell-chast-1/
[19] https://help.keenetic.com/hc/ru/articles/115005881865-%D0%9D%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B0-%D0%91%D1%80%D0%B0%D0%BD%D0%B4%D0%BC%D0%B0%D1%83%D1%8D%D1%80%D0%B0-Windows-%D0%B4%D0%BB%D1%8F-%D0%BF%D0%BE%D0%B4%D0%BA%D0%BB%D1%8E%D1%87%D0%B5%D0%BD%D0%B8%D0%B9-%D0%B8%D0%B7-%D1%81%D0%B5%D1%82%D0%B8-%D0%B7%D0%B0-VPN-%D1%81%D0%B5%D1%80%D0%B2%D0%B5%D1%80%D0%BE%D0%BC-Keenetic
[20] http://www.oszone.net/15874/Server-Manager
[21] https://winitpro.ru/index.php/2012/01/31/kak-aktivirovat-windows-remote-management-s-pomoshhyu-gruppovoj-politiki/
[22] https://learn.microsoft.com/ru-ru/powershell/scripting/security/remoting/running-remote-commands?view=powershell-7.5
[23] https://learn.microsoft.com/ru-ru/windows/win32/bits/using-winrm-windows-powershell-cmdlets-to-manage-bits-transfer-jobs
[24] https://serverspace.ru/support/help/upravlenie-polzovatelyami-cherez-powershell/
[25] https://docs.vmware.com/ru/VMware-Aria-Automation/8.14/Using-Automation-Orchestrator-Plugins/GUID-79518969-9B73-48E3-8B05-72C78179F555.html
[26] http://blog.activehelp.ru/posts/instrumenty/daem-prava-bespravnym/
[27] https://www.securitylab.ru/analytics/542439.php
[28] https://docs.vmware.com/ru/vRealize-Orchestrator/8.8/com.vmware.vrealize.orchestrator-use-plugins.doc/GUID-79518969-9B73-48E3-8B05-72C78179F555.html
[29] https://qna.habr.com/q/1033094
[30] https://support.kaspersky.com/kuma/3.2/ru-RU/248539.htm
[31] https://help.ptsecurity.com/projects/maxpatrol10/26.0/ru-RU/help/307512587
[32] https://learn.microsoft.com/ru-ru/windows/win32/winrm/installation-and-configuration-for-windows-remote-management
[33] https://techdocs.broadcom.com/ru/ru/vmware-cis/aria/aria-automation/8-17/vro-using-plug-ins-8-17/using-the-powershell-plug-in/configuring-the-powershell-plug-in/Chunk752399735/Chunk75239973-0.html
[34] https://www.samara-it.ru/?p=187
[35] https://latl.ru/coding/powershell/winrm_tune/
[36] https://serveradmin.ru/nastroit-udalennyj-dostup-po-wmi/
[37] https://xn--e1aebcfhqcaid9ao1at1g.xn--p1ai/pages/thycoticCredSSP/
[38] https://windowsnotes.ru/powershell-2/nastrojka-udalennogo-vzaimodejstviya-v-powershell-chast-2/
[39] https://learn.microsoft.com/ru-ru/powershell/scripting/security/remoting/powershell-remoting-faq?view=powershell-7.4&viewFallbackFrom=powershell-7.3
[40] https://winitpro.ru/index.php/2020/07/27/powershell-invoke-command-zapuska-komand-na-udalennyx-kompyuterax/
[41] https://lemiro.ru/archives/powershell-remoting-over-https.html

---
Answer from Perplexity: https://www.perplexity.ai/search/kak-v-json-zapisat-stroku-igno-80gMQRUxTC.mg0wTg9WE_g?utm_source=copy_output

#MERGE_NOTES
