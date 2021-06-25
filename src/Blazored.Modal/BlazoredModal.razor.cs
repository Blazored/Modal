using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    public partial class BlazoredModal
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        [CascadingParameter] private IModalService CascadedModalService { get; set; }

        [Parameter] public bool? HideHeader { get; set; }
        [Parameter] public bool? HideCloseButton { get; set; }
        [Parameter] public bool? DisableBackgroundCancel { get; set; }
        [Parameter] public string OverlayCustomClass { get; set; }
        [Parameter] public ModalPosition? Position { get; set; }
        [Parameter] public string PositionCustomClass { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public ModalAnimation Animation { get; set; }
        [Parameter] public bool? UseCustomLayout { get; set; }
        [Parameter] public bool? ContentScrollable { get; set; }
        [Parameter] public bool? FocusFirstElement { get; set; }

        private readonly Collection<ModalReference> Modals = new Collection<ModalReference>();
        private readonly ModalOptions GlobalModalOptions = new ModalOptions();

        protected override void OnInitialized()
        {
            if (CascadedModalService == null)
            {
                throw new InvalidOperationException($"{GetType()} requires a cascading parameter of type {nameof(IModalService)}.");
            }

            ((ModalService)CascadedModalService).OnModalInstanceAdded += Update;
            ((ModalService)CascadedModalService).OnModalCloseRequested += CloseInstance;
            NavigationManager.LocationChanged += CancelModals;

            GlobalModalOptions.Class = Class;
            GlobalModalOptions.DisableBackgroundCancel = DisableBackgroundCancel;
            GlobalModalOptions.HideCloseButton = HideCloseButton;
            GlobalModalOptions.HideHeader = HideHeader;
            GlobalModalOptions.Position = Position;
            GlobalModalOptions.PositionCustomClass = PositionCustomClass;
            GlobalModalOptions.Animation = Animation;
            GlobalModalOptions.OverlayCustomClass = OverlayCustomClass;

            GlobalModalOptions.UseCustomLayout = UseCustomLayout;
            GlobalModalOptions.ContentScrollable = ContentScrollable;
            GlobalModalOptions.FocusFirstElement = FocusFirstElement;
        }

        internal async void CloseInstance(ModalReference modal, ModalResult result)
        {
            if (modal.ModalInstanceRef != null)
            {
                // Gracefully close the modal
                await modal.ModalInstanceRef.CloseAsync(result);
            }
            else
            {
                await DismissInstance(modal, result);
            }
        }

        internal void CloseInstance(Guid Id)
        {
            var reference = GetModalReference(Id);
            CloseInstance(reference, ModalResult.Ok<object>(null));
        }

        internal void CancelInstance(Guid Id)
        {
            var reference = GetModalReference(Id);
            CloseInstance(reference, ModalResult.Cancel());
        }

        internal Task DismissInstance(Guid Id, ModalResult result)
        {
            var reference = GetModalReference(Id);
            return DismissInstance(reference, result);
        }

        internal async Task DismissInstance(ModalReference modal, ModalResult result)
        {
            if (modal != null)
            {
                await JSRuntime.InvokeVoidAsync("BlazoredModal.deactivateFocusTrap", modal.Id);
                modal.Dismiss(result);
                Modals.Remove(modal);
                await InvokeAsync(StateHasChanged);
            }
        }

        private async void CancelModals(object sender, LocationChangedEventArgs e)
        {
            foreach (var modalReference in Modals.ToList())
            {
                await JSRuntime.InvokeVoidAsync("BlazoredModal.deactivateFocusTrap", modalReference.Id);
                modalReference.Dismiss(ModalResult.Cancel());
            }

            Modals.Clear();
            await InvokeAsync(StateHasChanged);
        }

        private async Task Update(ModalReference modalReference)
        {
            await JSRuntime.InvokeVoidAsync("BlazoredModal.activateScrollLock", modalReference.Id);
            Modals.Add(modalReference);
            await InvokeAsync(StateHasChanged);
        }

        private ModalReference GetModalReference(Guid Id)
        {
            return Modals.SingleOrDefault(x => x.Id == Id);
        }
    }
}
