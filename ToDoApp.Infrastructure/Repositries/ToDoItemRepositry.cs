using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entity;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Repositries;

public class ToDoItemRepositry:IToDoRepositry
{
    private readonly AppDbContext _context;
    private readonly IGenericRepositry<ToDoItem> _genericRepositry;
    public ToDoItemRepositry(AppDbContext context)
    {
        _context = context;
        _genericRepositry = new GenericRepositry<ToDoItem>(_context);

    }

    public List<ToDoItem> GetAll(Expression<Func<ToDoItem, bool>>[] where = null, params Expression<Func<ToDoItem, object>>[] children)
    {
        return _genericRepositry.GetAll(where, children);
    }

    public ToDoItem GetById(int id)
    {
        //return _context.ToDoItems.Find(id);
        return _context.ToDoItems.FirstOrDefault(i => i.Id == id);
    }

    public ToDoItem Create(ToDoItem item)
    {
        _context.Add(item);
        _context.SaveChanges();
        return item;
    }

    public bool Update(ToDoItem entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(int id, UpdateToDoItemDto item)
    {
        var toDoItem = _context.ToDoItems.Find(id);
        if (toDoItem != null)
        {
            toDoItem.Description = item.Description;
            toDoItem.Title = item.Title;
            toDoItem.Tags = item.Tags;
            toDoItem.DueDate = item.DueDate;
            toDoItem.PriorityLevel = item.PriorityLevel;
            toDoItem.CategoryId = item.CategoryId;
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        return _genericRepositry.Delete(id);
    }

    public List<ToDoItem> Search(string query)
    {
        var todoItems = _context.ToDoItems.Where(i => EF.Functions.Like(i.Title,$"%{query}%")
                                                      || i.Title.Contains(query) ||
                                                      i.Tags.Contains(query))
                                                        .ToList();
        return todoItems.ToList();
    }

    public bool UpdateComplete(int id, bool status)
    {
        var todoItem = _context.ToDoItems.Find(id);
        if (todoItem == null)
        {
            throw new Exception("To Do item not found.");
        }

        todoItem.IsComplete = status;
        _context.SaveChanges();
        return true;
    }
}