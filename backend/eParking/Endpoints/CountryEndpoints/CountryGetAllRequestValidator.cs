using FluentValidation;
using static eParking.Endpoints.CountryEndpoints.CountryGetAllEndpoint;

namespace eParking.Endpoints.CountryEndpoints;

public class CountryGetAllRequestValidator : AbstractValidator<CountryGetAllRequest>
{
    public CountryGetAllRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 500)
            .WithMessage("PageSize must be between 1 and 500.");

        RuleFor(x => x.Q)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Q))
            .WithMessage("Search query (Q) cannot exceed 200 characters.");
    }
}
