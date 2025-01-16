using FluentValidation;

namespace FasterCrmApp.Services.Validation
{
    public static class ValidationTool
    {
        public static void Validate<T>(AbstractValidator<T> validator, T entity)
        {
            var validationResult = validator.Validate(entity);

            // ValidationTool sınıfı, FluentValidation doğrulayıcılarını(validator) kullanarak bir nesne üzerinde doğrulama yapar. Eğer doğrulama başarısız olursa, bu araç bir CustomValidationException fırlatır.

            // IsValid: Doğrulamanın başarılı olup olmadığını belirtir.
            // Errors: Eğer doğrulama başarısızsa, tüm doğrulama hatalarını içeren bir List<ValidationFailure> döner.

            if (!validationResult.IsValid)
                throw new CustomValidationException(validationResult.Errors);
            //CustomValidationException fırlatıldığında, catch bloğu bunu yakalar. (ClientService incelenebilir.)
        }
    }
}
