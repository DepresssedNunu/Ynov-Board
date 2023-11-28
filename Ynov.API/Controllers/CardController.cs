using Microsoft.AspNetCore.Mvc;
using Ynov.Business.DTOitem;
using Ynov.Business.Dtos;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardServices _cardServices;

    public CardController(ICardServices cardServices)
    {
        _cardServices = cardServices;
    }

    //Get all cards
    [HttpGet]
    public ActionResult<List<Card>> Get()
    {
        // Appel du service
        BusinessResult<List<Card>> getCardResult = _cardServices.Get();

        // Création de la réponse
        if (getCardResult.IsSuccess)
        {
            return Ok(getCardResult.Result);
        }

        // Gestion des erreurs
        BusinessError? error = getCardResult.Error;
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
    
    // Get a specific card
    [HttpGet("{id}")]
    public ActionResult<Card> GetCard(long id)
    {
        BusinessResult<Card> getCardResult = _cardServices.Get(id);

        if (getCardResult.IsSuccess)
        {
            return Ok(getCardResult.Result);
        }

        BusinessError? error = getCardResult.Error;
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

    [HttpGet("search")]
    public ActionResult<List<Card>> Search([FromQuery] SearchQuery parameters, bool caseSensible)
    {
        BusinessResult<List<Card>> getSearchCardsResult = _cardServices.Search(parameters, caseSensible);

        if (getSearchCardsResult.IsSuccess)
        {
            return Ok(getSearchCardsResult.Result);
        }

        BusinessError? error = getSearchCardsResult.Error;
        switch (error.Reason)
        {
            case BusinessErrorReason.BusinessRule:
                return BadRequest(error?.ErrorMessage);
            case BusinessErrorReason.NotFound:
                return NotFound(error?.ErrorMessage);
            default:
                return BadRequest(error?.ErrorMessage);
        }
    }

    // ADD CARD with description and name to a specific board
    [HttpPost]
    public ActionResult<Card> Add([FromBody] CreateCardDto cardDto)
    {
        Card card = new()
        {
            Name = cardDto.Name,
            Description = cardDto.Description,
            BoardId = cardDto.BoardId,
            Priority = cardDto.Priority
        };

        BusinessResult<Card> addCardResult = _cardServices.Add(card);

        if (addCardResult.IsSuccess)
        {
            Card result = addCardResult.Result!;

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        BusinessError? error = addCardResult.Error;
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

    //Modify card name AND description
    [HttpPut("{id}")]
    public ActionResult<Card> Modify(long id, [FromBody] ModifyCardDto cardDto)
    {
        Card card = new()
        {
            Name = cardDto.Name,
            Description = cardDto.Description,
        };
        
        BusinessResult<Card> updateCardResult = _cardServices.Modify(id, card);

        if (updateCardResult.IsSuccess)
        {
            return Ok(updateCardResult.Result);
        }

        BusinessError? error = updateCardResult.Error;
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

    //Modify card name
    [HttpPatch("{id}/name")]
    public ActionResult<Card> ModifyCardName(long id, [FromBody] ModifyCardNameDto cardDto)
    {
        Card card = new()
        {
            Name = cardDto.Name,
        };
        
        BusinessResult<Card> updateCardResult = _cardServices.ModifyCardName(id, card);

        if (updateCardResult.IsSuccess)
        {
            return Ok(updateCardResult.Result);
        }

        BusinessError? error = updateCardResult.Error;
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

    //Modify card name
    [HttpPatch("{id}/description")]
    public ActionResult<Card> ModifyCardDescription(long id, [FromBody] ModifyCardDescriptionDto cardDto)
    {
        Card card = new()
        {
            Description = cardDto.Description,
        };
        
        BusinessResult<Card> updateCardResult = _cardServices.ModifyCardDescription(id, card);

        if (updateCardResult.IsSuccess)
        {
            return Ok(updateCardResult.Result);
        }

        BusinessError? error = updateCardResult.Error;
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

    //Move a card form a board to an other
    [HttpPatch("{id}/move")]
    public ActionResult<Card> Move(long id, long newId)
    {
        BusinessResult<Card> updateCardResult = _cardServices.Move(id, newId);

        if (updateCardResult.IsSuccess)
        {
            return Ok(updateCardResult.Result);
        }

        BusinessError? error = updateCardResult.Error;
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
    
    //Set or change the priority label of a card
    [HttpPatch("{id}/set_priority")]
    public ActionResult<Card> SetPriority(long id, Priority priority)
    {
        BusinessResult<Card> updateCardResult = _cardServices.SetPriority(id, priority);

        if (updateCardResult.IsSuccess)
        {
            return Ok(updateCardResult.Result);
        }

        BusinessError? error = updateCardResult.Error;
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

    // Delete card
    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        BusinessResult deleteCardResult = _cardServices.Delete(id);

        if (deleteCardResult.IsSuccess)
        {
            return Ok();
        }

        BusinessError? error = deleteCardResult.Error;

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