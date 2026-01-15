using eParking.Data;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryGetByIdEndpoint;

namespace eParking.Endpoints.CountryEndpoints;

[Route("countries")]
public class CountryGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<CountryGetByIdRequest>
    .WithActionResult<CountryGetByIdResponse>
{
    [HttpGet("{id}")]
    public override async Task<ActionResult<CountryGetByIdResponse>> HandleAsync([FromRoute] CountryGetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var country = await db.Countries
            .Where(c => c.ID == request.ID)
            .Select(c => new CountryGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (country == null)
        {
            return NotFound("Country not found");
        }

        return Ok(country);
    }

    public class CountryGetByIdRequest
    {
        public int ID { get; set; }
    }

    public class CountryGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

