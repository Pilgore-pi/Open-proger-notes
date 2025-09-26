// Параметр времени в секундах
using System.Diagnostics;

Console.Write("Выключить компьютер через (мин): ");

if (int.TryParse(Console.ReadLine(), out var argument) is false)
    EndProgram();

int timeInSeconds = argument * 60; // 30 минут

// Команда PowerShell для отложенного выключения компьютера
string command = $"shutdown /s /t {timeInSeconds}";

// Запуск PowerShell с командой
RunPowerShellCommand(command);


static void RunPowerShellCommand(string command) {
    using var process = new Process();
    process.StartInfo.FileName = "powershell.exe";
    process.StartInfo.Arguments = $"-Command {command}";
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    _ = process.Start();

    string output = process.StandardOutput.ReadToEnd();
    string error = process.StandardError.ReadToEnd();

    process.WaitForExit();

    if (!string.IsNullOrEmpty(output)) {
        Console.WriteLine("Вывод:");
        Console.WriteLine(output);
        EndProgram();
    }

    if (!string.IsNullOrEmpty(error)) {
        Console.WriteLine("Ошибка:");
        Console.WriteLine(error);
        _ = Console.ReadKey(true);
        Environment.Exit(0);
        EndProgram();
    }
}

static void EndProgram() {
    Console.Write("Программа завершила работу...");
    _ = Console.ReadKey(true);
    Environment.Exit(0);
}
