using System.ComponentModel.DataAnnotations;

namespace Ynov.Business.Models;

public class Label
{
    [Key] public long Id { get; set;}
    public string Name { get; set; } = null!;
}