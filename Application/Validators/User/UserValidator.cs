using Application.Dtos;
using FluentValidation;

namespace Application.Validators.User
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty")
                .NotNull().WithMessage("User ID cannot be null")
                .Must(BeAValidGuid).WithMessage("Invalid User ID format");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username cannot be empty")
                .NotNull().WithMessage("Username cannot be null")
                .MaximumLength(50).WithMessage("Username length cannot exceed 50 characters");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .NotNull().WithMessage("Password cannot be null")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }

        private bool BeAValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}