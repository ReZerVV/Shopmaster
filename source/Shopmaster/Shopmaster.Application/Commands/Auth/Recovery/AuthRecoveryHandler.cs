using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.Recovery;

public class AuthRecoveryHandler : IRequestHandler<AuthRecoveryRequest>
{
    private readonly IUserRepository _userRepository;

    public AuthRecoveryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task Handle(AuthRecoveryRequest request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetByEmail(request.Email) is not User user)
        {
            throw new ApplicationException("User with given email not found");
        }

        user.IsRecovery = true;
        _userRepository.Update(user);

        return Task.CompletedTask;
    }
}
