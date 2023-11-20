using Ynov.Business.Dtos;

namespace Ynov.Business.Models;

public static class ModelsExtensions
{
    public static BoardDto AsDto(this Board board)
    {
        return new BoardDto(
            board.CardList,
            board.Name,
            board.Id
        );
    }
}