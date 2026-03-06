using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsGetAllEndpoint;

namespace eParking.Endpoints.ParkingSpotsEndpoints;

[Route(ApiRouteConstants.ParkingSpots)]
[MyAuthorization(isAdmin: true, isUser: true, allowAnonymous: true)]
public class ParkingSpotsGetAllEndpoint(ApplicationDbContext db, IHttpContextAccessor httpContext) : MyEndpointBaseAsync
    .WithRequest<ParkingSpotsGetAllRequest>
    .WithResult<MyPagedList<ParkingSpotsGetAllResponse>>
{
    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<ParkingSpotsGetAllResponse>> HandleAsync([FromQuery] ParkingSpotsGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = from spot in db.ParkingSpots
                    join zone in db.ParkingZones on spot.ZoneId equals zone.ID
                    select new { spot, zone };

        // Filter by name: read from query string if binding fails; uses DisplayNameSearch (normalized, no diacritics)
        var nameFromQuery = httpContext.HttpContext?.Request.Query["name"].FirstOrDefault();
        var searchName = !string.IsNullOrWhiteSpace(request.Name)
            ? request.Name.Trim()
            : nameFromQuery?.Trim();
        if (!string.IsNullOrWhiteSpace(searchName))
        {
            var name = searchName.ToLowerInvariant()
                .Replace("š", "s").Replace("č", "c").Replace("ć", "c").Replace("ž", "z").Replace("đ", "d");
            query = query.Where(x =>
                (x.spot.DisplayNameSearch != null && x.spot.DisplayNameSearch.Contains(name)) ||
                (x.spot.DisplayName != null && x.spot.DisplayName.ToLower().Contains(searchName.ToLower())) ||
                x.zone.Name.ToLower().Contains(name) ||
                x.spot.ParkingNumber.ToString().Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim().ToLowerInvariant()
                .Replace("š", "s").Replace("č", "c").Replace("ć", "c").Replace("ž", "z").Replace("đ", "d");
            query = query.Where(x =>
                (x.spot.DisplayNameSearch != null && x.spot.DisplayNameSearch.Contains(q)) ||
                (x.spot.DisplayName != null && x.spot.DisplayName.ToLower().Contains(request.Q.Trim().ToLower())) ||
                x.zone.Name.ToLower().Contains(q) ||
                x.spot.ParkingNumber.ToString().Contains(q));
        }

        // Filter by zone: read zoneGroup from query string (request binding sometimes fails)
        var zoneGroupFromQuery = httpContext.HttpContext?.Request.Query["zoneGroup"].FirstOrDefault();
        int? zoneFilter = null;
        if (!string.IsNullOrEmpty(zoneGroupFromQuery) && int.TryParse(zoneGroupFromQuery, out var parsed))
            zoneFilter = parsed;
        else
            zoneFilter = request.ZoneGroup;
        if (zoneFilter.HasValue && zoneFilter.Value > 0)
            query = query.Where(x => x.spot.ZoneId == zoneFilter!.Value);
        // Filter by exact ZoneId (optional, for admin or direct zone filter)
        else if (request.ZoneId.HasValue && request.ZoneId.Value > 0)
            query = query.Where(x => x.spot.ZoneId == request.ZoneId.Value);

        // Only available (IsActive)
        if (request.OnlyAvailable == true)
            query = query.Where(x => x.spot.IsActive);

        // Open now – Baščaršija 08:00–23:00, Vijećnica and Aria 00–24 (always open)
        if (request.OpenNow == true)
        {
            var hour = DateTime.UtcNow.Hour;
            var isWithinBascarsijaHours = hour >= 8 && hour < 23;
            query = query.Where(x =>
                (x.spot.ZoneId != 1 || x.spot.ParkingNumber != 2)  // Vijećnica or Aria → always show
                || isWithinBascarsijaHours);                        // Baščaršija (Zone 1, no. 2) only 08–23
        }

        // Project to response (include ZoneName and DisplayName for display/sort/search)
        var projectedQuery = query.Select(x => new ParkingSpotsGetAllResponse
        {
            ID = x.spot.ID,
            ParkingNumber = x.spot.ParkingNumber,
            ParkingSpotTypeId = x.spot.ParkingSpotTypeId,
            ZoneId = x.spot.ZoneId,
            ZoneName = x.zone.Name,
            DisplayName = x.spot.DisplayName,
            IsActive = x.spot.IsActive
        });

        // Sort
        var sortBy = (request.SortBy ?? "").Trim().ToLower();
        projectedQuery = sortBy switch
        {
            "name" => projectedQuery.OrderBy(x => x.DisplayName ?? x.ZoneName).ThenBy(x => x.ParkingNumber),
            "price" => projectedQuery.OrderBy(x => x.ParkingNumber),
            "availability" => projectedQuery.OrderByDescending(x => x.IsActive).ThenBy(x => x.ParkingNumber),
            _ => projectedQuery.OrderBy(x => x.ZoneId).ThenBy(x => x.ParkingNumber)
        };

        var result = await MyPagedList<ParkingSpotsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);
        return result;
    }

    public class ParkingSpotsGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        /// <summary>1 = Zone 1 (Vijećnica, Baščaršija), 2 = Zone 2 (Aria)</summary>
        [FromQuery(Name = "zoneGroup")]
        public int? ZoneGroup { get; set; }
        public int? ZoneId { get; set; }
        public bool? OnlyAvailable { get; set; }
        public bool? OpenNow { get; set; }
        public string? SortBy { get; set; } = string.Empty;
    }

    public class ParkingSpotsGetAllResponse
    {
        public int ID { get; set; }
        public int ParkingNumber { get; set; }
        public int ParkingSpotTypeId { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
