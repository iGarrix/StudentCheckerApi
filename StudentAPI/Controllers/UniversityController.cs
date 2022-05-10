using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Body;
using StudentAPI.Repositories;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly UniversityRepository _rep;

        public UniversityController(UniversityRepository rep)
        {
            _rep = rep;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_rep.GetTrackers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("StudentTracker")]
        public async Task<IActionResult> StudentTracker([FromBody] StudentTrackerBody studentTrackerBody)
        {
            try
            {
                var result = await _rep.ChangeChecker(studentTrackerBody.Email, studentTrackerBody.Visit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
