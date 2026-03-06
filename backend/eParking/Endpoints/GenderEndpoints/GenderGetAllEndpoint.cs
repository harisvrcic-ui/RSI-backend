using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;
using static eParking.Endpoints.GenderEndpoints.GenderGetAllEndpoint;
using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using eParking.Data.Models;

namespace eParking.Endpoints.GenderEndpoints;

[Route(ApiRouteConstants.Genders)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class GenderGetAllEndpoint(ApplicationDbContext db, IMyDistributedCacheService cache, IHttpContextAccessor httpContextAccessor) : MyEndpointBaseAsync
    .WithRequest<GenderGetAllRequest>
    .WithResult<MyPagedList<GenderGetAllResponse>>
{
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan CacheTimeout = TimeSpan.FromSeconds(1.5); // Do not wait for Redis too long if unavailable
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<GenderGetAllResponse>> HandleAsync([FromQuery] GenderGetAllRequest request, CancellationToken cancellationToken = default)
    {
        string? cacheKey = null;
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(CacheTimeout);

            var version = await cache.GetGendersCacheVersionAsync(cts.Token);
            cacheKey = $"genders:{version}:{request.PageNumber}:{request.PageSize}:{(request.Q ?? "").Trim()}";

            var cachedJson = await cache.GetStringAsync(cacheKey, cts.Token);
            if (!string.IsNullOrEmpty(cachedJson))
            {
                var payload = JsonSerializer.Deserialize<GenderCachePayload>(cachedJson, JsonOptions);
                if (payload != null)
                {
                    httpContextAccessor.HttpContext?.Response.Headers.Append("X-Cache-Status", "HIT");
                    return MyPagedList<GenderGetAllResponse>.FromCache(payload.DataItems, payload.TotalCount, payload.CurrentPage, payload.PageSize);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Timeout – fall through to database, do not wait for Redis
        }
        catch
        {
            // Cache unavailable (e.g. Redis down) – fall through to database query
        }

        // Base query for genders
        var query = db.Set<Gender>()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            query = query.Where(g => g.Name != null && g.Name.Contains(q));
        }

        var projectedQuery = query.Select(g => new GenderGetAllResponse
        {
            ID = g.ID,
            Name = g.Name ?? string.Empty,
            CreatedAt = g.CreatedAt,
            IsActive = g.IsActive
        });

        var result = await MyPagedList<GenderGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        httpContextAccessor.HttpContext?.Response.Headers.Append("X-Cache-Status", "MISS");

        if (!string.IsNullOrEmpty(cacheKey))
        {
            try
            {
                var payloadToCache = new GenderCachePayload
                {
                    DataItems = result.DataItems,
                    TotalCount = result.TotalCount,
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize
                };
                var json = JsonSerializer.Serialize(payloadToCache, JsonOptions);
                await cache.SetStringAsync(cacheKey, json, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheExpiration }, cancellationToken);
            }
            catch
            {
                // Ignore cache write failure
            }
        }

        return result;
    }

    private class GenderCachePayload
    {
        public GenderGetAllResponse[] DataItems { get; set; } = [];
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    // DTO for request with pagination and filtering support
    public class GenderGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty; // Text query for search
    }

    // DTO for response
    public class GenderGetAllResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
