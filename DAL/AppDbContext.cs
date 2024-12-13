using Microsoft.EntityFrameworkCore;
using Domain;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<Shareholder> Shareholders { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<ShareholderInBusiness> ShareholdersInBusinesses { get; set; }
    public DbSet<ShareholderType> ShareholdersTypes { get; set; }
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
    }

}