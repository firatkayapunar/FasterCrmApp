using FluentValidation;

namespace FasterCrmApp.Services.Validation
{
    // FluentValidation vs FluentValidation.AspNetCore Farkı

    // FluentValidation paketi sadece temel doğrulama desteği sunar.Kendi doğrulayıcılarını(AbstractValidator<T>) manuel olarak oluşturup çağırman gerekir.

    // FluentValidation.AspNetCore paketi ise ASP.NET Core'un model bağlama (model binding) ve model doğrulama (model validation) süreçlerine entegre olur. Böylece, ValidationTool gibi manuel bir doğrulama yönetimine gerek kalmadan, FluentValidation doğrudan çalışır.

    // Özetle, FluentValidation.AspNetCore yüklendiğinde ASP.NET Core’un varsayılan Data Annotations (örneğin, [Required], [MaxLength]) doğrulayıcıları devre dışı kalır ve FluentValidation otomatik olarak devreye girer.

    // Not:

    // ✅ FluentValidation temel paketi(FluentValidation) yalnızca manuel doğrulama içindir.
    // ❌ asp-for ile bind yaptığımızda HTML elementlerinde data-val-* doğrulama öznitelikleri(Data Attributes) oluşmaz.

    // ✅ FluentValidation.AspNetCore, ASP.NET Core’un model doğrulama sürecine entegre olur.
    // ✅ asp-for ile bağladığında HTML'de data-val-* doğrulama öznitelikleri otomatik olarak oluşur.
    // ✅ Data Annotations gibi çalışır ve jQuery Validation ile birlikte client-side doğrulama yapılabilir.

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
