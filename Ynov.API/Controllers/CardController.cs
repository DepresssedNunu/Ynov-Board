using Microsoft.AspNetCore.Mvc;
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
    [HttpPost("card/add/")]
    public ActionResult<Board> AddCard(int id, string description, string name)
    {
        if (id > BoardList.listBoard.Count - 1)
        {
            return NotFound($"The board number {id} wasn't found ");
        } //check if the board exist

        Board currentBoard = BoardList.listBoard[id];

        currentBoard.CardList.Add(new Card(name, description, currentBoard.Id)); //add the card to the board

        return Ok("Card added: " + currentBoard.CardList[^1].Name + ": " +
                  currentBoard.CardList[^1].Description); //return the last card added
    }


    // DELETE CARD with description and name to a specific board
    [HttpDelete("card/{id}/delete/")]
    public ActionResult<Board> DeleteCard(int id)
    {
        //Get the board contenting the card
        var boardWithCard = BoardList.listBoard
            .FirstOrDefault(board => board.CardList
                .Any(card => card.Id == id));

        // check if the card {id} exists
        if (boardWithCard == null)
        {
            return NotFound($"Card with ID {id} wasn't found.");
        }
        
        var cardToDelete = boardWithCard.CardList.FirstOrDefault(card => card.Id == id);
        
        if (cardToDelete != null)
        {
            boardWithCard.CardList.Remove(cardToDelete);
            return Ok($"Card with ID {id} has been deleted.");
        }
        return NotFound($"Card with ID {id} wasn't found.");
    }

    //update card description
    [HttpPost("card/update/")]
    public ActionResult<Board> ModifyCard(int id, string name, string description)
    {
        if (id > BoardList.listBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        } //check if the board exist

        Board currentBoard = BoardList.listBoard[id];

        if (currentBoard.CardList.Count == 0)
        {
            return NotFound($"The board number {id} has no card");
        } //check if the board has cards

        var card = currentBoard.CardList.Find(card => card.Name == name); //find the card with the name

        if (card == null)
        {
            return NotFound($"The card {name} wasn't found");
        }

        card.Description = description;
        return Ok($"Card {card.Name} has been modified !");
    }

    //Modify card name AND description
    [HttpPost("card/modify/")]
    public ActionResult<Board> ModifyCard(int id, string name, string description, string newName)
    {
        if (id > BoardList.listBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        } //check if the board exist

        Board currentBoard = BoardList.listBoard[id];
        if (currentBoard.CardList.Count == 0)
        {
            return NotFound($"The board number {id} has no card");
        } //check if the board has cards

        var card = currentBoard.CardList.Find(card => card.Name == name); //find the card with the name
        if (card == null)
        {
            return NotFound($"The card {name} wasn't found");
        }

        card.Description = description;
        card.Name = newName;
        return Ok($"Card {card.Name} has been modified !");
    }
}