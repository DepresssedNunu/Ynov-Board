using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IServices;

public interface IBoardServices
{
    public BusinessResult<List<Board>> Get(); 
    
    public BusinessResult<Board> Get(long id);
    
    public BusinessResult<Board> Add(Board board);
    
    public BusinessResult<Board> Modify(long id, Board mBoard);
    
    public BusinessResult Delete(long id);
    
    public BusinessResult<Board> Sort(long id, SortValues query);
}