using System.ComponentModel.DataAnnotations;

namespace Ynov.Business.Models;

public class Checklist
{
    [Key] public long Id { get; set; }
    [Required] public string Name { get; set; }
    public List<ChecklistItem> ChecklistItems { get; set; } = new();
    [Required] public long CardId { get; set; }
}