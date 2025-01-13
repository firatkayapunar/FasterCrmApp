namespace FasterCrmApp.Entities.Results
{
    public class Result
    {
        protected Result(bool success, string message, IEnumerable<string> errors)
        {
            Success = success;
            Message = message;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public bool Success { get; private set; } // İşlemin başarılı olup olmadığını belirtir
        public string Message { get; private set; } // İşlemle ilgili mesaj
        public IEnumerable<string> Errors { get; private set; } // Hataların listesi
        public bool IsSuccess => Success; // Daha okunabilir bir başarı kontrolü
        public string CombinedErrors => string.Join("; ", Errors); // Hataları tek bir stringde birleştirir

        public static Result SuccessResult(string message = "")
        {
            return new Result(true, message, Enumerable.Empty<string>());
        }

        public static Result FailureResult(string message, IEnumerable<string> errors)
        {
            return new Result(false, message, errors ?? Enumerable.Empty<string>());
        }
    }

    public class Result<T> : Result
        where T : class
    {
        public T Data { get; private set; } // Döndürülen veri

        private Result(bool success, string message, IEnumerable<string> errors, T data = null!)
            : base(success, message, errors)
        {
            Data = data;
        }

        public static Result<T> SuccessResult(T data, string message = "")
        {
            return new Result<T>(true, message, Enumerable.Empty<string>(), data);
        }

        /*
         Aşağıdaki uyarı, türetilmiş sınıfta aynı isimle bir metot tanımlandığında, dönüş tipleri farklı olsa bile, base sınıftaki metodu gizlediğini derleyicinin fark etmesi nedeniyle oluşuyor. C#'da metotlar yalnızca isimlerine (ve parametre türlerine) göre sınıflandırıldığından, dönüş tipi farklı olsa bile aynı isimde bir metot tanımlandığında "gizleme" (hiding) durumu ortaya çıkar.

        Neden Böyle Bir Uyarı Geliyor?
        
        Base sınıfta tanımlanan:
        public static Result FailureResult(string message, IEnumerable<string> errors)

        Derived sınıfta tanımlanan:
        public static Result<T> FailureResult(string message, IEnumerable<string> errors)

        Bu iki metot aynı imzaya sahiptir (isim ve parametreler aynı olduğu için). Ancak dönüş tipleri farklıdır:
            - Base sınıftaki Result döndürür.
            - Derived sınıftaki Result<T> döndürür.

        Derleyici, dönüş tipini dikkate almadığı için bu iki metot aynı kabul edilir ve türetilmiş sınıftaki metot, base sınıftaki metodu "gizler". Bunun sonucunda derleyici şu uyarıyı verir:

        Result<T>.FailureResult(string, IEnumerable<string>) hides inherited member Result.FailureResult(string, IEnumerable<string>). Use the new keyword if hiding was intended.

        Bu uyarı, kodun doğru çalışmasına engel değildir.

        Eğer base sınıftaki metodu bilerek gizlemek istiyorsak, türetilmiş sınıfta new anahtar sözcüğünü kullanabiliriz. Bu, gizleme işleminin bilinçli bir tercih olduğunu derleyiciye bildirir.
         */
        public static new Result<T> FailureResult(string message, IEnumerable<string> errors)
        {
            return new Result<T>(false, message, errors ?? Enumerable.Empty<string>());
        }
    }
}
