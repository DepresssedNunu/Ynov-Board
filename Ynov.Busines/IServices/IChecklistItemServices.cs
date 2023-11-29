using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IServices;

public interface IChecklistItemServices
{
    public BusinessResult<List<ChecklistItem>> Get();
    public BusinessResult<ChecklistItem?> Get(long id);
    public BusinessResult<ChecklistItem> Add(ChecklistItem checklistItem);
    public BusinessResult<ChecklistItem?> Modify(long id, ChecklistItem checklistItem);
    public BusinessResult<ChecklistItem?> SetStatus(long id, bool status);
    public BusinessResult Delete(long id);
}