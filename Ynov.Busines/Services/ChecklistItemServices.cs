using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.Business.Services;

public class ChecklistItemServices : IChecklistItemServices
{
    private readonly IChecklistRepository _checklistRepository;
    private readonly IChecklistItemRepository _checklistItemRepository;

    public ChecklistItemServices(IChecklistRepository checklistRepository,
        IChecklistItemRepository checklistItemRepository)
    {
        _checklistRepository = checklistRepository;
        _checklistItemRepository = checklistItemRepository;
    }

    public BusinessResult<List<ChecklistItem>> Get()
    {
        List<ChecklistItem> checklists = _checklistItemRepository.Get();
        return BusinessResult<List<ChecklistItem>>.FromSuccess(checklists);
    }

    public BusinessResult<ChecklistItem?> Get(long id)
    {
        ChecklistItem? checklistItem = _checklistItemRepository.Get(id);
        if (checklistItem is null)
        {
            return BusinessResult<ChecklistItem>.FromError($"The checklist item {id} do not exist",
                BusinessErrorReason.NotFound);
        }

        return BusinessResult<ChecklistItem>.FromSuccess(checklistItem);
    }
    public BusinessResult<ChecklistItem> Add(ChecklistItem checklistItem)
    {
        var checklist = _checklistRepository.Get(checklistItem.ChecklistId);

        if (checklist is null)
        {
            return BusinessResult<ChecklistItem>.FromError(
                $"The checklist {checklistItem.ChecklistId} do not exist, checklist item cannot be created.",
                BusinessErrorReason.NotFound);
        }

        checklistItem = _checklistItemRepository.Add(checklist, checklistItem);

        return BusinessResult<ChecklistItem>.FromSuccess(checklistItem);
    }

    public BusinessResult<ChecklistItem?> Modify(long id, ChecklistItem cChecklistItem)
    {
        ChecklistItem? checklistItem = _checklistItemRepository.Get(id);

        if (checklistItem is null)
        {
            return BusinessResult<ChecklistItem>.FromError($"The checklist item {id} do not exist",
                BusinessErrorReason.NotFound);
        }

        checklistItem.Name = cChecklistItem.Name;
        _checklistItemRepository.Modify(checklistItem);
        return BusinessResult<ChecklistItem>.FromSuccess(checklistItem);
    }
    
    public BusinessResult<ChecklistItem?> SetStatus(long id, bool status)
    {
        ChecklistItem? checklistItem = _checklistItemRepository.Get(id);
        
        if (checklistItem is null)
        {
            return BusinessResult<ChecklistItem>.FromError($"The checklist item {id} do not exist",
                BusinessErrorReason.NotFound);
        }

        checklistItem.Status = status;
        _checklistItemRepository.SetStatus(checklistItem);
        
        return BusinessResult<ChecklistItem>.FromSuccess(checklistItem);

    }


    public BusinessResult Delete(long id)
    {
        ChecklistItem? checklistItem = _checklistItemRepository.Get(id);

        if (checklistItem is null)
        {
            return BusinessResult<ChecklistItem>.FromError($"The checklist item {id} do not exist",
                BusinessErrorReason.NotFound);
        }

        _checklistItemRepository.Delete(checklistItem);
        return BusinessResult.FromSuccess();
    }
}