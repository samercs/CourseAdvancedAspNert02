using FluentValidation;
using Mapster;
using MediatR;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enum;

namespace ToDoApp.Application.CQRS.Commands;

public class AddToDoItemCommand : IRequest<bool>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public string UserId { get; set; }
    public List<string> Tags { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public int CategoryId { get; set; }
}

public class AddToDoItemCommandValidator : AbstractValidator<AddToDoItemCommand>
{
    public AddToDoItemCommandValidator()
    {
        RuleFor(i => i.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        RuleFor(i => i.Tags)
            .NotEmpty()
            .WithMessage("Tags is required");
        RuleFor(i => i.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category Id should be grater than zero");
    }
}

public class AddToDoItemCommandHandler(IToDoRepositry repositry) : IRequestHandler<AddToDoItemCommand,bool>
{
    public async Task<bool> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddToDoItemCommandValidator();
        var validationResult = validator.Validate(request);
        if (validationResult.IsValid)
        {
            var todoItem = request.Adapt<ToDoItem>();
            repositry.Create(todoItem);
            return true;
        }

        throw new FluentValidation.ValidationException(validationResult.Errors);
    }
}