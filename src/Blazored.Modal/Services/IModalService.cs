using System;

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
        /// <param name="componentType">Type of component to display.</param>
        void Show(string title, Type contentType);

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="style">Modal's custom CSS style selector.</param>
        void Show(string title, Type componentType, string style);

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        void Show(string title, Type contentType, ModalParameters parameters);

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="componentType">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="style">Modal's custom CSS style selector.</param>
        void Show(string title, Type componentType, ModalParameters parameters, string style);

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
