using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Repositories;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly LessonRepository _rep;

        public LessonController(LessonRepository rep)
        {
            _rep = rep;
        }

        [HttpGet("GetLessons")]
        public IActionResult GetLessons()
        {
            try
            {
                var result = _rep.GetAllLessons();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
