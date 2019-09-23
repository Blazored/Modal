using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal
{
    public class BlazoredModalBase : ComponentBase, IDisposable
    {
        [Inject] protected IModalService ModalService { get; set; }

        [Parameter] public bool HideCloseButton { get; set; }
        [Parameter] public bool DisableBackgroundCancel { get; set; }

        protected bool IsVisible { get; set; }
        protected string Title { get; set; }
        protected RenderFragment Content { get; set; }
        protected ModalParameters Parameters { get; set; }
        protected string Style { get; set; }

        protected override void OnInitialized()
        {
            ((ModalService)ModalService).OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
        }

        public void ShowModal(string title, RenderFragment content, ModalParameters parameters, string style)
        {
            Title = title;
            Content = content;
            Parameters = parameters;
            Style = style ?? "blazored-modal";

            IsVisible = true;
            StateHasChanged();
        }

        internal void CloseModal(ModalResult modalResult)
        {
            IsVisible = false;
            Title = "";
            Content = null;
            Style = "";

            StateHasChanged();
        }

        protected void HandleBackgroundClick()
        {
            if (DisableBackgroundCancel) return;

            ModalService.Cancel();
        }

        public void Dispose()
        {
            ((ModalService)ModalService).OnShow -= ShowModal;
            ModalService.OnClose -= CloseModal;
        }
    }
}
