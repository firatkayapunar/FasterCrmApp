namespace FasterCrmApp.Models.Results
{
    public class Result
    {
        protected Result(bool success, string message, IDictionary<string, IEnumerable<string>>? errors = null)
        {
            IsSuccess = success;
            Message = message;
            Errors = errors ?? new Dictionary<string, IEnumerable<string>>();
        }

        protected Result(bool success, string message, IEnumerable<string>? errorMessages = null)
        {
            IsSuccess = success;
            Message = message;

            if (errorMessages != null && errorMessages.Any())
            {
                // new Dictionary<string, IEnumerable<string>> bir sözlük(dictionary) nesnesidir.Bu yapı, anahtar-değer(key - value) çiftleri ile veri depolayan bir koleksiyon türüdür.

                // var errors = new Dictionary<string, IEnumerable<string>>
                // {
                //  { "General", new List<string> { "An error occurred.", "Another error occurred." } },
                //  { "Name", new List<string> { "Name is required.", "Name must be at least 3 characters." } }
                // };

                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", errorMessages }
                };
            }
            else
            {
                Errors = new Dictionary<string, IEnumerable<string>>();
            }
        }

        public bool IsSuccess { get; } // İşlemin başarılı olup olmadığını belirtir
        public string Message { get; } // İşlemle ilgili mesaj
        public IDictionary<string, IEnumerable<string>> Errors { get; } // Hataların alan adıyla birlikte listesi

        // Başarılı durumlar için statik metod
        public static Result SuccessResult(string message = "")
        {
            return new Result(true, message, (IDictionary<string, IEnumerable<string>>?)null);
        }

        // Hatalı durumlar için alan bazlı hatalar ile
        public static Result FailureResult(string message, IDictionary<string, IEnumerable<string>> errors)
        {
            return new Result(false, message, errors);
        }

        // Hatalı durumlar için genel hata mesajları ile
        public static Result FailureResult(string message, IEnumerable<string> errorMessages)
        {
            return new Result(false, message, errorMessages);
        }
    }

    public class Result<T> : Result where T : class
    {
        private Result(bool success, string message, T? data = null, IEnumerable<string>? errorMessages = null)
            : base(success, message, errorMessages)
        {
            Data = data;
        }

        // Döndürülen veri
        public T? Data { get; }

        // Başarılı durumlar için statik metod
        public static Result<T> SuccessResult(T data, string message = "")
        {
            return new Result<T>(true, message, data);
        }

        // Aşağıdaki uyarı, türetilmiş sınıfta aynı isimle bir metot tanımlandığında, dönüş tipleri farklı olsa bile, base sınıftaki metodu gizlediğini derleyicinin fark etmesi nedeniyle oluşuyor. C#'da metotlar yalnızca isimlerine (ve parametre türlerine) göre sınıflandırıldığından, dönüş tipi farklı olsa bile aynı isimde bir metot tanımlandığında "gizleme" (hiding) durumu ortaya çıkar.

        // Neden Böyle Bir Uyarı Geliyor?

        // Base sınıfta tanımlanan:
        // public static Result FailureResult(string message, IEnumerable<string> errors)

        // Derived sınıfta tanımlanan:
        // public static Result<T> FailureResult(string message, IEnumerable<string> errors)

        // Bu iki metot aynı imzaya sahiptir (isim ve parametreler aynı olduğu için). Ancak dönüş tipleri farklıdır:
        // - Base sınıftaki Result döndürür.
        // - Derived sınıftaki Result<T> döndürür.

        // Derleyici, dönüş tipini dikkate almadığı için bu iki metot aynı kabul edilir ve türetilmiş sınıftaki metot, base sınıftaki metodu "gizler".

        // Bunun sonucunda derleyici şu uyarıyı verir:
        // Result<T>.FailureResult(string, IEnumerable<string>) hides inherited member Result.FailureResult(string, IEnumerable<string>). Use the new keyword if hiding was intended.

        // Bu uyarı, kodun doğru çalışmasına engel değildir.

        // Eğer base sınıftaki metodu bilerek gizlemek istiyorsak, türetilmiş sınıfta new anahtar sözcüğünü kullanabiliriz. Bu, gizleme işleminin bilinçli bir tercih olduğunu derleyiciye bildirir.
        public static new Result<T> FailureResult(string message, IEnumerable<string> errorMessages)
        {
            return new Result<T>(false, message, null, errorMessages);
        }
    }
}
