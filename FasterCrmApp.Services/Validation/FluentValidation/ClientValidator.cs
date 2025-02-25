using FasterCrmApp.Entities.Concrete;
using FluentValidation;

// Validation  => Doğrulama
// Validator   => Doğrulayıcı
// Validate    => Doğrula

namespace FasterCrmApp.Services.Validation.FluentValidation
{
    internal class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            // Name alanı boş olamaz ve 60 karakteri geçemez.
            RuleFor(client => client.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(60).WithMessage("Name cannot exceed 60 characters.");

            // Email alanı boş olamaz ve doğru bir email formatında olmalı.
            RuleFor(client => client.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(60).WithMessage("Email cannot exceed 60 characters.");

            // Phone alanı en fazla 25 karakter olmalı.
            RuleFor(client => client.Phone)
                .MaximumLength(25).WithMessage("Phone cannot exceed 25 characters.");

            // Description alanı en fazla 500 karakter olmalı.
            RuleFor(client => client.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
