using Microsoft.Extensions.Caching.Distributed;

namespace eParking.Services;

/// <summary>Redis cache implementation via IDistributedCache – SetString/GetString (StackExchange.Redis on backend).</summary>
public class MyDistributedCacheService(IDistributedCache cache) : IMyDistributedCacheService
{
    public Task<string?> GetStringAsync(string key, CancellationToken cancellationToken = default)
        => cache.GetStringAsync(key, cancellationToken);

    public Task SetStringAsync(string key, string value, DistributedCacheEntryOptions? options = null, CancellationToken cancellationToken = default)
        => cache.SetStringAsync(key, value, options ?? new DistributedCacheEntryOptions(), cancellationToken);

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => cache.RemoveAsync(key, cancellationToken);

    private const string GendersVersionKey = "genders:_version";

    public async Task InvalidateGendersAsync(CancellationToken cancellationToken = default)
    {
        var v = await GetGendersCacheVersionAsync(cancellationToken);
        var next = int.TryParse(v, out var num) ? (num + 1).ToString() : "1";
        await cache.SetStringAsync(GendersVersionKey, next, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365) }, cancellationToken);
    }

    public async Task<string> GetGendersCacheVersionAsync(CancellationToken cancellationToken = default)
    {
        var v = await cache.GetStringAsync(GendersVersionKey, cancellationToken);
        return string.IsNullOrEmpty(v) ? "1" : v;
    }
}
