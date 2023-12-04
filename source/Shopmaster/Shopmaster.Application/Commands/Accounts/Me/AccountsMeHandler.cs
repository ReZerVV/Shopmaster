using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Accounts.Me;

public class AccountsMeHandler : IRequestHandler<AccountsMeRequest, AccountsMeResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountsMeHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public Task<AccountsMeResponse> Handle(AccountsMeRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_userRepository.GetById(userId) is not User user)
        {
            throw new ApplicationException("User not found");
        }

        return Task.FromResult(
            new AccountsMeResponse(
                Id: user.Id.ToString(),
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email
            )
        );
    }
}
