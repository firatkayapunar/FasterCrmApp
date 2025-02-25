namespace FasterCrmApp.UI.Models
{
    public class BaseModalViewModel
    {
        public string ID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string TitleID { get; set; } = string.Empty;
        public string ActionButtonText { get; set; } = string.Empty;
        public string ActionButtonClass { get; set; } = string.Empty;
        public string OnActionClick { get; set; } = string.Empty;
        public ModalInputViewModel ModelInputViewModel { get; set; } = null!;
    }
}
