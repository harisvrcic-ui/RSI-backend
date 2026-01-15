namespace eParking.Endpoints.ReservationTypeEndpoints;

using eParking.Endpoints.ReservationTypeEndpoints;
using FluentValidation;
using static eParking.Endpoints.ReservationTypeEndpoints.ReservationTypeUpdateOrInsertEndpoint;

public class ReservationTypeUpdateOrInsertValidator : AbstractValidator<ReservationTypeUpdateOrInsertEndpoint.ReservationTypeUpdateOrInsertRequest>
{
    public ReservationTypeUpdateOrInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ReservationType name is required")
            .Matches(@"^[a-zA-ZÀ-ÿčćžšđČĆŽŠĐ\s]+$").WithMessage("ReservationType name can only contain letters and spaces")
            .MaximumLength(100).WithMessage("ReservationType name cannot exceed 100 characters");
    }
}

