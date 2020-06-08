﻿using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal
{
    public partial class BlazoredModalInstance
    {
        [CascadingParameter] private Blazored.Modal.BlazoredModal Parent { get; set; }
        [CascadingParameter] private Blazored.Modal.ModalOptions GlobalModalOptions { get; set; }

        [Parameter] public Blazored.Modal.ModalOptions Options { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public Guid Id { get; set; }

        private string Position { get; set; }
        private string Class { get; set; }
        private bool HideHeader { get; set; }
        private bool HideCloseButton { get; set; }
        private bool DisableBackgroundCancel { get; set; }

        protected override void OnInitialized()
        {
            ConfigureInstance();
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
        public void Close()
        {
            Close(ModalResult.Ok<object>(null));
        }

        /// <summary>
        /// Closes the modal with the specified <paramref name="modalResult"/>.
        /// </summary>
        /// <param name="modalResult"></param>
        public void Close(ModalResult modalResult)
        {
            Parent.DismissInstance(Id, modalResult);
        }

        /// <summary>
        /// Closes the modal and returns a cancelled ModalResult.
        /// </summary>
        public void Cancel()
        {
            Parent.DismissInstance(Id, ModalResult.Cancel());
        }

        private void ConfigureInstance()
        {
            Position = SetPosition();
            Class = SetClass();
            HideHeader = SetHideHeader();
            HideCloseButton = SetHideCloseButton();
            DisableBackgroundCancel = SetDisableBackgroundCancel();
        }

        private string SetPosition()
        {
            Blazored.Modal.ModalPosition position;
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
                position = Blazored.Modal.ModalPosition.Center;
            }

            switch (position)
            {
                case Blazored.Modal.ModalPosition.Center:
                    return "blazored-modal-center";
                case Blazored.Modal.ModalPosition.TopLeft:
                    return "blazored-modal-topleft";
                case Blazored.Modal.ModalPosition.TopRight:
                    return "blazored-modal-topright";
                case Blazored.Modal.ModalPosition.BottomLeft:
                    return "blazored-modal-bottomleft";
                case Blazored.Modal.ModalPosition.BottomRight:
                    return "blazored-modal-bottomright";
                default:
                    return "blazored-modal-center";
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

        private void HandleBackgroundClick()
        {
            if (DisableBackgroundCancel) return;

            Parent.CancelInstance(Id);
        }
    }
}
