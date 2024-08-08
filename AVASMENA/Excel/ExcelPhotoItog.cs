using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Excel
{
    public partial class ExcelHelper
    {
        //метод для запуска скрина основной таблцы
        public static async Task ScreenExcel()
        {
            string path = filePath;
            Logger.Log("Метод ScreenExcel вызван.");

            try
            {
                Logger.Log($"Проверка существования файла: {path}");
                if (!System.IO.File.Exists(path))
                {
                    Logger.Log($"Файл не найден: {path}");
                    return;
                }

                string dataImagePath = Path.Combine(Path.GetTempPath(), "all_image.png");
                string chartImagePath = Path.Combine(Path.GetTempPath(), "chart_image.png");
                string avansTablePath;
                string categoryValueTablePath;
                string dnevnaya;
                string rashod;

                using (var workbook = new XLWorkbook(path))
                {
                    var worksheet = workbook.Worksheets.LastOrDefault();
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
                    categoryValueTablePath = SaveTableAsImage(worksheet.Range("E1:G4").RangeUsed(), "category_value_table.png");
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
        //метод для создания фото диаграммы
        private static void DrawPieChart(Graphics graphics, string[] categories, float[] values, Rectangle rect)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Log($"Ошибка метода создания фото диограммы:\n {ex}");
            }
        }
        //фото таблицы с отсеиванем пустых строк
        private static string SaveTableAsImage(IXLRange range, string fileName)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Log($"Ошибка метода создания фото таблицы:\n {ex}");
            }
            return ("ошибка с созранением фотографии таблиц в методе SaveTableAsImage");
        }
        //метод для придания цета в фотографии графика
        private static Color XLColorToColor(XLColor xlColor)
        {
            return Color.FromArgb(xlColor.Color.R, xlColor.Color.G, xlColor.Color.B);
        }
        //отправка фотографии в телеграмме
        private static async Task SendPhotosInOneMessage(string[] photoPaths)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Log($"Ошибка отпрвки фотографий таблицы:\n{ex}");
            }
        }
    }
}
