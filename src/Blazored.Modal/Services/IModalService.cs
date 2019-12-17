using System;
using Microsoft.AspNetCore.Components;

namespace Blazored.Modal.Services
{
    public interface IModalService
    {
        /// <summary>
        /// Invoked when the modal component closes.
        /// </summary>
        event Action<ModalResult> OnClose;

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        void Show<T>(string title) where T : ComponentBase;

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="options">Options to configure the modal.</param>
        void Show<T>(string title, ModalOptions options) where T : ComponentBase;

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        void Show<T>(string title, ModalParameters parameters) where T : ComponentBase;

        /// <summary>
        /// Shows the modal for the specified component type using the specified <paramref name="title"/>
        /// and the specified <paramref name="parameters"/> and settings a custom modal <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T">Type of component to display.</typeparam>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        /// <param name="onClose">Invoked when the modal component closes.</param>
        void Show<T>(string title, ModalParameters parameters = null, ModalOptions options = null, Action<ModalResult> onClose = null) where T : ComponentBase;

        /// <summary>
        /// Cancels the modal and invokes the <see cref="OnClose"/> event.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Closes the modal and invokes the <see cref="OnClose"/> event with the specified <paramref name="modalResult"/>.
        /// </summary>
        /// <param name="modalResult"></param>
        void Close(ModalResult modalResult);
    }
}
