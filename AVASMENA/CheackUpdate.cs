using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public class UpdateChecker
{
    // Путь к файлу version.json на сетевом диске
    private static readonly string VersionCheckPath = @"\\192.168.88.254\AVASMENAUpdate\publisher\version.json";

    // Путь к папке с обновлениями на сетевом диске
    private static readonly string UpdatePath = @"\\192.168.88.254\AVASMENAUpdate\publisher\Output\";

    // Текущая версия установленной программы
    private static readonly string CurrentVersion = "1.0.2";

    public static bool ShouldCloseApp { get; private set; } = false;

    public static async Task<bool> CheckForUpdatesAsync()
    {
        try
        {
            // Проверка наличия файла version.json
            if (File.Exists(VersionCheckPath))
            {
                // Чтение содержимого файла version.json
                var jsonContent = File.ReadAllText(VersionCheckPath);
                var json = JObject.Parse(jsonContent);

                // Извлечение версии и имени файла обновления из файла
                var latestVersion = json["version"].ToString();
                var setupFileName = json["filename"].ToString();

                // Сравнение текущей версии программы с последней доступной версией
                if (latestVersion != CurrentVersion)
                {
                    string setupFilePath = Path.Combine(UpdatePath, setupFileName);

                    if (File.Exists(setupFilePath))
                    {
                        ShowUpdateDialog(setupFilePath);
                        return true;
                    }
                    else
                    {
                        Logger.Log($"Файл {setupFileName} не найден по пути {UpdatePath}.");
                    }
                }
                else
                {
                    Logger.Log("У вас установлена последняя версия.");
                }
            }
            else
            {
                Logger.Log("Файл version.json не найден.");
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Ошибка при проверке обновлений: {ex.Message}");
        }

        return false;
    }

    private static void ShowUpdateDialog(string setupFilePath)
    {
        DialogResult result = MessageBox.Show(
            "Сначала нужно обновить, чтобы продолжить работу.",
            "Обновление доступно",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        if (result == DialogResult.OK)
        {
            ShouldCloseApp = true;
            DownloadAndInstallUpdate(setupFilePath);
        }
    }

    private static void DownloadAndInstallUpdate(string setupFilePath)
    {
        try
        {
            // Локальный путь для сохранения установочного файла
            string localSetupFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(setupFilePath));

            // Копирование файла с сетевого пути на локальный компьютер
            File.Copy(setupFilePath, localSetupFilePath, true);
            Logger.Log("Файл успешно скачан.");

            // Запуск установочного файла
            System.Diagnostics.Process.Start(localSetupFilePath);
            Logger.Log("Запуск установочного файла...");
        }
        catch (Exception ex)
        {
            Logger.Log($"Ошибка при скачивании или запуске обновления: {ex.Message}");
        }
    }
}