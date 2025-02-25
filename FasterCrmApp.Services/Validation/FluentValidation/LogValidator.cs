using FasterCrmApp.Entities.Concrete;
using FluentValidation;

namespace FasterCrmApp.Services.Validation.FluentValidation
{
    internal class LogValidator : AbstractValidator<Log>
    {
        public LogValidator()
        {
            // Text alanı boş olamaz ve 80 karakteri geçemez.
            RuleFor(log => log.Text)
                .NotEmpty().WithMessage("Text cannot be empty.")
                .MaximumLength(80).WithMessage("Text cannot exceed 60 characters.");
        }
    }
}
