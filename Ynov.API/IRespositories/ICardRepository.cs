using Ynov.API.DTOitem;
using Ynov.Busines.Models;

namespace Ynov.API.IRespositories;

public interface ICardRepository
{
    public List<Card> Get();
    public Card? Get(long id);
    public Card Add(Card card);
    public Card? ModifyCardName(Card card);
    public Card? ModifyCardDescription(Card card);
    public Card? Modify(Card card);
    public Card? Move(Card card, long newId);
    public List<Card> Search(SearchQuery parameters);
    public void Delete(Card card);
}