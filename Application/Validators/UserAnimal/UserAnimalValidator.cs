using Application.Dtos;
using FluentValidation;

namespace Application.Validators.UserAnimal
{
    public class UserAnimalValidator : AbstractValidator<UserAnimalDto>
    {
        public UserAnimalValidator()
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
}