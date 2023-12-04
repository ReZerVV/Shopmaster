using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.Confirm;

public class AuthConfirmHandler : IRequestHandler<AuthConfirmRequest>
{
    private readonly IUserRepository _userRepository;

    public AuthConfirmHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task Handle(AuthConfirmRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Link, out Guid userId))
        {
            throw new ApplicationException("Invalid link");
        }

        if (_userRepository.GetById(userId) is not User user)
        {
            throw new ApplicationException("User not found");
        }

        user.IsConfirmed = true;
        
        _userRepository.Update(user);

        return Task.CompletedTask;
    }
}
