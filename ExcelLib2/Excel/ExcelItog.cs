using ClosedXML.Excel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Excel
{
    public partial class ExcelHelper
    {
        private static readonly string filePath = Path.Combine(folderPath, "itog.xlsx");

        public static Task UpdateExcel(int viruchka)
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");

                    string shift = DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 21 ? "ночная" : "дневная";
                    string date = $"{DateTime.Now:dd} {shift}";

                    var existingRow = worksheet.RowsUsed().Skip(1).FirstOrDefault(row => row.Cell(1).GetString() == date);

                    if (existingRow != null)
                    {
                        existingRow.Cell(2).Value = viruchka;
                    }
                    else
                    {
                        int row = worksheet.LastRowUsed().RowNumber() + 1;
                        worksheet.Cell(row, 1).Value = date;
                        worksheet.Cell(row, 2).Value = viruchka;
                    }
                    AutoFitColumnsAndRows(worksheet);
                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                // Логирование ошибки
                Logger.Log($"Ошибка в методе UpdateExcel: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public static Task AddRecordToExcel(int amount, string comment, bool isAvans, string name)
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");
                    string date = $"{DateTime.Now:dd HH:mm}";

                    // Проверка существования строки с текущей датой
                    var existingRow = worksheet.RowsUsed().FirstOrDefault(row => row.Cell(1).GetString() == date);
                    if (!isAvans)
                    {
                        if (existingRow != null)
                        {
                            existingRow.Cell(3).Value = amount;
                            existingRow.Cell(4).Value = comment;
                        }
                        else
                        {
                            int row = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;
                            worksheet.Cell(row, 1).Value = date;
                            worksheet.Cell(row, 3).Value = amount;
                            worksheet.Cell(row, 4).Value = comment;
                        }
                    }

                    // Обновление авансов для указанного имени
                    if (isAvans)
                    {
                        amount *= -1;
                        UpdateAvansRecord(workbook, name, amount);
                    }
                    AutoFitColumnsAndRows(worksheet);

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
