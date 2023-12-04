namespace Shopmaster.Application.Commands.Auth.Refresh;

public record AuthRefreshResponse(
    string AccessToken,
    string RefreshToken
);