using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("/board")]
public class BoardController : ControllerBase
{
    private readonly ILogger<BoardController> _logger;

    public BoardController(ILogger<BoardController> logger)
    {
        _logger = logger;
    }

    //Get all boards
    [HttpGet("/board/all")]
    public ActionResult<Board> Get()
    {
        var data = BoardList.listBoard.Select(board => new
        {
            board.Id,
            board.Name,
            Cards = board.CardList.Select(card => new
            {
                card.Id,
                card.Name,
                card.Description,
                card.CreationDate,
            }).ToList()
        });
        return Ok(data);
    }

    //Get a specific board
    [HttpGet("/board/{id}")]
    public ActionResult<Board> Get(int id)
    {
        if (id > BoardList.listBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        }
        
        var data = BoardList.listBoard
            .Where(board => board.Id == id)
            .Select(board => new
        {
            board.Id,
            board.Name,
            Cards = board.CardList.Select(card => new
            {
                card.Id,
                card.Name,
                card.Description,
                card.CreationDate,
            }).ToList()
        });
        return Ok(data);
    }

    //Add a board
    [HttpPost("/add/{name}")]
    public ActionResult<Board> Get(string name)
    {
        Board board = new Board(name);
        return Ok("Board added: " + board.Name);
    }

    //Update the name of a board
    [HttpPost("/update/{id}/")]
    public ActionResult<Board> Modify(int id, string name)
    {
        return (id > BoardList.listBoard.Count)
            ? NotFound($"The board number {id} wasn't found ")
            : Ok(BoardList.listBoard[id].Name = name);
    }

    //Delete a board
    [HttpDelete("/board/{id}/delete/")]
    public ActionResult<Board> Delete(int id)
    {
        if (id > BoardList.listBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        }

        Board currentBoard = BoardList.listBoard[id];
        BoardList.listBoard.RemoveAt(id);
        return Ok("Board deleted:" + currentBoard.Name);
    }
}
    