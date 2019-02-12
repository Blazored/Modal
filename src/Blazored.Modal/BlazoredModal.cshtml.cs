using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal
{
    public class BlazoredModalBase : ComponentBase, IDisposable
    {
        protected bool IsVisible { get; set; }
        protected string Title { get; set; }
        protected RenderFragment Content { get; set; }
        protected ModalParameters Parameters { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        public void ShowModal(string title, RenderFragment content, ModalParameters parameters)
        {
            Title = title;
            Content = content;
            Parameters = parameters;

            IsVisible = true;
            StateHasChanged();
        }

        public void CloseModal()
        {
            IsVisible = false;
            Title = "";
            Content = null;

            StateHasChanged();
        }

        public void Dispose()
        {
            ((ModalService)ModalService).OnShow -= ShowModal;
            ModalService.OnClose -= CloseModal;
        }

        protected override void OnInit()
        {
            ((ModalService)ModalService).OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
        }
    }
}
