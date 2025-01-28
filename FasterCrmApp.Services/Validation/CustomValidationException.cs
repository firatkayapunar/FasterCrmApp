using FluentValidation.Results;

// Bu, custom bir exception sınıfıdır. CustomValidationException, C#'daki standart Exception sınıfından türetilmiştir. Yani, bu sınıf normal hata yakalama mekanizmalarıyla (try-catch) kullanılabilir.
public class CustomValidationException : Exception
{
    // Errors: Bu, hata mesajlarını tutan bir özellik.

    // Tipi: IDictionary<string, IEnumerable<string>>

    // string: Hangi alanın(örneğin bir formda bir input alanı) hata verdiğini belirler(PropertyName).
    // IEnumerable<string>: O alana ait hata mesajlarının bir listesi.

    public IDictionary<string, IEnumerable<string>> Errors { get; }

    // failures parametresi, IEnumerable<ValidationFailure> türündedir.Bu, doğrulama sırasında FluentValidation'dan dönen bir hata listesidir.

    // Örneğin:
    // [
    //    { PropertyName = "Name", ErrorMessage = "Name cannot be empty." },
    //    { PropertyName = "Email", ErrorMessage = "Email cannot be empty." },
    //    { PropertyName = "Email", ErrorMessage = "Email format is invalid." }
    // ]

    public CustomValidationException(IEnumerable<ValidationFailure> failures)
       : base("Validation failed.")
    {

        // GroupBy(failure => failure.PropertyName)
        // Bu ifade, failures listesini PropertyName(alan adı) değerine göre gruplar.

        // Örnek:
        // Yukarıdaki liste şu şekilde gruplandırılır:

        // [
        //    {
        //        Key = "Name",
        //        Values =
        //        [
        //            { ErrorMessage = "Name cannot be empty." }
        //        ]
        //    },
        //    {
        //        Key = "Email",
        //        Values =
        //        [
        //            { ErrorMessage = "Email cannot be empty." },
        //            { ErrorMessage = "Email format is invalid." }
        //        ]
        //    }
        // ]

        Errors = failures
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage)
            );

        // .ToDictionary(...)
        // Gruplandırılmış veriyi bir sözlük(Dictionary) yapısına dönüştürür.

        // - group.Key: Sözlüğün anahtarı, PropertyName (alan adı) olur.Örneğin: "Name", "Email".
        // - group.Select(failure => failure.ErrorMessage):
        //   - Gruplardaki her bir hatayı alır ve sadece ErrorMessage değerini seçer.
        //   - Bu, hata mesajlarının bir listesini oluşturur.

        // Örnek:
        // Gruplanmış veri şu yapıya dönüştürülür:

        // {
        //     "Name":  ["Name cannot be empty."],
        //     "Email": ["Email cannot be empty.", "Email format is invalid."]
        // }
    }
}
