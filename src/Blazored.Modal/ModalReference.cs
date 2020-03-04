using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    public class ModalReference
    {
        private TaskCompletionSource<ModalResult> _resultCompletion;

        private event Action<ModalResult> Closed;

        public ModalReference(RenderFragment modalInstance)
        {
            _resultCompletion = new TaskCompletionSource<ModalResult>();
            Closed += (result) => _resultCompletion.SetResult(result);
            ModalInstance = modalInstance;
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
