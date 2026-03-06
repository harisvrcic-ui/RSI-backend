using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CityEndpoints.CityGetByIdEndpoint;

namespace eParking.Endpoints.CityEndpoints;

[Route(ApiRouteConstants.Cities)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CityGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<CityGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<CityGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var city = await db.Cities
            .Where(c => c.ID == id)
            .Select(c => new CityGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name,
                CountryId = c.CountryId,
                CountryName = c.Country.Name
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (city == null)
        {
            return NotFound("City not found");
        }

        return Ok(city);
    }

    public class CityGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
} 