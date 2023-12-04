using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Application.Common.Services.Auth;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Auth.Register;

public class AuthRegisterHandler : IRequestHandler<AuthRegisterRequest>
{
    private const int ExpiryMinutes = 120;
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthRegisterHandler(IUserRepository userRepository, ITokenRepository tokenRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _emailSender = emailSender;
    }

    public Task Handle(AuthRegisterRequest request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetByEmail(request.Email) is not null)
        {
            throw new ApplicationException("User with given email already exists");
        }

        string passwordHash = _passwordHasher.Hash(request.Password);

        User user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = passwordHash
        };
        _userRepository.Add(user);
            
        string confirmationLink = $"http://localhost:3000/auth/verify/{user.Id}";
        _emailSender.Send(user.Email, "Confirmation account", $"Confiration link {confirmationLink}");

        return Task.CompletedTask;
    }
}
