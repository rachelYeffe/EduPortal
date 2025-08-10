using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EduPortal.Bl.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using EduPortal.Dto.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Wordprocessing;

namespace EduPortal.Bl.Services
{
    public class ExcelImportService : IExcelImport
    {
        private readonly IGraduate graduate;
        private readonly IMapper mapper;
        private IYeshivaStudent YeshivaStudent;

        public ExcelImportService(IGraduate dal, IMapper mapper,IYeshivaStudent dal1)
        {
            this.graduate = dal;
            this.mapper = mapper;
            this.YeshivaStudent = dal1;
        }

        public async Task ImportIGraduateFromExcel(Stream fileStream)
        {
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1); // גיליון ראשון
            var rowCount = worksheet.LastRowUsed().RowNumber();

            // מיפוי כותרות לעמודות
            var headers = new Dictionary<string, int>();
            var headerRow = worksheet.Row(1);
            int lastColumn = worksheet.LastColumnUsed().ColumnNumber();

            for (int col = 1; col <= lastColumn; col++)
            {
                string headerText = headerRow.Cell(col).GetString().Trim();
                if (!string.IsNullOrEmpty(headerText))
                {
                    headers[headerText] = col;
                }
            }

            var students = new List<Dto.Models.Graduate>();

            for (int row = 2; row <= rowCount; row++) 
            {
                try
                {
                    var graduate = new Dto.Models.Graduate
                    {
                        AccountNumber = GetCellValue(worksheet, row, headers, "מספר חשבון"),
                        LastName = GetCellValue(worksheet, row, headers, "שם משפחה"),
                        FirstName = GetCellValue(worksheet, row, headers, "שם פרטי+שם אמצעי"),
                        Institution = GetCellValue(worksheet, row, headers, "מוסד"),
                        IDNumber = GetCellValue(worksheet, row, headers, "מספר תעודת זהות"),
                        MobilePhone = GetCellValue(worksheet, row, headers, "טלפון נייד"),
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
                        HomePhone = GetCellValue(worksheet, row, headers, "טלפון בית אב"), 

                        Mail = GetCellValue(worksheet, row, headers, "דואר ברירת מחדל"),
                        Kind = GetCellValue(worksheet, row, headers, "סוג כרטיס"),
                        Cycle = GetCellValue(worksheet, row, headers, "מחזור"),
                        Age = GetCellValue(worksheet, row, headers, "גיל"),
                        FatherPhone = GetCellValue(worksheet, row, headers, "טלפון נייד אב"),
                        AddHomePhone = GetCellValue(worksheet, row, headers, "טלפון בית נוסף אב"),
                        FatherBusinessPhone = GetCellValue(worksheet, row, headers, "טלפון עסק אב"),
                        AddFatherBusinessPhone = GetCellValue(worksheet, row, headers, "טלפון עסק נוסף אב"),
                        Status = GetCellValue(worksheet, row, headers, "מצב משפחתי")
                    };

                    students.Add(graduate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"שגיאה בקריאת שורה {row}: {ex.Message}");
                }
            }

            foreach (var graduat in students)
            {
                try
                {
                    await graduate.CreateGraduate(graduat);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                    Console.WriteLine($"שגיאה בשמירת בוגר עם תעודת זהות {graduat.IDNumber}: {ex.Message}");
                }
            }
        }

        private string GetCellValue(IXLWorksheet worksheet, int row, Dictionary<string, int> headers, string columnName)
        {
            if (headers.TryGetValue(columnName, out int col))
            {
                var value = worksheet.Cell(row, col).GetString();
                return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
            }
            return string.Empty;
        }
        public async Task ImportYeshivaStudentsFromExcel(Stream fileStream)
        {
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1); // גיליון ראשון, או תחליפי לשם הגיליון הרלוונטי
            var rowCount = worksheet.LastRowUsed().RowNumber();

            // מיפוי כותרות לעמודות
            var headers = new Dictionary<string, int>();
            var headerRow = worksheet.Row(1);
            int lastColumn = worksheet.LastColumnUsed().ColumnNumber();

            for (int col = 1; col <= lastColumn; col++)
            {
                string headerText = headerRow.Cell(col).GetString().Trim();
                if (!string.IsNullOrEmpty(headerText))
                {
                    headers[headerText] = col;
                }
            }

            var students = new List<Dto.Models.YeshivaStudent>();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    var student = new Dto.Models.YeshivaStudent
                    {
                        FullName = GetCellValue(worksheet, row, headers, "שם ומשפחה"),
                        Phone = GetCellValue(worksheet, row, headers, "טלפון"),
                        IdNumber = GetCellValue(worksheet, row, headers, "תז"),
                        Address = GetCellValue(worksheet, row, headers, "כתובת"),
                        EntryDate = GetCellValue(worksheet, row, headers, "תאריך כניסה"),
                        Status = GetCellValue(worksheet, row, headers, "סטטוס")
                    };

                    students.Add(student);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"שגיאה בקריאת שורה {row}: {ex.Message}");
                }
            }

            foreach (var student in students)
            {
                try
                {
                    await YeshivaStudent.CreateYeshivaStudent(student);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message );
                    Console.WriteLine($"שגיאה בשמירת אברך עם תעודת זהות {student.IdNumber}: {ex.Message}");
                }
            }
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

            fileStream.Position = 0;
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed();

            var headers = worksheet.Row(1).Cells().ToList();

            int GetColIndex(string header) =>
                string.IsNullOrWhiteSpace(header)
                    ? -1
                    : headers.FindIndex(c => c.GetString().Trim() == header);

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
                string GetValue(int colIndex) =>
                    colIndex != -1 ? row.Cell(colIndex + 1).GetString().Trim() : "";

                var phone = NormalizePhone(GetValue(phoneColIndex));
                var fatherPhone = NormalizePhone(GetValue(fatherPhoneColIndex));
                var houseNumber = GetValue(houseNumberColIndex);
                var firstName = GetValue(firstNameColIndex);
                var lastName = GetValue(lastNameColIndex);
                var fatherName = GetValue(fatherNameColIndex);
                var className = GetValue(classNameColIndex);
                var address = GetValue(addressColIndex);

                result.Add(new ChildDetails
                {
                    Phone = phone,
                    FatherPhone = fatherPhone,
                    HouseNumber = houseNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    FatherName = fatherName,
                    Class = className,
                    Address = address
                });
            }

            return Task.FromResult(result);
        }

        private string NormalizePhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone) && phone.Length == 9 && phone.All(char.IsDigit) && !phone.StartsWith("0"))
                return "0" + phone;
            return phone;
        }

     
    }
}
