using Microsoft.AspNetCore.Mvc;
using Ynov.Business.DTOitem;
using Ynov.Business.Dtos;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ChecklistController : ControllerBase
{
    private readonly IChecklistServices _checklistServices;

    public ChecklistController(IChecklistServices checklistServices)
    {
        _checklistServices = checklistServices;
    }

    //Get all checklists
    [HttpGet]
    public ActionResult<List<Checklist>> Get()
    {
        // Appel du service
        BusinessResult<List<Checklist>> checklistResult = _checklistServices.Get();

        // Création de la réponse
        if (checklistResult.IsSuccess)
        {
            return Ok(checklistResult.Result);
        }

        // Gestion des erreurs
        BusinessError? error = checklistResult.Error;
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

    // Get a specific checklist
    [HttpGet("{id}")]
    public ActionResult<Checklist> Checklist(long id)
    {
        BusinessResult<Checklist?> checklistResult = _checklistServices.Get(id);

        if (checklistResult.IsSuccess)
        {
            return Ok(checklistResult.Result);
        }

        BusinessError? error = checklistResult.Error;
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

    // ADD an empty checklist to a specific card
    [HttpPost]
    public ActionResult<Checklist> Add([FromBody] CreateChecklistDto checklistDto)
    {
        Checklist checklist = new()
        {
            Name = checklistDto.Name,
            CardId = checklistDto.CardId
        };

        BusinessResult<Checklist> checklistResult = _checklistServices.Add(checklist);

        if (checklistResult.IsSuccess)
        {
            Checklist result = checklistResult.Result!;

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        BusinessError? error = checklistResult.Error;
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

    //Modify checklist name
    [HttpPut("{id}")]
    public ActionResult<Checklist> Modify(long id, [FromBody] EditChecklistDto checklistDto)
    {
        Checklist checklist = new()
        {
            Name = checklistDto.Name,
        };

        BusinessResult<Checklist> checklistResult = _checklistServices.Modify(id, checklist);

        if (checklistResult.IsSuccess)
        {
            return Ok(checklistResult.Result);
        }

        BusinessError? error = checklistResult.Error;
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

    // Delete checklist
    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        BusinessResult checklistResult = _checklistServices.Delete(id);

        if (checklistResult.IsSuccess)
        {
            return Ok();
        }

        BusinessError? error = checklistResult.Error;

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