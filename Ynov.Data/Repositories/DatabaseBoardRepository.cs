using Microsoft.EntityFrameworkCore;
using Ynov.Business.DTOitem;
using Ynov.Business.Exceptions;
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
        return _context.Boards
            .Include(b => b.CardList)
            .ThenInclude(c => c.Checklists)
            .ThenInclude(ch => ch.ChecklistItems)
            .ToList();
    }

    public Board? Get(long id)
    {
        return _context.Boards
            .Include(b => b.CardList)
            .ThenInclude(c => c.Checklists)
            .ThenInclude(ch => ch.ChecklistItems)
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

    public Board? Sort(Board board, SortValues sortQuery)
    {
        switch (sortQuery)
        {
            case SortValues.DateAscending:
                board.CardList = board.CardList.OrderBy(c => c.CreationDate).ToList();
                break;
            case SortValues.DateDescending:
                board.CardList = board.CardList.OrderByDescending(c => c.CreationDate).ToList();
                break;
            case SortValues.TitleAscending:
                board.CardList = board.CardList.OrderBy(c => c.Name).ToList();
                break;
            case SortValues.TitleDescending:
                board.CardList = board.CardList.OrderByDescending(c => c.Name).ToList();
                break;
            default:
                throw new InvalidSortQueryException("Invalid sort value");
        }

        return board;
    }

}