namespace ToDoApp.Domain.Entity;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ToDoItem> ToDoItems { get; set; }
}