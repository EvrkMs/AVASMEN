// ApiService
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

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
        public static string GetApiResponse(HttpClient client, string endpoint, string queryParams)
        {
            string url = $"{ApiBaseUrl}/{endpoint}?{queryParams}";
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request to {url} failed with status code {response.StatusCode}");
            }

            return response.Content.ReadAsStringAsync().Result;
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
        public static bool SendStartupRequest(List<string> nameList)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-KEY", ApiService.ApiKey);

                    // Сериализация nameList в JSON
                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(nameList);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync($"{ApiService.ApiBaseUrl}/admin/startup", content).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                    }

                    // Чтение ответа от сервера
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    if (responseBody.Contains("Excel file processed successfully."))
                    {
                        Console.WriteLine("Startup notification sent and processed successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Unexpected response from server.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending startup notification: {ex.Message}");
                return false;
            }
        }
    }
}