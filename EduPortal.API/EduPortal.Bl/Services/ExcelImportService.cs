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
        // פונקציות שלא ממומשות עדיין
        public Task ImportIGraduateFromExcel(Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public Task ImportYeshivaStudentsFromExcel(Stream fileStream)
        {
            throw new NotImplementedException();
        }

        // הפונקציה הקיימת שלך
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

                    var phone = NormalizeChildPhone(GetValue(phoneColIndex));
                    var fatherPhone = NormalizeChildPhone(GetValue(fatherPhoneColIndex));
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
            }
            catch
            {
                // NPOI (xls)
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

                    string GetValue(int colIndex) =>
                        colIndex != -1 ? row.GetCell(colIndex)?.ToString().Trim() ?? "" : "";

                    var phone = NormalizeChildPhone(GetValue(phoneColIndex));
                    var fatherPhone = NormalizeChildPhone(GetValue(fatherPhoneColIndex));
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
            }

            return Task.FromResult(result);
        }

        // ✅ פונקציה לנרמול מספרי טלפון
        public string NormalizeChildPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return "";

            var normalized = phone.Trim()
                                  .Replace("-", "")
                                  .Replace(" ", "");

            if (normalized.Length == 9 && normalized.All(char.IsDigit) && !normalized.StartsWith("0"))
                normalized = "0" + normalized;

            return normalized;
        }
    }
}
