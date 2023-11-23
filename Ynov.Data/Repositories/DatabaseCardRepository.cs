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

    public Card Add(Card card, Board board)
    {
        card.CreationDate = DateTime.UtcNow;
        board.CardList.Add(card);
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

    public Card? Move(Card mCard, long newId, Board newBoard)
    {
        var card = _context.Cards.Find(mCard.Id);
        var oldBoard = _context.Boards.Find(mCard.BoardId);
        if (card != null && oldBoard != null)
        {
            card.BoardId = newId;
            oldBoard.CardList.Remove(card);
            newBoard.CardList.Add(card);
            _context.SaveChanges();
        }

        return card;
    }
    
    public Card? SetPriority(Card mCard, Priority priority)
    {
        var card = _context.Cards.Find(mCard.Id);
        if (card != null)
        {
            Console.Write(priority);
            Console.WriteLine("----------------------------------------------------------------");
            card.Priority = priority;
            _context.SaveChanges();
        }

        return card;
    }
    
    public List<Card> Search(SearchQuery parameters, bool caseSensible)
    {
        if (!caseSensible)
        {
            parameters.Title = string.IsNullOrEmpty(parameters.Title) ? "" : parameters.Title.ToLower();
            parameters.Description = string.IsNullOrEmpty(parameters.Description) ? "" : parameters.Description.ToLower();
        }
        
        var cards = _context.Cards
            .Where(card =>
                (string.IsNullOrEmpty(parameters.Title) || card.Name.ToLower().Contains(parameters.Title)) &&
                (string.IsNullOrEmpty(parameters.Description) || card.Description.ToLower().Contains(parameters.Description)))
            .ToList();

        return cards;
    }

    public void Delete(Card card)
    {
        _context.Cards.Remove(card);
        _context.SaveChanges();
    }
}