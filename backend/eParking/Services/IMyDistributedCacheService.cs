using Microsoft.Extensions.Caching.Distributed;

namespace eParking.Services;

/// <summary>Redis distributed cache service – uses IDistributedCache (SetString/GetString).</summary>
public interface IMyDistributedCacheService
{
    Task<string?> GetStringAsync(string key, CancellationToken cancellationToken = default);
    Task SetStringAsync(string key, string value, DistributedCacheEntryOptions? options = null, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>Invalidates the genders cache (bumps version so all keys genders:* become invalid).</summary>
    Task InvalidateGendersAsync(CancellationToken cancellationToken = default);

    /// <summary>Returns current cache version for genders (for building cache key).</summary>
    Task<string> GetGendersCacheVersionAsync(CancellationToken cancellationToken = default);
}
