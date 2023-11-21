using Microsoft.EntityFrameworkCore;
using Ynov.Business.Models;

namespace Ynov.Data.Contexts;

public class BoardDbContext : DbContext
{
    public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options)
    {
    }
    
    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
}
