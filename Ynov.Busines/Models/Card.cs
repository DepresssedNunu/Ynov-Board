namespace Ynov.Busines.Models;

public class Card {
    private static int CardIdCounter = 1;
    
    public long Id { get; set; } 
    public string Name { get; set; }
    public string Description { get;  set; }
    public DateTime CreationDate { get; protected set; }
    public long BoardId { get; set; }
    
    public Card(string name, string description, long boardId)
    {
        Id = CardIdCounter++;
        BoardId = boardId;
        Name = name;
        Description = description;
        CreationDate = DateTime.Now;
    }

    public Card(string name)
    {
        Name = name;
        CreationDate = DateTime.Now;
    }
    
}