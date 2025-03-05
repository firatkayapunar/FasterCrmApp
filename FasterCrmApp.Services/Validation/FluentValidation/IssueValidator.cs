using FasterCrmApp.Entities.Concrete;
using FluentValidation;

namespace FasterCrmApp.Services.Validation.FluentValidation
{
    internal class IssueValidator : AbstractValidator<Issue>
    {
        public IssueValidator()
        {
            RuleFor(issue => issue.Summary)
                .NotEmpty().WithMessage("Summary cannot be empty.")
                .MaximumLength(60).WithMessage("Summary cannot exceed 60 characters.");
        }
    }
}
