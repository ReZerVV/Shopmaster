namespace Shopmaster.Application.Commands.Auth.Register;

public record AuthRegisterResponse(
    string AccessToken,
    string RefreshToken
);