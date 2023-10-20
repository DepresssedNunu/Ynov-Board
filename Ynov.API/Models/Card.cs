namespace Ynov.API.Models;

public class Card {
    public string Name { get; set; }
    public string Description { get;  set; }
    public DateTime CreationDate { get; protected set; }
    
    public Card(string name, string description) {
        Name = name;
        Description = description;
        CreationDate = DateTime.Now;
    }
}