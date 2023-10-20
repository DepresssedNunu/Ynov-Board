namespace Ynov.API.Models;

public class Card {
    private static int CardIdCounter = 0;
    
    internal int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get;  set; }
    public DateTime CreationDate { get; protected set; }
    public int BoardId { get; set; }
    
    public Card(string name, string description, int id)
    {
        Id = CardIdCounter++;
        BoardId = id;
        Name = name;
        Description = description;
        CreationDate = DateTime.Now;
    }
}