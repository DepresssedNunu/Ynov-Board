using Ynov.API.DTOitem;
using Ynov.Busines.Models;

namespace Ynov.API.IRespositories;

public interface IBoardRepository
{
    public List<Board> Get();
    public Board? Get(long id);
    public Board Add(Board board);
    public Board? Modify(Board board);
    public void Delete(Board board);
    public Board? Sort(Board board, SortValues query);
}