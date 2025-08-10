using EduPortal.Bl.Interfaces;
using EduPortal.Dal.Models;
using EduPortal.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YeshivaStudentController : Controller
    {
        public IYeshivaStudent _YeshivaStudent { get; set; }

        public YeshivaStudentController(IYeshivaStudent YeshivaStudent)
        {
            this._YeshivaStudent = YeshivaStudent;
        }
        [HttpGet]
        public async Task<ActionResult<List<Dto.Models.YeshivaStudent>>> ReadAllAsync()
        {
            try
            {
                List<Dto.Models.YeshivaStudent> AllYeshivaStudent = await _YeshivaStudent.GetYeshivaStudents();
                if (AllYeshivaStudent == null || !AllYeshivaStudent.Any())
                {
                    return NotFound("No YeshivaStudent found in the database.");
                }
                return Ok(AllYeshivaStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }

        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Dto.Models.YeshivaStudent>> GetByIdAsync([FromQuery(Name = "YeshivaStudentId")] string YeshivaStudentId)
        {
            try
            {
                Dto.Models.YeshivaStudent yeshivaStudent = await _YeshivaStudent.GetYeshivaStudentById(YeshivaStudentId);
                if (yeshivaStudent == null)
                {
                    return NotFound($"No yeshivaStudent with ID '{YeshivaStudentId}' found in the database.");
                }
                return Ok(yeshivaStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }


        }
    }
}
