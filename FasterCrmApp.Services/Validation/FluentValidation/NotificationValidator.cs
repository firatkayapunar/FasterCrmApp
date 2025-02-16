using FasterCrmApp.Entities;
using FluentValidation;

namespace FasterCrmApp.Services.Validation.FluentValidation
{
    internal class NotificationValidator : AbstractValidator<Notification>
    {
        public NotificationValidator()
        {
            // Text alanı boş olamaz ve 80 karakteri geçemez.
            RuleFor(notification => notification.Text)
                .NotEmpty().WithMessage("Text cannot be empty.")
                .MaximumLength(80).WithMessage("Text cannot exceed 60 characters.");
        }
    }
}
