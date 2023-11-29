using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Infrastructure.Persistence;
using Shopmaster.Infrastructure.Services.Auth;

namespace Shopmaster.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(configuration);

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITokenRepository, TokenRepository>();
        services.AddDbContext<ApplicationDbContext>(options => 
        {
            options.UseInMemoryDatabase("Shopmaster.InMemoryDatabase");
        });

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ITokenGenerator, TokenGenerator>();

        return services;
    }
}