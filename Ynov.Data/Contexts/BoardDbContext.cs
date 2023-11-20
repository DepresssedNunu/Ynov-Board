using Microsoft.EntityFrameworkCore;
using Ynov.Business.Models;

namespace Ynov.Data.Contexts;

public class BoardDbContext : DbContext
{
    public DbSet<Board> Board { get; set; }
    public DbSet<Card> Card { get; set; }
    
    public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options)
    {
    }
}