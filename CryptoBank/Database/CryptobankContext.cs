using CryptoBank.Features.News.Domain;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Database;

public class CryptobankContext : DbContext
{
    public required DbSet<News> News { get; set; }

    public CryptobankContext(DbContextOptions<CryptobankContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<News>()
            .Property(x => x.Id)
            .UseIdentityAlwaysColumn();
    }
}