using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IRespositories;

public interface IChecklistItemRepository
{
    public List<ChecklistItem> Get();
    public ChecklistItem? Get(long id);
    public ChecklistItem Add(Checklist checklist, ChecklistItem checklistItem);
    public ChecklistItem? Modify(ChecklistItem checklistItem);
    public ChecklistItem? SetStatus(ChecklistItem checklistItem);
    public void Delete(ChecklistItem checklistItem);
}