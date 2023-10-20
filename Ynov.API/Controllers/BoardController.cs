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
    [HttpGet("Board/all")]
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
    [HttpGet("Board/{id}")]
    public ActionResult<Board> Get(int id)
    {
        if (id > BoardList.listBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        }

        return Ok(BoardList.listBoard[id]);
    }

    //Add a board
    [HttpPost("Board/add/{name}")]
    public ActionResult<Board> Get(string name)
    {
        Board board = new Board(name);
        return Ok("Board added: " + board.Name);
    }

    //Update the name of a board
    [HttpPost("Board/update/")]
    public ActionResult<Board> Modify(int id, string name)
    {
        return (id > BoardList.listBoard.Count)
            ? NotFound($"The board number {id} wasn't found ")
            : Ok(BoardList.listBoard[id].Name = name);
    }

    //Delete a board
    [HttpDelete("board/delete/")]
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
    