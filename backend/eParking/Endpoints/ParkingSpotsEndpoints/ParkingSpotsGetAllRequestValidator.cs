using FluentValidation;
using static eParking.Endpoints.ParkingSpotsEndpoints.ParkingSpotsGetAllEndpoint;

namespace eParking.Endpoints.ParkingSpotsEndpoints;

public class ParkingSpotsGetAllRequestValidator : AbstractValidator<ParkingSpotsGetAllRequest>
{
    public ParkingSpotsGetAllRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 500)
            .WithMessage("PageSize must be between 1 and 500.");

        RuleFor(x => x.ZoneGroup)
            .GreaterThan(0)
            .When(x => x.ZoneGroup.HasValue)
            .WithMessage("ZoneGroup must be greater than 0 when provided.");

        RuleFor(x => x.ZoneId)
            .GreaterThan(0)
            .When(x => x.ZoneId.HasValue)
            .WithMessage("ZoneId must be greater than 0 when provided.");

        RuleFor(x => x.Q)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Q))
            .WithMessage("Search query (Q) cannot exceed 200 characters.");

        RuleFor(x => x.Name)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Name filter cannot exceed 200 characters.");
    }
}
