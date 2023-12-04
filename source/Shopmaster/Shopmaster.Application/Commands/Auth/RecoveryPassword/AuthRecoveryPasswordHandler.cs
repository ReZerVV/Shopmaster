using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.RecoveryPassword;

public class AuthRecoveryPasswordHandler : IRequestHandler<AuthRecoveryPasswordRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthRecoveryPasswordHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public Task Handle(AuthRecoveryPasswordRequest request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetById(request.Id) is not User user)
        {
            throw new ApplicationException("User with given id not found");
        }

        user.Password = _passwordHasher.Hash(request.Password);
        _userRepository.Update(user);

        return Task.CompletedTask;
    }
}
