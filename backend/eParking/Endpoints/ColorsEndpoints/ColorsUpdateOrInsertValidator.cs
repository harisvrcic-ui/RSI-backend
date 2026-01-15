namespace eParking.Endpoints.ColorsEndpoints;

using FluentValidation;
using static eParking.Endpoints.ColorsEndpoints.ColorsUpdateOrInsertEndpoint;

public class ColorsUpdateOrInsertValidator : AbstractValidator<ColorsUpdateOrInsertEndpoint.ColorsUpdateOrInsertRequest>
{
    public ColorsUpdateOrInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Color name is required")
            .Matches(@"^[a-zA-ZÀ-ÿčćžšđČĆŽŠĐ\s]+$").WithMessage("Color name can only contain letters and spaces")
            .MaximumLength(100).WithMessage("Color name cannot exceed 100 characters");
    }
}

