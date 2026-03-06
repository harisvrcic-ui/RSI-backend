using eParking.Data;
using eParking.Data.Models;
using eParking.Helper;
using eParking.Helper.Api;
using eParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.GenderEndpoints.GenderGetByIdEndpoint;

namespace eParking.Endpoints.GenderEndpoints;

[Route(ApiRouteConstants.Genders)]
[MyAuthorization(isAdmin: true, isUser: false)]
public class GenderGetByIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<GenderGetByIdRequest>
    .WithActionResult<GenderGetByIdResponse>
{
    [HttpGet(ApiRouteConstants.Id)]
    public override async Task<ActionResult<GenderGetByIdResponse>> HandleAsync([FromRoute] GenderGetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var gender = await db.Set<Gender>()
            .FirstOrDefaultAsync(g => g.ID == request.ID, cancellationToken);

        if (gender == null)
        {
            return NotFound($"Gender with ID {request.ID} not found.");
        }

        return Ok(new GenderGetByIdResponse
        {
            ID = gender.ID,
            Name = gender.Name,
            CreatedAt = gender.CreatedAt,
            IsActive = gender.IsActive
        });
    }

    // DTO for request
    public class GenderGetByIdRequest
    {
        public int ID { get; set; }
    }

    // DTO for response
    public class GenderGetByIdResponse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
