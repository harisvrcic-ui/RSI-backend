<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
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

<<<<<<< HEAD
        if (request.UserId.HasValue)
            query = query.Where(c => c.car.UserId == request.UserId.Value);

=======
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
        // Filter by search query (model, brand name, license plate)
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            query = query.Where(c =>
                (c.car.Model != null && c.car.Model.Contains(q)) ||
                (c.brand.Name != null && c.brand.Name.Contains(q)) ||
                (c.car.LicensePlate != null && c.car.LicensePlate.Contains(q)));
        }

<<<<<<< HEAD
        // Project to result type (BrandName/Model/LicensePlate null-safe; Picture as base64 string for consistent JSON)
        var projectedQuery = query
            .OrderBy(x => x.car.ID)
            .Select(x => new CarsGetAllResponse
            {
                ID = x.car.ID,
                BrandId = x.car.BrandId,
                BrandName = x.brand.Name ?? "",
                ColorId = x.car.ColorId,
                UserId = x.car.UserId,
                Model = x.car.Model ?? "",
                LicensePlate = x.car.LicensePlate ?? "",
                YearOfManufacture = x.car.YearOfManufacture,
                PictureBase64 = x.car.Picture != null ? Convert.ToBase64String(x.car.Picture) : null,
                IsActive = x.car.IsActive
            });

        // Create paginated response (when UserId is not sent, e.g. admin, returns all cars)
=======
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
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
        var result = await MyPagedList<CarsGetAllResponse>.CreateAsync(projectedQuery, request, cancellationToken);

        return result;
    }

    public class CarsGetAllRequest : MyPagedRequest
    {
        [FromQuery(Name = "q")]
        public string? Q { get; set; }
<<<<<<< HEAD

        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "pageNumber")]
        public new int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public new int PageSize { get; set; } = 20;
=======
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
    }

    public class CarsGetAllResponse
    {
<<<<<<< HEAD
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }
        [JsonPropertyName("brandName")]
        public string BrandName { get; set; } = string.Empty;
        [JsonPropertyName("colorId")]
        public int ColorId { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;
        [JsonPropertyName("licensePlate")]
        public string LicensePlate { get; set; } = string.Empty;
        [JsonPropertyName("yearOfManufacture")]
        public int YearOfManufacture { get; set; }
        [JsonPropertyName("picture")]
        public string? PictureBase64 { get; set; }
        [JsonPropertyName("isActive")]
=======
        public int ID { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public int ColorId { get; set; }
        public int UserId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public int YearOfManufacture { get; set; }
        public byte[]? Picture { get; set; }
>>>>>>> 9d8f07312ad0d0046110f2fb150f74fa5ef7b7f9
        public bool IsActive { get; set; } = true;
    }
}

