using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IServices;

public interface IChecklistServices
{
    public BusinessResult<List<Checklist>> Get();
    public BusinessResult<Checklist?> Get(long id);
    public BusinessResult<Checklist> Add(Checklist checklist);
    public BusinessResult<Checklist?> Modify(long id, Checklist checklist);
    public BusinessResult Delete(long id);
}