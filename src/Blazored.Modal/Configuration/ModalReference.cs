using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    public class ModalReference : IModalReference
    {
        private readonly TaskCompletionSource<ModalResult> _resultCompletion = new TaskCompletionSource<ModalResult>();
        private readonly Action<ModalResult> _closed;
        private readonly ModalService _modalService;

        public ModalReference(Guid modalInstanceId, RenderFragment modalInstance, ModalService modalService)
        {
            Id = modalInstanceId;
            ModalInstance = modalInstance;
            _closed = HandleClosed;
            _modalService = modalService;
        }

        public void Close()
        {
            _modalService.Close(this);
        }

        public void Close(ModalResult result)
        {
            _modalService.Close(this, result);
        }

        private void HandleClosed(ModalResult obj)
        {
            _ = _resultCompletion.TrySetResult(obj);
        }

        internal Guid Id { get; }
        internal RenderFragment ModalInstance { get; }
        internal BlazoredModalInstance ModalInstanceRef { get; set; }

        public Task<ModalResult> Result => _resultCompletion.Task;

        internal void Dismiss(ModalResult result)
        {
            _closed.Invoke(result);
        }
    }
}
