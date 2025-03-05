using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Services.Concrete
{
    public static class RoleHelper
    {
        private static readonly Dictionary<int, string> RoleDescriptions = new Dictionary<int, string>
        {
            { (int)Role.Admin, "Admin" },
            { (int)Role.User, "User" }
        };

        public static string GetRoleName(Role role)
        {
            // RoleDescriptions bir Dictionary<int, string> türündedir.
            // Bu metot, verilen role anahtarının(örneğin, Role.Admin (2) veya Role.User) sözlükte mevcut olup olmadığını kontrol eder. Eğer sözlükte bu anahtar varsa, true döndürür; yoksa false.

            // Eğer anahtar sözlükte bulunuyorsa, bu satır çalışır.
            // return RoleDescriptions[(int)role] sözlükteki ilgili anahtarın(role) değerini döndürür.

            // Örneğin:
            // Eğer role değeri Role.Admin (2) ise ve sözlükte bu anahtar Administrator ile eşleşmişse, "Administrator" döndürülür.

            if (RoleDescriptions.ContainsKey((int)role))
                return RoleDescriptions[(int)role];

            return "Unknown Role";
        }

        public static IReadOnlyDictionary<int, string> GetAllRoles()
        {
            // Sadece okunabilir olmasını sağladık.
            return RoleDescriptions;
        }
    }
}
