using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blazored.Modal
{
    public partial class BlazoredModal
    {
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter] public bool? HideHeader { get; set; }
        [Parameter] public bool? HideCloseButton { get; set; }
        [Parameter] public bool? DisableBackgroundCancel { get; set; }
        [Parameter] public ModalPosition? Position { get; set; }
        [Parameter] public string Class { get; set; }

        private readonly Collection<ModalReference> Modals = new Collection<ModalReference>();
        private readonly ModalOptions GlobalModalOptions = new ModalOptions();

        protected override void OnInitialized()
        {
            ((ModalService)ModalService).OnModalInstanceAdded += Update;
            ((ModalService) ModalService).OnModalCloseRequested += CloseInstance;
            NavigationManager.LocationChanged += CancelModals;

            GlobalModalOptions.Class = Class;
            GlobalModalOptions.DisableBackgroundCancel = DisableBackgroundCancel;
            GlobalModalOptions.HideCloseButton = HideCloseButton;
            GlobalModalOptions.HideHeader = HideHeader;
            GlobalModalOptions.Position = Position;
        }

        internal void CloseInstance(ModalReference modal, ModalResult result)
        {
            DismissInstance(modal.Id, result);
        }

        internal void CloseInstance(Guid Id)
        {
            DismissInstance(Id, ModalResult.Ok<object>(null));
        }

        internal void CancelInstance(Guid Id)
        {
            DismissInstance(Id, ModalResult.Cancel());
        }

        internal void DismissInstance(Guid Id, ModalResult result)
        {
            var reference = Modals.SingleOrDefault(x => x.Id == Id);

            if (reference != null)
            {
                reference.Dismiss(result);
                Modals.Remove(reference);
                StateHasChanged();
            }
        }

        private async void CancelModals(object sender, LocationChangedEventArgs e)
        {
            foreach (var modalReference in Modals)
            {
                modalReference.Dismiss(ModalResult.Cancel());
            }

            Modals.Clear();
            await InvokeAsync(StateHasChanged);
        }

        private async void Update(ModalReference modalReference)
        {
            Modals.Add(modalReference);
            await InvokeAsync(StateHasChanged);
        }
    }
}
