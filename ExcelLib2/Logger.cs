using System;
using System.IO;

public static class Logger
{
    private static readonly string logFilePath = @"C:\Logs\log.txt";

    static Logger()
    {
        // Создаем директорию, если она не существует
        string directoryPath = Path.GetDirectoryName(logFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static void Log(string message)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось записать лог: {ex.Message}");
        }
    }
}
