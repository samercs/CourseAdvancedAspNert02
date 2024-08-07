using Microsoft.AspNetCore.Identity;
using ToDoApp.Domain.Enum;

namespace ToDoApp.Domain.Entity;

public class ApplicationUser : IdentityUser
{
    public DateTime? BirthDate { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
}