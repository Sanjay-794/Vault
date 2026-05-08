namespace Devnet.Vault.Application.Features.Shared.Cache.Interfaces.Services;


public interface ICacheService
{

    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

    Task<T?> GetAsync<T>(string key);

    Task<bool> ExistsAsync(string key);

    Task RemoveAsync(string key);

    Task<bool> RefreshExpiryAsync(string key, TimeSpan expiry);
}
