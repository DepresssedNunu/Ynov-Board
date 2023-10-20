namespace Ynov.API.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    
    public TodoItem(string name, int id)
    {
        Id = id;
        Name = name;
    }
    
}

