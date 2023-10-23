using Microsoft.AspNetCore.Mvc;
using Ynov.API.DTOitem;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("board")]
public class BoardController : ControllerBase
{
    
    public static readonly List<Board> ListBoard = new List<Board>(); //work as the db
    
    private readonly ILogger<BoardController> _logger;

    public BoardController(ILogger<BoardController> logger)
    {
        _logger = logger;
    }

    //Get all boards
    [HttpGet("all")]
    public ActionResult<Board> Get()
    {
        var data = ListBoard.Select(board => new
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
    [HttpGet("{id}")]
    public ActionResult<Board> Get(int id)
    {
        if (id > ListBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        }
        
        var data = ListBoard
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
    [HttpPost("add")]
    public ActionResult<Board> Get([FromBody] string name)
    {
        Board board = new Board(name);
        return Ok("Board added: " + board.Name);
    }

    //Update the name of a board
    [HttpPut("{id}/update")]
    public ActionResult<Board> Modify(int id, [FromBody] string name)
    {
        return (id > ListBoard.Count) ? NotFound($"The board number {id} wasn't found ") : Ok(ListBoard[id].Name = name);
    }

    //Delete a board
    [HttpDelete("{id}/delete")]
    public ActionResult<Board> Delete(int id)
    {
        if (id > ListBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        }

        Board currentBoard = ListBoard[id];
        ListBoard.RemoveAt(id);
        return Ok("Board deleted:" + currentBoard.Name);
    }
    
    [HttpGet("{id}/sort")]
    public ActionResult<Board> SortBoard(int id, [FromQuery] SortValues query)
    {
        Board board = BoardController.ListBoard.FirstOrDefault(b => b.Id == id);
        
        if (board == null)
        {
            return NotFound($"The board {id} wasn't found");
        }
        
        switch (query)
        {
            case SortValues.TitleAscending:
                board.CardList = board.CardList.OrderBy(card => card.Name).ToList();
                break;
            case SortValues.TitleDescending:
                board.CardList = board.CardList.OrderByDescending(card => card.Name).ToList();
                break;
            case SortValues.DateAscending:
                board.CardList = board.CardList.OrderBy(card => card.CreationDate).ToList();
                break;
            case SortValues.DateDescending:
                board.CardList = board.CardList.OrderByDescending(card => card.CreationDate).ToList();
                break;
            default:
                return BadRequest($"Invalid sort value: {query}");
        }
        return Ok(board.CardList);
    }
}
    