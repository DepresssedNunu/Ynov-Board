using Ynov.Business.DTOitem;
using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.Business.Services;

public class CardServices : ICardServices
{
    private readonly ICardRepository _cardRepository;
    private readonly IBoardRepository _boardRepository;

    public CardServices(ICardRepository cardRepository, IBoardRepository boardRepository)
    {
        _cardRepository = cardRepository;
        _boardRepository = boardRepository;
    }

    public BusinessResult<List<Card>> Get()
    {
        List<Card> cards = _cardRepository.Get();

        return BusinessResult<List<Card>>.FromSuccess(cards);
    }

    public BusinessResult<Card> Get(long id)
    {
        Card? card = _cardRepository.Get(id);

        if (card is null)
        {
            return BusinessResult<Card>.FromError($"The card {id} do not exist", BusinessErrorReason.NotFound);
        }

        return BusinessResult<Card>.FromSuccess(card);
    }
    
    public BusinessResult<Card> Add(Card card)
    {
        Board? board = _boardRepository.Get(card.BoardId);

        if (board is null)
        {
            return BusinessResult<Card>.FromError($"The board {card.BoardId} do not exist, card cannot be created.", BusinessErrorReason.NotFound);
        }
        
        card = _cardRepository.Add(card);

        return BusinessResult<Card>.FromSuccess(card);
    }

    public BusinessResult<Card> Modify(long id, Card mCard)
    {
        Card? card = _cardRepository.Get(id);

        if (card is null)
        {
            return BusinessResult<Card>.FromError($"The card {id} do not exist", BusinessErrorReason.NotFound);
        }

        card.Name = mCard.Name;
        card.Description = mCard.Description;
        _cardRepository.Modify(card);
        return BusinessResult<Card>.FromSuccess(card);
    }

    public BusinessResult<Card> ModifyCardName(long id, Card mCard)
    {
        Card? card = _cardRepository.Get(id);

        if (card is null)
        {
            return BusinessResult<Card>.FromError($"The card {id} do not exist", BusinessErrorReason.NotFound);
        }

        card.Name = mCard.Name;
        _cardRepository.ModifyCardName(card);
        return BusinessResult<Card>.FromSuccess(card);
    }
    
    public BusinessResult<Card> ModifyCardDescription(long id, Card mCard)
    {
        Card? card = _cardRepository.Get(id);

        if (card is null)
        {
            return BusinessResult<Card>.FromError($"The card {id} do not exist", BusinessErrorReason.NotFound);
        }

        card.Description = mCard.Description;
        _cardRepository.ModifyCardDescription(card);
        return BusinessResult<Card>.FromSuccess(card);
    }

    public BusinessResult<Card> Move(long id, long newId)
    {
        Card? card = _cardRepository.Get(id);
        
        if (card is null)
        {
            return BusinessResult<Card>.FromError($"The card {id} do not exist", BusinessErrorReason.NotFound);
        }

        Board? board = _boardRepository.Get(newId);
        
        if (board is null)
        {
            return BusinessResult<Card>.FromError($"The board {id} do not exist", BusinessErrorReason.NotFound);
        }
        
        _cardRepository.Move(card, newId);
        
        return BusinessResult<Card>.FromSuccess(card);
    }

    public BusinessResult<List<Card>> Search(SearchQuery parameters)
    {
        List<Card>? cards = _cardRepository.Search(parameters);
        
        return BusinessResult<List<Card>>.FromSuccess(cards);
    }

    public BusinessResult Delete(long id)
    {
        Card? board = _cardRepository.Get(id);

        if (board is null)
        {
            return BusinessResult<Card>.FromError($"The board {id} do not exist", BusinessErrorReason.NotFound);
        }
        _cardRepository.Delete(board);
        return BusinessResult.FromSuccess();
    }
}