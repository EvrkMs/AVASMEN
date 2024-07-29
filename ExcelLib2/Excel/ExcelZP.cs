using ClosedXML.Excel;
using MaterialSkin.Controls;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    public partial class ExcelHelper
    {
        private static readonly string pather = Path.Combine(folderPath, "ZP.xlsx");

        public static Task ZPexcelОтчет(int zrp1, int zrp2, int zrp3, MaterialComboBox NameBox1, MaterialComboBox NameBox2, MaterialComboBox NameBox3, MaterialTextBox2 MinusBox, MaterialTextBox2 Minus3)
        {
            using (var workbook = new XLWorkbook(pather))
            {
                string name1 = NameBox1.Text;
                var worksheet1 = workbook.Worksheet(name1);
                string date = $"{DateTime.Now:dd/MM/HH}";

                // Проверка существования строки с текущей датой
                var existingRow1 = worksheet1.RowsUsed().FirstOrDefault(row => row.Cell(1).GetString() == date);

                if (existingRow1 != null)
                {
                    existingRow1.Cell(2).Value = zrp1;
                }
                else
                {
                    int emptyRowNameColl1 = worksheet1.LastRowUsed()?.RowNumber() + 1 ?? 1;
                    worksheet1.Cell(emptyRowNameColl1, 1).Value = date;
                    worksheet1.Cell(emptyRowNameColl1, 2).Value = zrp1;
                }
                worksheet1.Cell(2, 3).FormulaA1 = $"=SUM(B:B)";

                if (!MinusBox.Visible)
                {
                    workbook.Save();
                    return Task.CompletedTask;
                }

                string name2 = NameBox2.Text;
                var worksheet2 = workbook.Worksheet(name2);

                // Проверка существования строки с текущей датой
                var existingRow2 = worksheet2.RowsUsed().FirstOrDefault(row => row.Cell(1).GetString() == date);

                if (existingRow2 != null)
                {
                    existingRow2.Cell(2).Value = zrp2;
                }
                else
                {
                    int emptyRowNameColl2 = worksheet2.LastRowUsed()?.RowNumber() + 1 ?? 1;
                    worksheet2.Cell(emptyRowNameColl2, 1).Value = date;
                    worksheet2.Cell(emptyRowNameColl2, 2).Value = zrp2;
                }
                worksheet2.Cell(2, 3).FormulaA1 = $"=SUM(B:B)";

                if (!Minus3.Visible)
                {
                    workbook.Save();
                    return Task.CompletedTask;
                }

                string name3 = NameBox3.Text;
                var worksheet3 = workbook.Worksheet(name3);

                // Проверка существования строки с текущей датой
                var existingRow3 = worksheet3.RowsUsed().FirstOrDefault(row => row.Cell(1).GetString() == date);

                if (existingRow3 != null)
                {
                    existingRow3.Cell(2).Value = zrp3;
                }
                else
                {
                    int emptyRowNameColl3 = worksheet3.LastRowUsed()?.RowNumber() + 1 ?? 1;
                    worksheet3.Cell(emptyRowNameColl3, 1).Value = date;
                    worksheet3.Cell(emptyRowNameColl3, 2).Value = zrp3;
                }
                worksheet2.Cell(2, 3).FormulaA1 = $"=SUM(B:B)";

                workbook.Save();
            }

            return Task.CompletedTask;
        }

        private static void UpdateAvansRecord(XLWorkbook workbook, string name, int amount)
        {
            var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");
            var avansRow = worksheet.RowsUsed().FirstOrDefault(row => row.Cell(4).GetString() == $"{name} Аванс");

            if (avansRow != null)
            {
                // Извлекаем текущее значение и добавляем новое значение
                int currentAmount = avansRow.Cell(3).GetValue<int>();
                avansRow.Cell(3).Value = currentAmount + amount;
            }
            else
            {
                // Добавляем новую строку, если она не существует
                int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;
                worksheet.Cell(lastRow, 4).Value = $"{name} Аванс";
                worksheet.Cell(lastRow, 3).Value = amount;
            }
        }

        public static Task AvansMinus(int summ, string name, bool premia)
        {
            try
            {
                using (var workbook = new XLWorkbook(pather))
                {
                    var worksheet = workbook.Worksheet(name);
                    string date = $"{DateTime.Now:dd/MM/HH}";

                    int row = worksheet.LastRowUsed().RowNumber() + 1;
                    worksheet.Cell(row, 1).Value = date;
                    worksheet.Cell(row, 2).Value = summ;

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

        public static Task ЗаполнениеExcelInvent(int inventSum, ListBox listBoxNameInv)
        {
            using (var workbook = new XLWorkbook(pather))
            {
                foreach (var selectedItem in listBoxNameInv.SelectedItems)
                {
                    string name = selectedItem.ToString();
                    var worksheet = workbook.Worksheet(name);

                    string date = $"{DateTime.Now:dd/MM/HH}";

                    // Проверка существования строки с текущей датой
                    var existingRow = worksheet.RowsUsed().FirstOrDefault(row => row.Cell(1).GetString() == date);

                    if (existingRow != null)
                    {
                        existingRow.Cell(2).Value = inventSum;
                    }
                    else
                    {
                        int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;
                        worksheet.Cell(lastRow, 1).Value = date;
                        worksheet.Cell(lastRow, 2).Value = inventSum;
                    }
                    worksheet.Cell(2, 3).FormulaA1 = $"=SUM(B:B)";
                }

                workbook.Save();
            }

            return ScreenExcel(pather);
        }


        public static int GetCellValueAsInt(string sheetName)
        {
            using (var workbook = new XLWorkbook(pather))
            {
                var worksheet = workbook.Worksheet(sheetName);
                if (worksheet == null)
                    throw new ArgumentException($"Sheet with name {sheetName} does not exist.");

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
