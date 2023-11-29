using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IRespositories;

public interface IChecklistRepository
{
    public List<Checklist> Get();
    public Checklist? Get(long id);
    public Checklist Add(Checklist checklist, Card card);
    public Checklist? Modify(Checklist checklist);
    public void Delete(Checklist checklist);
}