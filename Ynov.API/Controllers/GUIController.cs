using Microsoft.AspNetCore.Mvc;
namespace Ynov.API.Controllers;


[ApiController]
[Route("/")]

public class GUIController  : ControllerBase { 

    
    //make the index.html file the default page
    [HttpGet]
    public IActionResult Index()
    {
        return new ContentResult()
        {
            Content = System.IO.File.ReadAllText("./Views/index.cshtml"),
            ContentType = "text/html"
        };
    }
    
    //link the css file
    [HttpGet("/css/index.css")]
    public IActionResult Css()
    {
        return new ContentResult()
        {
            Content = System.IO.File.ReadAllText("./css/index.css"),
            ContentType = "text/css"
        };
    }
    
    [HttpGet("/js/index.js")]
    public IActionResult Js()
    {
        return new ContentResult()
        {
            Content = System.IO.File.ReadAllText("./js/index.js"),
            ContentType = "text/javascript"
        };
    }
}
