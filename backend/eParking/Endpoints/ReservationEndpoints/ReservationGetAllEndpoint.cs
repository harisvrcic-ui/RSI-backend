using System.Text.Json.Serialization;
using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;
using static eParking.Endpoints.ReservationEndpoints.ReservationGetAllEndpoint;

namespace eParking.Endpoints.ReservationEndpoints;

[Route(ApiRouteConstants.Reservations)]
[MyAuthorization(isAdmin: true, isUser: true)]
public class ReservationGetAllEndpoint(ApplicationDbContext db, IMyAuthService auth) : MyEndpointBaseAsync
    .WithRequest<ReservationGetAllRequest>
    .WithResult<MyPagedList<ReservationGetAllResponse>>
{
    [HttpGet(ApiRouteConstants.Filter)]
    public override async Task<MyPagedList<ReservationGetAllResponse>> HandleAsync([FromQuery] ReservationGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var authInfo = auth.GetAuthInfoFromRequest();
        // Korisnik (nije admin) smije vidjeti samo svoje rezervacije
        if (!authInfo.IsAdmin && authInfo.IsLoggedIn)
            request.UserId = authInfo.UserId;

        var query = db.Reservations
            .AsQueryable();

        if (request.UserId.HasValue)
            query = query.Where(r => db.Cars.Any(c => c.ID == r.CarID && c.UserId == request.UserId.Value));

        if (request.OnlyActive == true)
            query = query.Where(r => r.EndDate >= DateTime.UtcNow);

        var q = request.Q?.Trim();
        if (!string.IsNullOrWhiteSpace(q))
        {
            if (int.TryParse(q, out var idVal))
                query = query.Where(r => r.ID == idVal);
            else
            {
                var qLower = q.ToLower();
                query = query.Where(r =>
                    db.ParkingSpots.Any(ps => ps.ID == r.ParkingSpotID && ((ps.DisplayNameSearch != null && ps.DisplayNameSearch.Contains(qLower)) || (ps.DisplayName != null && ps.DisplayName.ToLower().Contains(qLower)))) ||
                    db.ReservationTypes.Any(rt => rt.ID == r.ReservationTypeID && rt.Name != null && rt.Name.ToLower().Contains(qLower)) ||
                    db.Cars.Any(c => c.ID == r.CarID && ((c.Model != null && c.Model.ToLower().Contains(qLower)) || (c.LicensePlate != null && c.LicensePlate.ToLower().Contains(qLower)))));
            }
        }

        // Project to result type (join Cars for UserId, join ParkingSpots for DisplayName)
        var projectedQuery = query
            .Join(db.Cars, r => r.CarID, c => c.ID, (r, c) => new { r, c })
            .Join(db.ParkingSpots, rc => rc.r.ParkingSpotID, ps => ps.ID, (rc, ps) => new { rc.r, rc.c, ps })
            .Select(x => new ReservationGetAllResponse
            {
                ID = x.r.ID,
                CarID = x.r.CarID,
                ParkingSpotID = x.r.ParkingSpotID,
                ParkingSpotDisplayName = x.ps.DisplayName ?? ("#" + x.ps.ParkingNumber),
                ReservationTypeID = x.r.ReservationTypeID,
                StartDate = x.r.StartDate,
                EndDate = x.r.EndDate,
                FinalPrice = x.r.FinalPrice,
                UserId = x.c.UserId
            });

        // Create paginated response with filter
        var result = await MyPagedList<ReservationGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class ReservationGetAllRequest : MyPagedRequest
    {
        [FromQuery(Name = "q")]
        public string? Q { get; set; } = string.Empty;

        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "onlyActive")]
        public bool? OnlyActive { get; set; }
    }

    public class ReservationGetAllResponse
    {
        public int ID { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        public int CarID { get; set; }
        public int ParkingSpotID { get; set; }

        [JsonPropertyName("parkingSpotDisplayName")]
        public string? ParkingSpotDisplayName { get; set; }

        public int ReservationTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double FinalPrice { get; set; }
    }
}
