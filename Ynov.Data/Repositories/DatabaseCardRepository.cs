using Microsoft.EntityFrameworkCore;
using Ynov.Business.DTOitem;
using Ynov.Business.IRespositories;
using Ynov.Business.Models;
using Ynov.Data.Contexts;

namespace Ynov.Data.Repositories;

public class DatabaseCardRepository : ICardRepository
{
    private readonly BoardDbContext _context;

    public DatabaseCardRepository(BoardDbContext context)
    {
        _context = context;
    }

    public List<Card> Get()
    {
        return _context.Cards.ToList();
    }

    public Card? Get(long id)
    {
        return _context.Cards.FirstOrDefault(card => card.Id == id);
    }

    public Card Add(Card card)
    {
        _context.Cards.Add(card);
        _context.SaveChanges();
        return card;
    }

    public Card? ModifyCardName(Card mCard)
    {
        var card = _context.Cards.Find(mCard.Id);
        if (card != null)
        {
            card.Name = mCard.Name;
            _context.SaveChanges();
        }

        return card;
    }

    public Card? ModifyCardDescription(Card mCard)
    {
        var card = _context.Cards.Find(mCard.Id);
        if (card != null)
        {
            card.Description = mCard.Description;
            _context.SaveChanges();
        }

        return card;
    }

    public Card? Modify(Card mCard)
    {
        var card = _context.Cards.Find(mCard.Id);
        if (card != null)
        {
            card.Name = mCard.Name;
            card.Description = mCard.Description;
            _context.SaveChanges();
        }

        return card;
    }

    public Card? Move(Card mCard, long newId)
    {
        var card = _context.Cards.Find(mCard.Id);
        if (card != null)
        {
            card.Id = newId;
            _context.SaveChanges();
        }

        return card;
    }
    public List<Card> Search(SearchQuery parameters)
    {
        var cards = _context.Cards.Where(card => 
                (string.IsNullOrEmpty(parameters.Title) || card.Name.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(parameters.Description) || card.Description.Contains(parameters.Description, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        return cards;
    }

    public void Delete(Card card)
    {
        _context.Cards.Remove(card);
        _context.SaveChanges();
    }
}