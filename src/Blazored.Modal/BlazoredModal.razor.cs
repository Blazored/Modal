using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;

namespace Blazored.Modal
{
    using System.Threading.Tasks;

    public partial class BlazoredModal : IDisposable
    {
        const string _defaultStyle = "blazored-modal";
        const string _defaultPosition = "blazored-modal-center";

        [Inject] private IModalService ModalService { get; set; }

        [Inject] private IJSRuntime JSInterop { get; set; }

        [Parameter] public bool HideHeader { get; set; }
        [Parameter] public bool HideCloseButton { get; set; }
        [Parameter] public bool DisableBackgroundCancel { get; set; }
        [Parameter] public string Position { get; set; }
        [Parameter] public string Style { get; set; }

        private bool FocusModal { get; set; }
        private bool ComponentDisableBackgroundCancel { get; set; }
        private bool ComponentHideHeader { get; set; }
        private bool ComponentHideCloseButton { get; set; }
        private string ComponentPosition { get; set; }
        private string ComponentStyle { get; set; }
        private bool IsVisible { get; set; }
        private string Title { get; set; }
        private RenderFragment Content { get; set; }
        private ModalParameters Parameters { get; set; }

        private ElementReference ModalWrapper { get; set; }

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender && this.FocusModal)
            {
                await this.JSInterop.InvokeVoidAsync("BlazoredModal.setFocus", this.ModalWrapper);
                this.FocusModal = false;
            }
        }

        private async void ShowModal(string title, RenderFragment content, ModalParameters parameters, ModalOptions options)
        {
            Title = title;
            Content = content;
            Parameters = parameters;

            SetModalOptions(options);

            IsVisible = true;
            await InvokeAsync(StateHasChanged);
            this.FocusModal = true;
        }

        private async void CloseModal()
        {
            IsVisible = false;
            Title = "";
            Content = null;
            ComponentStyle = "";

            await InvokeAsync(StateHasChanged);
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

        private void ReactToControlCommands(KeyboardEventArgs e)
        {
            if (e.Code == "Escape")
            {
                ModalService.Cancel();
            }
        }
    }
}
