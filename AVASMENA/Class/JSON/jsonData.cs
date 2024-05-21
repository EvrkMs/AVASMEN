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
        public string TokenBot { get; set; }
        public long ForwardChat {  get; set; }
        public long ChatId { get; set; }

        private static readonly string jsonReserv = "Class\\JSON\\userData.json";
        private static readonly string jsonPath = "\\\\192.168.88.254\\AVASMENAUpdate\\Needed\\Config";
        private static readonly string jsonFilePath = $"{jsonPath}\\userData.json";
        public UserDataLoader()
        {
            CreateJsonIfNotExists();
            Users = new Dictionary<string, long>();
            Names = new Dictionary<string, int>();
            NamesZP = new Dictionary<string, int>();
            NameList = new List<string>();
            TokenBot = string.Empty;
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
        //сохранение изменений в списке сотрудников
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
            var rowЫ = dataGrid.Rows[0];
            rowЫ.Cells["ForwardChat"].Value = dataLoader.ForwardChat; // Вывод ForwardChat
            rowЫ.Cells["ChatId"].Value = dataLoader.ChatId; // Вывод ChatId
            rowЫ.Cells["TokenBot"].Value = dataLoader.TokenBot; // Вывод ChatId

        }
        public static void SaveButton_Click(DataGridView dataGrid)
        {
            UserDataLoader dataLoader = UserDataLoader.LoadFromFile();

            dataLoader.NameList.Clear();
            dataLoader.Users.Clear();
            dataLoader.Names.Clear();
            dataLoader.NamesZP.Clear();

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.IsNewRow) continue;

                var name = row.Cells["Name"].Value?.ToString();
                if (string.IsNullOrEmpty(name)) continue;

                dataLoader.NameList.Add(name);

                if (long.TryParse(row.Cells["Users"].Value?.ToString(), out long userValue))
                {
                    dataLoader.Users[name] = userValue;
                }

                if (int.TryParse(row.Cells["Names"].Value?.ToString(), out int nameValue))
                {
                    dataLoader.Names[name] = nameValue;
                }

                if (int.TryParse(row.Cells["NamesZP"].Value?.ToString(), out int nameZPValue))
                {
                    dataLoader.NamesZP[name] = nameZPValue;
                }
            }
            DataGridViewRow rows = dataGrid.Rows[0];
            if (long.TryParse(rows.Cells["ForwardChat"].Value?.ToString(), out long forwardChatValue))
            {
                dataLoader.ForwardChat = forwardChatValue;
            }
            if (long.TryParse(rows.Cells["ChatId"].Value?.ToString(), out long ChatIdValue))
            {
                dataLoader.ChatId = ChatIdValue;
            }
            string TokenBotValue = rows.Cells["TokenBot"].Value?.ToString();
            dataLoader.TokenBot = TokenBotValue;

            dataLoader.SaveToFile();
        }

    }
}