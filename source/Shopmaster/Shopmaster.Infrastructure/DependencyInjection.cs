using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shopmaster.Application.Commands.Auth;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Infrastructure.Persistence;
using Shopmaster.Infrastructure.Services.Auth;

namespace Shopmaster.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.AddTransient<IEmailSender, EmailSender>();
        
        services.AddAuthorization(configuration);

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAdvertRepository, AdvertRepository>();
        services.AddTransient<ITokenRepository, TokenRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
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

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions => jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)
                )
            });

        return services;
    }
}