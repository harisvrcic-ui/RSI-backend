using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CarsEndpoints.CarsDeleteEndpoint;

namespace eParking.Endpoints.CarsEndpoints;

[Route("Cars")]
public class CarsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Car = await db.Cars.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (Car == null)
        {
            throw new KeyNotFoundException("Car not found");
        }

        Car.IsActive = false;


        await db.SaveChangesAsync(cancellationToken);
    }


}

