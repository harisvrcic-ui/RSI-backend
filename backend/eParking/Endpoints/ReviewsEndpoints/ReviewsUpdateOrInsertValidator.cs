namespace eParking.Endpoints.ReviewsEndpoints;

using eParking.Endpoints.ReviewsEndpoints;
using FluentValidation;
using static eParking.Endpoints.ReviewsEndpoints.ReviewsUpdateOrInsertEndpoint;

public class ReviewsUpdateOrInsertValidator : AbstractValidator<ReviewsUpdateOrInsertEndpoint.ReviewsUpdateOrInsertRequest>
{
    public ReviewsUpdateOrInsertValidator()
    {
        RuleFor(x => x.ID)
              .GreaterThanOrEqualTo(0)
              .WithMessage("ID must be 0 (insert) or greater than 0 (update)");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User is required");

        RuleFor(x => x.ReservationId)
            .GreaterThan(0)
            .WithMessage("Reservation is required");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Comment is required and cannot exceed 500 characters");
    }
}

