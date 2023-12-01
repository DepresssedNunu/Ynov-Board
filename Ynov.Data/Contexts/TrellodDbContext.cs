using Microsoft.EntityFrameworkCore;
using Ynov.Business.Models;

namespace Ynov.Data.Contexts;

public class TrellodDbContext : DbContext
{
    public TrellodDbContext(DbContextOptions<TrellodDbContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Label> Labels { get; set; } = null!;
    public DbSet<Checklist> Checklists { get; set; } = null!;
    public DbSet<ChecklistItem> ChecklistsItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Label>()
            .HasIndex(l => l.Name)
            .IsUnique();
    }
}