using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Body;
using StudentAPI.Dto;
using StudentAPI.Entities.IdentityEntities;
using StudentAPI.Helper;
using StudentAPI.Repositories;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _rep;

        public AuthController(AuthRepository rep)
        {
            _rep = rep;
        }

        [HttpPost("LoginStudent")]
        public async Task<IActionResult> LoginStudent([FromBody] AuthBody authBody)
        {
            try
            {
                var result = await _rep.Login<Student, StudentDto>(authBody.Email, authBody.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LoginTeacher")]
        public async Task<IActionResult> LoginTeacher([FromBody] AuthBody authBody)
        {
            try
            {
                var result = await _rep.Login<Teacher, TeacherDto>(authBody.Email, authBody.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudent(string email)
        {
            try
            {
                var result = await _rep.Get<Student, StudentDto>(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTeacher")]
        public async Task<IActionResult> GetTeacher(string email)
        {
            try
            {
                var result = await _rep.Get<Teacher, TeacherDto>(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
