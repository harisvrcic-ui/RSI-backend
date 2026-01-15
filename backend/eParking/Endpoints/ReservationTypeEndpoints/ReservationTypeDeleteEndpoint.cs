using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationTypeEndpoints.ReservationTypeDeleteEndpoint;

namespace eParking.Endpoints.ReservationTypeEndpoints;

[Route("ReservationTypes")]
public class ReservationTypeDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ReservationType = await db.ReservationTypes.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (ReservationType == null)
        {
            throw new KeyNotFoundException("ReservationType not found");
        }

        db.ReservationTypes.Remove(ReservationType);


        await db.SaveChangesAsync(cancellationToken);
    }


}

