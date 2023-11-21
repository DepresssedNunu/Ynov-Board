using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IRespositories;

public interface ICardRepository
{
    public List<Card> Get();
    public Card? Get(long id);
    public Card Add(Card card, Board board);
    public Card? ModifyCardName(Card card);
    public Card? ModifyCardDescription(Card card);
    public Card? Modify(Card card);
    public Card? Move(Card card, long newId, Board board);
    public List<Card> Search(SearchQuery parameters);
    public void Delete(Card card);
}