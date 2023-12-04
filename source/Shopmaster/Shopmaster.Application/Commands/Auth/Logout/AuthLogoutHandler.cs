using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.Logout;

public class AuthLogoutHandler : IRequestHandler<AuthLogoutRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthLogoutHandler(IUserRepository userRepository, ITokenRepository tokenRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task Handle(AuthLogoutRequest request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        if (_tokenRepository.GetByUserId(userId) is not RefreshToken refreshToken)
        {
            throw new ApplicationException("User not found");
        }

        _tokenRepository.Remove(refreshToken);

        _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");

        return Task.CompletedTask;
    }
}
