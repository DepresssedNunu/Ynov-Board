using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IRespositories;

public interface IBoardRepository
{
    public List<Board> Get();
    public Board? Get(long id);
    public Board Add(Board board);
    public Board? Modify(Board board);
    public void Delete(Board board);
    public Board? Sort(Board board, SortValues query);
}