using Microsoft.EntityFrameworkCore;
using MiniLink.Domain.Models;

namespace MiniLink.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<Link> Links { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}