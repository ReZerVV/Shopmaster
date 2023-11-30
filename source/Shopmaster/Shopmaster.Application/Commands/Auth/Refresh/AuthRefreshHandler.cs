using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth;

public class AuthRefreshHandler : IRequestHandler<AuthRefreshRequest, AuthRefreshResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRefreshHandler(IHttpContextAccessor httpContextAccessor, ITokenRepository tokenRepository, ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenRepository = tokenRepository;
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public Task<AuthRefreshResponse> Handle(AuthRefreshRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_userRepository.GetById(userId) is not User user)
        {
            throw new ApplicationException("User not found");
        }
        
        if (_tokenRepository.GetByUserId(userId) is not RefreshToken refreshToken)
        {
            throw new ApplicationException("User not found");
        }

        string accessToken = _tokenGenerator.GenerateToken(user);

        return Task.FromResult(
            new AuthRefreshResponse(
                AccessToken: accessToken,
                RefreshToken: _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]
            )
        );
    }
}
