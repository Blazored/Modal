using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal
{
    public class BlazoredModalBase : ComponentBase, IDisposable
    {
        const string DefaultStyle = "blazored-modal";
        const string DefaultPosition = "blazored-modal-center";

        [Inject] protected IModalService ModalService { get; set; }

        [Parameter] public bool HideCloseButton { get; set; }
        [Parameter] public bool DisableBackgroundCancel { get; set; }
        [Parameter] public string Position { get; set; }
        [Parameter] public string Style { get; set; }

        protected bool ComponentDisableBackgroundCancel { get; set; }
        protected bool ComponentHideCloseButton { get; set; }
        protected string ComponentPosition { get; set; }
        protected string ComponentStyle { get; set; }
        protected ModalOptions Options { get; set; }
        protected bool IsVisible { get; set; }
        protected string Title { get; set; }
        protected RenderFragment Content { get; set; }
        protected ModalParameters Parameters { get; set; }

        protected override void OnInitialized()
        {
            ((ModalService)ModalService).OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
        }

        public void ShowModal(string title, RenderFragment content, ModalParameters parameters, ModalOptions options)
        {
            Title = title;
            Content = content;
            Parameters = parameters;

            SetModalOptions(options);

            IsVisible = true;
            StateHasChanged();
        }

        internal void CloseModal(ModalResult modalResult)
        {
            IsVisible = false;
            Title = "";
            Content = null;
            ComponentStyle = "";

            StateHasChanged();
        }

        protected void HandleBackgroundClick()
        {
            if (ComponentDisableBackgroundCancel) return;

            ModalService.Cancel();
        }

        public void Dispose()
        {
            ((ModalService)ModalService).OnShow -= ShowModal;
            ModalService.OnClose -= CloseModal;
        }

        private void SetModalOptions(ModalOptions options)
        {
            ComponentHideCloseButton = HideCloseButton;
            if (options.HideCloseButton.HasValue)
                ComponentHideCloseButton = options.HideCloseButton.Value;

            ComponentDisableBackgroundCancel = DisableBackgroundCancel;
            if (options.DisableBackgroundCancel.HasValue)
                ComponentDisableBackgroundCancel = options.DisableBackgroundCancel.Value;

            ComponentPosition = string.IsNullOrWhiteSpace(options.Position) ? Position : options.Position;
            if (string.IsNullOrWhiteSpace(ComponentPosition))
                ComponentPosition = DefaultPosition;

            ComponentStyle = string.IsNullOrWhiteSpace(options.Style) ? Style : options.Style;
            if (string.IsNullOrWhiteSpace(ComponentStyle))
                ComponentStyle = DefaultStyle;
        }
    }
}
