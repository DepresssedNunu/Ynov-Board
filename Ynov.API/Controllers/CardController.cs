using Microsoft.AspNetCore.Mvc;
using Ynov.API.DTOitem;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("/card")]
public class CardController : ControllerBase
{
    private readonly ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger)
    {
        _logger = logger;
    }

    //Get all cards
    [HttpGet("/card/all")]
    public ActionResult<Board> GetCard()
    {
        var data = BoardList.listBoard
            .Select(board => board.CardList)
            .ToList();

        return Ok(data);
    }


    // Get a specific card
    [HttpGet("/card/{id}")]
    public ActionResult<Board> GetCard(int id)
    {
        var data = BoardList.listBoard
            .SelectMany(board => board.CardList)
            .FirstOrDefault(card => card.Id == id);

        if (data == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }

        return Ok(data);
    }

    // ADD CARD with description and name to a specific board
    [HttpPost("/card/add")]
    public ActionResult<Board> AddCard([FromBody] CardInputModel cardInput)
    {
        if (cardInput.BoardId > BoardList.listBoard.Count - 1)
        {
            return NotFound($"The board number {cardInput.BoardId} wasn't found ");
        } //check if the board exist

        Board currentBoard = BoardList.listBoard[cardInput.BoardId];

        currentBoard.CardList.Add(new Card(cardInput.Name, cardInput.Description, currentBoard.Id)); //add the card to the board

        return Ok("Card added: " + currentBoard.CardList[^1].Name + ": " +
                  currentBoard.CardList[^1].Description); //return the last card added
    }

    // DELETE CARD
    [HttpDelete("/card/{id}/delete")]
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
    [HttpPatch("/card/{id}/update/description")]
    public ActionResult<Board> ModifyCardDescription(int id, [FromBody] string description)
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
    [HttpPatch("/card/{id}/update/title/")]
    public ActionResult<Board> ModifyCardName(int id, [FromBody] string name)
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
    [HttpPut("/card/modify")]
    public ActionResult<Board> ModifyCard([FromBody] CardInputModel model)
    {
        //Get the board contenting the card
        var boardWithCard = Board.GetBoard(model.BoardId);

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {model.BoardId} wasn't found.");
        }

        var card = boardWithCard.CardList.FirstOrDefault(card => card.Id == model.BoardId);

        if (card == null)
        {
            return NotFound($"The card {model.BoardId} wasn't found");
        }

        card.Description = model.Description;
        card.Name = model.Name;
        return Ok($"Card {card.Id} has been modified !");
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