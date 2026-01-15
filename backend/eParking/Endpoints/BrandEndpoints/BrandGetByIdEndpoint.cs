using eParking.Data;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.BrandEndpoints.BrandGetByIdEndpoint;

namespace eParking.Endpoints.BrandEndpoints;

[Route("brands")]
public class BrandGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<BrandGetByIdResponse>
{
    [HttpGet("{id}")]
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

