using Ynov.Business.Models;

namespace Ynov.Business.Models;

public class Board
{
    public List<Card> CardList { get; set; } = new();
    public string? Name { get; set; }
    public long Id { get; set; }
}