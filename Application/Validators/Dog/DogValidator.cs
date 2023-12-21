using Application.Dtos;
using FluentValidation;
using System.Data;

namespace Application.Validators.Dog
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name).NotEmpty().WithMessage("Dog Name can not be emptey")
                                    .NotNull().WithMessage("Dog Name can not be NULL");
        }
    }
}