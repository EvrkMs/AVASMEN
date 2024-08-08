using APIData;
using ClosedXML.Excel;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Design;
using Telegram.Bot;

namespace Excel
{
    public partial class ExcelHelper
    {
        private static readonly string folderPath = ConfigurationManager.AppSettings["ExcelFolder"];
        private static readonly string patherSeyf = Path.Combine(folderPath, "seyf.xlsx");
        private static ITelegramBotClient bot;

        public ExcelHelper()
        {
            LoadData();
            bot = new TelegramBotClient(DataStore.TokenBot);
        }
        private void LoadData()
        {
            DataStore.Initialize();
        }

        public static Task SeyfMinus(int plusSeyf)
        {
            try
            {
                using (var workbook = new XLWorkbook(patherSeyf))
                {
                    var worksheet = workbook.Worksheet("seyf");
                    string date = $"{DateTime.Now:dd/MM/HH}";

                    // Проверка существования строки с текущей датой
                    var existingRow = worksheet.RowsUsed().FirstOrDefault(row => row.Cell(1).GetString() == date);

                    if (existingRow != null)
                    {
                        existingRow.Cell(2).Value = plusSeyf;
                    }
                    else
                    {
                        int row = worksheet.LastRowUsed().RowNumber() + 1;
                        worksheet.Cell(row, 1).Value = date;
                        worksheet.Cell(row, 2).Value = plusSeyf;
                    }
                    worksheet.Cell(2, 3).FormulaA1 = $"=SUM(B:B)";

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public static int GetCellValueAsInt(string fileName, string sheetName)
        {
            return GetCellValue(fileName, sheetName);
        }

        public static int GetCellValue(string fileName, string sheetName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-KEY", ApiService.ApiKey);
                    string queryParams = $"fileName={fileName}&sheetName={sheetName}";
                    string responseContent = ApiService.GetApiResponse(client, "Excel/getcellvalue", queryParams);

                    var responseObject = JObject.Parse(responseContent);
                    if (responseObject["value"] != null) // Замена 'Value' на 'value'
                    {
                        return responseObject["value"].Value<int>(); // Корректный ключ в нижнем регистре
                    }
                    else
                    {
                        throw new Exception("Invalid response format");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1; // Или любое другое значение, указывающее на ошибку
            }
        }
        private static void AutoFitColumnsAndRows(IXLWorksheet worksheet)
        {
            worksheet.Columns().AdjustToContents(); // Автоматическое подстраивание ширины столбцов
            worksheet.Rows().AdjustToContents(); // Автоматическое подстраивание высоты строк
        }
    }
}