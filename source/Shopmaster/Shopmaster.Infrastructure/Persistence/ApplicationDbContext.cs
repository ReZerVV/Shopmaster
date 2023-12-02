using Microsoft.EntityFrameworkCore;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Advert>(options => 
        {
            options.Property(advert => advert.Images)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            options.HasOne(advert => advert.Category)
                .WithMany()
                .HasForeignKey(advert => advert.CategoryId);
            options.OwnsOne(advert => advert.Location, location =>
            {
                location.Property(location => location.City).HasColumnName("City");
                location.Property(location => location.Region).HasColumnName("Region");
                location.Property(location => location.Country).HasColumnName("Country");
            });
            options.OwnsOne(advert => advert.Price, price =>
            {
                price.Property(price => price.Currency).HasColumnName("Currency");
                price.Property(price => price.Value).HasColumnName("Value");
            });
        });
    }
}