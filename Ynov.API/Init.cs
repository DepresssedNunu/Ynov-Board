namespace Ynov.API;
using Business.Models;

public static class Init
{
    public static void Test()
    {
        // Create the first board
        Board board1 = new Board("Board 1");
        board1.CardList.Add(new Card("Card 1 for Board 1", "Description 1", board1.Id));
        board1.CardList.Add(new Card("Card 2 for Board 1", "Description 2", board1.Id));
        board1.CardList.Add(new Card("Card 3 for Board 1", "Description 3", board1.Id));

// Create the second board
        Board board2 = new Board("Board 2");
        board2.CardList.Add(new Card("Card 1 for Board 2", "Description 1", board2.Id));
        board2.CardList.Add(new Card("Card 2 for Board 2", "Description 2", board2.Id));
        board2.CardList.Add(new Card("Card 3 for Board 2", "Description 3", board2.Id));

// Create the third board
        Board board3 = new Board("Board 3");
        board3.CardList.Add(new Card("Card 1 for Board 3", "Description 1", board3.Id));
        board3.CardList.Add(new Card("Card 2 for Board 3", "Description 2", board3.Id));
        board3.CardList.Add(new Card("Card 3 for Board 3", "Description 3", board3.Id));
    }
}