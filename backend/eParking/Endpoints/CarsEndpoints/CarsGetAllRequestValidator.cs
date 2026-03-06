using FluentValidation;
using static eParking.Endpoints.CarsEndpoints.CarsGetAllEndpoint;

namespace eParking.Endpoints.CarsEndpoints;

public class CarsGetAllRequestValidator : AbstractValidator<CarsGetAllRequest>
{
    public CarsGetAllRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be at least 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 500)
            .WithMessage("PageSize must be between 1 and 500.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .When(x => x.UserId.HasValue)
            .WithMessage("UserId must be greater than 0 when provided.");

        RuleFor(x => x.Q)
            .MaximumLength(200)
            .When(x => !string.IsNullOrEmpty(x.Q))
            .WithMessage("Search query (Q) cannot exceed 200 characters.");
    }
}
