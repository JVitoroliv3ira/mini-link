using Microsoft.EntityFrameworkCore;
using MiniLink.Domain.Models;
using MiniLink.Infrastructure.Configurations;

namespace MiniLink.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<Link> Links { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("SQ_LINKS", "public");
        
        modelBuilder.ApplyConfiguration(new LinkConfiguration());
    }
}