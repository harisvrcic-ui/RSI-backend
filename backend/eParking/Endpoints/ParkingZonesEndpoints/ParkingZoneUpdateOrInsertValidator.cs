namespace eParking.Endpoints.ParkingZonesEndpoints;

using FluentValidation;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesUpdateOrInsertEndpoint;

public class ParkingZonesUpdateOrInsertValidator : AbstractValidator<ParkingZonesUpdateOrInsertEndpoint.ParkingZonesUpdateOrInsertRequest>
{
    public ParkingZonesUpdateOrInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ParkingZone name is required")
            .Matches(@"^[a-zA-ZÀ-ÿčćžšđČĆŽŠĐ\s]+$").WithMessage("ParkingZone name can only contain letters and spaces")
            .MaximumLength(100).WithMessage("ParkingZone name cannot exceed 100 characters");
    }
}

