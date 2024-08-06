using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Dtos;

namespace ToDoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController(AuthService authService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Register(RegisterDto dto)
        {
            var result = authService.Register(dto);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var result = authService.Login(dto);
            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
