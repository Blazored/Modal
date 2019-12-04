using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal
{
    public partial class BlazoredModal : IDisposable
    {
        const string _defaultStyle = "blazored-modal";
        const string _defaultPosition = "blazored-modal-center";

        [Inject] private IModalService ModalService { get; set; }

        [Parameter] public bool HideHeader { get; set; }
        [Parameter] public bool HideCloseButton { get; set; }
        [Parameter] public bool DisableBackgroundCancel { get; set; }
        [Parameter] public string Position { get; set; }
        [Parameter] public string Style { get; set; }

        private bool ComponentDisableBackgroundCancel { get; set; }
        private bool ComponentHideHeader { get; set; }
        private bool ComponentHideCloseButton { get; set; }
        private string ComponentPosition { get; set; }
        private string ComponentStyle { get; set; }
        private bool IsVisible { get; set; }
        private string Title { get; set; }
        private RenderFragment Content { get; set; }
        private ModalParameters Parameters { get; set; }

        /// <summary>
        /// Sets the title for the modal being displayed
        /// </summary>
        /// <param name="title">Text to display as the title of the modal</param>
        public void SetTitle(string title)
        {
            Title = title;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            ((ModalService)ModalService).OnShow += ShowModal;
            ((ModalService)ModalService).CloseModal += CloseModal;
        }

        private void ShowModal(string title, RenderFragment content, ModalParameters parameters, ModalOptions options)
        {
            Title = title;
            Content = content;
            Parameters = parameters;

            SetModalOptions(options);

            IsVisible = true;
            StateHasChanged();
        }

        private void CloseModal()
        {
            IsVisible = false;
            Title = "";
            Content = null;
            ComponentStyle = "";

            StateHasChanged();
        }

        private void HandleBackgroundClick()
        {
            if (ComponentDisableBackgroundCancel) return;

            ModalService.Cancel();
        }

        private void SetModalOptions(ModalOptions options)
        {
            ComponentHideHeader = HideHeader;
            if (options.HideHeader.HasValue)
                ComponentHideHeader = options.HideHeader.Value;
            
            ComponentHideCloseButton = HideCloseButton;
            if (options.HideCloseButton.HasValue)
                ComponentHideCloseButton = options.HideCloseButton.Value;

            ComponentDisableBackgroundCancel = DisableBackgroundCancel;
            if (options.DisableBackgroundCancel.HasValue)
                ComponentDisableBackgroundCancel = options.DisableBackgroundCancel.Value;

            ComponentPosition = string.IsNullOrWhiteSpace(options.Position) ? Position : options.Position;
            if (string.IsNullOrWhiteSpace(ComponentPosition))
                ComponentPosition = _defaultPosition;

            ComponentStyle = string.IsNullOrWhiteSpace(options.Style) ? Style : options.Style;
            if (string.IsNullOrWhiteSpace(ComponentStyle))
                ComponentStyle = _defaultStyle;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((ModalService)ModalService).OnShow -= ShowModal;
                ((ModalService)ModalService).CloseModal -= CloseModal;
            }
        }
    }
}
