using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ColorsEndpoints.ColorsUpdateOrInsertEndpoint;

namespace eParking.Endpoints.ColorsEndpoints;

[Route("Colors")]
public class ColorsUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ColorsUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ColorsUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        Colors? Color;

        if (isInsert)
        {
            Color = new Colors();
            db.Colors.Add(Color);
        }
        else
        {
            Color = await db.Colors.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (Color == null)
            {
                throw new KeyNotFoundException("Color not found");
            }
        }

        Color.Name = request.Name;
        Color.HexCode = request.HexCode;

        await db.SaveChangesAsync(cancellationToken);
    }

    public class ColorsUpdateOrInsertRequest
    {
        
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty;
    }
}

