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
        /// Invoked when the modal component closes. Passed directly to Show method
        /// Seporated from OnClose event to free programmers from handle subscribe/unsubscribe 
        /// </summary>
        protected Action<ModalResult> OnCustomClose;


        /// <summary>
        /// Internal event used to trigger the modal component to show.
        /// </summary>
        internal event Action<string, RenderFragment, ModalParameters, ModalOptions> OnShow;

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        public void Show(string title, Type componentType)
        {
            Show(title, componentType, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        public void Show(string title, Type componentType, ModalOptions options)
        {
            Show(title, componentType, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public void Show(string title, Type componentType, ModalParameters parameters)
        {
            Show(title, componentType, parameters, new ModalOptions());
        }

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public void Show(string title, Type componentType, ModalParameters parameters, ModalOptions options)
        {
            Show(title, componentType, parameters, options);
        }

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public void Show(string title, Type componentType, ModalParameters parameters, ModalOptions options, Action<ModalResult> onClose = null)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(componentType))
            {
                throw new ArgumentException($"{componentType.FullName} must be a Blazor Component");
            }

            //SetKey need to avoid blazor bug:
            //when I show seccond dialog in onclose on first dialog with the same component type
            //blazor do not refresh content inside modal window
            var content = new RenderFragment(x => { x.OpenComponent(1, componentType); x.SetKey(Guid.NewGuid()); x.CloseComponent(); });

            this.OnCustomClose = onClose;

            OnShow?.Invoke(title, content, parameters, options);
        }

        /// <summary>
        /// Closes the modal and invokes the <see cref="OnClose"/> event.
        /// </summary>
        public void Cancel()
        {
            OnClose?.Invoke(ModalResult.Cancel());
        }

        /// <summary>
        /// Closes the modal and invokes the <see cref="OnClose"/> event with the specified <paramref name="modalResult"/>.
        /// </summary>
        /// <param name="modalResult"></param>
        public void Close(ModalResult modalResult)
        {
            OnClose?.Invoke(modalResult);
            OnCustomClose?.Invoke(modalResult);
        }

        /// <inheritdoc cref="IModalService.Show{T}(string, ModalParameters, ModalOptions)"/>
        public void Show<T>(string title, ModalParameters parameters = null, ModalOptions options = null) where T : ComponentBase
        {
            Show<T>(title, parameters, options, null);
        }

        /// <inheritdoc cref="IModalService.Show{T}(string, ModalParameters, ModalOptions, Action{ModelResult})"/>
        public void Show<T>(string title, ModalParameters parameters = null, ModalOptions options = null, Action<ModalResult> onClose = null) where T : ComponentBase
        {
            Show(title, 
                 typeof(T),
                 parameters ?? new ModalParameters(),
                 options ?? new ModalOptions(),
                 onClose);
        }
    }
}
