using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.GenderEndpoints.GenderUpdateOrInsertEndpoint;

namespace eParking.Endpoints.GenderEndpoints;

[Route(ApiRouteConstants.Genders)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class GenderUpdateOrInsertEndpoint(ApplicationDbContext db, IMyDistributedCacheService cache) : MyEndpointBaseAsync
    .WithRequest<GenderUpdateOrInsertRequest>
    .WithResult<GenderUpdateOrInsertResponse>
{
    [HttpPost]
    public override async Task<GenderUpdateOrInsertResponse> HandleAsync([FromBody] GenderUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        Gender gender;

        if (request.ID.HasValue)
        {
            // Update existing gender
            gender = await db.Set<Gender>().FirstOrDefaultAsync(g => g.ID == request.ID.Value, cancellationToken);
            if (gender == null)
            {
                throw new Exception($"Gender with ID {request.ID.Value} not found.");
            }

            gender.Name = request.Name;
            gender.IsActive = request.IsActive;
        }
        else
        {
            // Create new gender
            gender = new Gender
            {
                Name = request.Name,
                IsActive = request.IsActive
            };

            db.Set<Gender>().Add(gender);
        }

        await db.SaveChangesAsync(cancellationToken);

        await cache.InvalidateGendersAsync(cancellationToken);

        return new GenderUpdateOrInsertResponse
        {
            ID = gender.ID,
            Name = gender.Name,
            CreatedAt = gender.CreatedAt,
            IsActive = gender.IsActive
        };
    }

    // DTO for request
    public class GenderUpdateOrInsertRequest
    {
        public int? ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    // DTO for response
    public class GenderUpdateOrInsertResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
