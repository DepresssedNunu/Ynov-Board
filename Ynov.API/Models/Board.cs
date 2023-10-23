namespace Ynov.API.Models;

public class Board {
    private static int IdCounter = 1; //keep track of the number of boards

    public List<Card> CardList = new();
    public string Name { get; protected internal set; }
    internal int Id { get; set; }
    
    public Board(string name)
    {
        Name = name;
        Id = IdCounter++;
        BoardList.listBoard.Add(this);
    }

    public static Board GetBoard(int id)
    {
        var boardWithCard = BoardList.listBoard
            .FirstOrDefault(board => board.CardList
                .Any(card => card.Id == id));

        return boardWithCard;
    }
}