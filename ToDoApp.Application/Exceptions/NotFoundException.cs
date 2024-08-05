namespace ToDoApp.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object id): base($"Entity of type {name} not found. Where id ={id}")
    {
    }
}