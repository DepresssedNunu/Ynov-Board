using Ynov.Business.DTOitem;
using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Models;

using System.Collections.Generic;

namespace Ynov.Business.Services;

public class BoardServices : IBoardServices
{
    private readonly IBoardRepository _boardRepository;

    public BoardServices(IBoardRepository boardRepository)
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