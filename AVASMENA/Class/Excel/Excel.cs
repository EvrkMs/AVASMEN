// Excel.cs
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

namespace Excel
{
    public static class ExcelHelper
    {
        private static readonly string folderPath = "\\\\192.168.88.254\\AVASMENAUpdate\\Needed\\excel";
        private static readonly string filePath = $"{folderPath}\\itog.xlsx";
        private static readonly string pather = $"{folderPath}\\ZP.xlsx";
        private static readonly string patherSeyf = $"{folderPath}\\seyf.xlsx";
        private static readonly Dictionary<string, int> nameZP = UserDataLoader.LoadFromFile().NamesZP;
        public static Task UpdateExcel(int itog, int viruchka)
        {
            try
            {
                // Открытие существующего файла Excel
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");

                    // Определение смены
                    string shift = DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 21 ? "ночная" : "дневная";
                    // Добавление информации о смене к дате
                    string date = $"{DateTime.Now: dd} {shift}";

                    // Поиск существующей строки с аналогичной датой и временем (без секунд)
                    var existingRow = worksheet.RowsUsed().Skip(1).FirstOrDefault(row => row.Cell(1).Value.ToString() == date);

                    if (existingRow != null)
                    {
                        // Обновление информации в существующей строке
                        existingRow.Cell(1).Value = date;
                        existingRow.Cell(2).Value = itog;
                        existingRow.Cell(3).Value = viruchka;
                    }
                    else
                    {
                        // Поиск первой пустой строки
                        int row = worksheet.LastRowUsed().RowNumber() + 1;

                        // Добавление данных в новую строку
                        worksheet.Cell(row, 1).Value = date;
                        worksheet.Cell(row, 2).Value = itog;
                        worksheet.Cell(row, 3).Value = viruchka;
                    }

                    // Сохранение файла Excel
                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем и выводим ошибку
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return Task.CompletedTask;
        }
        //Заполнение ZP excelq
        public static Task ZPexcelОтчет(int zrp1, int zrp2, MaterialComboBox NameBox1, MaterialComboBox NameBox2, MaterialTextBox2 MinusBox)
        {
            using (var workbook = new XLWorkbook(pather))
            {
                var worksheet = workbook.Worksheets.LastOrDefault();
                // Получаем имя из NameBox1
                string name1 = NameBox1.Text;

                // Проверяем, есть ли такое имя в словаре nameZP
                if (nameZP.TryGetValue(name1, out int nameColl1))
                {

                    // Находим следующую пустую строку в столбце NameColl1
                    int emptyRowNameColl1 = worksheet.Column(nameColl1).CellsUsed().Count() + 1;

                    // Заполняем новую строку в столбце NameColl1 (zrp1)
                    worksheet.Cell(emptyRowNameColl1, nameColl1).Value = zrp1;
                    worksheet.Cell(emptyRowNameColl1, nameColl1 + 1).Value = "_";

                    // Обновляем формулу в первой строке для столбца NameColl1 (zrp1)
                    worksheet.Cell(1, nameColl1 + 1).FormulaA1 = $"SUM({worksheet.Column(nameColl1).FirstCell().Address}:{worksheet.Cell(emptyRowNameColl1, nameColl1).Address})";
                }

                // Если MinusBox не видим, то добавляем только в NameColl1
                if (!MinusBox.Visible)
                {
                    workbook.Save();
                    return Task.CompletedTask;
                }

                // Получаем имя из NameBox2
                string name2 = NameBox2.Text;

                // Проверяем, есть ли такое имя в словаре nameZP
                if (nameZP.TryGetValue(name2, out int nameColl2))
                {
                    // Находим следующую пустую строку в столбце NameColl2
                    int emptyRowNameColl2 = worksheet.Column(nameColl2).CellsUsed().Count() + 1;

                    // Заполняем новую строку в столбце NameColl2 (zrp2)
                    worksheet.Cell(emptyRowNameColl2, nameColl2).Value = zrp2;

                    // Обновляем формулу в первой строке для столбца NameColl2 (zrp2)
                    worksheet.Cell(1, nameColl2 + 1).FormulaA1 = $"SUM({worksheet.Column(nameColl2).FirstCell().Address}:{worksheet.Cell(emptyRowNameColl2, nameColl2).Address})";
                }

                workbook.Save();
            }

            return Task.CompletedTask;
        }
        //Заполнение Ексаль по Расходу
        public static Task RashodExcel(int summ, string comm)
        {
            try
            {
                // Открытие существующего файла Excel
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");

                    string date = $"{DateTime.Now:dd} {DateTime.Now:HH}:{DateTime.Now:mm}";

                    date = $"{DateTime.Now:dd} {DateTime.Now.Hour}:{DateTime.Now.Minute}";
                    // Поиск первой пустой строки
                    int row = worksheet.LastRowUsed().RowNumber() + 1;

                    // Добавление данных в новую строку
                    worksheet.Cell(row, 1).Value = date;
                    worksheet.Cell(row, 2).Value = "_";
                    worksheet.Cell(row, 3).Value = "_";
                    worksheet.Cell(row, 4).Value = summ;
                    worksheet.Cell(row, 4).Value = comm;

                    // Формулы для суммирования значений
                    worksheet.Cell(2, 6).FormulaA1 = $"=SUM(B2:B{row + 1})";
                    worksheet.Cell(2, 7).FormulaA1 = $"=SUM(D2:D{row + 1})";
                    worksheet.Cell(2, 8).FormulaA1 = "=F2-G2";

                    // Сохранение файла Excel
                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                // Обрабатываем и выводим ошибку
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return Task.CompletedTask;
        }
        //Отправка скрины Ексель
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

                    Console.WriteLine("Приступаем к преобразованию в png");

                    // Получаем размеры таблицы Excel
                    int width = worksheet.ColumnsUsed().Count();
                    int height = worksheet.RowsUsed().Count();

                    // Определяем шрифт для содержимого ячеек
                    Font cellFont = new Font("Arial", 12);

                    // Массив для хранения ширины каждого столбца
                    int[] columnWidths = new int[width];

                    // Создаем Graphics из временного Bitmap для измерения текста
                    using (Bitmap tempBitmap = new Bitmap(1, 1))
                    using (Graphics tempGraphics = Graphics.FromImage(tempBitmap))
                    {
                        // Определяем максимальные размеры ячеек по столбцам
                        for (int col = 1; col <= width; col++)
                        {
                            int maxWidth = 0;
                            for (int row = 1; row <= height; row++)
                            {
                                // Получаем значение ячейки
                                string cellValue = worksheet.Cell(row, col).GetValue<string>();

                                // Измеряем размеры текста в ячейке
                                SizeF textSize = tempGraphics.MeasureString(cellValue, cellFont);

                                // Обновляем максимальную ширину для текущего столбца
                                maxWidth = Math.Max(maxWidth, (int)textSize.Width);
                            }
                            // Сохраняем максимальную ширину для текущего столбца
                            columnWidths[col - 1] = maxWidth + 10; // Добавляем немного запаса
                        }
                    }

                    // Определяем общую ширину и высоту изображения
                    int totalWidth = columnWidths.Sum();
                    int cellHeight = new Font(cellFont.FontFamily, cellFont.Size, cellFont.Style).Height + 10; // Высота строки с запасом
                    int totalHeight = height * cellHeight;

                    // Создаем новый Bitmap с размерами таблицы
                    using (Bitmap screenshot = new Bitmap(totalWidth, totalHeight))
                    using (Graphics graphics = Graphics.FromImage(screenshot))
                    {
                        // Задаем белый фон
                        graphics.Clear(Color.White);

                        // Заполняем ячейки таблицы
                        using (Font font = new Font("Arial", 12))
                        {
                            int xOffset = 0;
                            for (int col = 1; col <= width; col++)
                            {
                                int yOffset = 0;
                                for (int row = 1; row <= height; row++)
                                {
                                    // Получаем значение ячейки
                                    string cellValue = worksheet.Cell(row, col).GetValue<string>();

                                    // Определяем координаты и размеры прямоугольника для текущей ячейки
                                    RectangleF cellRect = new RectangleF(xOffset, yOffset, columnWidths[col - 1], cellHeight);

                                    // Заполняем ячейку
                                    graphics.DrawString(cellValue, font, Brushes.Black, cellRect);

                                    // Переходим к следующей строке
                                    yOffset += cellHeight;
                                }
                                // Переходим к следующему столбцу
                                xOffset += columnWidths[col - 1];
                            }
                        }

                        // Сохраняем скриншот в файл
                        string screenshotPath = Path.Combine(folderPath, "excel_table_screenshot.png");
                        screenshot.Save(screenshotPath, ImageFormat.Png);

                        // Выводим сообщение о завершении операции
                        Console.WriteLine("Скриншот таблицы успешно сохранен.");

                        await Telegrame.PhotoExcel(screenshotPath);

                        // Выводим сообщение о завершении операции
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
        public static Task ExcelCreated()
        {
            // Создаем папку, если она не существует
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // Проверяем существование файла
            if (!File.Exists(pather))
            {
                using (var workbook = new XLWorkbook())
                {
                    // Создаем новый лист в книге Excel
                    var worksheet = workbook.Worksheets.Add("1");

                    // Заполняем столбцы по указанным именам и добавляем формулы суммирования
                    foreach (var name in nameZP)
                    {
                        string currentName = name.Key;
                        int columnNumber = name.Value;

                        // Заполняем столбец именами
                        worksheet.Cell(1, columnNumber).Value = currentName;

                        string sumColumnAddress = worksheet.Cell(2, columnNumber).Address.ColumnLetter;
                        worksheet.Cell(1, columnNumber + 1).FormulaA1 = $"SUM({sumColumnAddress}:{sumColumnAddress})";
                    }

                    // Сохраняем книгу Excel
                    workbook.SaveAs(pather);
                }
            }
            if (!File.Exists(patherSeyf))
            {
                using (var workbook = new XLWorkbook())
                {
                    // Создаем новый лист в книге Excel
                    var worksheet = workbook.Worksheets.Add("seyf");

                    worksheet.Cell(1, 1).Value = 0;
                    worksheet.Cell(1, 2).FormulaA1 = "=SUM(A1:A2)";

                    // Сохраняем книгу Excel
                    workbook.SaveAs(patherSeyf);
                }
            }
            if (!System.IO.File.Exists(filePath))
            {
                // Создание нового файла Excel
                using (var workbook = new XLWorkbook())
                {
                    // Добавление листа с текущим годом и месяцем
                    var now = DateTime.Now;
                    var worksheet = workbook.Worksheets.Add($"{now.Year}.{now:MM}");

                    // Установка заголовков
                    worksheet.Cell(1, 1).Value = "Дата и время";
                    worksheet.Cell(1, 2).Value = "итоги дней";
                    worksheet.Cell(1, 3).Value = "выручка дней";
                    worksheet.Cell(1, 4).Value = "расходы";
                    worksheet.Cell(1, 5).Value = "_";
                    worksheet.Cell(1, 6).Value = "Выручка за месяц";
                    worksheet.Cell(1, 7).Value = "Расходы";
                    worksheet.Cell(1, 8).Value = "Итог";

                    //заполнение пропусков
                    worksheet.Cell(2, 1).Value = "_";
                    worksheet.Cell(2, 2).Value = "_";
                    worksheet.Cell(2, 3).Value = "_";
                    worksheet.Cell(2, 4).Value = "_";
                    worksheet.Cell(2, 5).Value = "_";

                    // Формулы для суммирования значений
                    worksheet.Cell(2, 6).FormulaA1 = "=SUM(C2:C320)";
                    worksheet.Cell(2, 7).FormulaA1 = "=SUM(D2:D320)";
                    worksheet.Cell(2, 8).FormulaA1 = "=F2-G2";


                    // Сохранение файла Excel
                    workbook.SaveAs(filePath);
                }
            }
            using (var workbook = new XLWorkbook(filePath))
            {
                var now = DateTime.Now;
                // Проверяем существование листа
                if (!workbook.Worksheets.TryGetWorksheet($"{now.Year}.{now:MM}", out var worksheet))
                {
                    workbook.Worksheets.Add($"{now.Year}.{now:MM}");
                }
            }
            using (var workbook = new XLWorkbook(patherSeyf))
            {
                var now = DateTime.Now;
                // Проверяем существование листа
                if (!workbook.Worksheets.TryGetWorksheet($"seyf", out var worksheet))
                {
                    workbook.Worksheets.Add($"seyf");
                }
            }

            return Task.CompletedTask;
        }
        //Заполнение ексель если аванс
        public static Task AvansExcel(string name, int inventSum)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                // Получаем последний лист в книге или создаем новый, если листов нет
                var worksheet = workbook.Worksheet($"{DateTime.Now.Year}.{DateTime.Now:MM}");
                int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;

                // Записываем значение в ячейку
                worksheet.Cell(lastRow, 1).Value = "_";
                worksheet.Cell(lastRow, 2).Value = "_";
                worksheet.Cell(lastRow, 3).Value = "_";
                worksheet.Cell(lastRow, 4).Value = inventSum;
                worksheet.Cell(lastRow, 5).Value = $"Аванс {name}";

                workbook.Save();
            }
            using (var workbook = new XLWorkbook(pather))
            {
                // Получаем последний лист в книге или создаем новый, если листов нет
                var worksheet = workbook.Worksheets.LastOrDefault();
                if (nameZP.TryGetValue(name, out int columnNumber))
                {
                    int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;

                    // Записываем значение в ячейку
                    worksheet.Cell(lastRow, columnNumber).Value = inventSum;
                    worksheet.Cell(lastRow, columnNumber + 1).Value = "_";

                    // Обновляем формулу суммирования
                    worksheet.Cell(1, columnNumber + 1).FormulaA1 = $"SUM({worksheet.Column(columnNumber).FirstCell().Address}:{worksheet.Cell(lastRow, columnNumber).Address})";
                }
                workbook.Save();
            }
            return Task.CompletedTask;
        }
        //Заполнение ексель таблицы по Инаенту
        public static async Task ЗаполнениеExcelInvent(int inventSum, ListBox listBoxNameInv)
        {
            using (var workbook = new XLWorkbook(pather))
            {
                // Получаем последний лист в книге или создаем новый, если листов нет
                var worksheet = workbook.Worksheets.LastOrDefault();

                foreach (var selectedItem in listBoxNameInv.SelectedItems)
                {
                    string name = selectedItem.ToString();

                    if (nameZP.TryGetValue(name, out int columnNumber))
                    {
                        int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;

                        // Записываем значение в ячейку
                        worksheet.Cell(lastRow, columnNumber).Value = inventSum;
                        worksheet.Cell(lastRow, columnNumber + 1).Value = "_";

                        // Обновляем формулу суммирования
                        worksheet.Cell(1, columnNumber + 1).FormulaA1 = $"SUM({worksheet.Column(columnNumber).FirstCell().Address}:{worksheet.Cell(lastRow, columnNumber).Address})";
                    }
                    else
                    {
                        MessageBox.Show($"Столбец для имени {name} не найден в словаре nameZP.");
                    }
                }

                workbook.Save();
            }

            await ScreenExcel(pather);
        }
        //Отображение Excel таблицы во вкладук "Проверка отчетов"
        public static Task ExcelViewer(DataGridView dataGridView, ComboBox comboBox, int i)
        {
            string path = "non";
            if (i == 0)
            {
                path = filePath;
            }
            else if (i == 1)
            {
                path = pather;
            }
            else if (i == 3)
            {
                path = patherSeyf;
            }

            // Очищаем существующие данные в DataGridView
            if (dataGridView.DataSource != null)
            {
                dataGridView.DataSource = null;
            }

            // Создаем новый DataTable
            DataTable dt = new DataTable();

            // Загружаем данные из Excel файла в DataTable с помощью ClosedXML
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                // Выбираем лист Excel, выбранный в ComboBox
                IXLWorksheet workSheet = workBook.Worksheet(comboBox.SelectedItem.ToString());

                // Инициализируем столбцы DataTable
                bool columnsInitialized = false;

                // Проходимся по всем строкам в листе Excel, начиная с первой строки
                foreach (IXLRow row in workSheet.RowsUsed())
                {
                    // Если это первая строка, добавляем столбцы в DataTable
                    if (!columnsInitialized)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            string columnName = cell.Value.ToString();
                            if (!dt.Columns.Contains(columnName))
                            {
                                dt.Columns.Add(columnName);
                            }
                            else
                            {
                                // Добавляем уникальное имя столбца, если дубликат найден
                                int suffix = 1;
                                while (dt.Columns.Contains($"{columnName}_{suffix}"))
                                {
                                    suffix++;
                                }
                                dt.Columns.Add($"{columnName}_{suffix}");
                            }
                        }
                        columnsInitialized = true;
                    }
                    else
                    {
                        // Создаем новую строку в DataTable
                        DataRow newRow = dt.NewRow();

                        // Заполняем данные ячеек DataTable данными из Excel
                        int colIndex = 0;
                        foreach (IXLCell cell in row.CellsUsed())
                        {
                            while (colIndex < cell.Address.ColumnNumber - 1)
                            {
                                newRow[colIndex] = "_";
                                colIndex++;
                            }
                            newRow[colIndex] = cell.IsEmpty() ? "_" : cell.Value;
                            colIndex++;
                        }

                        // Заполняем оставшиеся ячейки пустыми значениями, если колонок больше, чем ячеек в строке
                        while (colIndex < dt.Columns.Count)
                        {
                            newRow[colIndex] = "_";
                            colIndex++;
                        }

                        dt.Rows.Add(newRow);
                    }
                }
            }

