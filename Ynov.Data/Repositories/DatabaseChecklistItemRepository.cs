using Ynov.Business.IRespositories;
using Ynov.Business.Models;
using Ynov.Data.Contexts;

namespace Ynov.Data.Repositories;

public class DatabaseChecklistItemRepository : IChecklistItemRepository
{
    private readonly TrellodDbContext _context;

    public DatabaseChecklistItemRepository(TrellodDbContext context)
    {
        _context = context;
    }

    public List<ChecklistItem> Get()
    {
        return _context.ChecklistsItems.ToList();
    }

    public ChecklistItem? Get(long id)
    {
        return _context.ChecklistsItems.FirstOrDefault(c => c.Id == id);
    }

    public ChecklistItem Add(Checklist checklist, ChecklistItem checklistItem)
    {
        checklist.ChecklistItems.Add(checklistItem);
        _context.ChecklistsItems.Add(checklistItem);
        _context.SaveChanges();
        return checklistItem;
    }

    public ChecklistItem? Modify(ChecklistItem cChecklistItem)
    {
        var checklistItem = _context.ChecklistsItems.Find(cChecklistItem.Id);
        if (checklistItem != null)
        {
            checklistItem.Name = cChecklistItem.Name;
            _context.SaveChanges();
        }

        return checklistItem;
    }

    public ChecklistItem? SetStatus(ChecklistItem cChecklistItem)
    {
        var checklistItem = _context.ChecklistsItems.Find(cChecklistItem.Id);
        if (checklistItem != null)
        {
            checklistItem.Status = cChecklistItem.Status;
            _context.SaveChanges();
        }
        
        return checklistItem;
    }


    public void Delete(ChecklistItem checklistItem)
    {
        _context.ChecklistsItems.Remove(checklistItem);
        _context.SaveChanges();
    }
}