using Application.Queries.Birds.GetBirdsByColor;
using FluentValidation;

public class GetBirdsByColorValidator : AbstractValidator<GetBirdsByColorQuery>
{
    public GetBirdsByColorValidator()
    {
        RuleFor(query => query.Color).NotEmpty().WithMessage("Color cannot be empty")
                                     .MaximumLength(50).WithMessage("Color length cannot exceed 50 characters");
    }
}
