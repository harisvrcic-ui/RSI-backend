using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.CountryEndpoints.CountryUpdateOrInsertEndpoint;

namespace eParking.Endpoints.CountryEndpoints;

[Route(ApiRouteConstants.Countries)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class CountryUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<CountryUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] CountryUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        Country? country;

        if (isInsert)
        {
            country = new Country();
            db.Countries.Add(country);
        }
        else
        {
            country = await db.Countries.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (country == null)
            {
                throw new KeyNotFoundException("Country not found");
            }
        }

        country.Name = request.Name;

        await db.SaveChangesAsync(cancellationToken);
    }

    public class CountryUpdateOrInsertRequest
    {
        public int? ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

