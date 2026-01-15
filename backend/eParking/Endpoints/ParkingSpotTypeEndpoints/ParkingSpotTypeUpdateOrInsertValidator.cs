namespace eParking.Endpoints.ParkingSpotTypeEndpoints;

using FluentValidation;
using static eParking.Endpoints.ParkingSpotTypeEndpoints.ParkingSpotTypeUpdateOrInsertEndpoint;

public class ParkingSpotTypeUpdateOrInsertValidator : AbstractValidator<ParkingSpotTypeUpdateOrInsertEndpoint.ParkingSpotTypeUpdateOrInsertRequest>
{
    public ParkingSpotTypeUpdateOrInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Parking Spot Type name is required")
            .Matches(@"^[a-zA-ZÀ-ÿčćžšđČĆŽŠĐ\s]+$").WithMessage("Parking Spot Type name can only contain letters and spaces")
            .MaximumLength(100).WithMessage("Parking Spot Type name cannot exceed 100 characters");
    }
}

