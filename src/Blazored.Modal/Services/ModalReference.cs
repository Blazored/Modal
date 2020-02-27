using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Blazored.Modal.Services
{
    /// <summary>
    ///  A reference to the currently opened (active) modal.
    /// </summary>
    public class ModalReference
    {
        private TaskCompletionSource<ModalResult> _resultCompletion;

        public event Action<ComponentBase> Showed;
        public event Action<ModalResult> Closed;

        public ModalReference(
            Type componentType,
            string title,
            ModalParameters parameters,
            ModalOptions options)
        {
            ComponentType = componentType;
            Title = title;
            Parameters = parameters;
            Options = options;

            _resultCompletion = new TaskCompletionSource<ModalResult>();
            Closed += (result) => _resultCompletion.SetResult(result);
        }

        public Type ComponentType { get; }

        /// <summary>
        /// Null by default. It will be set after <see cref="Showed"/> event is invoked.
        /// </summary>
        public ComponentBase ComponentInstance { get; protected set; }
        public string Title { get; }
        public ModalParameters Parameters { get; }
        public ModalOptions Options { get; }

        public Task<ModalResult> Result => _resultCompletion.Task;
        

        internal virtual void SetComponentInstance(ComponentBase instance)
        {
            this.ComponentInstance = instance;
            Showed?.Invoke(instance);
        }

        internal virtual void Close(ModalResult result)
        {
            this.Closed.Invoke(result);
        }
    }
}