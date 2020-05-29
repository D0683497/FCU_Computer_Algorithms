using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace HW01
{
    public class Helpers
    {
        public static List<int> GenderateRadomNumbers(int seed)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<int> randList = new List<int>(Enumerable.Range(1, seed));
            return randList.OrderBy(o => rand.Next()).ToList<int>();
        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public class Record
        {
            public string SortName { get; set; }
            public string Seed { get; set; }
            public string Round { get; set; }
            public string Time { get; set; }
        }

        public static void WriteExcel(List<Record> records)
        {
            string sortName = records[0].SortName;

            if (sortName == "Bubble Sort")
            {
                CreateExcel(records);
            }
            else
            {
                EditExcel(records);
            }
        }

        private static void CreateExcel(List<Record> records)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(Config.FileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = records[0].SortName };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                #region Header

                Row row = new Row();
                row.Append(
                    new Cell() { CellValue = new CellValue("演算法名稱"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("亂數總數"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("回合數"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("時間(秒)"), DataType = CellValues.String }
                );
                sheetData.AppendChild(row);

                #endregion

                #region Content

                foreach (var record in records)
                {
                    row = new Row();
                    row.Append(new Cell() { CellValue = new CellValue(record.SortName), DataType = CellValues.String });
                    row.Append(new Cell() { CellValue = new CellValue(record.Seed), DataType = CellValues.String });
                    row.Append(new Cell() { CellValue = new CellValue(record.Round), DataType = CellValues.String });
                    row.Append(new Cell() { CellValue = new CellValue(record.Time), DataType = CellValues.String });
                    sheetData.AppendChild(row);
                }

                #endregion

                worksheetPart.Worksheet.Save();
            }
        }

        private static void EditExcel(List<Record> records)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(Config.FileName, true))
            {
                WorksheetPart newWorksheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet();
                Sheets sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = document.WorkbookPart.GetIdOfPart(newWorksheetPart);

                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Count() > 0)
                {
                    sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }
                Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = records[0].SortName };
                sheets.Append(sheet);
                newWorksheetPart.Worksheet.Save();

                SheetData sheetData = newWorksheetPart.Worksheet.AppendChild(new SheetData());

                #region Header

                Row row = new Row();
                row.Append(
                    new Cell() { CellValue = new CellValue("演算法名稱"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("亂數總數"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("回合數"), DataType = CellValues.String },
                    new Cell() { CellValue = new CellValue("時間(秒)"), DataType = CellValues.String }
                );
                sheetData.AppendChild(row);

                #endregion

                #region Content

                foreach (var record in records)
                {
                    row = new Row();
                    row.Append(new Cell() { CellValue = new CellValue(record.SortName), DataType = CellValues.String });
                    row.Append(new Cell() { CellValue = new CellValue(record.Seed), DataType = CellValues.String });
                    row.Append(new Cell() { CellValue = new CellValue(record.Round), DataType = CellValues.String });
                    row.Append(new Cell() { CellValue = new CellValue(record.Time), DataType = CellValues.String });
                    sheetData.AppendChild(row);
                }

                #endregion

                newWorksheetPart.Worksheet.Save();
            }
        }
    }
}