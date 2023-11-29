using Microsoft.EntityFrameworkCore;
using Ynov.Business.IRespositories;
using Ynov.Business.Models;
using Ynov.Data.Contexts;

namespace Ynov.Data.Repositories;

public class DatabaseChecklistRepository : IChecklistRepository
{
    private readonly TrellodDbContext _context;

    public DatabaseChecklistRepository(TrellodDbContext context)
    {
        _context = context;
    }

    public List<Checklist> Get()
    {
        return _context.Checklists
            .Include(ch => ch.ChecklistItems)
            .ToList();
    }

    public Checklist? Get(long id)
    {
        return _context.Checklists
            .Include(ch => ch.ChecklistItems)
            .FirstOrDefault(c => c.Id == id);
    }

    public Checklist Add(Checklist checklist, Card card)
    {
        card.Checklists.Add(checklist);
        _context.Checklists.Add(checklist);
        _context.SaveChanges();
        return checklist;
    }

    public Checklist? Modify(Checklist cChecklist)
    {
        var checklist = _context.Checklists.Find(cChecklist.Id);
        if (checklist != null)
        {
            checklist.Name = cChecklist.Name;
            _context.SaveChanges();
        }

        return checklist;
    }

    public void Delete(Checklist checklist)
    {
        _context.Checklists.Remove(checklist);
        _context.SaveChanges();
    }
}