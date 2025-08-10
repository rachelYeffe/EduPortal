using DocumentFormat.OpenXml.Spreadsheet;
using EduPortal.Bl.Interfaces;
using EduPortal.Bl.Services;
using EduPortal.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        public IGraduate _graduate { get; set; }
        public IYeshivaStudent _iYeshivaStudent; 
        public  IExcelImport _excelImport;
        public  ISearch _search;
        public SearchController(IExcelImport excelImportService, IYeshivaStudent _IYeshivaStudent, IGraduate _Graduate, ISearch _search)
        {
            this._excelImport = excelImportService;
            this._iYeshivaStudent = _IYeshivaStudent;
            this._graduate = _Graduate;
            this._search = _search;
        }

        

        [HttpPost]
        public async Task<IActionResult> UploadAndMatch(
    [FromForm] IFormFile file,
    [FromForm] string phone = "",
    [FromForm] string fatherPhone = "",
    [FromForm] string houseNumber = "",
    [FromForm] string firstName = "",
    [FromForm] string lastName = "",
    [FromForm] string fatherName = "",
    [FromForm] string className = "",
    [FromForm] string address = ""
)
        {

            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                using var stream = file.OpenReadStream();
                List<ChildDetails> listNumOfChild = await _excelImport.ExtractPhonePairs(stream,phone, fatherPhone, houseNumber, firstName, lastName, fatherName, className, address);
                List<Graduate> listGraduate = await _graduate.GetGraduates();
                List<Dto.Models.YeshivaStudent> listYeshivaStudent = await _iYeshivaStudent.GetYeshivaStudents();

                List<SearchResult> results = await _search.SearchGraduateAndGraduateByPhone(listNumOfChild, listGraduate, listYeshivaStudent);

                return Ok(results);
            }
            catch (Exception ex)
            {
                // כאן אפשר לרשום לוג או טיפול שגיאות נוסף
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}

