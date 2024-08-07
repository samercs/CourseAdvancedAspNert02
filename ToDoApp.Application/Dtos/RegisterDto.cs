﻿using ToDoApp.Domain.Enum;

namespace ToDoApp.Application.Dtos;

public class RegisterDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
}