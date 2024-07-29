using ClosedXML.Excel;
using OfficeOpenXml.FormulaParsing.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Excel
{
    public partial class ExcelHelper
    {
        private static string folderPath = "\\\\192.168.88.254\\AVASMENAUpdate\\Needed\\excel";
        private static string patherSeyf = Path.Combine(folderPath, "seyf.xlsx");
        private static List<string> nameList;
        private static string token;
        private static ITelegramBotClient bot;

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
    }
}