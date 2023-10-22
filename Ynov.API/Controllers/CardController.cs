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
        var data = BoardList.ListBoard
            .Select(board => board.CardList)
            .ToList();

        return Ok(data);
    }


    // Get a specific card
    [HttpGet("/card/{id}")]
    public ActionResult<Board> GetCard(int id)
    {
        var data = BoardList.ListBoard
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
        if (id > BoardList.ListBoard.Count - 1)
        {
            return NotFound($"The board number {id} wasn't found ");
        } //check if the board exist

        Board currentBoard = BoardList.ListBoard[id];

        currentBoard.CardList.Add(new Card(name, description, currentBoard.Id)); //add the card to the board

        return Ok("Card added: " + currentBoard.CardList[^1].Name + ": " +
                  currentBoard.CardList[^1].Description); //return the last card added
    }


    // DELETE CARD
    [HttpDelete("card/{id}/delete/")]
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
    [HttpPatch("card/{id}/update/description/")]
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
    [HttpPatch("card/{id}/update/title/")]
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
    [HttpPost("card/modify/")]
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
}