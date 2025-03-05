using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Services.Concrete
{
    public static class LeadTypeHelper
    {
        private static readonly Dictionary<int, string> LeadTypeDescriptions = new Dictionary<int, string>
        {
            { (int)LeadType.Waiting, "Bekliyor" },
            { (int)LeadType.Offered, "Tekliflendi" },
            { (int)LeadType.ToCall, "Aranacak" },
            { (int)LeadType.OnSale, "Satışta" },
            { (int)LeadType.Completed, "Tamamalandı" }
        };

        public static string GetLeadTypeName(LeadType leadType)
        {
            if (LeadTypeDescriptions.ContainsKey((int)leadType))
                return LeadTypeDescriptions[(int)leadType];

            return "Unknown Lead Type";
        }

        public static IReadOnlyDictionary<int, string> GetAllLeadTypes()
        {
            return LeadTypeDescriptions;
        }
    }
}
