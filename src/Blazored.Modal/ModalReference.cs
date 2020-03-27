using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    public class ModalReference
    {
        private TaskCompletionSource<ModalResult> _resultCompletion = new TaskCompletionSource<ModalResult>();

        private Action<ModalResult> Closed;

        private IModalService ModalService { get; }

        public ModalReference(Guid modalInstanceId, RenderFragment modalInstance, IModalService modalService)
        {
            Id = modalInstanceId;
            ModalInstance = modalInstance;
            Closed = HandleClosed;
            ModalService = modalService;
        }

        public void Close()
        {
            ModalService.Close(this);
        }

        public void Close(ModalResult result)
        {
            ModalService.Close(this, result);
        }

        private void HandleClosed(ModalResult obj)
        {
            _ = _resultCompletion.TrySetResult(obj);
        }

        internal Guid Id { get; set; }
        internal RenderFragment ModalInstance { get; set; }

        public Task<ModalResult> Result => _resultCompletion.Task;

        internal void Dismiss(ModalResult result)
        {
            Closed.Invoke(result);
        }
    }
}
