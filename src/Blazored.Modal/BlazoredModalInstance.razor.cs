using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Blazored.Modal;

public partial class BlazoredModalInstance : IDisposable
{
    [CascadingParameter] private BlazoredModal Parent { get; set; } = default!;
    [CascadingParameter] private ModalOptions GlobalModalOptions { get; set; } = default!;

    [Parameter, EditorRequired] public RenderFragment Content { get; set; } = default!;
    [Parameter, EditorRequired] public ModalOptions Options { get; set; } = default!;
    [Parameter] public string? Title { get; set; }
    [Parameter] public Guid Id { get; set; }

    private string? Position { get; set; }
    private string? ModalClass { get; set; }
    private bool HideHeader { get; set; }
    private bool HideCloseButton { get; set; }
    private bool DisableBackgroundCancel { get; set; }
    private string? OverlayAnimationClass { get; set; }
    private string? OverlayCustomClass { get; set; }
    private ModalAnimationType? AnimationType { get; set; }
    private bool ActivateFocusTrap { get; set; }
    public bool UseCustomLayout { get; set; }
    public FocusTrap? FocusTrap { get; set; }


    [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "This is assigned in Razor code and isn't currently picked up by the tooling.")]
    private ElementReference _modalReference;
    private bool _setFocus;
    private bool _disableNextRender;

    // Temporarily add a tabindex of -1 to the close button so it doesn't get selected as the first element by activateFocusTrap
    private readonly Dictionary<string, object> _closeBtnAttributes = new() { { "tabindex", "-1" } };

    protected override bool ShouldRender()
    {
        if (!_disableNextRender)
        {
            return true;
        }
        
        _disableNextRender = false;
        return false;
    }

    protected override void OnInitialized() 
        => ConfigureInstance();

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _closeBtnAttributes.Clear();
            StateHasChanged();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_setFocus)
        {
            if (FocusTrap is not null)
            {
                await FocusTrap.SetFocus();
            }
            _setFocus = false;
        }
    }

    /// <summary>
    /// Sets the title for the modal being displayed
    /// </summary>
    /// <param name="title">Text to display as the title of the modal</param>
    public void SetTitle(string title)
    {
        Title = title;
        StateHasChanged();
    }

    /// <summary>
    /// Closes the modal with a default Ok result />.
    /// </summary>
    public async Task CloseAsync() 
        => await CloseAsync(ModalResult.Ok());

    /// <summary>
    /// Closes the modal with the specified <paramref name="modalResult"/>.
    /// </summary>
    /// <param name="modalResult"></param>
    public async Task CloseAsync(ModalResult modalResult)
    {
        // Fade out the modal, and after that actually remove it
        if (AnimationType is ModalAnimationType.FadeInOut)
        {
            OverlayAnimationClass += " fade-out";
            StateHasChanged();
            
            await Task.Delay(400); // Needs to be a bit more than the animation time because of delays in the animation being applied between server and client (at least when using blazor server side), I think.
        }

        await Parent.DismissInstance(Id, modalResult);
    }

    /// <summary>
    /// Closes the modal and returns a cancelled ModalResult.
    /// </summary>
    public async Task CancelAsync() 
        => await CloseAsync(ModalResult.Cancel());
    
    /// <summary>
    /// Closes the modal returning the specified <paramref name="payload"/> in a cancelled ModalResult.
    /// </summary>
    public async Task CancelAsync<TPayload>(TPayload payload) 
        => await CloseAsync(ModalResult.Cancel(payload));

    private void ConfigureInstance()
    {
        AnimationType = SetAnimation();
        Position = SetPosition();
        ModalClass = SetModalClass();
        HideHeader = SetHideHeader();
        HideCloseButton = SetHideCloseButton();
        DisableBackgroundCancel = SetDisableBackgroundCancel();
        UseCustomLayout = SetUseCustomLayout();
        OverlayCustomClass = SetOverlayCustomClass();
        ActivateFocusTrap = SetActivateFocusTrap();
        OverlayAnimationClass = SetAnimationClass();
        Parent.OnModalClosed += AttemptFocus;
    }

    private void AttemptFocus() 
        => _setFocus = true;

    private bool SetUseCustomLayout()
    {
        if (Options.UseCustomLayout.HasValue)
        {
            return Options.UseCustomLayout.Value;
        }

        if (GlobalModalOptions.UseCustomLayout.HasValue)
        {
            return GlobalModalOptions.UseCustomLayout.Value;
        }

        return false;
    }

    private string SetPosition()
    {
        ModalPosition position;

        if (Options.Position.HasValue)
        {
            position = Options.Position.Value;
        }
        else if (GlobalModalOptions.Position.HasValue)
        {
            position = GlobalModalOptions.Position.Value;
        }
        else
        {
            position = ModalPosition.TopCenter;
        }

        switch (position)
        {
            case ModalPosition.TopCenter:
                return "";

            case ModalPosition.TopLeft:
                return "position-topleft";

            case ModalPosition.TopRight:
                return "position-topright";
            
            case ModalPosition.Middle:
                return "position-middle";

            case ModalPosition.BottomLeft:
                return "position-bottomleft";

            case ModalPosition.BottomRight:
                return "position-bottomright";

            case ModalPosition.Custom:
                if (!string.IsNullOrWhiteSpace(Options.PositionCustomClass))
                    return Options.PositionCustomClass;
                if (!string.IsNullOrWhiteSpace(GlobalModalOptions.PositionCustomClass))
                    return GlobalModalOptions.PositionCustomClass;

                throw new InvalidOperationException("Position set to Custom without a PositionCustomClass set");

            default:
                return "";
        }
    }
    
    private string SetSize()
    {
        ModalSize size;

        if (Options.Size.HasValue)
        {
            size = Options.Size.Value;
        }
        else if (GlobalModalOptions.Size.HasValue)
        {
            size = GlobalModalOptions.Size.Value;
        }
        else
        {
            size = ModalSize.Medium;
        }

        switch (size)
        {
            case ModalSize.Small:
                return "size-small";

            case ModalSize.Medium:
                return "size-medium";

            case ModalSize.Large:
                return "size-large";
            
            case ModalSize.ExtraLarge:
                return "size-extra-large";

            case ModalSize.Custom:
                if (!string.IsNullOrWhiteSpace(Options.SizeCustomClass))
                    return Options.SizeCustomClass;
                if (!string.IsNullOrWhiteSpace(GlobalModalOptions.SizeCustomClass))
                    return GlobalModalOptions.SizeCustomClass;

                throw new InvalidOperationException("Size set to Custom without a SizeCustomClass set");
            
            case ModalSize.Automatic:
                return "size-automatic";

            default:
                return "size-medium";
        }
    }

    private string SetModalClass()
    {
        var modalClass = string.Empty;

        if (!string.IsNullOrWhiteSpace(Options.Class))
            modalClass = Options.Class;

        if (string.IsNullOrWhiteSpace(modalClass) && !string.IsNullOrWhiteSpace(GlobalModalOptions.Class))
            modalClass = GlobalModalOptions.Class;

        if (string.IsNullOrWhiteSpace(modalClass))
        {
            modalClass = "blazored-modal";
            modalClass += $" {SetSize()}";
        }

        return modalClass;
    }

    private ModalAnimationType SetAnimation() 
        => Options.AnimationType ?? GlobalModalOptions.AnimationType ?? ModalAnimationType.FadeInOut;

    private string SetAnimationClass() 
        => AnimationType is ModalAnimationType.FadeInOut ? "fade-in" : string.Empty;

    private bool SetHideHeader()
    {
        if (Options.HideHeader.HasValue)
            return Options.HideHeader.Value;

        if (GlobalModalOptions.HideHeader.HasValue)
            return GlobalModalOptions.HideHeader.Value;

        return false;
    }

    private bool SetHideCloseButton()
    {
        if (Options.HideCloseButton.HasValue)
            return Options.HideCloseButton.Value;

        if (GlobalModalOptions.HideCloseButton.HasValue)
            return GlobalModalOptions.HideCloseButton.Value;

        return false;
    }

    private bool SetDisableBackgroundCancel()
    {
        if (Options.DisableBackgroundCancel.HasValue)
            return Options.DisableBackgroundCancel.Value;

        if (GlobalModalOptions.DisableBackgroundCancel.HasValue)
            return GlobalModalOptions.DisableBackgroundCancel.Value;

        return false;
    }

    private string SetOverlayCustomClass()
    {
        if (!string.IsNullOrWhiteSpace(Options.OverlayCustomClass))
            return Options.OverlayCustomClass;

        if (!string.IsNullOrWhiteSpace(GlobalModalOptions.OverlayCustomClass))
            return GlobalModalOptions.OverlayCustomClass;

        return string.Empty;
    }

    private bool SetActivateFocusTrap()
    {
        if (Options.ActivateFocusTrap.HasValue)
            return Options.ActivateFocusTrap.Value;

        if (GlobalModalOptions.ActivateFocusTrap.HasValue)
            return GlobalModalOptions.ActivateFocusTrap.Value;

        return true;
    }

    private async Task HandleBackgroundClick()
    {
        if (DisableBackgroundCancel)
        {
            _disableNextRender = true;
            return;
        }

        await CancelAsync();
    }

    void IDisposable.Dispose() 
        => Parent.OnModalClosed -= AttemptFocus;
}