using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("card")]
public class CardController : ControllerBase
{
    private readonly ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger)
    {
        _logger = logger;
    }

    //Get all cards
    [HttpGet("all")]
    public ActionResult<Board> GetCard()
    {
        var data = BoardController.ListBoard
            .Select(board => board.CardList)
            .ToList();

        return Ok(data);
    }


    // Get a specific card
    [HttpGet("{id}")]
    public ActionResult<Board> GetCard(int id)
    {
        var data = BoardController.ListBoard
            .SelectMany(board => board.CardList)
            .FirstOrDefault(card => card.Id == id);

        if (data == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        return Ok(data);
    }


    // ADD CARD with description and name to a specific board
    [HttpPost("add")]
    public ActionResult<Board> AddCard(int id, string description, string name)
    {
        if (id > BoardController.ListBoard.Count - 1)
        {
            return NotFound($"The board number {id} wasn't found ");
        } //check if the board exist

        Board currentBoard = BoardController.ListBoard[id];

        currentBoard.CardList.Add(new Card(name, description, currentBoard.Id)); //add the card to the board

        return Ok("Card added: " + currentBoard.CardList[^1].Name + ": " +
                  currentBoard.CardList[^1].Description); //return the last card added
    }


    // DELETE CARD
    [HttpDelete("{id}/delete/")]
    public ActionResult<Board> DeleteCard(int id)
    {
        //Get the board contenting the card
        var boardWithCard = Board.GetBoard(id);

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        var card = boardWithCard.CardList.FirstOrDefault(card => card.Id == id);

        if (card != null)
        {
            boardWithCard.CardList.Remove(card);
            return Ok($"Card with ID {id} has been deleted.");
        }

        return NotFound($"Card with ID {id} wasn't found.");
    }

    //update card description
    [HttpPatch("{id}/update/description/")]
    public ActionResult<Board> ModifyCardDescription(int id, string description)
    {
        //Get the board contenting the card
        var boardWithCard = Board.GetBoard(id);

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        var card = boardWithCard.CardList.FirstOrDefault(card => card.Id == id);

        if (card == null)
        {
            return NotFound($"The card {id} wasn't found");
        }

        card.Description = description;
        return Ok($"Card {card.Name} has been modified !");
    }

    //update card name
    [HttpPatch("{id}/update/title/")]
    public ActionResult<Board> ModifyCardName(int id, string name)
    {
        //Get the board contenting the card
        var boardWithCard = Board.GetBoard(id);

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        var card = boardWithCard.CardList.FirstOrDefault(card => card.Id == id);

        if (card == null)
        {
            return NotFound($"The card {id} wasn't found");
        }

        card.Name = name;
        return Ok($"Card {card.Id} has been modified !");
    }

    //Modify card name AND description
    [HttpPut("modify/")]
    public ActionResult<Board> ModifyCard(int id, string name, string description)
    {
        //Get the board contenting the card
        var boardWithCard = Board.GetBoard(id);

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        var card = boardWithCard.CardList.FirstOrDefault(card => card.Id == id);

        if (card == null)
        {
            return NotFound($"The card {id} wasn't found");
        }

        card.Description = description;
        card.Name = name;
        return Ok($"Card {card.Id} has been modified !");
    }

    [HttpPatch("{id}/update/board/")]
    public ActionResult ModifyCardBoard(int id, int newBoardID)
    {
        //Get the board contenting the card
        var boardWithCard = Board.GetBoard(id);

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        var card = boardWithCard.CardList.FirstOrDefault(card => card.Id == id);

        if (card == null)
        {
            return NotFound($"The card {id} wasn't found");
        }

        //Get the new board
        var newBoard = BoardController.ListBoard.FirstOrDefault(b => b.Id == newBoardID);

        // check if the board {newID} exists
        if (newBoard == null)
        {
            return NotFound($"Board with ID {newBoardID} wasn't found.");
        }

        // change the board id of the card
        card.BoardId = newBoardID;

        boardWithCard.CardList.Remove(card);
        newBoard.CardList.Add(card);

        return Ok($"The card {id} was move to board {newBoardID}");
    }
    
    [HttpGet("/card/search")]
    public ActionResult<Board> Search([FromQuery] SearchQuery parameters)
    {
        var cards = BoardList.listBoard
            .SelectMany(board => board.CardList) // Flatten the list of cards from all boards
            .Where(card => 
                (string.IsNullOrEmpty(parameters.Title) || card.Name.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(parameters.Description) || card.Description.Contains(parameters.Description, StringComparison.OrdinalIgnoreCase)))
            .ToList();
        
        if (!cards.Any())
        {
            return NotFound("No cards found matching the search criteria.");
        }
        
        return Ok(cards);
    }

    [HttpGet("/card/{id}/sort")]
    public ActionResult<Board> SortBoard(int boardId, [FromQuery] SortValues query)
    {
        Board board = BoardList.listBoard.FirstOrDefault(b => b.Id == boardId);
        
        if (board == null)
        {
            return NotFound($"The board {boardId} wasn't found");
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

        return board;
    }
}