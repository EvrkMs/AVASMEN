using ClosedXML.Excel;
using jsonData;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelegramCode;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace Excel
{
    public static class ExcelHelper
    {
        private static readonly string folderPath = "\\\\192.168.88.254\\AVASMENAUpdate\\Needed\\excel";
        private static readonly string filePath = Path.Combine(folderPath, "itog.xlsx");
        private static readonly string pather = Path.Combine(folderPath, "ZP.xlsx");
        private static readonly string patherSeyf = Path.Combine(folderPath, "seyf.xlsx");
        private static readonly List<string> nameList = UserDataLoader.LoadFromFile().NameList;

        public static Task ExcelCreated()
        {
            EnsureDirectoryExists(folderPath);
            CreateExcelFileIfNotExists(pather, nameList);
            CreateSeyfFileIfNotExists(patherSeyf);
            CreateMonthlyFileIfNotExists(filePath);
            EnsureWorksheetExists(filePath, $"{DateTime.Now.Year}.{DateTime.Now:MM}");
            EnsureWorksheetExists(patherSeyf, "seyf");
            EnsureWorksheetExistsName(pather, nameList);

            return Task.CompletedTask;
        }

        private static void EnsureWorksheetExistsName(string path, List<string> sheetNames)
        {
            foreach (var sheetName in sheetNames)
            {
                using (var workbook = new XLWorkbook(path))
                {
                    if (!workbook.Worksheets.TryGetWorksheet(sheetName, out var worksheet))
                    {
                        worksheet = workbook.Worksheets.Add(sheetName);
                        if (sheetName.Contains("."))
                        {
                            SetupMonthlyWorksheetHeaders(worksheet);
                        }
                        else
                        {
                            SetupSeyfAndZpWorksheet(worksheet);
                        }
                        workbook.Save();
                    }
                }
            }

        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void CreateExcelFileIfNotExists(string path, List<string> names)
        {
            if (!File.Exists(path))
            {
                using (var workbook = new XLWorkbook())
                {
                    foreach (var name in names)
                    {
                        var worksheet = workbook.Worksheets.Add(name);
                        SetupSeyfAndZpWorksheet(worksheet);
                    }
                    workbook.SaveAs(path);
                }
            }
        }

        private static void CreateSeyfFileIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("seyf");
                    SetupSeyfAndZpWorksheet(worksheet);
                    workbook.SaveAs(path);
                }
            }
        }

        private static void SetupSeyfAndZpWorksheet(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "дата";
            worksheet.Cell(1, 2).Value = "суммы";
            worksheet.Cell(1, 3).Value = "сумма";
            worksheet.Cell(2, 2).Value = 0;
            worksheet.Cell(2, 3).FormulaA1 = "=SUM(B:B)";
        }

        private static void CreateMonthlyFileIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add($"{DateTime.Now.Year}.{DateTime.Now:MM}");
                    SetupMonthlyWorksheetHeaders(worksheet);
                    workbook.SaveAs(path);
                }
            }
        }

        private static void SetupMonthlyWorksheetHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "Дата и время";
            worksheet.Cell(1, 2).Value = "итоги дней";
            worksheet.Cell(1, 3).Value = "выручка дней";
            worksheet.Cell(1, 4).Value = "расходы";
            worksheet.Cell(1, 5).Value = "_";
            worksheet.Cell(1, 6).Value = "Выручка за месяц";
            worksheet.Cell(1, 7).Value = "Расходы";
            worksheet.Cell(1, 8).Value = "Итог";

            worksheet.Cell(2, 6).FormulaA1 = "=SUM(C:C)";
            worksheet.Cell(2, 7).FormulaA1 = "=SUM(D:D)";
            worksheet.Cell(2, 8).FormulaA1 = "=F2-G2";

            int row = 2;
            int zpColum = 4;
            int nameColumn = 5;
        }

        private static void EnsureWorksheetExists(string path, string sheetName)
        {
            using (var workbook = new XLWorkbook(path))
            {
                if (!workbook.Worksheets.TryGetWorksheet(sheetName, out var worksheet))
                {
                    worksheet = workbook.Worksheets.Add(sheetName);
                    if (sheetName.Contains("."))
                    {
                        SetupMonthlyWorksheetHeaders(worksheet);
                    }
                    else
                    {
                        SetupSeyfAndZpWorksheet(worksheet);
                    }
                    workbook.Save();
                }
            }
        }

        public static Task UpdateExcel(int itog, int viruchka)
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
                        existingRow.Cell(2).Value = itog;
                        existingRow.Cell(3).Value = viruchka;
                    }
                    else
                    {
                        int row = worksheet.LastRowUsed().RowNumber() + 1;
                        worksheet.Cell(row, 1).Value = date;
                        worksheet.Cell(row, 2).Value = itog;
                        worksheet.Cell(row, 3).Value = viruchka;
                    }

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public static Task ZPexcelОтчет(int zrp1, int zrp2, MaterialComboBox NameBox1, MaterialComboBox NameBox2, MaterialTextBox2 MinusBox)
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

                workbook.Save();
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
                            existingRow.Cell(4).Value = amount;
                            existingRow.Cell(5).Value = comment;
                        }
                        else
                        {
                            int row = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;
                            worksheet.Cell(row, 1).Value = date;
                            worksheet.Cell(row, 4).Value = amount;
                            worksheet.Cell(row, 5).Value = comment;
                        }
                    }

                    // Обновление авансов для указанного имени
                    if (isAvans)
                    {
                        amount *= -1;
                        UpdateAvansRecord(workbook, name, amount);
                    }

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return Task.CompletedTask;
        }
        private static void UpdateAvansRecord(XLWorkbook workbook, string name, int amount)
        {
            var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");
            var avansRow = worksheet.RowsUsed().FirstOrDefault(row => row.Cell(5).GetString() == $"{name} Аванс");

            if (avansRow != null)
            {
                // Извлекаем текущее значение и добавляем новое значение
                int currentAmount = avansRow.Cell(4).GetValue<int>();
                avansRow.Cell(4).Value = currentAmount + amount;
            }
            else
            {
                // Добавляем новую строку, если она не существует
                int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;
                worksheet.Cell(lastRow, 5).Value = $"{name} Аванс";
                worksheet.Cell(lastRow, 4).Value = amount;
            }
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


        public static async Task ScreenExcel(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Файл не найден: {path}");
                    return;
                }

                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheets.LastOrDefault();
                    if (worksheet == null)
                    {
                        Console.WriteLine("Не удалось найти рабочий лист.");
                        return;
                    }

                    int width = worksheet.ColumnsUsed().Count();
                    int height = worksheet.RowsUsed().Count();
                    Font cellFont = new Font("Arial", 12);
                    int[] columnWidths = new int[width];

                    using (Bitmap tempBitmap = new Bitmap(1, 1))
                    using (Graphics tempGraphics = Graphics.FromImage(tempBitmap))
                    {
                        for (int col = 1; col <= width; col++)
                        {
                            int maxWidth = 0;
                            for (int row = 1; row <= height; row++)
                            {
                                string cellValue = worksheet.Cell(row, col).GetString();
                                SizeF textSize = tempGraphics.MeasureString(cellValue, cellFont);
                                maxWidth = Math.Max(maxWidth, (int)textSize.Width);
                            }
                            columnWidths[col - 1] = maxWidth + 10;
                        }
                    }

                    int totalWidth = columnWidths.Sum();
                    int cellHeight = new Font(cellFont.FontFamily, cellFont.Size, cellFont.Style).Height + 10;
                    int totalHeight = height * cellHeight;

                    using (Bitmap screenshot = new Bitmap(totalWidth, totalHeight))
                    using (Graphics graphics = Graphics.FromImage(screenshot))
                    {
                        graphics.Clear(Color.White);

                        using (Font font = new Font("Arial", 12))
                        {
                            int xOffset = 0;
                            for (int col = 1; col <= width; col++)
                            {
                                int yOffset = 0;
                                for (int row = 1; row <= height; row++)
                                {
                                    string cellValue = worksheet.Cell(row, col).GetString();
                                    RectangleF cellRect = new RectangleF(xOffset, yOffset, columnWidths[col - 1], cellHeight);
                                    graphics.DrawString(cellValue, font, Brushes.Black, cellRect);
                                    yOffset += cellHeight;
                                }
                                xOffset += columnWidths[col - 1];
                            }
                        }

                        string screenshotPath = Path.Combine(folderPath, "excel_table_screenshot.png");
                        screenshot.Save(screenshotPath, ImageFormat.Png);
                        Console.WriteLine("Скриншот таблицы успешно сохранен.");
                        await Telegrame.PhotoExcel(screenshotPath);
                        Console.WriteLine("Скриншот таблицы успешно отправлен в Telegram.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static Task ExcelViewer(DataGridView dataGridView, ComboBox comboBox, int i)
        {
            string path = GetPathByIndex(i);

            if (dataGridView.DataSource != null)
            {
                dataGridView.DataSource = null;
            }

            DataTable dt = new DataTable();

            using (var workbook = new XLWorkbook(path))
            {
                var worksheet = workbook.Worksheet(comboBox.SelectedItem.ToString());
                bool columnsInitialized = false;

                foreach (var row in worksheet.RowsUsed())
                {
                    if (!columnsInitialized)
                    {
                        foreach (var cell in row.Cells())
                        {
                            string columnName = cell.GetString();
                            dt.Columns.Add(dt.Columns.Contains(columnName) ? $"{columnName}_{dt.Columns.Count}" : columnName);
                        }
                        columnsInitialized = true;
                    }
                    else
                    {
                        var newRow = dt.NewRow();
                        int colIndex = 0;

                        foreach (var cell in row.CellsUsed())
                        {
                            while (colIndex < cell.Address.ColumnNumber - 1)
                            {
                                newRow[colIndex] = "_";
                                colIndex++;
                            }
                            newRow[colIndex] = cell.GetString();
                            colIndex++;
                        }

                        while (colIndex < dt.Columns.Count)
                        {
                            newRow[colIndex] = "_";
                            colIndex++;
                        }

                        dt.Rows.Add(newRow);
                    }
                }
            }

            Console.WriteLine($"Columns in DataTable: {dt.Columns.Count}");
            foreach (DataColumn column in dt.Columns)
            {
                Console.WriteLine($"Column: {column.ColumnName}");
            }

            dataGridView.DataSource = dt;
            dataGridView.ForeColor = Color.Black;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            return Task.CompletedTask;
        }

        public static Task SaveDataToExcel(string selectedSheetName, DataGridView dataGridViewExcel, int i)
        {
            string path = GetPathByIndex(i);

            if (!string.IsNullOrEmpty(selectedSheetName) && dataGridViewExcel.Rows.Count > 0)
            {
                try
                {
                    using (var workbook = new XLWorkbook(path))
                    {
                        // Получаем лист по имени
                        var worksheet = workbook.Worksheet(selectedSheetName);

                        for (int columnIndex = 0; columnIndex < dataGridViewExcel.Columns.Count; columnIndex++)
                        {
                            worksheet.Cell(1, columnIndex + 1).Value = dataGridViewExcel.Columns[columnIndex].HeaderText;
                        }

                        for (int rowIndex = 0; rowIndex < dataGridViewExcel.Rows.Count; rowIndex++)
                        {
                            for (int columnIndex = 0; columnIndex < dataGridViewExcel.Columns.Count; columnIndex++)
                            {
                                var cellValue = dataGridViewExcel.Rows[rowIndex].Cells[columnIndex].Value;
                                if (cellValue != null)
                                {
                                    // Проверка на числовое значение и установка формата для столбца B в seyf и ZP
                                    if ((i == 1 || i == 3) && columnIndex == 1)
                                    {
                                        if (int.TryParse(cellValue.ToString(), out int intValue))
                                        {
                                            worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(intValue);
                                        }
                                        else
                                        {
                                            worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(cellValue.ToString());
                                        }
                                    }
                                    // Проверка на числовое значение и установка формата для столбцов C и D в itog.xlsx
                                    else if (i == 0 && (columnIndex == 2 || columnIndex == 3))
                                    {
                                        if (int.TryParse(cellValue.ToString(), out int intValue))
                                        {
                                            worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(intValue);
                                        }
                                        else
                                        {
                                            worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(cellValue.ToString());
                                        }
                                    }
                                    else
                                    {
                                        worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(cellValue.ToString());
                                    }
                                }
                            }
                        }

                        if (i == 0)
                        {
                            worksheet.Cell(2, 6).FormulaA1 = $"=SUM(C:C)";
                            worksheet.Cell(2, 7).FormulaA1 = $"=SUM(D:D)";
                            worksheet.Cell(2, 8).FormulaA1 = "=F2-G2";
                        }
                        else if (i == 1 || i == 3)
                        {
                            worksheet.Cell(2, 3).FormulaA1 = $"=SUM(B:B)";
                        }

                        workbook.SaveAs(path);
                    }

                    MessageBox.Show("Данные успешно сохранены в лист Excel.", "Сохранение завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных в Excel: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите лист Excel и убедитесь, что есть данные для сохранения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return Task.CompletedTask;
        }

        public static Task LoadGrindSheet(DataGridView dataGrid, string selectedSheetName, int i)
        {
            string path = GetPathByIndex(i);

            using (var workbook = new XLWorkbook(path))
            {
                var worksheet = workbook.Worksheet(selectedSheetName);
                dataGrid.DataSource = worksheet.RangeUsed().AsTable();
                dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            return Task.CompletedTask;
        }

        public static Task LoadSheet(MaterialComboBox ComBox, int i)
        {
            string path = GetPathByIndex(i);

            List<string> sheetNames = new List<string>();

            using (var workbook = new XLWorkbook(path))
            {
                sheetNames.AddRange(workbook.Worksheets.Select(worksheet => worksheet.Name));
            }

            ComBox.DataSource = sheetNames;

            return Task.CompletedTask;
        }

        private static string GetPathByIndex(int index)
        {
            switch (index)
            {
                case 0: return filePath;
                case 1: return pather;
                case 3: return patherSeyf;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}