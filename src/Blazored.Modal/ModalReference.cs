using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    public class ModalReference
    {
        private TaskCompletionSource<ModalResult> _resultCompletion = new TaskCompletionSource<ModalResult>();

        private event Action<ModalResult> Closed;

        public ModalReference(Guid modalInstanceId, RenderFragment modalInstance)
        {
            Id = modalInstanceId;
            ModalInstance = modalInstance;
            Closed += (result) => _resultCompletion.SetResult(result);
        }

        internal Guid Id { get; set; }
        internal RenderFragment ModalInstance { get; set; }

        public Task<ModalResult> Result => _resultCompletion.Task;

        internal virtual void Dismiss(ModalResult result)
        {
            Closed.Invoke(result);
        }
    }
}
