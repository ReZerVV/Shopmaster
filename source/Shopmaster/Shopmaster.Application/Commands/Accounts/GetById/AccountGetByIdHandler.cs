using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Accounts.GetById;

public class AccountsGetByIdHandler : IRequestHandler<AccountsGetByIdRequest, AccountGetByIdResponse>
{
    private readonly IUserRepository _userRepository;

    public AccountsGetByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<AccountGetByIdResponse> Handle(AccountsGetByIdRequest request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetById(request.Id) is not User user)
        {
            throw new ApplicationException("User with given id not found");
        }

        return Task.FromResult(
            new AccountGetByIdResponse(
                Id: user.Id.ToString(),
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email
            )
        );
    }
}
