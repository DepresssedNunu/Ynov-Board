namespace Ynov.API.Models;

public class Board {
    public static int IdCounter = 0; //keep track of the number of boards

    public List<Card> CardList = new();
    public string Name { get; protected internal set; }
    public int Id { get; protected set; }
    
    public Board(string name)
    {
        Name = name;
        Id = IdCounter++;
        BoardList.listBoard.Add(this);
    }
}