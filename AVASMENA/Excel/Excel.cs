using APIData;
using ClosedXML.Excel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Excel
{
    public partial class ExcelHelper
    {
        private static readonly string folderPath = "\\\\192.168.88.254\\AVASMENAUpdate\\Needed\\excel";
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
            if (fileName == "ZP")
                fileName = pather;
            else if(fileName == "s")
                fileName = patherSeyf;
            else
                fileName = filePath;

            using (var workbook = new XLWorkbook(fileName))
            {
                var worksheet = workbook.Worksheet(sheetName) ?? throw new ArgumentException($"Sheet with name {sheetName} does not exist.");
                var cell = worksheet.Cell(2, 3);
                var cellValue = cell.GetValue<string>(); // Получаем значение ячейки как строку

                if (int.TryParse(cellValue, out int result))
                {
                    return result;
                }
                else if (double.TryParse(cellValue, out double doubleResult))
                {
                    return (int)doubleResult; // Преобразуем из double в int
                }
                else
                {
                    throw new InvalidDataException($"Cell at row {2}, column {3} does not contain a valid integer.");
                }
            }
        }

    }
}