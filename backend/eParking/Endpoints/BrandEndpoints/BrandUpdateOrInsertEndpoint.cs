using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.BrandEndpoints.BrandUpdateOrInsertEndpoint;


namespace eParking.Endpoints.BrandEndpoints;

[Route("brands")]
public class BrandUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<BrandUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] BrandUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        Brand? brand;

        if (isInsert)
        {
            brand = new Brand();
            db.Brands.Add(brand);
        }
        else
        {
            brand = await db.Brands.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (brand == null)
            {
                throw new KeyNotFoundException("Brand not found");
            }
        }

        brand.Name = request.Name;
        brand.Logo = request.Logo;
        brand.IsActive = request.IsActive;


        await db.SaveChangesAsync(cancellationToken);
    }

    public class BrandUpdateOrInsertRequest
    {
        public int? ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Logo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

