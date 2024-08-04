using Mapster;
using MediatR;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Application.CQRS.Queries;

public class GetAllQuery: IRequest<List<ToDoItemDto>>
{
    public string Title { get; set; }
    public string Tag { get; set; }
}

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<ToDoItemDto>>
{
    private readonly IToDoRepositry _repositry;

    public GetAllQueryHandler(IToDoRepositry repositry)
    {
        _repositry = repositry;
    }

    public async Task<List<ToDoItemDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var todoitems = _repositry.GetAll();
        if (!string.IsNullOrEmpty(request.Title))
        {
            todoitems = todoitems.Where(i => i.Title.Contains(request.Title)).ToList();
        }

        return todoitems.Adapt<List<ToDoItemDto>>();
    }
}