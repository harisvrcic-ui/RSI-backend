namespace eParking.Endpoints.ReservationEndpoints;

using eParking.Endpoints.ReservationEndpoints;
using FluentValidation;
using static eParking.Endpoints.ReservationEndpoints.ReservationUpdateOrInsertEndpoint;

public class ReservationUpdateOrInsertValidator : AbstractValidator<ReservationUpdateOrInsertEndpoint.ReservationUpdateOrInsertRequest>
{
    public ReservationUpdateOrInsertValidator()
    {
        RuleFor(x => x.ID)
              .GreaterThanOrEqualTo(0)
              .WithMessage("ID must be 0 (insert) or greater than 0 (update)");
        RuleFor(x => x.CarID)
        .GreaterThan(0)
        .WithMessage("Car is required");

        RuleFor(x => x.ParkingSpotID)
            .GreaterThan(0)
            .WithMessage("Parking spot is required");

        RuleFor(x => x.ReservationTypeID)
            .GreaterThan(0)
            .WithMessage("Reservation type is required");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("StartDate must be before EndDate");

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("EndDate must be in the future");

        RuleFor(x => x.FinalPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("FinalPrice cannot be negative");
    }
}

