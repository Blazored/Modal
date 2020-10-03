using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace Blazored.Modal
{
    public partial class BlazoredModalInstance
    {
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [CascadingParameter] private BlazoredModal Parent { get; set; }
        [CascadingParameter] private ModalOptions GlobalModalOptions { get; set; }

        [Parameter] public ModalOptions Options { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public Guid Id { get; set; }

        private string Position { get; set; }
        private string Class { get; set; }
        private bool HideHeader { get; set; }
        private bool HideCloseButton { get; set; }
        private bool DisableBackgroundCancel { get; set; }

		private string AnimationDuration {
            get {
                var duration = (Options?.Animation?.Duration ?? GlobalModalOptions?.Animation?.Duration ?? 0) * 1000;
                return FormattableString.Invariant($"{duration}ms");
            }
        }

        public bool UseCustomLayout { get; set; }

        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "This is assigned in Razor code and isn't currently picked up by the tooling.")]
        private ElementReference _modalReference;

        // Temporarily add a tabindex of -1 to the close button so it doesn't get selected as the first element by activateFocusTrap
        private readonly Dictionary<string, object> _closeBtnAttributes = new Dictionary<string, object> { { "tabindex", "-1" } };

        protected override void OnInitialized()
        {
            ConfigureInstance();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("BlazoredModal.activateFocusTrap", _modalReference, Id);
                _closeBtnAttributes.Clear();
                StateHasChanged();
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
        public async Task Close()
        {
            await Close(ModalResult.Ok<object>(null));
        }

        /// <summary>
        /// Closes the modal with the specified <paramref name="modalResult"/>.
        /// </summary>
        /// <param name="modalResult"></param>
        public async Task Close(ModalResult modalResult)
        {
            // Fade out the modal, and after that actually remove it
            if (Options.Animation?.Type == ModalAnimationType.FadeOut || Options.Animation?.Type == ModalAnimationType.FadeInOut)
            {
                Class += " blazored-modal-fade-out";
                StateHasChanged();
                if (Options?.Animation?.Duration > 0)
                {
                    await Task.Delay((int)(Options?.Animation?.Duration * 1000) + 100); // Needs to be a bit more than the animation time because of delays in the animation being applied between server and client (at least when using blazor server side), I think.
                }
            }

            await Parent.DismissInstance(Id, modalResult);
        }

        /// <summary>
        /// Closes the modal and returns a cancelled ModalResult.
        /// </summary>
        public async Task Cancel()
        {
            await Close(ModalResult.Cancel());
        }

        private void ConfigureInstance()
        {
            Position = SetPosition();
            Class = SetClass();
            HideHeader = SetHideHeader();
            HideCloseButton = SetHideCloseButton();
            DisableBackgroundCancel = SetDisableBackgroundCancel();
            UseCustomLayout = SetUseCustomLayout();
        }

        private bool SetUseCustomLayout()
        {
            if (Options.UseCustomLayout.HasValue)
            {
                return Options.UseCustomLayout.Value;
            }
            else if (GlobalModalOptions.UseCustomLayout.HasValue)
            {
                return GlobalModalOptions.UseCustomLayout.Value;
            }
            else
            {
                return false;
            }
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
                position = ModalPosition.Center;
            }

            switch (position)
            {
                case ModalPosition.Center:
                    return "blazored-modal-center";
                case ModalPosition.TopLeft:
                    return "blazored-modal-topleft";
                case ModalPosition.TopRight:
                    return "blazored-modal-topright";
                case ModalPosition.BottomLeft:
                    return "blazored-modal-bottomleft";
                case ModalPosition.BottomRight:
                    return "blazored-modal-bottomright";
                case ModalPosition.Custom:
                    if (string.IsNullOrWhiteSpace(Options.PositionCustomClass))
                        throw new InvalidOperationException("Position set to Custom without a PositionCustomClass set.");
                    return Options.PositionCustomClass;
                default:
                    return "blazored-modal-center";
            }
        }

        private string SetClass()
        {
            var modalClass = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.Class))
                modalClass = Options.Class;

            if (string.IsNullOrWhiteSpace(modalClass) && !string.IsNullOrWhiteSpace(GlobalModalOptions.Class))
                modalClass = GlobalModalOptions.Class;

            if (string.IsNullOrWhiteSpace(modalClass))
                modalClass = "blazored-modal";

            string animationClass = SetAnimationClass();
            if (!string.IsNullOrWhiteSpace(animationClass))
                modalClass += $" {animationClass}";

            string scrollableClass = SetScrollableClass();
            if (!string.IsNullOrWhiteSpace(scrollableClass))
                modalClass += $" {scrollableClass}";

            return modalClass;
        }

        private string SetAnimationClass()
        {
            ModalAnimation animation;
            animation = Options?.Animation ??
                        GlobalModalOptions?.Animation ?? new ModalAnimation(ModalAnimationType.None, 0);

            if (animation.Type == ModalAnimationType.FadeIn || animation.Type == ModalAnimationType.FadeInOut)
            {
                return "blazored-modal-fade-in";
            }

            return string.Empty;
        }

        private string SetScrollableClass()
        {
            if (Options.ContentScrollable == true || GlobalModalOptions?.ContentScrollable == true)
            {
                return "blazored-modal-scrollable";
            }

            return string.Empty;
        }

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

        private async Task HandleBackgroundClick()
        {
            if (DisableBackgroundCancel) return;

            await Cancel();
        }
    }
}
