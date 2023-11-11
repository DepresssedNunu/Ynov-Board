using Ynov.API.DTOitem;
using Ynov.API.IRespositories;
using Ynov.Busines.Models;

namespace Ynov.Data.Repositories;

public class InMemoryCardRepository : ICardRepository
{
    public static long _idCounter = 1;
    public static List<Card> _cards = new();

    public List<Card> Get()
    {
        return _cards;
    }

    public Card? Get(long id)
    {
        return _cards.Find(b => b.Id == id);
    }

    public Card Add(Card card)
    {
        card.Id = _idCounter++;
        _cards.Add(card);
        return card;
    }

    public Card? ModifyCardName(Card cCard)
    {
        Card board = _cards.Single(c => c.Id == cCard.Id);
        board.Name = cCard.Name;
        return board;
    }
    
    public Card? ModifyCardDescription(Card cCard)
    {
        Card board = _cards.Single(c => c.Id == cCard.Id);
        board.Description = cCard.Description;
        return board;
    }
    
    public Card? Modify(Card cCard)
    {
        Card card = _cards.Single(c => c.Id == cCard.Id);
        card.Description = cCard.Description;
        card.Name = cCard.Name;
        return card;
    }

    public Card? Move(Card cCard, long newId)
    {
        Card card = _cards.Single(c => c.Id == cCard.Id);
        card.Id = newId;
        return card;
    }

    public List<Card> Search(SearchQuery parameters)
    {
        
        var cards = _cards.Where(card => 
                (string.IsNullOrEmpty(parameters.Title) || card.Name.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(parameters.Description) || card.Description.Contains(parameters.Description, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        return cards;
    }

    public void Delete(Card card)
    {
        _cards.Remove(card);
    }

    
}