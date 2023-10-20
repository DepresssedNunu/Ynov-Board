using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("/")]
    
public class BoardController : ControllerBase { //make sure the boardController is a child of ControllerBase
    
    private readonly ILogger<BoardController> _logger;

    public BoardController(ILogger<BoardController> logger)
    {
        _logger = logger;
    }

    //----------------------Board Part----------------------
    
    
    
    //Get all boards
    [HttpGet("Board/List")]
    public ActionResult<Board> Get()
    {
        return Ok(BoardList.listBoard);
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
        return (id > BoardList.listBoard.Count) ? NotFound($"The board number {id} wasn't found ") :  Ok(BoardList.listBoard[id].Name = name);
    }
    
    
    
    //Delete a board
    [HttpDelete("delete/")]
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
    
    
    
    //----------------------Card Part----------------------
    
    //TODO: NEED TO WORK ON THIS CODE SINCE IT DOESNT WORK
    [HttpGet("/listAllCard")]
    public ActionResult<Board> GetCard()
    {
        var cardInfo = BoardList.listBoard
            .Select(board => board.CardList
                .Select(card => $"{card.Name} : {card.Description}")
                .ToList())
            .ToList();
        string result = "List of Cards of all Boards:\n" + string.Join("\n", cardInfo);
        return Ok(result);
    }
    
    
    
    
    // GET CARD with description and name to a specific board
    [HttpGet("Board/{id}/listCard")]
    public ActionResult<Board> GetCard(int id){   
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
    public ActionResult<Board> AddCard(int id, string description, string name){
        
        if (id > BoardList.listBoard.Count - 1) { return NotFound($"The board number {id} wasn't found "); } //check if the board exist
        
        BoardList.listBoard[id].CardList.Add(new Card(name, description)); //add the card to the board
        
        return Ok("Card added: " + BoardList.listBoard[id].CardList[^1].Name + ": " + BoardList.listBoard[id].CardList[^1].Description); //return the last card added
    }
    
    
    
    // DELETE CARD with description and name to a specific board
    [HttpDelete("card/delete/")]
    public ActionResult<Board> DeleteCard(int id, string name)
    {
        if (id > BoardList.listBoard.Count) { return NotFound($"The board number {id} wasn't found "); } //check if the board exist
        Board currentBoard = BoardList.listBoard[id];
        
        if (currentBoard.CardList.Count == 0) { return NotFound($"The board number {id} has no card"); } //check if the board has cards
        
        var card = currentBoard.CardList.Find(card => card.Name == name); //find the card with the name
        return (card == null) ? NotFound($"The card {name} wasn't found") : Ok($"Card {card.Name} has been Removed !" + currentBoard.CardList.Remove(card));
    }

    //update card description
    [HttpPost("card/update/")]
    public ActionResult<Board> ModifyCard(int id, string name, string description)
    {
        if (id > BoardList.listBoard.Count) { return NotFound($"The board number {id} wasn't found "); } //check if the board exist
        Board currentBoard = BoardList.listBoard[id];

        if (currentBoard.CardList.Count == 0) { return NotFound($"The board number {id} has no card"); } //check if the board has cards

        var card = currentBoard.CardList.Find(card => card.Name == name); //find the card with the name

        if (card == null) { return NotFound($"The card {name} wasn't found"); }

        card.Description = description;
        return Ok($"Card {card.Name} has been modified !");
    }

    //Modify card name AND description
    [HttpPost("card/modify/")]
    public ActionResult<Board> ModifyCard(int id, string name, string description, string newName)
    {
        if (id > BoardList.listBoard.Count) { return NotFound($"The board number {id} wasn't found "); } //check if the board exist
        Board currentBoard = BoardList.listBoard[id];
        if (currentBoard.CardList.Count == 0) { return NotFound($"The board number {id} has no card"); } //check if the board has cards
        var card = currentBoard.CardList.Find(card => card.Name == name); //find the card with the name
        if (card == null) { return NotFound($"The card {name} wasn't found"); }
        card.Description = description;
        card.Name = newName;
        return Ok($"Card {card.Name} has been modified !");
    }

    //
}
