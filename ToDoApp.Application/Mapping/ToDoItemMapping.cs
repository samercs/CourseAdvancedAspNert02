using Mapster;
using ToDoApp.Application.Dtos;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Application.Mapping;

public class ToDoItemMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Put your mapping logic here
        config
            .NewConfig<ToDoItem, ToDoItemDto>()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
    }
}