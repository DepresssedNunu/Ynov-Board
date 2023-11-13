using Ynov.Business.DTOitem;
using Ynov.Business.IRespositories;
using Ynov.Business.Models;

namespace Ynov.Data.Repositories;

public class InMemoryBoardRepository : IBoardRepository
{
    public static long _idCounter = 1;
    public static List<Board> _boards = new();

    public List<Board> Get()
    {
        return _boards;
    }

    public Board? Get(long id)
    {
        return _boards.Find(b => b.Id == id);
    }

    public Board Add(Board board)
    {
        board.Id = _idCounter++;
        _boards.Add(board);
        return board;
    }

    public Board? Modify(Board mBoard)
    {
        Board board = _boards.Single(b => b.Id == mBoard.Id);
        board.Name = mBoard.Name;
        return board;
    }

    public void Delete(Board board)
    {
        _boards.Remove(board);
    }

    public Board? Sort(Board mBoard, SortValues query)
    {
        Board board = _boards.Single(b => b.Id == mBoard.Id);

        switch (query)
        {
            case SortValues.TitleAscending:
                board.CardList = board.CardList.OrderBy(card => card.Name).ToList();
                break;
            case SortValues.TitleDescending:
                board.CardList = board.CardList.OrderByDescending(card => card.Name).ToList();
                break;
            case SortValues.DateAscending:
                board.CardList = board.CardList.OrderBy(card => card.CreationDate).ToList();
                break;
            case SortValues.DateDescending:
                board.CardList = board.CardList.OrderByDescending(card => card.CreationDate).ToList();
                break;
            default:
                return null;
        }

        return board;
    }
}