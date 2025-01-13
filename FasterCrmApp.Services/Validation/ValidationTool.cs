using FluentValidation;

namespace FasterCrmApp.Services.Validation
{
    public static class ValidationTool
    {
        public static void Validate<T>(AbstractValidator<T> validator, T entity)
        {
            var validationResult = validator.Validate(entity);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
        }
    }
}
