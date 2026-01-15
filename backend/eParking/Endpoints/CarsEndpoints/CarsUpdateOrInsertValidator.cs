namespace eParking.Endpoints.CarsEndpoints;

using FluentValidation;
using static eParking.Endpoints.CarsEndpoints.CarsUpdateOrInsertEndpoint;

public class CarsUpdateOrInsertValidator : AbstractValidator<CarsUpdateOrInsertEndpoint.CarsUpdateOrInsertRequest>
{
    public CarsUpdateOrInsertValidator()
    {
        RuleFor(x => x.Model)
             .NotEmpty().WithMessage("Model is required")
             .MaximumLength(100).WithMessage("Model cannot exceed 100 characters");

        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage("License plate is required")
            .Matches(@"^[A-Z0-9\-]+$").WithMessage("License plate can only contain uppercase letters, numbers and dashes")
            .MaximumLength(10).WithMessage("License plate cannot exceed 10 characters");

        RuleFor(x => x.YearOfManufacture)
            .InclusiveBetween(1886, DateTime.Now.Year).WithMessage("Year of manufacture is invalid");

        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("BrandId must be greater than 0");

        RuleFor(x => x.ColorId)
            .GreaterThan(0).WithMessage("ColorId must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");
    }
}

