using Microsoft.EntityFrameworkCore;
using UsernameService.Models;

namespace UsernameService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UsernameRecord> UsernameRecords => Set<UsernameRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsernameRecord>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}
