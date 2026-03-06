using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ColorsEndpoints.ColorsGetByIdEndpoint;

namespace eParking.Endpoints.ColorsEndpoints;

[Route(ApiRouteConstants.Colors)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ColorsGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ColorsGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<ColorsGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var Color = await db.Colors
            .Where(c => c.ID == id)
            .Select(c => new ColorsGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name,
                HexCode = c.HexCode

            })
            .FirstOrDefaultAsync(cancellationToken);

        if (Color == null)
        {
            return NotFound("Color not found");
        }

        return Ok(Color);
    }

    public class ColorsGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
    }
}

