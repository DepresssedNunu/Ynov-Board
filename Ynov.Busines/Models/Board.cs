using System.ComponentModel.DataAnnotations;
using Ynov.Business.Models;

namespace Ynov.Business.Models;

public class Board
{
    [Required]
    [StringLength(20)]
    public string Name { get; set; }
    public long Id { get; set; }
    
    public List<Card> CardList { get; set; } = new();
}