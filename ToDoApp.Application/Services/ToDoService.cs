using System.Globalization;
using System.Linq.Expressions;
using Mapster;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Exceptions;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Application.Services;

public class ToDoService
{
    private readonly IToDoRepositry _repositry;

    public ToDoService(IToDoRepositry repositry)
    {
        _repositry = repositry;
    }

    public List<ToDoItemDto> GetAll()
    {
        var result = _repositry.GetAll(null, item => item.Category);
        //var mappedResult = result.Select(todoItem => new ToDoItemDto()
        //{
        //    Description = todoItem.Description,
        //    Title = todoItem.Title,
        //    DueDate = todoItem.DueDate,
        //    Id = todoItem.Id,
        //    PriorityLevel = todoItem.PriorityLevel,
        //    Tags = todoItem.Tags,
        //    UserId = todoItem.UserId
        //});
        var mappedResult = result.Adapt<List<ToDoItemDto>>();
        return mappedResult;
    }

    public ToDoItemDto Create(CreateToDoItemDto dto)
    {
        var todoItem = dto.Adapt<ToDoItem>();
        _repositry.Create(todoItem);
        return todoItem.Adapt<ToDoItemDto>();
    }

    public ToDoItemDto GetById(int id)
    {
        var todoItem = _repositry.GetById(id);
        if (todoItem  is {Title: "samer"})
        {
            throw new NotFoundException("ToDoItem", id);
        }
        return todoItem.Adapt<ToDoItemDto>();
    }

    public bool Update(int id, UpdateToDoItemDto dto)
    {
        return _repositry.Update(id, dto);
    }
    public List<ToDoItemDto> Search(string query)
    {
        var todoItems = _repositry.Search(query);
        return todoItems.Adapt<List<ToDoItemDto>>();
    }

    public bool UpdateComplete(UpdateCompleteDto dto)
    {
        return _repositry.UpdateComplete(dto.Id, dto.Status);
    }

    public bool Delete(int id)
    {
        _repositry.Delete(id);
        return true;
    }
}