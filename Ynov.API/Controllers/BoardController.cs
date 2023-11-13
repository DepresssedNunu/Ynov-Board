using Microsoft.AspNetCore.Mvc;
using Ynov.Business.DTOitem;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly IBoardServices _boardServices;

    public BoardController(IBoardServices boardServices)
    {
        _boardServices = boardServices;
    }

    //Get all boards
    [HttpGet]
    public ActionResult<Board> Get()
    {
        // Appel du service
        BusinessResult<List<Board>> getBoardsResult = _boardServices.Get();

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
        BusinessResult<Board> getBoardResult = _boardServices.Get(id);

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
        BusinessResult<Board> sortBoardResult = _boardServices.Sort(id, query);
        
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
        BusinessResult<Board> addBoardResult = _boardServices.Add(board);

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
        BusinessResult<Board> updateBoardResult = _boardServices.Modify(id, mBoard);

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
    public ActionResult Delete(long id)
    {
        BusinessResult deleteBoardResult = _boardServices.Delete(id);

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