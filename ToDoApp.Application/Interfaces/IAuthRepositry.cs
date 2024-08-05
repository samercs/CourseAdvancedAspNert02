using ToDoApp.Application.Dtos;

namespace ToDoApp.Application.Interfaces;

public interface IAuthRepositry
{
    bool Register(RegisterDto dto);
    LoginResponseDto Login(LoginDto dto);
}