namespace Ynov.Business.Models;

public class ChecklistItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; } = false;
    public long ChecklistId { get; set; }
}