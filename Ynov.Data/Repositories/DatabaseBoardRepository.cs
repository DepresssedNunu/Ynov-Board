// using Microsoft.EntityFrameworkCore;
// using Ynov.Business.DTOitem;
// using Ynov.Business.IRespositories;
// using Ynov.Business.Models;
// using Ynov.Data.Contexts;
//
// namespace Ynov.Data.Repositories;
//
// public class DatabaseBoardRepository : IBoardRepository
// {
//     
//     private readonly BoardDbContext _boardDbContext;
//     
//     public DatabaseBoardRepository(BoardDbContext boardDbContext)
//     {
//         _boardDbContext = boardDbContext;
//     }
//     
//     public List<Board> Get()
//     {
//         return _boardDbContext.Board.Include(t => t.Category).ToList();
//     }
//
//     public Board? Get(long id)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Board Add(Board board)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Board? Modify(Board board)
//     {
//         throw new NotImplementedException();
//     }
//
//     public void Delete(Board board)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Board? Sort(Board board, SortValues query)
//     {
//         throw new NotImplementedException();
//     }
// }