using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eParking.Data;
using eParking.Helper.Api;
using eParking.Data.Models;

namespace Festify.Endpoints.GenderEndpoints;

[Route("genders")]
public class GenderDeleteEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithoutResult
{
    [HttpDelete("{id}")]
    public override async Task HandleAsync([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var gender = await db.Set<Gender>()
            .FirstOrDefaultAsync(g => g.ID == id, cancellationToken);

        if (gender == null)
        {
            throw new Exception($"Gender with ID {id} not found.");
        }

        gender.IsActive = false;

        db.Genders.Update(gender);

        await db.SaveChangesAsync(cancellationToken);

    }
}
