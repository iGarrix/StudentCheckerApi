using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> LoginStudent(string email, string password)
        {
            try
            {
                var result = await _rep.Login<Student, StudentDto>(email, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LoginTeacher")]
        public async Task<IActionResult> LoginTeacher(string email, string password)
        {
            try
            {
                var result = await _rep.Login<Teacher, TeacherDto>(email, password);
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
