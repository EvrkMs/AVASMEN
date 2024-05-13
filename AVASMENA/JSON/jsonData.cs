using System;
using System.Collections.Generic;
using System.IO;
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

        public UserDataLoader()
        {
            Users = new Dictionary<string, long>();
            Names = new Dictionary<string, int>();
            NamesZP = new Dictionary<string, int>();
            NameList = new List<string>();
        }

        public static UserDataLoader LoadFromFile(string filePath)
        {
            try
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<UserDataLoader>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                return new UserDataLoader();
            }
        }
    }
}
