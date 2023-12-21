// GuidValidator
using FluentValidation;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(guid => guid)
            .NotEmpty().WithMessage("Dog Id can not be empty").WithErrorCode("EmptyGuid")
            .NotNull().WithMessage("Dog Id can not be null").WithErrorCode("NullGuid")
            .Must(BeAValidGuid).WithMessage("Invalid Dog Id format").WithErrorCode("InvalidGuid");
    }

    private bool BeAValidGuid(Guid guid)
    {
        // Ett giltigt Guid
        return guid != Guid.Empty;
    }
}
