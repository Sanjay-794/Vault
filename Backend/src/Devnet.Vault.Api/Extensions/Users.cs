using System.Security.Claims;

namespace Devnet.Vault.Api.Extensions;

public static class VaultUsers
{
    public static long GetUserId(this HttpContext context)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdClaim, out var userId))
            return userId;

        return 0;
    }

    public static string? GetRequestIpAddress(this HttpContext context)
    {
        return context.Connection.RemoteIpAddress?.ToString();
    }
}
