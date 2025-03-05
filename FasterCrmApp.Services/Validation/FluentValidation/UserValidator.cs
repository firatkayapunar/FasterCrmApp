using FasterCrmApp.Entities.Concrete;
using FluentValidation;

namespace FasterCrmApp.Services.Validation.FluentValidation
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            // Name alanı boş olamaz ve 60 karakteri geçemez.
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(60).WithMessage("Name cannot exceed 60 characters.");

            // Email alanı boş olamaz ve doğru bir email formatında olmalı.
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(60).WithMessage("Email cannot exceed 60 characters.");

            // Username alanı boş olamaz ve 25 karakteri geçemez.
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .MaximumLength(25).WithMessage("Username cannot exceed 25 characters.");

            // Password alanı boş olamaz ve 100 karakteri geçemez.
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MaximumLength(150).WithMessage("Password cannot exceed 100 characters.");
        }
    }
}
