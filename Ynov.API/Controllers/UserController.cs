using Microsoft.AspNetCore.Mvc;
using Ynov.Business.Dtos;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserServices _userServices;
    
    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    //Get all users
    [HttpGet]
    public ActionResult<User> Get()
    {
        BusinessResult<List<User>> getUsersResult = _userServices.Get();
        
        if (getUsersResult.IsSuccess)
        {
            return Ok(getUsersResult.Result);
        }
        
        BusinessError? error = getUsersResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }
    
    //Get a specific user
    [HttpGet("{id}")]
    public ActionResult<User> Get(long id)
    {
        BusinessResult<User> getresult = _userServices.Get(id);

        if (getresult.IsSuccess)
        {
            return Ok(getresult.Result);
        }

        BusinessError? error = getresult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }
    
    //Add a user
    [HttpPost]
    public ActionResult<User> Add([FromBody] UserDto userDto)
    {
        User user = new()
        {
            Name = userDto.Name,
            PasswordHash = userDto.Password,
            Email = userDto.Email
        };
        
        BusinessResult<User> addResult = _userServices.Add(user);

        if (addResult.IsSuccess)
        {
            User result = addResult.Result!;
            
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        BusinessError? error = addResult.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            case BusinessErrorReason.Forbidden:
                return Forbid(error.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }
    
    //Update the name of an user
    [HttpPatch("{id}/name")]
    public ActionResult<User> ModifyUsername(long id, [FromBody] ModifyUserNameDto userDto)
    {
        User user = new()
        {
            Name = userDto.Name
        };

        BusinessResult<User> result = _userServices.ModifyUserName(id, user);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        BusinessError? error = result.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            case BusinessErrorReason.Forbidden:
                return Forbid(error.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

    //Update the name of an user
    [HttpPatch("{id}/password")]
    public ActionResult<User> ModifyPassword(long id, [FromBody] ModifyPasswordDto userDto)
    {
        User user = new()
        {
            PasswordHash = userDto.NewPassword
        };

        BusinessResult<User> result = _userServices.ChangePassword(id, user, userDto.OldPassword);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        BusinessError? error = result.Error;
        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            case BusinessErrorReason.Forbidden:
                return Forbid(error.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }
    
    // Delete user
    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        BusinessResult deleteUserResult = _userServices.Delete(id);

        if (deleteUserResult.IsSuccess)
        {
            return Ok();
        }

        BusinessError? error = deleteUserResult.Error;

        switch (error?.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

}