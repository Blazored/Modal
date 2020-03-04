namespace Blazored.Modal
{
    public class ModalOptions
    {
        public ModalPosition Position { get; set; } = ModalPosition.Center;
        public string Class { get; set; } = "blazored-modal";
        public bool DisableBackgroundCancel { get; set; } = false;
        public bool HideHeader { get; set; } = false;
        public bool HideCloseButton { get; set; } = false;
    }
}
