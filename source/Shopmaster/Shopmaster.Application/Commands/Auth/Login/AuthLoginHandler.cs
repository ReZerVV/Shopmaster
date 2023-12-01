using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.Login;

public class AuthLoginHandler : IRequestHandler<AuthLoginRequest, AuthLoginResponse>
{
    private const int ExpiryMinutes = 120;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ITokenRepository _tokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthLoginHandler(ITokenGenerator tokenGenerator, ITokenRepository tokenRepository, IPasswordHasher passwordHasher, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _tokenGenerator = tokenGenerator;
        _tokenRepository = tokenRepository;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<AuthLoginResponse> Handle(AuthLoginRequest request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetByEmail(request.Email) is not User user)
        {
            throw new ApplicationException("User with given email not found");
        }

        if (!user.IsConfirmed)
        {
            throw new ApplicationException("Account not activated");
        }

        if (!_passwordHasher.Verify(request.Password, user.Password))
        {
            throw new ApplicationException("Invalid password");
        }

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

        return Task.FromResult(
            new AuthLoginResponse(
                AccessToken: accessToken,
                RefreshToken: refreshToken
            )
        );
    }
}
