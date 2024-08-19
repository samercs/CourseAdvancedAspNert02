namespace ToDoApp.Application.Interfaces;

public interface IRoleRepositry
{
    bool CreateRole(string name);
    bool AddUserToRole(string userId, string roleName);
}