Готовый код:

```csharp
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MyApplication.Models
{
    /// <summary> Логика авторизации по учетной записи Windows </summary>
    public class UserAuth
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(
            string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);
        
        private const int LOGON32_LOGON_NETWORK = 3;
        private const int LOGON32_PROVIDER_DEFAULT = 0;
        
        private static string GetErrorMessage(int errorCode) => errorCode switch
        {
            1326 => "Неверное имя пользователя или пароль",
            1331 => "Учётная запись отключена",
            1909 => "Срок действия пароля истёк",
            1355 => "Домен недоступен",
            _ => $"Ошибка аутентификации (код: {errorCode})"
        };
        
        /// <summary> Попытка авторизации и вывод сообщения об ошибке при неудачной попытке </summary>
        /// <param name="username">имя пользователя в ОС</param>
        /// <param name="password">пароль пользователя в ОС</param>
        /// <param name="errorMessage">сообщение об ошибке или null, если ошибки нет</param>
        /// <returns>Логическое значение</returns>
        public static bool TryAuthenticateUser(string username, SecureString password, out string errorMessage)
        {
            errorMessage = null;
            IntPtr token = IntPtr.Zero;
            IntPtr passwordPtr = IntPtr.Zero;
            
            try
            {
                // Разделение домена и имени пользователя
                string domain;
                string user;
                var parts = username.Split('\\');
                if (parts.Length == 2)
                {
                    domain = parts[0];
                    user = parts[1];
                }
                else
                {
                    domain = Environment.UserDomainName;
                    user = username;
                }
                
                // Конвертация SecureString в BSTR
                passwordPtr = Marshal.SecureStringToBSTR(password);
                
                bool result = LogonUser
                (
                    user,
                    domain,
                    Marshal.PtrToStringBSTR(passwordPtr),
                    LOGON32_LOGON_NETWORK,
                    LOGON32_PROVIDER_DEFAULT,
                    out token
                );
                
                if (result) return true;
                
                int error = Marshal.GetLastWin32Error();
                errorMessage = GetErrorMessage(error);
            }
            finally
            {
                if (token != IntPtr.Zero) CloseHandle(token);
                if (passwordPtr != IntPtr.Zero) Marshal.ZeroFreeBSTR(passwordPtr);
            }
            return false;
        }
    }
}
```

Реализация аутентификации с ручным вводом учётных данных через Windows API требует использования функции `LogonUser` из advapi32.dll и правильной обработки токенов. Вот пошаговая инструкция:

### 1. Импорт необходимых функций

```csharp
using System.Runtime.InteropServices;

public class WindowsAuthHelper
{
    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool LogonUser(
        string lpszUsername,
        string lpszDomain,
        string lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        out IntPtr phToken);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hHandle);
    
    private const int LOGON32_LOGON_NETWORK = 3;
    private const int LOGON32_PROVIDER_DEFAULT = 0;
}
```

### 2. Реализация метода проверки

```csharp
public (bool Success, string Error) ValidateCredentials(string username, string password)
{
    IntPtr token = IntPtr.Zero;
    try
    {
        var domainAndUser = username.Split('\\');
        if (domainAndUser.Length != 2)
            return (false, "Неверный формат имени (требуется domain\\username)");

        bool result = LogonUser(
            domainAndUser[1],    // Имя пользователя
            domainAndUser[0],    // Домен
            password,
            LOGON32_LOGON_NETWORK,
            LOGON32_PROVIDER_DEFAULT,
            out token);

        if (!result)
        {
            int errorCode = Marshal.GetLastWin32Error();
            return (false, GetErrorMessage(errorCode));
        }

        return (true, "Успешная аутентификация");
    }
    finally
    {
        if (token != IntPtr.Zero)
            CloseHandle(token);
    }
}

private string GetErrorMessage(int errorCode)
{
    return errorCode switch
    {
        1326 => "Неверное имя пользователя или пароль",
        1331 => "Учётная запись отключена",
        1909 => "Срок действия пароля истёк",
        1355 => "Домен недоступен",
        _ => $"Ошибка аутентификации (код: {errorCode})"
    };
}
```

### 3. Пример формы аутентификации

```csharp
public partial class LoginForm : Form
{
    public string Username => txtUsername.Text;
    public string Password => txtPassword.Text;
    
    public LoginForm()
    {
        InitializeComponent();
        txtPassword.UseSystemPasswordChar = true;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        var validator = new WindowsAuthHelper();
        var (success, message) = validator.ValidateCredentials(txtUsername.Text, txtPassword.Text);
        
        if (success)
        {
            LogEvent("Успешный вход: " + txtUsername.Text);
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            LogEvent("Ошибка входа: " + message);
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LogEvent(string message)
    {
        if (!EventLog.SourceExists("MyApp"))
            EventLog.CreateEventSource("MyApp", "Application");
        
        EventLog.WriteEntry("MyApp", message, EventLogEntryType.Information);
    }
}
```

### 4. Основные особенности реализации:

1. **Безопасность передачи пароля**:

- Используйте `SecureString` для хранения пароля в памяти:

```csharp
using System.Security;

public bool ValidateCredentials(string username, SecureString securePassword)
{
    IntPtr unmanagedString = IntPtr.Zero;
    try
    {
        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
        string password = Marshal.PtrToStringUni(unmanagedString);
        // Далее вызов LogonUser
    }
    finally
    {
        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
    }
}
```

1. **Обработка ошибок**:

- Конкретные коды ошибок Windows:
  - 1326: ERROR_LOGON_FAILURE
  - 1331: ERROR_ACCOUNT_DISABLED
  - 1909: ERROR_PASSWORD_EXPIRED

1. **Интеграция с Active Directory**:

- Для проверки членства в группах:

```csharp
using System.DirectoryServices.AccountManagement;

public bool IsUserInGroup(string username, string groupName)
{
    using (var context = new PrincipalContext(ContextType.Domain))
    using (var user = UserPrincipal.FindByIdentity(context, username))
    {
        return user.IsMemberOf(context, IdentityType.Name, groupName);
    }
}
```

### 5. Рекомендации по безопасности:

1. **Шифрование конфигурации**:

- Используйте Protected Configuration для App.config:

```xml
<connectionStrings configProtectionProvider="RsaProtectedConfigurationProvider">
    <EncryptedData>...</EncryptedData>
</connectionStrings>
```

1. **Ограничение попыток входа**:

```csharp
private int failedAttempts = 0;

void CheckLoginAttempts()
{
    if (++failedAttempts >= 3)
    {
        MessageBox.Show("Превышено количество попыток");
        Application.Exit();
    }
}
```

1. **Аудит безопасности**:

- Включите аудит в App.config:
```xml
<system.serviceModel>
    <behaviors>
        <serviceBehaviors>
            <behavior>
                <serviceSecurityAudit 
                    AuditLogLocation="Application"
                    ServiceAuthorizationAuditLevel="SuccessOrFailure"
                    MessageAuthenticationAuditLevel="SuccessOrFailure"/>
            </behavior>
        </serviceBehaviors>
    </behaviors>
</system.serviceModel>
```

### 6. Тестирование решения

Для проверки работы:
1. Создайте тестовую учётную запись в AD
2. Попробуйте войти с правильными и неправильными данными
3. Проверьте записи в Event Viewer (`eventvwr.msc`) в разделе "Журналы Windows -> Приложение"

**Важно**: Приложение должно быть запущено с правами, позволяющими выполнять операции аутентификации. Для доменных пользователей может потребоваться запуск от имени администратора.

#C-Sharp #C-Sharp/P-Invoke #OS/Windows
