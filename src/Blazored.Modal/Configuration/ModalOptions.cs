namespace Blazored.Modal;

public class ModalOptions
{
    public ModalPosition? Position { get; set; }
    public string? PositionCustomClass { get; set; }
    public string? OverlayCustomClass { get; set; }
    public string? Class { get; set; }
    public bool? DisableBackgroundCancel { get; set; }
    public bool? HideHeader { get; set; }
    public bool? HideCloseButton { get; set; }
    public ModalAnimation? Animation { get; set; }
    public bool? UseCustomLayout { get; set; }
    public bool? ContentScrollable { get; set; }
    public bool? ActivateFocusTrap { get; set; }
}