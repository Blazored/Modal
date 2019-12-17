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
        /// Invoked when the modal component closes. Passed directly to Show method
        /// Seporated from OnClose event to free programmers from handle subscribe/unsubscribe 
        /// </summary>
        protected Action<ModalResult> OnClosePassedToShow;

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
        /// <param name="onClose">Invoked when the modal component closes.</param>
        public void Show<T>(string title, ModalParameters parameters, ModalOptions options, Action<ModalResult> onClose = null) where T : ComponentBase
        {
            if (!typeof(ComponentBase).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException($"{typeof(T).FullName} must be a Blazor Component");
            }

            parameters = parameters ?? new ModalParameters();
            options = options ?? new ModalOptions();

            //SetKey need to avoid blazor bug:
            //when I show seccond dialog in onclose on first dialog with the same component type
            //blazor do not refresh content inside modal window
            var content = new RenderFragment(x => { x.OpenComponent(1, typeof(T)); x.SetKey(Guid.NewGuid()); x.CloseComponent(); });

            this.OnClosePassedToShow = onClose;

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
            OnClosePassedToShow?.Invoke(modalResult);
        }
    }
}
