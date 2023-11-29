using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IServices;

public interface ILabelServices
{
    public BusinessResult<List<Label>> Get(); 
    
    public BusinessResult<Label> Get(long id);
    
    public BusinessResult<Label> Add(Label board);
    
    public BusinessResult<Label> Modify(long id, Label mLabel);
    
    public BusinessResult Delete(long id);
}