using AVASMENA;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск проверки обновлений перед запуском формы
            if (CheckForUpdatesAsync().GetAwaiter().GetResult())
            {
                Application.Run(new MainForm());
            }
        }

        static async Task<bool> CheckForUpdatesAsync()
        {
            return await UpdateChecker.CheckForUpdatesAsync();
        }
    }
}
