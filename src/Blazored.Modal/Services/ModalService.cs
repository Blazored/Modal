using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal.Services
{
    public class ModalService : IModalService
    {
        /// <summary>
        /// Invoked when the modal component closes.
        /// </summary>
        public event Action<ModalResult> OnClose;

        /// <summary>
        /// Internal event used to close the modal instance.
        /// </summary>
        internal event Action CloseModal;

        /// <summary>
        /// Internal event used to trigger the modal component to show.
        /// </summary>
        internal event Action<string, RenderFragment, ModalParameters, ModalOptions> OnShow;

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        public void Show<T>(string title) where T : ComponentBase
        {
            Show<T>(title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        public void Show<T>(string title, ModalOptions options) where T : ComponentBase
        {
            Show<T>(title, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public void Show<T>(string title, ModalParameters parameters) where T : ComponentBase
        {
            Show<T>(title, parameters, new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>, 
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public void Show<T>(string title, ModalParameters parameters, ModalOptions options) where T : ComponentBase
        {
            if (!typeof(ComponentBase).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException($"{typeof(T).FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x => { x.OpenComponent(1, typeof(T)); x.CloseComponent(); });

            OnShow?.Invoke(title, content, parameters, options);
        }

        /// <summary>
        /// Closes the modal and invokes the <see cref="OnClose"/> event.
        /// </summary>
        public void Cancel()
        {
            CloseModal?.Invoke();
            OnClose?.Invoke(ModalResult.Cancel());
        }

        /// <summary>
        /// Closes the modal and invokes the <see cref="OnClose"/> event with the specified <paramref name="modalResult"/>.
        /// </summary>
        /// <param name="modalResult"></param>
        public void Close(ModalResult modalResult)
        {
            CloseModal?.Invoke();
            OnClose?.Invoke(modalResult);
        }
    }
}
