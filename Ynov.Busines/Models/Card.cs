using System.ComponentModel.DataAnnotations;

namespace Ynov.Business.Models;

public class Card
{
    public long Id { get; set; }
    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    
    [Required]
    public long BoardId { get; set; }
    public Priority? Priority { get; set; }
}

public enum Priority
{
    None,
    Low,
    Medium,
    High,
    Urgent
}