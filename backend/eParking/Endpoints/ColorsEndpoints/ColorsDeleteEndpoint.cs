using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ColorsEndpoints.ColorsDeleteEndpoint;

namespace eParking.Endpoints.ColorsEndpoints;

[Route("Colors")]
public class ColorsDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Color = await db.Colors.SingleOrDefaultAsync(x => x.ID == id, cancellationToken);

        if (Color == null)
        {
            throw new KeyNotFoundException("Color not found");
        }


        db.Colors.Remove(Color);


        await db.SaveChangesAsync(cancellationToken);
    }


}

