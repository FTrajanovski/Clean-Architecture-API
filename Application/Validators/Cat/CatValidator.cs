using Application.Dtos;
using FluentValidation;

namespace Application.Validators.Cat
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(cat => cat.Name).NotEmpty().WithMessage("Cat Name cannot be empty")
                                    .NotNull().WithMessage("Cat Name cannot be NULL");
            // Add other validation rules for CatDto properties
        }
    }
}