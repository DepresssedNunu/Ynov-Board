using Microsoft.AspNetCore.Mvc;
using Ynov.API.Models;

namespace Ynov.API.IServices;

public interface IBoardService
{
    public ActionResult<Board> Get();

    public ActionResult<Board> Add(string name);

    public ActionResult<Board> Modify(int id, string name);

    public ActionResult<Board> Delete(int id);
    
}