using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CarsEndpoints.CarsUpdateOrInsertEndpoint;

namespace eParking.Endpoints.CarsEndpoints;

[Route(ApiRouteConstants.Cars)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CarsUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<CarsUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] CarsUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        Cars? Car;

        if (isInsert)
        {
            Car = new Cars();
            db.Cars.Add(Car);
        }
        else
        {
            Car = await db.Cars.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (Car == null)
            {
                throw new KeyNotFoundException("Car not found");
            }
        }

    
        Car.BrandId = request.BrandId;
        Car.ColorId = request.ColorId;
        Car.UserId = request.UserId;
        Car.Model = request.Model;
        Car.LicensePlate = request.LicensePlate;
        Car.YearOfManufacture = request.YearOfManufacture;
        Car.Picture = request.Picture;
        Car.IsActive = request.IsActive;

        await db.SaveChangesAsync(cancellationToken);
    }

    public class CarsUpdateOrInsertRequest
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

