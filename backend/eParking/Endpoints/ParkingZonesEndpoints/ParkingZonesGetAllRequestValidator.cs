using FluentValidation;
using static eParking.Endpoints.ParkingZonesEndpoints.ParkingZonesGetAllEndpoint;

namespace eParking.Endpoints.ParkingZonesEndpoints;

public class ParkingZonesGetAllRequestValidator : AbstractValidator<ParkingZonesGetAllRequest>
{
    public ParkingZonesGetAllRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.Q)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Q))
            .WithMessage("Search query (Q) cannot exceed 200 characters.");
    }
}
