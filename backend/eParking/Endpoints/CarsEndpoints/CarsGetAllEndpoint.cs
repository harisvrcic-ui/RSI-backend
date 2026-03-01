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
            .Join(db.Brands, car => car.BrandId, brand => brand.ID, (car, brand) => new { car, brand })
            .AsQueryable();

        // Filter by search query (model, brand name, license plate)
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            query = query.Where(c =>
                (c.car.Model != null && c.car.Model.Contains(q)) ||
                (c.brand.Name != null && c.brand.Name.Contains(q)) ||
                (c.car.LicensePlate != null && c.car.LicensePlate.Contains(q)));
        }

        // Project to result type
        var projectedQuery = query.Select(x => new CarsGetAllResponse
        {
            ID = x.car.ID,
            BrandId = x.car.BrandId,
            BrandName = x.brand.Name,
            ColorId = x.car.ColorId,
            UserId = x.car.UserId,
            Model = x.car.Model,
            LicensePlate = x.car.LicensePlate,
            YearOfManufacture = x.car.YearOfManufacture,
            Picture = x.car.Picture,
            IsActive = x.car.IsActive
        });

        // Create paginated response with filter
        var result = await MyPagedList<CarsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class CarsGetAllRequest : MyPagedRequest
    {
        [FromQuery(Name = "q")]
        public string? Q { get; set; }
    }

    public class CarsGetAllResponse
    {
        public int ID { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public int ColorId { get; set; }
        public int UserId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public int YearOfManufacture { get; set; }
        public byte[]? Picture { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

