using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blazored.Modal
{
    public partial class BlazoredModal
    {
        [Inject] private IModalService ModalService { get; set; }

        [Parameter] public bool HideHeader { get; set; }
        [Parameter] public bool HideCloseButton { get; set; }
        [Parameter] public bool DisableBackgroundCancel { get; set; }
        [Parameter] public string Position { get; set; }
        [Parameter] public string Class { get; set; }

        private readonly Collection<ModalReference> Modals = new Collection<ModalReference>();

        protected override void OnInitialized()
        {
            ((ModalService)ModalService).OnModalInstanceAdded += Update;
        }

        internal void CloseInstance(Guid Id)
        {
            CloseInstance(Id, ModalResult.Ok<object>(null));
        }

        internal void CloseInstance(Guid Id, ModalResult result)
        {
            var reference = Modals.SingleOrDefault(x => x.Id == Id);

            if (reference != null)
            {
                reference.Dismiss(result);
                Modals.Remove(reference);
                StateHasChanged();
            }
        }

        internal void CancelInstance(Guid Id)
        {
            var reference = Modals.SingleOrDefault(x => x.Id == Id);

            if (reference != null)
            {
                reference.Dismiss(ModalResult.Cancel());
                Modals.Remove(reference);
                StateHasChanged();
            }
        }

        private async void Update(ModalReference modalReference)
        {
            Modals.Add(modalReference);
            await InvokeAsync(StateHasChanged);
        }
    }
}
