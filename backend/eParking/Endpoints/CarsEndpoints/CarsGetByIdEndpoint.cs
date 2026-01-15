using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CarsEndpoints.CarsGetByIdEndpoint;

namespace eParking.Endpoints.CarsEndpoints;

[Route("Cars")]
public class CarsGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<CarsGetByIdResponse>
{
    [HttpGet("{id}")]
    public override async Task<ActionResult<CarsGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Car = await db.Cars
            .Where(c => c.ID == id)
            .Select(c => new CarsGetByIdResponse
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
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (Car == null)
        {
            return NotFound("Car not found");
        }

        return Ok(Car);
    }

    public class CarsGetByIdResponse
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

