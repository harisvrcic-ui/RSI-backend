using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationTypeEndpoints.ReservationTypeGetByIdEndpoint;

namespace eParking.Endpoints.ReservationTypeEndpoints;

[Route(ApiRouteConstants.ReservationTypes)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class ReservationTypeGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<ReservationTypeGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<ReservationTypeGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var ReservationType = await db.ReservationTypes
            .Where(c => c.ID == id)
            .Select(c => new ReservationTypeGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name,
                Price = c.Price
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (ReservationType == null)
        {
            return NotFound("ReservationType not found");
        }

        return Ok(ReservationType);
    }

    public class ReservationTypeGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}

