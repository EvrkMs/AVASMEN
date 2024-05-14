using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace jsonData
{
    public class UserDataLoader
    {
        public Dictionary<string, long> Users { get; set; }
        public Dictionary<string, int> Names { get; set; }
        public Dictionary<string, int> NamesZP { get; set; }
        public List<string> NameList { get; set; }

        private static string jsonReserv = "JSON\\userData.json";
        private static string jsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "Config");
        private static string jsonFilePath = $"{jsonPath}\\userData.json";
        public UserDataLoader()
        {
            Users = new Dictionary<string, long>();
            Names = new Dictionary<string, int>();
            NamesZP = new Dictionary<string, int>();
            NameList = new List<string>();
        }

        public static Task CreateJsonIfNotExists()
        {
            try
            {
                if (!System.IO.File.Exists(jsonPath))
                {
                    Directory.CreateDirectory(jsonPath);
                }
                // Проверяем, существует ли основной JSON файл
                if (!System.IO.File.Exists(jsonFilePath))
                {
                    // Если основной файл не существует, копируем резервный файл на его место
                    if (System.IO.File.Exists(jsonReserv))
                    {
                        System.IO.File.Copy(jsonReserv, jsonFilePath);
                    }
                    else
                    {
                        throw new FileNotFoundException("Резервный JSON файл не найден.", jsonReserv);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений, если что-то пошло не так
                Console.WriteLine($"Ошибка при создании JSON файла: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public static UserDataLoader LoadFromFile()
        {
            try
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<UserDataLoader>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                return new UserDataLoader();
            }
        }

        public void SaveToFile()
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFilePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
            }
        }

        public static void SaveBttn(DataGridView dataGrid)
        {
            UserDataLoader dataLoader = LoadFromFile();

            dataGrid.Rows.Clear();

            foreach (var name in dataLoader.NameList)
            {
                var rowIndex = dataGrid.Rows.Add();
                var row = dataGrid.Rows[rowIndex];

                row.Cells["Name"].Value = name;
                row.Cells["Users"].Value = dataLoader.Users.ContainsKey(name) ? dataLoader.Users[name].ToString() : string.Empty;
                row.Cells["Names"].Value = dataLoader.Names.ContainsKey(name) ? dataLoader.Names[name].ToString() : string.Empty;
                row.Cells["NamesZP"].Value = dataLoader.NamesZP.ContainsKey(name) ? dataLoader.NamesZP[name].ToString() : string.Empty;
            }
        }
    }
}
