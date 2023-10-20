namespace Ynov.API.Models;

public class Card {
    public string Name { get; set; }
    public string Description { get;  set; }
    public DateTime CreationDate { get; protected set; }
    
    public int boardId { get; set; }
    public Card(string name, string description, int id) {
        boardId = id;
        Name = name;
        Description = description;
        CreationDate = DateTime.Now;
    }
}