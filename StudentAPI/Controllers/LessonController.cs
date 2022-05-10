using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Body;
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

        [HttpGet("GetStudentLessons")]
        public IActionResult GetStudentLessons(string Id)
        {
            try
            {
                var result = _rep.GetStudentLesson(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LessonsTrackerChange")]
        public async Task<IActionResult> LessonsTrackerChange([FromBody] ChangeLessonBody changeLessonBody)
        {
            try
            {
                var result =  await _rep.LessonChange(changeLessonBody.Id, changeLessonBody.Email, changeLessonBody.LessonVisit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
