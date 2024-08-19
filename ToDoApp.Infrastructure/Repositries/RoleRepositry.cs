using Microsoft.AspNetCore.Identity;
using ToDoApp.Application.Exceptions;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Infrastructure.Repositries;

public class RoleRepositry(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager): IRoleRepositry
{
    public bool CreateRole(string name)
    {
        var result = roleManager.CreateAsync(new IdentityRole()
        {
            Name = name,
            Id = Guid.NewGuid().ToString()
        }).Result;
        return result.Succeeded;
    }

    public bool AddUserToRole(string userId, string roleName)
    {
        var user = userManager.FindByIdAsync(userId).Result;
        if (user is null)
        {
            throw new NotFoundException("User", userId);
        }

        var result = userManager.AddToRoleAsync(user, roleName).Result;
        return result.Succeeded;
    }
}