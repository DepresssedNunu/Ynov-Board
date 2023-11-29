using Microsoft.AspNetCore.Mvc;
using Ynov.Business.DTOitem;
using Ynov.Business.Dtos;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.API.Controllers;

[Route("[controller]")]
[ApiController]
public class LabelController : ControllerBase
{
    private readonly ILabelServices _labelServices;

    public LabelController(ILabelServices labelServices)
    {
        _labelServices = labelServices;
    }

    //Get all labels
    [HttpGet]
    public ActionResult<Label> Get()
    {
        BusinessResult<List<Label>> getLabelsResult = _labelServices.Get();
        
        if (getLabelsResult.IsSuccess)
        {
            return Ok(getLabelsResult.Result);
        }
        
        BusinessError? error = getLabelsResult.Error;
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

    //Get a specific label
    [HttpGet("{id}")]
    public ActionResult<Label> Get(long id)
    {
        BusinessResult<Label> getLabelResult = _labelServices.Get(id);

        if (getLabelResult.IsSuccess)
        {
            return Ok(getLabelResult.Result);
        }

        BusinessError? error = getLabelResult.Error;
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

    //Add a label
    [HttpPost]
    public ActionResult<Label> Add([FromBody] LabelDto labelDto)
    {
        Label label = new()
        {
            Name = labelDto.Name
        };
        
        BusinessResult<Label> addLabelResult = _labelServices.Add(label);

       // Création de la réponse
        if (addLabelResult.IsSuccess)
        {
            Label result = addLabelResult.Result!;
            
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // Gestion des erreurs
        BusinessError? error = addLabelResult.Error;
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

    //Update the name of a label
    [HttpPut("{id}")]
    public ActionResult<Label> Modify(long id, [FromBody] LabelDto labelDto)
    {
        Label label = new()
        {
            Name = labelDto.Name
        };

        BusinessResult<Label> updateLabelResult = _labelServices.Modify(id, label);

        if (updateLabelResult.IsSuccess)
        {
            return Ok(updateLabelResult.Result);
        }

        BusinessError? error = updateLabelResult.Error;
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

    //Delete a label
    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        BusinessResult deleteLabelResult = _labelServices.Delete(id);

        if (deleteLabelResult.IsSuccess)
        {
            return Ok();
        }

        BusinessError? error = deleteLabelResult.Error;
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