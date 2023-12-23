using Application.Dtos;
using FluentValidation;

public class UpdateUserAnimalValidator : AbstractValidator<UserAnimalDto>
{
    public UpdateUserAnimalValidator()
    {
        RuleFor(userAnimal => userAnimal.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty")
            .NotNull().WithMessage("User ID cannot be null")
            .Must(BeAValidGuid).WithMessage("Invalid User ID format");

        RuleFor(userAnimal => userAnimal.AnimalId)
            .NotEmpty().WithMessage("Animal ID cannot be empty")
            .NotNull().WithMessage("Animal ID cannot be null")
            .Must(BeAValidGuid).WithMessage("Invalid Animal ID format");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
