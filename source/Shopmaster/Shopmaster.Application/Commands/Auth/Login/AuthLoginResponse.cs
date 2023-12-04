namespace Shopmaster.Application.Commands.Auth.Login;

public record AuthLoginResponse(
    string AccessToken,
    string RefreshToken
);