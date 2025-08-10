using EduPortal.Bl.Interfaces;
using EduPortal.Bl.Services;
using EduPortal.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelImportController : Controller
    {
        private readonly IExcelImport excelImportService;

        public ExcelImportController(IExcelImport excelImportService)
        {
            this.excelImportService = excelImportService;
        }

        [HttpPost("UploadExcelGraduate")]
        public async Task<IActionResult> UploadExcelGraduate(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("לא נבחר קובץ");

            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("ניתן להעלות רק קובץ Excel (סיומת xlsx או xls)");

            try
            {
                using var stream = file.OpenReadStream();
                await excelImportService.ImportIGraduateFromExcel(stream);
                return Ok("הקובץ נטען בהצלחה");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בעיבוד הקובץ: {ex.Message}");
            }
        }

        [HttpPost("UploadExcelYeshivaStudent")]
        public async Task<IActionResult> UploadExcelYeshivaStudent(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("לא נבחר קובץ");

            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("ניתן להעלות רק קובץ Excel (סיומת xlsx או xls)");

            try
            {
                using var stream = file.OpenReadStream();
                await excelImportService.ImportYeshivaStudentsFromExcel(stream);
                return Ok("הקובץ נטען בהצלחה");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בעיבוד הקובץ: {ex.Message}");
            }
        }

    }
}

