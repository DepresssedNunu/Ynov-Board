using System.ComponentModel.DataAnnotations;
using Ynov.Business.Models;

namespace Ynov.Business.Models;

public class Board
{
    [Key] public long Id { get; set; }
    [Required] [StringLength(20)] public string Name { get; set; }
    public List<Card> CardList { get; set; } = new();
}