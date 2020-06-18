﻿using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

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
        private string AnimationClass { get; set; }
        private bool HideHeader { get; set; }
        private bool HideCloseButton { get; set; }
        private bool DisableBackgroundCancel { get; set; }

        private ElementReference _modalReference;

        protected override void OnInitialized()
        {
            ConfigureInstance();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("BlazoredModal.activateFocusTrap", _modalReference, Id);
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
                AnimationClass = "blazored-modal-fade-out";
                StateHasChanged();
                await Task.Delay((int)(Options?.Animation?.Duration * 1000) + 100); // Needs to be a bit more than the animation time because of delays in the animation being applied between server and client (at least when using blazor server side), I think.
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
            AnimationClass = SetAnimationClass();
            HideHeader = SetHideHeader();
            HideCloseButton = SetHideCloseButton();
            DisableBackgroundCancel = SetDisableBackgroundCancel();
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
                default:
                    return "blazored-modal-center";
            }
        }

        private async Task HandleWrapperKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Escape")
            {
                await Parent.DismissInstance(Id, ModalResult.Cancel());
            }
        }

        private string SetClass()
        {
            if (!string.IsNullOrWhiteSpace(Options.Class))
                return Options.Class;

            if (!string.IsNullOrWhiteSpace(GlobalModalOptions.Class))
                return GlobalModalOptions.Class;

            return "blazored-modal";
        }

        private string SetAnimationClass()
        {
            switch (Options.Animation?.Type)
            {
                case ModalAnimationType.None:
                    return string.Empty;
                case ModalAnimationType.FadeIn:
                case ModalAnimationType.FadeInOut:
                    return "blazored-modal-fade-in";
                case ModalAnimationType.FadeOut:
                    //return "blazored-modal-fade-out";
                    return string.Empty;
                    //return "blazored-modal-fade-in-out";
                    return string.Empty;
                default:
                    return string.Empty;
            }
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
