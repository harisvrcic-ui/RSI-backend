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

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            //query = query.Where(c => c.Name.Contains(request.Q));
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
        public string? Q { get; set; } = string.Empty;
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

