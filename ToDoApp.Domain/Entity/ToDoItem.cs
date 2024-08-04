using ToDoApp.Domain.Enum;

namespace ToDoApp.Domain.Entity;

public class ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public string UserId { get; set; }
    public List<string> Tags { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

public class UpdateToDoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? DueDate { get; set; }
    public string UserId { get; set; }
    public List<string> Tags { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public int CategoryId { get; set; }

}