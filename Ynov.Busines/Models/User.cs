using System.ComponentModel.DataAnnotations;
using Ynov.Business.Models;

namespace Ynov.Business.Models;

public class User
{
    [Required]
    [StringLength(20)]
    public string Name { get; set; }
    [Required]
    [StringLength(40)]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string PasswordHash { get; set; }
    public long Id { get; set; }
}