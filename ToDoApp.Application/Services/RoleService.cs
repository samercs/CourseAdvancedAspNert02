using ToDoApp.Application.Interfaces;

namespace ToDoApp.Application.Services;

public class RoleService(IRoleRepositry repositry)
{
    public bool CreateRole(string name)
    {
        return repositry.CreateRole(name);
    }
    public bool AddUserToRole(string userId, string roleName)
    {
        return repositry.AddUserToRole(userId, roleName);
    }
}