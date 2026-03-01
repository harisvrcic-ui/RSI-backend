using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;
using static eParking.Endpoints.ReservationEndpoints.ReservationGetAllEndpoint;

namespace eParking.Endpoints.ReservationEndpoints;

[Route("Reservations")]
public class ReservationGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReservationGetAllRequest>
    .WithResult<MyPagedList<ReservationGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<ReservationGetAllResponse>> HandleAsync([FromQuery] ReservationGetAllRequest request, CancellationToken cancellationToken = default)
    {
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

        // Project to result type
        var projectedQuery = query.Select(c => new ReservationGetAllResponse
        {
            ID = c.ID,
            CarID = c.CarID,
            ParkingSpotID = c.ParkingSpotID,
            ReservationTypeID = c.ReservationTypeID,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
            FinalPrice = c.FinalPrice
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
        public int CarID { get; set; }
        public int ParkingSpotID { get; set; }
        public int ReservationTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double FinalPrice { get; set; }
    }
}

