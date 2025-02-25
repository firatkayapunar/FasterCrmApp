using FasterCrmApp.Entities.Concrete;
using FluentValidation;

namespace FasterCrmApp.Services.Validation.FluentValidation
{
    internal class LeadValidator : AbstractValidator<Lead>
    {
        public LeadValidator()
        {
            // Name alanı boş olamaz ve 60 karakteri geçemez.
            RuleFor(client => client.Summary)
                .NotEmpty().WithMessage("Summary cannot be empty.")
                .MaximumLength(250).WithMessage("Summary cannot exceed 60 characters.");

            // Email alanı boş olamaz ve doğru bir email formatında olmalı.
            RuleFor(client => client.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 60 characters.");
        }
    }
}
