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
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        [Parameter] public bool? HideHeader { get; set; }
        [Parameter] public bool? HideCloseButton { get; set; }
        [Parameter] public bool? DisableBackgroundCancel { get; set; }
        [Parameter] public ModalPosition? Position { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public ModalAnimation Animation { get; set; }

        [Parameter] public bool? UseCustomLayout { get; set; }

        [Parameter] public RenderFragment<BlazoredModalInstance> ModalTemplate { get; set; }

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
            GlobalModalOptions.Animation = Animation;

            GlobalModalOptions.UseCustomLayout = UseCustomLayout;
        }

        internal async void CloseInstance(ModalReference modal, ModalResult result)
        {
            await DismissInstance(modal.Id, result);
        }

        internal async Task CloseInstance(Guid Id)
        {
            await DismissInstance(Id, ModalResult.Ok<object>(null));
        }

        internal async Task CancelInstance(Guid Id)
        {
            await DismissInstance(Id, ModalResult.Cancel());
        }

        internal async Task DismissInstance(Guid Id, ModalResult result)
        {
            var reference = Modals.SingleOrDefault(x => x.Id == Id);

            if (reference != null)
            {
                await JSRuntime.InvokeVoidAsync("BlazoredModal.deactivateFocusTrap", Id);
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
            await JSRuntime.InvokeVoidAsync("BlazoredModal.activateScrollLock");
            Modals.Add(modalReference);
            await InvokeAsync(StateHasChanged);
        }
    }
}
