using ClosedXML.Excel;
using EduPortal.Bl.Interfaces;
using EduPortal.Dto.Models;
using NPOI.HSSF.UserModel;   // HSSFWorkbook
using NPOI.SS.UserModel;     // ISheet, IRow, ICell
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduPortal.Bl.Services
{
    public class ExcelImportService : IExcelImport
    {
        private readonly IGraduate graduate;
        private readonly IYeshivaStudent yeshivaStudent;

        public ExcelImportService(IGraduate graduate, IYeshivaStudent yeshivaStudent)
        {
            this.graduate = graduate;
            this.yeshivaStudent = yeshivaStudent;
        }

        public async Task ImportIGraduateFromExcel(Stream fileStream)
        {
            fileStream.Position = 0;
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);
            var rowCount = worksheet.LastRowUsed().RowNumber();
            var headers = MapHeaders(worksheet);

            var graduates = new List<Graduate>();

            for (int row = 2; row <= rowCount; row++)
            {
                var grad = new Graduate
                {
                    AccountNumber = GetCellValue(worksheet, row, headers, "מספר חשבון"),
                    LastName = GetCellValue(worksheet, row, headers, "שם משפחה"),
                    FirstName = GetCellValue(worksheet, row, headers, "שם פרטי+שם אמצעי"),
                    Institution = GetCellValue(worksheet, row, headers, "מוסד"),
                    IDNumber = GetCellValue(worksheet, row, headers, "מספר תעודת זהות"),
                    MobilePhone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון נייד")),
                    Passport = GetCellValue(worksheet, row, headers, "מספר דרכון"),
                    Street = GetCellValue(worksheet, row, headers, "רחוב בית"),
                    HouseNumber = GetCellValue(worksheet, row, headers, "מספר בית בית"),
                    Apartment = GetCellValue(worksheet, row, headers, "דירה בית"),
                    City = GetCellValue(worksheet, row, headers, "ישוב בית"),
                    Entrance = GetCellValue(worksheet, row, headers, "כניסה בית"),
                    FatherCity = GetCellValue(worksheet, row, headers, "ישוב בית אב"),
                    FatherStreet = GetCellValue(worksheet, row, headers, "רחוב בית אב"),
                    FatherHouseNumber = GetCellValue(worksheet, row, headers, "מספר בית בית אב"),
                    FatherEntrance = GetCellValue(worksheet, row, headers, "כניסה בית אב"),
                    FatherApartment = GetCellValue(worksheet, row, headers, "דירה בית אב"),
                    HomePhone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון בית אב")),
                    FatherPhone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון נייד אב")),
                    AddHomePhone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון בית נוסף אב")),
                    FatherBusinessPhone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון עסק אב")),
                    AddFatherBusinessPhone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון עסק נוסף אב")),
                    Mail = GetCellValue(worksheet, row, headers, "דואר ברירת מחדל"),
                    Kind = GetCellValue(worksheet, row, headers, "סוג כרטיס"),
                    Cycle = GetCellValue(worksheet, row, headers, "מחזור"),
                    Age = GetCellValue(worksheet, row, headers, "גיל"),
                    Status = GetCellValue(worksheet, row, headers, "מצב משפחתי")
                };
                graduates.Add(grad);
            }

            foreach (var g in graduates)
                await graduate.CreateGraduate(g);
        }

        public async Task ImportYeshivaStudentsFromExcel(Stream fileStream)
        {
            fileStream.Position = 0;
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);
            var rowCount = worksheet.LastRowUsed().RowNumber();
            var headers = MapHeaders(worksheet);

            var students = new List<Dto.Models.YeshivaStudent>();

            for (int row = 2; row <= rowCount; row++)
            {
                var student = new Dto.Models.YeshivaStudent
                {
                    FullName = GetCellValue(worksheet, row, headers, "שם ומשפחה"),
                    Phone = NormalizePhone(GetCellValue(worksheet, row, headers, "טלפון")),
                    IdNumber = GetCellValue(worksheet, row, headers, "תז"),
                    Address = GetCellValue(worksheet, row, headers, "כתובת"),
                    EntryDate = GetCellValue(worksheet, row, headers, "תאריך כניסה"),
                    Status = GetCellValue(worksheet, row, headers, "סטטוס")
                };
                students.Add(student);
            }

            foreach (var s in students)
                await yeshivaStudent.CreateYeshivaStudent(s);
        }

        public Task<List<ChildDetails>> ExtractPhonePairs(
            Stream fileStream,
            string phoneHeader = null,
            string fatherPhoneHeader = null,
            string houseNumberHeader = null,
            string firstNameHeader = null,
            string lastNameHeader = null,
            string fatherNameHeader = null,
            string classNameHeader = null,
            string addressHeader = null)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("Invalid or empty file stream.");

            var result = new List<ChildDetails>();

            try
            {
                // ClosedXML (xlsx)
                fileStream.Position = 0;
                using var workbook = new XLWorkbook(fileStream);
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();
                var headers = worksheet.Row(1).Cells().ToList();

                int GetColIndex(string header) =>
                    string.IsNullOrWhiteSpace(header) ? -1 :
                    headers.FindIndex(c => c.GetString().Trim() == header);

                int phoneColIndex = GetColIndex(phoneHeader);
                int fatherPhoneColIndex = GetColIndex(fatherPhoneHeader);
                int houseNumberColIndex = GetColIndex(houseNumberHeader);
                int firstNameColIndex = GetColIndex(firstNameHeader);
                int lastNameColIndex = GetColIndex(lastNameHeader);
                int fatherNameColIndex = GetColIndex(fatherNameHeader);
                int classNameColIndex = GetColIndex(classNameHeader);
                int addressColIndex = GetColIndex(addressHeader);

                foreach (var row in rows.Skip(1))
                {
                    string GetValue(int colIndex) => colIndex != -1 ? row.Cell(colIndex + 1).GetString().Trim() : "";
                    result.Add(new ChildDetails
                    {
                        Phone = NormalizeChildPhone(GetValue(phoneColIndex)),
                        FatherPhone = NormalizeChildPhone(GetValue(fatherPhoneColIndex)),
                        HouseNumber = GetValue(houseNumberColIndex),
                        FirstName = GetValue(firstNameColIndex),
                        LastName = GetValue(lastNameColIndex),
                        FatherName = GetValue(fatherNameColIndex),
                        Class = GetValue(classNameColIndex),
                        Address = GetValue(addressColIndex)
                    });
                }
            }
            catch
            {
                // NPOI fallback (xls)
                fileStream.Position = 0;
                using var hssfWorkbook = new HSSFWorkbook(fileStream);
                var sheet = hssfWorkbook.GetSheetAt(0);
                var headerRow = sheet.GetRow(0);

                int GetColIndex(string header)
                {
                    if (string.IsNullOrWhiteSpace(header)) return -1;
                    for (int i = 0; i < headerRow.LastCellNum; i++)
                        if (headerRow.GetCell(i)?.ToString().Trim() == header)
                            return i;
                    return -1;
                }

                int phoneColIndex = GetColIndex(phoneHeader);
                int fatherPhoneColIndex = GetColIndex(fatherPhoneHeader);
                int houseNumberColIndex = GetColIndex(houseNumberHeader);
                int firstNameColIndex = GetColIndex(firstNameHeader);
                int lastNameColIndex = GetColIndex(lastNameHeader);
                int fatherNameColIndex = GetColIndex(fatherNameHeader);
                int classNameColIndex = GetColIndex(classNameHeader);
                int addressColIndex = GetColIndex(addressHeader);

                for (int rowIdx = 1; rowIdx <= sheet.LastRowNum; rowIdx++)
                {
                    var row = sheet.GetRow(rowIdx);
                    if (row == null) continue;

                    string GetValue(int colIndex) => colIndex != -1 ? row.GetCell(colIndex)?.ToString().Trim() ?? "" : "";

                    result.Add(new ChildDetails
                    {
                        Phone = NormalizeChildPhone(GetValue(phoneColIndex)),
                        FatherPhone = NormalizeChildPhone(GetValue(fatherPhoneColIndex)),
                        HouseNumber = GetValue(houseNumberColIndex),
                        FirstName = GetValue(firstNameColIndex),
                        LastName = GetValue(lastNameColIndex),
                        FatherName = GetValue(fatherNameColIndex),
                        Class = GetValue(classNameColIndex),
                        Address = GetValue(addressColIndex)
                    });
                }
            }

            return Task.FromResult(result);
        }

        private Dictionary<string, int> MapHeaders(IXLWorksheet worksheet)
        {
            var headers = new Dictionary<string, int>();
            var headerRow = worksheet.Row(1);
            int lastColumn = worksheet.LastColumnUsed().ColumnNumber();
            for (int col = 1; col <= lastColumn; col++)
            {
                string headerText = headerRow.Cell(col).GetString().Trim();
                if (!string.IsNullOrEmpty(headerText))
                    headers[headerText] = col;
            }
            return headers;
        }

        private string GetCellValue(IXLWorksheet worksheet, int row, Dictionary<string, int> headers, string columnName)
        {
            if (headers.TryGetValue(columnName, out int col))
            {
                var value = worksheet.Cell(row, col).GetString();
                return string.IsNullOrWhiteSpace(value) ? "" : value.Trim();
            }
            return "";
        }

        public string NormalizePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return "";

            var normalized = phone.Trim().Replace("-", "").Replace(" ", "");
            if (normalized.Length == 9 && normalized.All(char.IsDigit) && !normalized.StartsWith("0"))
                normalized = "0" + normalized;
            return normalized;
        }

        public string NormalizeChildPhone(string phone)
        {
            return NormalizePhone(phone);
        }
    }
}