            // Задаем DataTable в качестве источника данных для DataGridView
            dataGridView.DataSource = dt;

            // Устанавливаем черный цвет текста в таблице
            dataGridView.ForeColor = System.Drawing.Color.Black;

            // Запрещаем редактирование ячеек в таблице
            dataGridView.ReadOnly = true;

            // Запрещаем изменение размеров столбцов пользователем
            dataGridView.AllowUserToResizeColumns = false;

            // Запрещаем изменение размеров строк пользователем
            dataGridView.AllowUserToResizeRows = false;

            // Запрещаем выделение ячеек пользователем
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            return Task.CompletedTask;
        }
        public static Task SaveDataToExcel(string selectedSheetName, DataGridView dataGridViewExcel, int i)
        {
            string path = "non";
            if (i == 0)
            {
                path = filePath;
            }
            if (i == 1)
            {
                path = pather;
            }
            if (i == 3)
            {
                path = patherSeyf;
            }
            // Проверяем, что выбранное имя листа не пустое и DataGridView не пуст
            if (!string.IsNullOrEmpty(selectedSheetName) && dataGridViewExcel.Rows.Count > 0)
            {
                try
                {
                    // Загружаем книгу Excel
                    using (var workbook = new XLWorkbook())
                    {
                        // Создаем новый лист
                        var worksheet = workbook.Worksheets.Add(selectedSheetName);

                        // Вставляем заголовки столбцов
                        for (int columnIndex = 0; columnIndex < dataGridViewExcel.Columns.Count; columnIndex++)
                        {
                            worksheet.Cell(1, columnIndex + 1).Value = dataGridViewExcel.Columns[columnIndex].HeaderText;
                        }

                        // Добавляем данные из DataGridView в Excel
                        for (int rowIndex = 0; rowIndex < dataGridViewExcel.Rows.Count; rowIndex++)
                        {
                            for (int columnIndex = 0; columnIndex < dataGridViewExcel.Columns.Count; columnIndex++)
                            {
                                var cellValue = dataGridViewExcel.Rows[rowIndex].Cells[columnIndex].Value;

                                if (cellValue != null)
                                {
                                    // Преобразуем значение в числовой формат, если возможно
                                    if (int.TryParse(cellValue.ToString(), out int intValue))
                                    {
                                        worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(intValue);
                                    }
                                    else
                                    {
                                        worksheet.Cell(rowIndex + 2, columnIndex + 1).SetValue(cellValue.ToString());
                                    }
                                }
                            }
                        }
                        if(i == 0)
                        {
                            // Формулы для суммирования значений
                            worksheet.Cell(2, 6).FormulaA1 = $"=SUM(B2:B{dataGridViewExcel.Rows.Count + 1})";
                            worksheet.Cell(2, 7).FormulaA1 = $"=SUM(D2:D{dataGridViewExcel.Rows.Count + 1})";
                            worksheet.Cell(2, 8).FormulaA1 = "=F2-G2";
                        }
                        if (i == 1)
                        {
                            foreach (var name in nameZP)
                            {
                                string currentName = name.Key;
                                int columnNumber = name.Value;

                                // Заполняем столбец именами
                                worksheet.Cell(1, columnNumber).Value = currentName;

                                string sumColumnAddress = worksheet.Cell(2, columnNumber).Address.ColumnLetter;
                                worksheet.Cell(1, columnNumber + 1).FormulaA1 = $"SUM({sumColumnAddress}:{sumColumnAddress})";
                            }
                        }
                        if(i == 3)
                        {
                            int lastRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 1;
                            worksheet.Cell(1, 2).FormulaA1 = $"=SUM(A2:A{lastRow})";
                        }
                        

                        // Сохраняем изменения
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
            string path = "non";
            if (i == 0)
            {
                path = filePath;
            }
            if (i == 1)
            {
                path = pather;
            }
            if (i == 3)
            {
                path = patherSeyf;
            }
            using (var workbook = new XLWorkbook(path))
            {
                var worksheet = workbook.Worksheet(selectedSheetName);
                dataGrid.DataSource = worksheet.RangeUsed().AsTable();
                // Автоматическое заполнение столбцов DataGridView
                dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            return Task.CompletedTask;
        }
        public static Task LoadSheet(MaterialComboBox ComBox, int i)
        {
            string path = "non";
            if (i == 0) path = filePath;
            if (i == 1) path = pather;
            if (i == 3) path = patherSeyf;

            List<string> sheetNames = new List<string>();

            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    sheetNames.Add(worksheet.Name);
                }
            }

            ComBox.DataSource = sheetNames;

            return Task.CompletedTask;
        }
        public static Task SeyfMinus(int PlusSeyf)
        {
            using (XLWorkbook workbook = new XLWorkbook(patherSeyf))
            {
                var worksheet = workbook.Worksheets.Worksheet("seyf");

                int row = worksheet.LastRowUsed().RowNumber() + 1;
                worksheet.Cell(row, 1).Value = PlusSeyf;

                worksheet.Cell(1, 2).FormulaA1 = $"=SUM(A2:A{row})";

                workbook.Save();

            }
            return Task.CompletedTask;
        }
    }
}