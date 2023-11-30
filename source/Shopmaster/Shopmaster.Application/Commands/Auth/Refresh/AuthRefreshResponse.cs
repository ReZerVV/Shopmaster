namespace Shopmaster.Application.Commands.Auth;

public record AuthRefreshResponse(
    string AccessToken,
    string RefreshToken
);