using ClosedXML.Excel;
using jsonData;
using MaterialSkin.Controls;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
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
        private static readonly string token = UserDataLoader.LoadFromFile().TokenBot;
        private static readonly ITelegramBotClient bot = new TelegramBotClient(token);

        public static Task ExcelCreated()
        {
            EnsureDirectoryExists(folderPath);
            CreateExcelFileIfNotExists(pather, nameList);
            CreateSeyfFileIfNotExists(patherSeyf);
            CreateMonthlyFileIfNotExists(filePath, $"{DateTime.Now.Year}.{DateTime.Now:MM}");
            EnsureWorksheetExists(filePath, $"{DateTime.Now.Year}.{DateTime.Now:MM}", true); // Передаем true для создания таблицы
            EnsureWorksheetExists(patherSeyf, "seyf", true); // Передаем true для создания таблицы
            EnsureWorksheetExistsName(pather, nameList);

            return Task.CompletedTask;
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
            if (!System.IO.File.Exists(path))
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
            if (!System.IO.File.Exists(path))
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("seyf");
                    SetupSeyfAndZpWorksheet(worksheet);
                    workbook.SaveAs(path);
                }
            }
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
                        SetupSeyfAndZpWorksheet(worksheet);
                        workbook.Save();
                    }
                }
            }
        }
        private static void SetupMonthlyWorksheetHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "Дата и время";
            worksheet.Cell(1, 2).Value = "выручка дней";
            worksheet.Cell(1, 3).Value = "расходы";
            worksheet.Cell(1, 4).Value = "_";
            worksheet.Cell(1, 5).Value = "Категория";
            worksheet.Cell(1, 6).Value = "Значение";
            worksheet.Cell(1, 7).Value = "Выручка месяц";
            worksheet.Cell(2, 7).FormulaA1 = "=SUM(B:B)";
            worksheet.Cell(2, 1).Value = "Доход по отчётам";

            worksheet.Cell(2, 5).Value = "Расходы";
            worksheet.Cell(2, 6).FormulaA1 = "=SUM(C65:C10000)"; // Расходы начинаются с 65 строки и далее
            worksheet.Cell(3, 5).Value = "Авансы";
            worksheet.Cell(3, 6).FormulaA1 = "=SUM(C3:C64)";
            worksheet.Cell(4, 5).Value = "Итог";
            worksheet.Cell(4, 6).FormulaA1 = "=G2-F2-F3";

            int row = 3; // Начальная строка для добавления дней
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                string dayString = day.ToString("D2"); // Форматирование дня в двухзначный формат
                worksheet.Cell(row, 1).Value = $"{dayString} ночная";
                row++;
                worksheet.Cell(row, 1).Value = $"{dayString} дневная";
                row++;
            }
            worksheet.Cell(65, 1).Value = "Выписанные расходы";

            int rowAvans = 3; // Начальная строка для добавления авансов
            int avansColumn = 3;
            int nameColumn = 4;
            foreach (var name in nameList)
            {
                worksheet.Cell(rowAvans, avansColumn).Value = 0;
                var cell = worksheet.Cell(rowAvans, nameColumn);
                cell.Value = $"{name} Аванс";
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                rowAvans += 1;
            }

            // Применение цвета к диапазонам ячеек
            worksheet.Range("A1:J1").Style.Fill.BackgroundColor = XLColor.Green;
            worksheet.Range("A2:B2").Style.Fill.BackgroundColor = XLColor.Green;
            worksheet.Range("A3:B64").Style.Fill.BackgroundColor = XLColor.LightBlue;
            worksheet.Range("A65:D100").Style.Fill.BackgroundColor = XLColor.LightPink;
            worksheet.Range("C3:D12").Style.Fill.BackgroundColor = XLColor.LightPink;

            // Автоматически подстраиваем высоту строк
            AutoFitColumnsAndRows(worksheet);
        }

        private static void CreateChartWithEPPlus(string filePath, string sheetName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Установите лицензию на некоммерческое использование
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];

                // Удаление существующих диаграмм с таким же именем, если они существуют
                var existingChart = worksheet.Drawings.FirstOrDefault(d => d.Name == "Круговая диаграмма");
                if (existingChart != null)
                {
                    worksheet.Drawings.Remove(existingChart);
                }

                var chart = worksheet.Drawings.AddChart("Круговая диаграмма", eChartType.Pie) as ExcelPieChart;
                chart.Title.Text = "Категория и Значение";
                chart.Series.Add(worksheet.Cells["F2:F4"], worksheet.Cells["E2:E4"]);
                chart.SetPosition(5, 0, 5, 0); // Позиционируем диаграмму на F6
                chart.SetSize(600, 400); // Устанавливаем размер диаграммы

                // Настройка меток данных для отображения процентов
                foreach (var series in chart.Series.Cast<ExcelPieChartSerie>())
                {
                    series.DataLabel.ShowPercent = true;
                }

                package.Save();
            }
        }
        private static void AutoFitColumnsAndRows(IXLWorksheet worksheet)
        {
            worksheet.Columns().AdjustToContents(); // Автоматическое подстраивание ширины столбцов
            worksheet.Rows().AdjustToContents(); // Автоматическое подстраивание высоты строк
        }
        private static void CreateMonthlyFileIfNotExists(string path, string sheetName)
        {
            if (!System.IO.File.Exists(path))
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(sheetName);
                    SetupMonthlyWorksheetHeaders(worksheet);
                    workbook.SaveAs(path);
                }

                // Создание диаграммы с использованием EPPlus
                CreateChartWithEPPlus(path, sheetName);
            }
        }
        private static void EnsureWorksheetExists(string path, string sheetName, bool isCreating)
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
                    workbook.SaveAs(path);
                }
            }

            // Создание диаграммы с использованием EPPlus
            if (isCreating)
            {
                CreateChartWithEPPlus(path, sheetName);
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
            Logger.Log("Метод ScreenExcel вызван.");

            try
            {
                Logger.Log($"Проверка существования файла: {path}");
                if (!System.IO.File.Exists(path))
                {
                    Logger.Log($"Файл не найден: {path}");
                    return;
                }

                string dataImagePath = Path.Combine(Path.GetTempPath(), "data_image.png");
                string chartImagePath = Path.Combine(Path.GetTempPath(), "chart_image.png");
                string avansTablePath;
                string categoryValueTablePath;
                string dnevnaya;
                string rashod;

                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        Logger.Log("Не удалось найти рабочий лист.");
                        return;
                    }

                    Logger.Log("Начинается сохранение данных рабочего листа в изображение.");
                    // Сохранение данных рабочего листа в изображение
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
                                string cellValue = worksheet.Cell(row, col).GetFormattedString();
                                SizeF textSize = tempGraphics.MeasureString(cellValue, cellFont);
                                maxWidth = Math.Max(maxWidth, (int)textSize.Width);
                            }
                            columnWidths[col - 1] = maxWidth + 10;
                        }
                    }

                    int totalWidth = columnWidths.Sum();
                    int cellHeight = new Font(cellFont.FontFamily, cellFont.Size, cellFont.Style).Height + 10;
                    int totalHeight = height * cellHeight;

                    using (Bitmap dataBitmap = new Bitmap(totalWidth, totalHeight))
                    using (Graphics graphics = Graphics.FromImage(dataBitmap))
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
                                    var cell = worksheet.Cell(row, col);
                                    string cellValue = cell.GetFormattedString();
                                    var cellRect = new RectangleF(xOffset, yOffset, columnWidths[col - 1], cellHeight);

                                    // Закрашиваем ячейку, если есть заливка
                                    if (cell.Style.Fill.BackgroundColor.ColorType == XLColorType.Color)
                                    {
                                        var backgroundColor = XLColorToColor(cell.Style.Fill.BackgroundColor);
                                        using (Brush brush = new SolidBrush(backgroundColor))
                                        {
                                            graphics.FillRectangle(brush, cellRect);
                                        }
                                    }

                                    graphics.DrawString(cellValue, font, Brushes.Black, cellRect);
                                    yOffset += cellHeight;
                                }
                                xOffset += columnWidths[col - 1];
                            }
                        }

                        dataBitmap.Save(dataImagePath, ImageFormat.Png);
                        Logger.Log("Скриншот данных таблицы успешно сохранен.");
                    }

                    Logger.Log("Создание и сохранение изображения диаграммы.");
                    // Создание и отправка изображения диаграммы
                    var categoryRange = worksheet.Range("E2:E4");
                    var valueRange = worksheet.Range("F2:F4");

                    string[] categories = categoryRange.Cells().Select(cell => cell.GetString()).ToArray();
                    float[] values = valueRange.Cells().Select(cell =>
                    {
                        if (float.TryParse(cell.GetString(), out float result))
                        {
                            return result;
                        }
                        else
                        {
                            Logger.Log($"Не удалось преобразовать значение '{cell.GetString()}' в число.");
                            return 0; // Или выберите другой способ обработки некорректных данных
                        }
                    }).ToArray();

                    var chartBitmap = new Bitmap(600, 400);
                    using (var chartGraphics = Graphics.FromImage(chartBitmap))
                    {
                        chartGraphics.Clear(Color.White);

                        // Рисуем круговую диаграмму
                        DrawPieChart(chartGraphics, categories, values, new Rectangle(50, 50, 500, 300));
                    }

                    chartBitmap.Save(chartImagePath, ImageFormat.Png);
                    Logger.Log("Скриншот диаграммы успешно сохранен.");

                    // Сохранение изображений таблиц
                    Logger.Log("Сохранение изображений таблиц.");
                    avansTablePath = SaveTableAsImage(worksheet.Range("C3:D64").RangeUsed(), "avans_table.png");
                    categoryValueTablePath = SaveTableAsImage(worksheet.Range("E1:F4").RangeUsed(), "category_value_table.png");
                    dnevnaya = SaveTableAsImage(worksheet.Range("A1:B64").RangeUsed(), "dnevnaya.png");
                    rashod = SaveTableAsImage(worksheet.Range("A65:E700").RangeUsed(), "rashod.png");

                    // Отправка всех изображений в виде альбома
                    Logger.Log("Отправка всех изображений в виде альбома.");
                    await bot.SendTextMessageAsync(-1002198769956, $"{DateTime.Now: yyyy.MM.dd}", replyToMessageId: 27);
                    await SendPhotosInOneMessage(new[] { dataImagePath, dnevnaya, rashod, avansTablePath, categoryValueTablePath, chartImagePath });
                    Logger.Log("Изображения успешно отправлены.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Произошла ошибка: {ex.Message}");
                Logger.Log(ex.StackTrace);
            }
        }

        private static void DrawPieChart(Graphics graphics, string[] categories, float[] values, Rectangle rect)
        {
            if (categories.Length != values.Length || categories.Length == 0) return;

            float total = values.Sum();
            float[] angles = values.Select(v => v / total * 360).ToArray();
            Color[] colors = { Color.Red, Color.Green, Color.Blue };

            float startAngle = 0;
            for (int i = 0; i < angles.Length; i++)
            {
                graphics.FillPie(new SolidBrush(colors[i % colors.Length]), rect, startAngle, angles[i]);
                startAngle += angles[i];
            }

            // Рисуем легенду
            for (int i = 0; i < categories.Length; i++)
            {
                graphics.FillRectangle(new SolidBrush(colors[i % colors.Length]), 10, 10 + i * 20, 10, 10);
                float percentage = (values[i] / total) * 100;
                graphics.DrawString($"{categories[i]} ({percentage:F2}%)", new Font("Arial", 12), Brushes.Black, 30, 10 + i * 20);
            }
        }

        private static string SaveTableAsImage(IXLRange range, string fileName)
        {
            int width = range.ColumnCount();
            int height = range.RowCount();  // Используем только используемые строки
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
                        string cellValue = range.Cell(row, col).GetFormattedString();
                        SizeF textSize = tempGraphics.MeasureString(cellValue, cellFont);
                        maxWidth = Math.Max(maxWidth, (int)textSize.Width);
                    }
                    columnWidths[col - 1] = maxWidth + 10;
                }
            }

            int totalWidth = columnWidths.Sum();
            int cellHeight = new Font(cellFont.FontFamily, cellFont.Size, cellFont.Style).Height + 10;
            int totalHeight = height * cellHeight;

            using (Bitmap tableBitmap = new Bitmap(totalWidth, totalHeight))
            using (Graphics graphics = Graphics.FromImage(tableBitmap))
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
                            var cell = range.Cell(row, col);
                            string cellValue = cell.GetFormattedString();
                            var cellRect = new RectangleF(xOffset, yOffset, columnWidths[col - 1], cellHeight);

                            // Закрашиваем ячейку, если есть заливка
                            if (cell.Style.Fill.BackgroundColor.ColorType == XLColorType.Color)
                            {
                                var backgroundColor = XLColorToColor(cell.Style.Fill.BackgroundColor);
                                using (Brush brush = new SolidBrush(backgroundColor))
                                {
                                    graphics.FillRectangle(brush, cellRect);
                                }
                            }

                            graphics.DrawString(cellValue, font, Brushes.Black, cellRect);
                            yOffset += cellHeight;
                        }
                        xOffset += columnWidths[col - 1];
                    }
                }

                string imagePath = Path.Combine(Path.GetTempPath(), fileName);
                tableBitmap.Save(imagePath, ImageFormat.Png);
                return imagePath;
            }
        }

        private static Color XLColorToColor(XLColor xlColor)
        {
            return Color.FromArgb(xlColor.Color.R, xlColor.Color.G, xlColor.Color.B);
        }

        private static async Task SendPhotosInOneMessage(string[] photoPaths)
        {
            var mediaGroup = new List<IAlbumInputMedia>();

            foreach (var path in photoPaths)
            {
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var inputFile = new InputMedia(stream, Path.GetFileName(path));
                mediaGroup.Add(new InputMediaPhoto(inputFile));
            }

            await bot.SendMediaGroupAsync(new ChatId(-1002198769956), mediaGroup, replyToMessageId: 27);
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