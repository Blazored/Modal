using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Blazored.Modal.Services
{
    public interface IModalService
    {
        /// <summary>
        /// Shows the modal with the component type.
        /// </summary>
        ModalReference Show<T>() where T : ComponentBase;

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        ModalReference Show<T>(string title) where T : ComponentBase;

        /// <summary>
        /// Shows the modal using the specified title and component type.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show<T>(string title, ModalOptions options) where T : ComponentBase;

        /// <summary>
        /// Shows the modal using the specified <paramref name="title"/> and <paramref name="componentType"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        ModalReference Show<T>(string title, ModalParameters parameters) where T : ComponentBase;

        /// <summary>
        /// Shows the modal for the specified component type using the specified <paramref name="title"/>
        /// and the specified <paramref name="parameters"/> and settings a custom modal <paramref name="options"/>
        /// </summary>
        /// <typeparam name="T">Type of component to display.</typeparam>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show<T>(string title, ModalParameters parameters = null, ModalOptions options = null) where T : ComponentBase;

        /// <summary>
        /// Shows the modal with the specific component type.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        ModalReference Show(Type contentComponent);

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        ModalReference Show(Type contentComponent, string title);

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show(Type contentComponent, string title, ModalOptions options);

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        ModalReference Show(Type contentComponent, string title, ModalParameters parameters);

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>, 
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options);
    }
}
