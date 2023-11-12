using Microsoft.AspNetCore.Mvc;
using Ynov.API.DTOitem;
using Ynov.Busines.Models;

namespace Ynov.API.IServices;

public interface IBoardServices
{
    public BusinessResult<List<Board>> Get(); 
    
    public BusinessResult<Board> Get(long id);
    
    public BusinessResult<Board> Add(Board board);
    
    public BusinessResult<Board> Modify(long id, Board mBoard);
    
    public BusinessResult Delete(long id);
    
    public BusinessResult<Board> Sort(long id, SortValues query);
}