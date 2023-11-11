using Ynov.Busines.Models;

namespace Ynov.Busines.Models;

public class Board {
    public List<Card> CardList = new();
    public string Name { get; set; }
    public long Id { get; set; }
    
    public Board(string name)
    {
        Name = name;
    }
}