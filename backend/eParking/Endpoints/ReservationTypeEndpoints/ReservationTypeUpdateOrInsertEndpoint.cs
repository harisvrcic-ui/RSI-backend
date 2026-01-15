using eParking.Data;
using eParking.Data.Models;
using eParking.Helper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static eParking.Endpoints.ReservationTypeEndpoints.ReservationTypeUpdateOrInsertEndpoint;


namespace eParking.Endpoints.ReservationTypeEndpoints;

[Route("ReservationTypes")]
public class ReservationTypeUpdateOrInsertEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync
    .WithRequest<ReservationTypeUpdateOrInsertRequest>
    .WithoutResult
{
    [HttpPost]
    public override async Task HandleAsync([FromBody] ReservationTypeUpdateOrInsertRequest request, CancellationToken cancellationToken = default)
    {
        bool isInsert = request.ID == null || request.ID == 0;
        ReservationType? ReservationType;

        if (isInsert)
        {
            ReservationType = new ReservationType();
            db.ReservationTypes.Add(ReservationType);
        }
        else
        {
            ReservationType = await db.ReservationTypes.SingleOrDefaultAsync(x => x.ID == request.ID, cancellationToken);

            if (ReservationType == null)
            {
                throw new KeyNotFoundException("ReservationType not found");
            }
        }

        ReservationType.Name = request.Name;
        ReservationType.Price = request.Price;
       


        await db.SaveChangesAsync(cancellationToken);
    }

    public class ReservationTypeUpdateOrInsertRequest
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}

