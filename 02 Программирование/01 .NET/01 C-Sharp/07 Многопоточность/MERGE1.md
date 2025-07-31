```cs
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShellScriptExecutor;

// \\Main\обмен\365\Отдел\Пришутов_Андрей\message.html
class Program
{
    private const string UsersFilePath = @"users.txt";
    
    static void Main(string[] args)
    {
        try
        {
            string htmlPath;
            if (args.Length < 1)
            {
                Colored("Введите путь к HTML файлу: ", ConsoleColor.Yellow);
                Console.ForegroundColor = ConsoleColor.Cyan;
                htmlPath = Console.ReadLine();
            }
            else htmlPath = args[0];

            string usersFile = Path.Join(AppContext.BaseDirectory, UsersFilePath);
            var users = File.ReadAllLines(usersFile);
        
            // Запускаем параллельное выполнение
            Parallel.ForEachAsync(users, async (username, _) =>
                await OpenHtmlFileForUserAsync(username, htmlPath));
        }
        catch (Exception e)
        {
            ColoredLine("Произошло исключение: " + e.Message, ConsoleColor.Magenta);
        }
        
        ColoredLine("Программа завершила выполнение...", ConsoleColor.Cyan);
        Console.ReadKey(true);
    }

    private static async Task OpenHtmlFileForUserAsync(string username, string htmlFilePath)
    {
        await Task.Run(() =>
        {
            // Получаем профиль пользователя
            string profilePath = $@"C:\Users\{username}";

            // Проверяем, существует ли профиль пользователя
            if (Directory.Exists(profilePath))
            {
                // Запускаем ассоциированное приложение
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = htmlFilePath,
                    WorkingDirectory = profilePath,
                    UseShellExecute = true
                };

                try { Process.Start(processStartInfo); }
                catch (Exception ex) {
                    ColoredLine($"Ошибка при открытии файла для пользователя {username}: {ex.Message}", ConsoleColor.Magenta);
                }
            }
            else ColoredLine($"Профиль пользователя {username} не найден.", ConsoleColor.Red);
        });
    }

    static void OpenHtmlFileOnRemoteComputer(string computerIp, string filePath)
    {
        // Создание параметров подключения к удаленному компьютеру
        var connectionInfo = new WSManConnectionInfo(
            new Uri($"http://{computerIp}:5985/wsman"),
            "http://schemas.microsoft.com/powershell/Microsoft.PowerShell",
            PSCredential.Empty
        );
        
        // Создание пространства имен для выполнения команд PowerShell
        using var runspace = RunspaceFactory.CreateRunspace(connectionInfo);
        runspace.Open();
        
        using var powerShell = PowerShell.Create();
        powerShell.Runspace = runspace;

        // Команда для открытия HTML файла
        string script = "Start-Process $path -UseShellExecute $true";

        powerShell.AddScript(script);
        powerShell.AddParameter("path", filePath);
        
        // Выполнение команды
        powerShell.Invoke();

        // Проверка на ошибки
        if (powerShell.Streams.Error.Count <= 0) return;
        
        foreach (var error in powerShell.Streams.Error)
            Console.WriteLine($"Ошибка PowerShell.Streams.Error: {error}");
    }
    
    static void Colored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void ColoredLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
```

#MERGE_NOTES
