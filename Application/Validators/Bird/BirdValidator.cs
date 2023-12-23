using FluentValidation;
using Application.Dtos;

namespace Application.Validators.Bird
{
    public class BirdValidator : AbstractValidator<BirdDto>
    {
        public BirdValidator()
        {
            RuleFor(bird => bird.Name).NotEmpty().WithMessage("Bird Name cannot be empty")
                                       .NotNull().WithMessage("Bird Name cannot be NULL");

            RuleFor(bird => bird.Color).NotEmpty().WithMessage("Bird Color cannot be empty")
                                        .MaximumLength(50).WithMessage("Bird Color length cannot exceed 50 characters");
        }
    }
}
