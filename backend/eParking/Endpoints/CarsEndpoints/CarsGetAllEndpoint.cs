using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CarsEndpoints.CarsGetAllEndpoint;


namespace eParking.Endpoints.CarsEndpoints;

[Route("Cars")]
public class CarsGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<CarsGetAllRequest>
    .WithResult<MyPagedList<CarsGetAllResponse>>
{
    [HttpGet("filter")]
    public override async Task<MyPagedList<CarsGetAllResponse>> HandleAsync([FromQuery] CarsGetAllRequest request, CancellationToken cancellationToken = default)
    {
        var query = db.Cars
            .AsQueryable();

        // Filter by search query
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            //query = query.Where(c => c.Name.Contains(request.Q));
        }

        // Project to result type
        var projectedQuery = query.Select(c => new CarsGetAllResponse
        {
            ID = c.ID,
            BrandId = c.BrandId,
            ColorId = c.ColorId,
            UserId = c.UserId,
            Model = c.Model,
            LicensePlate = c.LicensePlate,
            YearOfManufacture = c.YearOfManufacture,
            Picture = c.Picture,
            IsActive = c.IsActive
        });

        // Create paginated response with filter
        var result = await MyPagedList<CarsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class CarsGetAllRequest : MyPagedRequest
    {
        public string? Q { get; set; } = string.Empty;
    }

    public class CarsGetAllResponse
    {
        public int ID { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int UserId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public int YearOfManufacture { get; set; }
        public byte[]? Picture { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

