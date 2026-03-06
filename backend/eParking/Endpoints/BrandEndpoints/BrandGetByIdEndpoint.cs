using eParking.Data;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.BrandEndpoints.BrandGetByIdEndpoint;

namespace eParking.Endpoints.BrandEndpoints;

[Route(ApiRouteConstants.Brands)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class BrandGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<BrandGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<BrandGetByIdResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        var brand = await db.Brands
            .Where(c => c.ID == id)
            .Select(c => new BrandGetByIdResponse
            {
                ID = c.ID,
                Name = c.Name,
                Logo = c.Logo,
                IsActive = c.IsActive,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (brand == null)
        {
            return NotFound("Brand not found");
        }

        return Ok(brand);
    }

    public class BrandGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public bool IsActive { get; set; }
    }
}

