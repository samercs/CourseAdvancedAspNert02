using ToDoApp.Domain.Enum;

namespace ToDoApp.Application.Dtos;

public class CreateToDoItemDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string UserId { get; set; }
    public List<string> Tags { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public int CategoryId { get; set; }
}

public class ToDoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string UserId { get; set; }
    public List<string> Tags { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public string CategoryName { get; set; }
}