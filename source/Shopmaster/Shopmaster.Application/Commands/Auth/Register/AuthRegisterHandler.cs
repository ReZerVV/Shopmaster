using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.Register;

public class AuthRegisterHandller : IRequestHandler<AuthRegisterRequest, AuthRegisterResponse>
{
    private const int ExpiryMinutes = 120;
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthRegisterHandller(IUserRepository userRepository, ITokenRepository tokenRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _emailSender = emailSender;
    }

    public Task<AuthRegisterResponse> Handle(AuthRegisterRequest request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetByEmail(request.Email) is not null)
        {
            throw new ApplicationException("User with given email already exists");
        }

        string passwordHash = _passwordHasher.Hash(request.Password);

        User user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = passwordHash
        };
        _userRepository.Add(user);
            
        string confirmationLink = $"{_configuration.GetSection("Host").Value}/api/v1/confirm/{user.Id}";
        _emailSender.Send(user.Email, "Confirmation account", $"Confiration link {confirmationLink}");

        string accessToken = _tokenGenerator.GenerateToken(user);

        string refreshToken = _tokenGenerator.GenerateToken(user);

        var existsRefreshToken = _tokenRepository.GetByUserId(user.Id);
        if (existsRefreshToken is not null)
        {
            existsRefreshToken.Token = refreshToken;
            existsRefreshToken.Expiry = DateTime.UtcNow.AddMinutes(ExpiryMinutes);
            _tokenRepository.Update(existsRefreshToken);
        }
        else
        {
            RefreshToken newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = refreshToken,
                Expiry = DateTime.UtcNow.AddMinutes(ExpiryMinutes)
            };
            _tokenRepository.Add(newRefreshToken);
        }

        var response = _httpContextAccessor.HttpContext.Response;
        response.Cookies.Append("refreshToken", refreshToken);

        return Task.FromResult(
            new AuthRegisterResponse(
                AccessToken: accessToken,
                RefreshToken: refreshToken
            )
        );
    }
}
