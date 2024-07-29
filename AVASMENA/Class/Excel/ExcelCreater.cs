using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Excel
{
    public partial class ExcelHelper
    {
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
    }
}
