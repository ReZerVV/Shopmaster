using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Accounts.EditPassword;

public class AccountsEditPasswordHandler : IRequestHandler<AccountsEditPasswordRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountsEditPasswordHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IHttpContextAccessor httpContextAccessor)
    {
        this._userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }


    public Task Handle(AccountsEditPasswordRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_userRepository.GetById(userId) is not User user)
        {
            throw new ApplicationException("User with given id not found");
        }

        if (!_passwordHasher.Verify(request.OldPassword, user.Password))
        {
            throw new ApplicationException("Password mismatch");
        }

        user.Password = _passwordHasher.Hash(request.NewPassword);
        _userRepository.Update(user);

        return Task.CompletedTask;
    }
}
