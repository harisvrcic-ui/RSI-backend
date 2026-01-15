namespace eParking.Endpoints.BrandEndpoints;

using FluentValidation;
using static eParking.Endpoints.BrandEndpoints.BrandUpdateOrInsertEndpoint;

public class BrandUpdateOrInsertValidator : AbstractValidator<BrandUpdateOrInsertEndpoint.BrandUpdateOrInsertRequest>
{
    public BrandUpdateOrInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Brand name is required")
            .Matches(@"^[a-zA-ZÀ-ÿčćžšđČĆŽŠĐ\s]+$").WithMessage("Brand name can only contain letters and spaces")
            .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters");
    }
}

