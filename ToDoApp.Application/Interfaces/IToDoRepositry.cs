using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Dtos;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Application.Interfaces
{
    public interface IToDoRepositry: IGenericRepositry<ToDoItem>
    {
        bool Update(int id, UpdateToDoItemDto item);
        List<ToDoItem> Search(string query);
        bool UpdateComplete(int id, bool status);
    }
}
