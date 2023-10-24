using Ynov.API.Controllers;

namespace Ynov.API.Models;

public class Board {
    public static readonly List<Board> ListBoard = new List<Board>(); //work as the db
    
    private static int _idCounter = 1; //keep track of the number of boards

    public List<Card> CardList = new();
    public string Name { get; protected internal set; }
    internal int Id { get;}
    
    public Board(string name)
    {
        Name = name;
        Id = _idCounter++;
        ListBoard.Add(this);
    }

    public static Board GetBoard(int id)
    {
        var boardWithCard = ListBoard
            .FirstOrDefault(board => board.CardList
                .Any(card => card.Id == id));

        return boardWithCard;
    }
}