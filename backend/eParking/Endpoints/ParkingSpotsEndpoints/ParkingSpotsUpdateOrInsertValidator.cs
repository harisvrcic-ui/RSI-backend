namespace eParking.Endpoints.ParkingSpotsEndpoints;

using FluentValidation;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsUpdateOrInsertEndpoint;

public class ParkingSpotsUpdateOrInsertValidator : AbstractValidator<ParkingSpotsUpdateOrInsertEndpoint.ParkingSpotsUpdateOrInsertRequest>
{
    public ParkingSpotsUpdateOrInsertValidator()
    {
        RuleFor(x => x.ID)
              .GreaterThanOrEqualTo(0)
              .WithMessage("ID must be 0 (insert) or greater than 0 (update)");

        RuleFor(x => x.ParkingNumber)
            .GreaterThan(0)
            .WithMessage("Parking number must be greater than 0");

        RuleFor(x => x.ParkingSpotTypeId)
            .GreaterThan(0)
            .WithMessage("Parking spot type is required");

        RuleFor(x => x.ZoneId)
            .GreaterThan(0)
            .WithMessage("Zone is required");
    }
}

