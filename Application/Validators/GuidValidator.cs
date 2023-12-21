using FluentValidation;
using System;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(guid => guid)
            .NotEmpty().WithMessage("Id cannot be empty").WithErrorCode("EmptyGuid")
            .NotNull().WithMessage("Id cannot be null").WithErrorCode("NullGuid")
            .Must(BeAValidGuid).WithMessage("Invalid Id format").WithErrorCode("InvalidGuid");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
