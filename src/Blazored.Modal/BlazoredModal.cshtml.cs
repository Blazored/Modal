using Blazored.Modal.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace Blazored.Modal
{
    public class BlazoredModalBase : BlazorComponent, IDisposable
    {
        protected bool IsVisible { get; set; }
        protected string Title { get; set; }
        protected RenderFragment Content { get; set; }

        [Inject] private IModalService ModalService { get; set; }

        public void ShowModal(string title, RenderFragment content)
        {
            Title = title;
            Content = content;
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
            ModalService.OnShow -= ShowModal;
            ModalService.OnClose -= CloseModal;
        }

        protected override void OnInit()
        {
            ModalService.OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
        }
    }
}
