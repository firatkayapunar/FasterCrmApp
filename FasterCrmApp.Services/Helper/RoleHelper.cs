using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Services.Concrete
{
    public static class RoleHelper
    {
        private static readonly Dictionary<Role, string> RoleDescriptions = new Dictionary<Role, string>
        {
            { Role.Admin, "Administrator" },
            { Role.User, "User" }
        };

        public static string GetRoleName(Role role)
        {
            // RoleDescriptions bir Dictionary<Role, string> türündedir.
            // Bu metot, verilen role anahtarının(örneğin, Role.Admin veya Role.User) sözlükte mevcut olup olmadığını kontrol eder. Eğer sözlükte bu anahtar varsa, true döndürür; yoksa false.

            // Eğer anahtar sözlükte bulunuyorsa, bu satır çalışır.
            // return RoleDescriptions[role] sözlükteki ilgili anahtarın(role) değerini döndürür.
            
            // Örneğin:
            // Eğer role değeri Role.Admin ise ve sözlükte bu anahtar Administrator ile eşleşmişse, "Administrator" döndürülür.

            if (RoleDescriptions.ContainsKey(role))
                return RoleDescriptions[role];

            return "Unknown Role";
        }

        public static IReadOnlyDictionary<Role, string> GetAllRoles()
        {
            // Sadece okunabilir olmasını sağladık.
            return RoleDescriptions;
        }
    }
}
