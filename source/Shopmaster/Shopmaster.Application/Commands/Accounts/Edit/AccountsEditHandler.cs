using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Accounts.Edit;

public class AccountsEditHandler : IRequestHandler<AccountsEditRequest, AccountsEditResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountsEditHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<AccountsEditResponse> Handle(AccountsEditRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_userRepository.GetById(userId) is not User user)
        {
            throw new ApplicationException("User with given id not found");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        _userRepository.Update(user);

        return Task.FromResult(
            new AccountsEditResponse(
                Id: user.Id.ToString(),
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email
            )
        );
    }
}
