using ClosedXML.Excel;
using System;
using System.IO;

namespace Excel
{
    public class ExcelService
    {
        private readonly string _pather;
        private readonly string _patherSeyf;
        private readonly string _filePath;

        public ExcelService(string pather, string patherSeyf, string filePath)
        {
            _pather = pather;
            _patherSeyf = patherSeyf;
            _filePath = filePath;
        }

        public int GetCellValueAsInt(string fileName, string sheetName)
        {
            if (fileName == "ZP")
                fileName = _pather;
            else if (fileName == "s")
                fileName = _patherSeyf;
            else
                fileName = _filePath;

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File {fileName} does not exist.");
            }

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