using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("[controller]")]
    
public class BoardController : ControllerBase { //make sure the boardController is a child of ControllerBase
    
    //----------------------Board Part----------------------
    
    
    
    //Get all boards
    [HttpGet("List")]
    public ActionResult<Board> Get()
    {
        return Ok(BoardList.listBoard);
    }
    
    
    
    //Get a specific board
    [HttpGet("{id}")]
    public ActionResult<Board> Get(int id)
    {
        if (id > BoardList.listBoard.Count)
        {
            return NotFound($"The board number {id} wasn't found ");
        }
        return Ok(BoardList.listBoard[id]);
    }
    
    
    
    //Add a board
    [HttpPost("add/{name}")]
    public ActionResult<Board> Get(string name)
    {
        Board board = new Board(name);
        return Ok("Board added: " + board.Name);
    }
    
    
    
    //Modify the name of a board
    [HttpPost("modify/{id}/{name}")]
    public ActionResult<Board> Modify(int id, string name)
    {
        return (id > BoardList.listBoard.Count) ? NotFound($"The board number {id} wasn't found ") :  Ok(BoardList.listBoard[id].Name = name);
    }
    
    
    
    //Delete a board
    [HttpDelete("delete/{id}")]
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
    
    
    
    
    //NEED TO WORK ON THIS CODE SINCE IT DOESNT WORK
    [HttpGet("listBoard/listCard")]
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
    [HttpGet("listBoard/{id}/listCard")]
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
    [HttpPost("listBoard/{id}/listCard/add/{name}/{description}")]
    public ActionResult<Board> AddCard(int id, string description, string name){
        
        if (id > BoardList.listBoard.Count - 1) { return NotFound($"The board number {id} wasn't found "); } //check if the board exist
        
        BoardList.listBoard[id].CardList.Add(new Card(name, description)); //add the card to the board
        
        return Ok("Card added: " + BoardList.listBoard[id].CardList[^1].Name + ": " + BoardList.listBoard[id].CardList[^1].Description); //return the last card added
    }
    
    
    
    // DELETE CARD with description and name to a specific board
    [HttpDelete("listBoard/{id}/listCard/delete/{name}")]
    public ActionResult<Board> DeleteCard(int id, string name)
    {
        if (id > BoardList.listBoard.Count) { return NotFound($"The board number {id} wasn't found "); } //check if the board exist
        Board currentBoard = BoardList.listBoard[id];
        
        if (currentBoard.CardList.Count == 0) { return NotFound($"The board number {id} has no card"); } //check if the board has cards
        
        var card = currentBoard.CardList.Find(card => card.Name == name); //find the card with the name
        return (card == null) ? NotFound($"The card {name} wasn't found") : Ok($"Card {card.Name} has been Removed !" + currentBoard.CardList.Remove(card));
    }
}
