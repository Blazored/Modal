using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    partial class BlazoredConfirmationModal
    {
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; }

        [Parameter] public ConfirmationModalOptions Options {  get; set; }

        async Task Close() => await BlazoredModal.CloseAsync(ModalResult.Ok(true));
        async Task Cancel() => await BlazoredModal.CancelAsync();

        protected override void OnInitialized()
        {
            if (Options == null) {
                Options = new ConfirmationModalOptions();
            }

            base.OnInitialized();
        }
    }
}
