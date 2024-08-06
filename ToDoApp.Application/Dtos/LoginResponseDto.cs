namespace ToDoApp.Application.Dtos;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string UserId { get; set; }
    public bool IsSuccess { get; set; }
}