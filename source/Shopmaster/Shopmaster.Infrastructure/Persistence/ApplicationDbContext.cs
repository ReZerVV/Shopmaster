using Microsoft.EntityFrameworkCore;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}