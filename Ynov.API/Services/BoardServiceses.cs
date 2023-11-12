using Microsoft.AspNetCore.Mvc;
using Ynov.API.DTOitem;
using Ynov.API.IRespositories;
using Ynov.API.IServices;
using Ynov.Busines.Models;

namespace Ynov.API.Services;

public class BoardServiceses : IBoardServices
{
    private readonly IBoardRepository _boardRepository;

    public BoardServiceses(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public BusinessResult<List<Board>> Get()
    {
        List<Board> boards = _boardRepository.Get();

        return BusinessResult<List<Board>>.FromSuccess(boards);
    }

    public BusinessResult<Board> Get(long id)
    {
        Board? board = _boardRepository.Get(id);

        if (board is null)
        {
            return BusinessResult<Board>.FromError($"The board {id} do not exist", BusinessErrorReason.NotFound);
        }

        return BusinessResult<Board>.FromSuccess(board);
    }
    
    public BusinessResult<Board> Add(Board board)
    {
        board = _boardRepository.Add(board);

        return BusinessResult<Board>.FromSuccess(board);
    }

    public BusinessResult<Board> Modify(long id, Board mBoard)
    {
        Board? board = _boardRepository.Get(id);

        if (board is null)
        {
            return BusinessResult<Board>.FromError($"The board {id} do not exist", BusinessErrorReason.NotFound);
        }

        board.Name = mBoard.Name;
        _boardRepository.Modify(board);
        return BusinessResult<Board>.FromSuccess(board);
    }

    public BusinessResult Delete(long id)
    {
        Board? board = _boardRepository.Get(id);

        if (board is null)
        {
            return BusinessResult<Board>.FromError($"The board {id} do not exist", BusinessErrorReason.NotFound);
        }
        _boardRepository.Delete(board);
        return BusinessResult.FromSuccess();
    }
    
    public BusinessResult<Board> Sort(long id, SortValues query)
    {
        Board? board = _boardRepository.Get(id);

        if (board is null)
        {
            return BusinessResult<Board>.FromError($"The board {id} do not exist", BusinessErrorReason.NotFound);
        }

        board = _boardRepository.Sort(board, query);

        if (board is null)
        {
            return BusinessResult<Board>.FromError($"The query do not exists", BusinessErrorReason.BusinessRule);
        }
        
        return BusinessResult<Board>.FromSuccess(board);
    }
}