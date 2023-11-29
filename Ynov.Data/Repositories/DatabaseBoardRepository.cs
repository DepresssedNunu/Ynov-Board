using Microsoft.EntityFrameworkCore;
using Ynov.Business.DTOitem;
using Ynov.Business.IRespositories;
using Ynov.Business.Models;
using Ynov.Data.Contexts;

namespace Ynov.Data.Repositories;

public class DatabaseBoardRepository : IBoardRepository
{
    private readonly TrellodDbContext _context;

    public DatabaseBoardRepository(TrellodDbContext trellodDbContext)
    {
        _context = trellodDbContext;
    }

    public List<Board> Get()
    {
        return _context.Boards.Include(b => b.CardList).ToList();
    }

    public Board? Get(long id)
    {
        return _context.Boards.Include(b => b.CardList)
            .FirstOrDefault(b => b.Id == id);
    }

    public Board Add(Board board)
    {
        _context.Boards.Add(board);
        _context.SaveChanges();
        return board;
    }

    public Board? Modify(Board mBoard)
    {
        var board = _context.Boards.Find(mBoard.Id);
        if (board != null)
        {
            board.Name = mBoard.Name;
            _context.SaveChanges();
        }

        return board;
    }

    public void Delete(Board board)
    {
        _context.Boards.Remove(board);
        _context.SaveChanges();
    }

    public Board? Sort(Board board, SortValues query)
    {
        throw new NotImplementedException();
    }
}