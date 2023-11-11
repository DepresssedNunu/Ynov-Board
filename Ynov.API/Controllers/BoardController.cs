using Microsoft.AspNetCore.Mvc;
using Ynov.API.DTOitem;
using Ynov.API.IServices;
using Ynov.Busines.Models;

namespace Ynov.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    //Get all boards
    [HttpGet]
    public ActionResult<Board> Get()
    {
        // Appel du service
        BusinessResult<List<Board>> getBoardsResult = _boardService.Get();

        // Création de la réponse
        if (getBoardsResult.IsSuccess)
        {
            return Ok(getBoardsResult.Result);
        }

        // Gestion des erreurs
        BusinessError? error = getBoardsResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

    //Get a specific board
    [HttpGet("{id}")]
    public ActionResult<Board> Get(long id)
    {
        BusinessResult<Board> getBoardResult = _boardService.Get(id);

        if (getBoardResult.IsSuccess)
        {
            return Ok(getBoardResult.Result);
        }

        BusinessError? error = getBoardResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }
    
    [HttpGet("{id}/sort")]
    public ActionResult<Board> SortBoard(long id, [FromQuery] SortValues query)
    {
        BusinessResult<Board> sortBoardResult = _boardService.Sort(id, query);
        
        if (sortBoardResult.IsSuccess)
        {
            return Ok(sortBoardResult.Result);
        }

        BusinessError? error = sortBoardResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

    //Add a board
    [HttpPost]
    public ActionResult<Board> Add([FromBody] Board board)
    {
        BusinessResult<Board> addBoardResult = _boardService.Add(board);

       // Création de la réponse
        if (addBoardResult.IsSuccess)
        {
            Board result = addBoardResult.Result!;
            
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // Gestion des erreurs
        BusinessError? error = addBoardResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

    //Update the name of a board
    [HttpPut("{id}")]
    public ActionResult<Board> Modify(long id, [FromBody] Board mBoard)
    {
        BusinessResult<Board> updateBoardResult = _boardService.Modify(id, mBoard);

        if (updateBoardResult.IsSuccess)
        {
            return Ok(updateBoardResult.Result);
        }

        BusinessError? error = updateBoardResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

    //Delete a board
    [HttpDelete("{id}")]
    public ActionResult<Board> Delete(long id)
    {
        BusinessResult deleteBoardResult = _boardService.Delete(id);

        if (deleteBoardResult.IsSuccess)
        {
            return Ok();
        }

        BusinessError? error = deleteBoardResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }
}