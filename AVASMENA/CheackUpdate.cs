using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

public class UpdateChecker
{
    // URL API для проверки версии
    private static readonly string VersionCheckUrl = "http://192.168.88.254:3001/api/version?apiKey=853ac29b5540bc9f72df1cda971d24bb1e349cd2de7319661f442c538d79dfdd";

    // URL для загрузки обновления
    private static readonly string DownloadUrl = "http://192.168.88.254:3001/api/download?apiKey=853ac29b5540bc9f72df1cda971d24bb1e349cd2de7319661f442c538d79dfdd";

    // Текущая версия установленной программы
    private static readonly string CurrentVersion = "1.0.9";

    public static bool ShouldCloseApp { get; private set; } = false;

    public static async Task<bool> CheckForUpdatesAsync()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Запрос версии с сервера
                HttpResponseMessage response = await client.GetAsync(VersionCheckUrl);
                response.EnsureSuccessStatusCode();
                string jsonContent = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonContent);

                // Извлечение версии и имени файла обновления из файла
                var latestVersion = json["version"].ToString();
                var setupFileName = json["filename"].ToString();

                // Сравнение текущей версии программы с последней доступной версией
                if (latestVersion != CurrentVersion)
                {
                    ShowUpdateDialog();
                    return false;
                }
                else
                {
                    Logger.Log("У вас установлена последняя версия.");
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Ошибка при проверке обновлений: {ex.Message}");
        }

        return true;
    }

    private static void ShowUpdateDialog()
    {
        DialogResult result = MessageBox.Show(
            "Сначала нужно обновить, чтобы продолжить работу.",
            "Обновление доступно",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        if (result == DialogResult.OK)
        {
            DownloadAndInstallUpdate().GetAwaiter().GetResult();
        }
    }

    private static async Task DownloadAndInstallUpdate()
    {
        try
        {
            // Локальный путь для сохранения установочного файла
            string localSetupFilePath = Path.Combine(Path.GetTempPath(), "setup.exe");

            using (HttpClient client = new HttpClient())
            {
                // Загрузка файла обновления
                HttpResponseMessage response = await client.GetAsync(DownloadUrl);
                response.EnsureSuccessStatusCode();

                using (var fs = new FileStream(localSetupFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fs);
                }
                Logger.Log("Файл успешно скачан.");

                // Запуск установочного файла
                System.Diagnostics.Process.Start(localSetupFilePath);
                Logger.Log("Запуск установочного файла...");
            }
        }
        catch (Exception ex)
        {
            Logger.Log($"Ошибка при скачивании или запуске обновления: {ex.Message}");
        }
    }
}