using EduPortal.Bl.Interfaces;
using EduPortal.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GraduateController : Controller
    {
        public IGraduate _Graduate { get; set; }

        public GraduateController(IGraduate graduate)
        {
            _Graduate= graduate;
        }
        [HttpGet]
        public async Task<ActionResult<List<Graduate>>> ReadAllAsync()
        {
            try
            {
                List<Graduate> AllGraduate = await _Graduate.GetGraduates();
                if (AllGraduate == null || !AllGraduate.Any())
                {
                    return NotFound("No Graduates found in the database.");
                }
                return Ok(AllGraduate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }

        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Graduate>> GetByIdAsync([FromQuery(Name = "GraduateId")] string GraduateId)
        {
            try
            {
                Graduate graduate = await _Graduate.GetGraduateById(GraduateId);
                if (graduate == null)
                {
                    return NotFound($"No Graduate with ID '{GraduateId}' found in the database.");
                }
                return Ok(graduate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}

