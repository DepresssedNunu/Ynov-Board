using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.Business.Services;

public class ChecklistServices : IChecklistServices
{
    private readonly ICardRepository _cardRepository;
    private readonly IChecklistRepository _checklistRepository;

    public ChecklistServices(IChecklistRepository checklistRepository, ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
        _checklistRepository = checklistRepository;
    }

    public BusinessResult<List<Checklist>> Get()
    {
        List<Checklist> checklists = _checklistRepository.Get();
        return BusinessResult<List<Checklist>>.FromSuccess(checklists);
    }

    public BusinessResult<Checklist?> Get(long id)
    {
        Checklist? checklist = _checklistRepository.Get(id);
        if (checklist is null)
        {
            return BusinessResult<Checklist>.FromError($"The checklist {id} do not exist", BusinessErrorReason.NotFound);
        }

        return BusinessResult<Checklist>.FromSuccess(checklist);
    }

    public BusinessResult<Checklist> Add(Checklist checklist)
    {
        Card? card = _cardRepository.Get(checklist.CardId);

        if (card is null)
        {
            return BusinessResult<Checklist>.FromError($"The card {checklist.CardId} do not exist, checklist cannot be created.", BusinessErrorReason.NotFound);
        }
        
        checklist = _checklistRepository.Add(checklist, card);
        return BusinessResult<Checklist>.FromSuccess(checklist);
    }

    public BusinessResult<Checklist?> Modify(long id, Checklist cChecklist)
    {
        Checklist? checklist = _checklistRepository.Get(id);

        if (checklist is null)
        {
            return BusinessResult<Checklist>.FromError($"The checklist {id} do not exist", BusinessErrorReason.NotFound);
        }

        checklist.Name = cChecklist.Name;
        _checklistRepository.Modify(checklist);
        return BusinessResult<Checklist>.FromSuccess(checklist);

    }

    public BusinessResult Delete(long id)
    {
        Checklist? checklist = _checklistRepository.Get(id);

        if (checklist is null)
        {
            return BusinessResult<Checklist>.FromError($"The checklist {id} do not exist", BusinessErrorReason.NotFound);
        }
        
        _checklistRepository.Delete(checklist);
        return BusinessResult.FromSuccess();
    }
}