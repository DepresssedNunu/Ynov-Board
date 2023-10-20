using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.Controllers;


[ApiController]
[Route("[controller]")]
public class TodoItemController : ControllerBase 
{
    static List<TodoItem> todoItems = new();
        
        
    [HttpGet("items")]
    public ActionResult<TodoItem> Get()
    {
        return Ok(todoItems);
    }
        
    [HttpGet("items/{id}")]
    public ActionResult<TodoItem> Get(int id)
    {
        return Ok(todoItems[id]);
    }
        
    [HttpGet("items/add/{name}")]
    public ActionResult<TodoItem> Get(string name)
    {
        TodoItem todoItem = new TodoItem(name, todoItems.Count + 1);
        todoItems.Add(todoItem);
        return Ok("Item added: " + todoItem.Name);
    }
        
    [HttpGet("items/delete/{id}")]
    public ActionResult<TodoItem> Delete(int id)
    {
        TodoItem currentItem = todoItems[id]; 
        todoItems.RemoveAt(id);
        return Ok("Item deleted:" + currentItem.Name);
    }

    [HttpGet("items/modify/{id}/{name}")]
    public ActionResult<TodoItem> Modify(int id, string name)
    {
        todoItems[id].Name = name;
        return Ok("Item modified :" + todoItems[id].Name + "| New name : " + name);
    }
        
    [HttpGet("items/done/{id}")]
    public ActionResult<TodoItem> Done(int id)
    {
        todoItems[id].IsComplete = true;
        return Ok("Item done :" + todoItems[id].Name);
    }
}