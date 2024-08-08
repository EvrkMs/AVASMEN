// ApiService
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net.Http;

namespace APIData
{
    public static class ApiService
    {
        public static readonly string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static readonly string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        public static void LoadData()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                LoadUserData(client);
                LoadSettingData(client);
            }
        }

        private static void LoadUserData(HttpClient client)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}/User").Result;
                response.EnsureSuccessStatusCode();
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                JArray usersArray = JArray.Parse(jsonResponse);

                foreach (var user in usersArray)
                {
                    string name = user["name"].ToString();
                    long telegramId = (long)user["telegramId"];
                    int count = (int)user["count"];

                    if (!DataStore.NameList.Contains(name))
                    {
                        DataStore.NameList.Add(name);
                    }
                    DataStore.Users[name] = telegramId;
                    DataStore.Names[name] = count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе данных User: {ex.Message}");
            }
        }

        private static void LoadSettingData(HttpClient client)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}/TelegramSettings/settings").Result;
                response.EnsureSuccessStatusCode();
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                JObject settings = JObject.Parse(jsonResponse);

                DataStore.TokenBot = settings["tokenBot"].ToString();
                DataStore.ForwardChat = (long)settings["forwardChat"];
                DataStore.ChatId = (long)settings["chatId"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе данных Setting: {ex.Message}");
            }
        }
    }
}