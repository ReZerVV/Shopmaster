using Microsoft.EntityFrameworkCore;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(options =>
        {
            options.HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders);
            options.HasOne(order => order.Advert)
                .WithMany(advert => advert.Orders);
        });

        modelBuilder.Entity<Advert>(options => 
        {
            options.HasOne(advert => advert.Seller)
                .WithMany(seller => seller.Adverts);

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