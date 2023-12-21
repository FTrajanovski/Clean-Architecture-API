using Application.Queries.Dogs.GetByProperties;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Dog
{
    // Skapa en ny valideringsklass för parametrarna i GetDogsByProperties
    public class GetDogsByPropertiesValidator : AbstractValidator<GetDogsByPropertiesQuery>
    {
        public GetDogsByPropertiesValidator()
        {
            RuleFor(query => query.Breed)
                .NotEmpty().WithMessage("Breed cannot be empty")
                .MaximumLength(50).WithMessage("Breed length cannot exceed 50 characters");

            RuleFor(query => query.Weight)
                .GreaterThanOrEqualTo(0).WithMessage("Weight must be a non-negative value");
        }
    }
}