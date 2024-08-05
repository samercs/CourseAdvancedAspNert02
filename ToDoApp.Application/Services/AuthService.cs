using ToDoApp.Application.Dtos;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Application.Services;

public class AuthService(IAuthRepositry repositry)
{
    public bool Register(RegisterDto dto)
    {
        return repositry.Register(dto);
    }

    public LoginResponseDto Login(LoginDto dto)
    {
        return repositry.Login(dto);
    }
}