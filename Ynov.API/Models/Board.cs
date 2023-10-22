namespace Ynov.API.Models;

public class Board {
    private static int _idCounter; //keep track of the number of boards

    public readonly List<Card> CardList = new();
    public string Name { get; protected internal set; }
    internal int Id { get;}
    
    public Board(string name)
    {
        Name = name;
        Id = _idCounter++;
        BoardList.ListBoard.Add(this);
    }

    public static Board GetBoard(int id)
    {
        var boardWithCard = BoardList.ListBoard
            .FirstOrDefault(board => board.CardList
                .Any(card => card.Id == id));

        return boardWithCard;
    }
}