using FluentValidation;
using static eParking.Endpoints.CityEndpoints.CityGetAllEndpoint;

namespace eParking.Endpoints.CityEndpoints;

public class CityGetAllRequestValidator : AbstractValidator<CityGetAllRequest>
{
    public CityGetAllRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100.");

        RuleFor(x => x.CountryId)
            .GreaterThan(0)
            .When(x => x.CountryId.HasValue)
            .WithMessage("CountryId must be greater than 0 when provided.");

        RuleFor(x => x.Q)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Q))
            .WithMessage("Search query (Q) cannot exceed 200 characters.");
    }
}
