using System.ComponentModel.DataAnnotations;

namespace Ynov.Business.Models;

public class Card
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; protected set; }
    public long BoardId { get; set; }
    public Board? Board { get; set; }

    public Card()
    {
    }
}