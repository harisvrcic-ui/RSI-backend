using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsUpdateOrInsertEndpoint;
using static eParking.Endpoints.CountryEndpoints.CountryUpdateOrInsertEndpoint;

namespace eParking.Endpoints.ParkingSpotsEndpoints;

[Route(ApiRouteConstants.ParkingSpots)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ParkingSpotsUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingSpotsUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ParkingSpotsUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        ParkingSpots? ParkingSpot;

        if (isInsert)
        {
            ParkingSpot = new ParkingSpots();
            db.ParkingSpots.Add(ParkingSpot);
        }
        else
        {
            ParkingSpot = await db.ParkingSpots.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (ParkingSpot == null)
            {
                throw new KeyNotFoundException("ParkingSpot not found");
            }
        }

        ParkingSpot.ParkingNumber = request.ParkingNumber;
        ParkingSpot.ParkingSpotTypeId = request.ParkingSpotTypeId;
        ParkingSpot.ZoneId = request.ZoneId;


        await db.SaveChangesAsync(cancellationToken);
    }

    public class ParkingSpotsUpdateOrInsertRequest
    {
        public int ID { get; set; }
        public int ParkingNumber { get; set; }
        public int ParkingSpotTypeId { get; set; }
        public int ZoneId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

