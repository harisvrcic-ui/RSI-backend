using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotTypeEndpoints.ParkingSpotTypeUpdateOrInsertEndpoint;
using static eParking.Endpoints.CountryEndpoints.CountryUpdateOrInsertEndpoint;

namespace eParking.Endpoints.ParkingSpotTypeEndpoints;

[Route("ParkingSpotTypes")]
public class ParkingSpotTypeUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingSpotTypeUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ParkingSpotTypeUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        ParkingSpotType? ParkingSpotType;

        if (isInsert)
        {
            ParkingSpotType = new ParkingSpotType();
            db.ParkingSpotTypes.Add(ParkingSpotType);
        }
        else
        {
            ParkingSpotType = await db.ParkingSpotTypes.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (ParkingSpotType == null)
            {
                throw new KeyNotFoundException("ParkingSpotType not found");
            }
        }

        ParkingSpotType.Name = request.Name;
        ParkingSpotType.Description = request.Description;
        ParkingSpotType.PriceMultiplier = request.PriceMultiplier;


        await db.SaveChangesAsync(cancellationToken);
    }

    public class ParkingSpotTypeUpdateOrInsertRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceMultiplier { get; set; }
    }
}

