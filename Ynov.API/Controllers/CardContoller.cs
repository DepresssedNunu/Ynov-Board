using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("/card")]
public class CardContoller : ControllerBase
{
    private readonly ILogger<CardContoller> _logger;

    public CardContoller(ILogger<CardContoller> logger)
    {
        _logger = logger;
    }
    
    //Get all cards
    [HttpGet("/card/all")]
    public ActionResult<Board> GetCard()
    {
        var data = BoardList.listBoard
            .Select(board => board.CardList
                .Select(card => new
                {
                    card.BoardId,
                    card.Id,
                    card.Name,
                    card.Description,
                    card.CreationDate,
                }).ToList())
            .ToList();

        return Ok(data);
    }


    // Get a specific card
    [HttpGet("Board/{id}/listCard/")]
    public ActionResult<Board> GetCard(int id)
    {
        if (id > BoardList.listBoard.Count)
        {
            NotFound($"The board number {id} wasn't found ");
        }

        var board = BoardList.listBoard[id];
        var cardInfo = board.CardList
            .Select(card => $"{card.Name} : {card.Description}")
            .ToList();

        return Ok($"List of Cards of the Board {board.Name}:\n" + string.Join("\n", cardInfo));
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
    [HttpDelete("card/delete/")]
    public ActionResult<Board> DeleteCard(int id, string name)
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
        return (card == null)
            ? NotFound($"The card {name} wasn't found")
            : Ok($"Card {card.Name} has been Removed !" + currentBoard.CardList.Remove(card));
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