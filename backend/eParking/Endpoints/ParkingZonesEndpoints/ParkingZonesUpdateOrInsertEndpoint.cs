using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesUpdateOrInsertEndpoint;
using static eParking.Endpoints.CountryEndpoints.CountryUpdateOrInsertEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

[Route(ApiRouteConstants.ParkingZones)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ParkingZonesUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ParkingZonesUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ParkingZonesUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        ParkingZones? ParkingZone;

        if (isInsert)
        {
            ParkingZone = new ParkingZones();
            db.ParkingZones.Add(ParkingZone);
        }
        else
        {
            ParkingZone = await db.ParkingZones.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (ParkingZone == null)
            {
                throw new KeyNotFoundException("ParkingZone not found");
            }
        }

        ParkingZone.Name = request.Name;
        ParkingZone.IsActive = request.IsActive;
       


        await db.SaveChangesAsync(cancellationToken);
    }

    public class ParkingZonesUpdateOrInsertRequest
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}

