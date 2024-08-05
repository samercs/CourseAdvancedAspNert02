using Microsoft.AspNetCore.Identity;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Infrastructure.Repositries;

public class AuthRepositry(UserManager<IdentityUser> userManager):IAuthRepositry
{
    public bool Register(RegisterDto dto)
    {
        var user = new IdentityUser()
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        var result = userManager.CreateAsync(user, dto.Password).Result;
        if (result.Succeeded)
        {
            return true;
        }

        throw new Exception($"Registration Faild. {result.Errors}");
    }

    public LoginResponseDto Login(LoginDto dto)
    {
        throw new NotImplementedException();
    }
}