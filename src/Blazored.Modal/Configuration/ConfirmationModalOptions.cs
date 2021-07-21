namespace Blazored.Modal
{
    public class ConfirmationModalOptions
    {
        public ModalOptions OtherOptions { get; set; } = new ModalOptions();
        public string Title { get; set; } = "Are you sure you want to close the modal.";
        public string Content { get; set; }
        public string CloseButtonText { get; set; } = "Yes";
        public string CloseButtonClass { get; set; } = "btn btn-outline-danger";
        public string CancelButtonText { get; set; } = "No";
        public string CancelButtonClass { get; set; } = "btn btn-primary";
    }
}