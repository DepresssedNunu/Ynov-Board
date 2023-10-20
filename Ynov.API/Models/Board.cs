namespace Ynov.API.Models;

public class Board {
    private static int IdCounter = 0; //keep track of the number of boards

    public List<Card> CardList = new();
    public string Name { get; protected internal set; }
    internal int Id { get; set; }
    
    public Board(string name)
    {
        Name = name;
        Id = IdCounter++;
        BoardList.listBoard.Add(this);
    }
}