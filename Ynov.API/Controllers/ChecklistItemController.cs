using Microsoft.AspNetCore.Mvc;
using Ynov.Business.DTOitem;
using Ynov.Business.Dtos;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ChecklistItemController : ControllerBase
{
    private readonly IChecklistItemServices _checklistItemServices;

    public ChecklistItemController(IChecklistItemServices checklistItemServices)
    {
        _checklistItemServices = checklistItemServices;
    }

    //Get all checklists
    [HttpGet]
    public ActionResult<List<ChecklistItem>> Get()
    {
        // Appel du service
        BusinessResult<List<ChecklistItem>> checklistResult = _checklistItemServices.Get();

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
    public ActionResult<ChecklistItem> Checklist(long id)
    {
        BusinessResult<ChecklistItem?> checklistResult = _checklistItemServices.Get(id);

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

    // ADD an checklist item into a checklist
    [HttpPost]
    public ActionResult<ChecklistItem> Add([FromBody] CreateChecklistItemDto checklistItemDto)
    {
        ChecklistItem checklistItem = new()
        {
            Name = checklistItemDto.Name,
            ChecklistId = checklistItemDto.ChecklistId
        };

        BusinessResult<ChecklistItem> checklistResult = _checklistItemServices.Add(checklistItem);

        if (checklistResult.IsSuccess)
        {
            ChecklistItem result = checklistResult.Result!;

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

    //Modify checklist item name
    [HttpPut("{id}")]
    public ActionResult<ChecklistItem> Modify(long id, [FromBody] EditChecklistItemDto checklistItemDto)
    {
        ChecklistItem checklistItem = new()
        {
            Name = checklistItemDto.Name,
        };

        BusinessResult<ChecklistItem> checklistResult = _checklistItemServices.Modify(id, checklistItem);

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
    
    //Modify checklist item status
    [HttpPut("{id}/status")]
    public ActionResult<ChecklistItem> Modify(long id, [FromBody] bool status)
    {

        BusinessResult<ChecklistItem> checklistResult = _checklistItemServices.SetStatus(id, status);

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

    // Delete checklist item
    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        BusinessResult checklistResult = _checklistItemServices.Delete(id);

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