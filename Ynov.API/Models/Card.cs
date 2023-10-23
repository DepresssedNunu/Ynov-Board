namespace Ynov.API.Models;

public class Card {
    private static int CardIdCounter = 1;
    
    internal int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get;  set; }
    public DateTime CreationDate { get; protected set; }
    public int BoardId { get; set; }
    
    public Card(string name, string description, int boardId)
    {
        Id = CardIdCounter++;
        BoardId = boardId;
        Name = name;
        Description = description;
        CreationDate = DateTime.Now;
    }
    
}