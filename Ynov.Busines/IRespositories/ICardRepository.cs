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
    public Card? Move(Card card, long newId, Board newBoard);
    public Card? SetPriority(Card card, Priority priority);
    public Card? SetUser(Card card, User user);
    public List<Card> Search(SearchQuery parameters, bool caseSensible);
    public void Delete(Card card);
}