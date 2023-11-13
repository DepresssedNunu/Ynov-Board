using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IServices;

public interface ICardServices
{
    public BusinessResult<List<Card>> Get();

    public BusinessResult<Card> Get(long id);

    public BusinessResult<Card> Add(Card card);

    public BusinessResult<Card> Modify(long id, Card card);

    public BusinessResult<Card> ModifyCardName(long id, Card card);

    public BusinessResult<Card> ModifyCardDescription(long id, Card card); 
    
    public BusinessResult<Card> Move(long id, long newId);

    public BusinessResult<List<Card>> Search(SearchQuery parameters);
    
    public BusinessResult Delete(long id);
}