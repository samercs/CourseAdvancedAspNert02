using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Dtos;

namespace ToDoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(RoleDto dto)
        {
            roleService.CreateRole(dto.RoleName);
            return Ok();
        }

        [HttpPost("assign-role")]
        public IActionResult AssignRole(string roleName, string userId)
        {
            roleService.AddUserToRole(userId, roleName);
            return Ok();
        }
    }
}
