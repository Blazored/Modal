using System;
using Microsoft.AspNetCore.Components;

namespace Blazored.Modal.Services
{
    public interface IModalService
    {

        /// <summary>
        /// Shows a modal generic containing a <paramref name="component"/>.
        /// </summary>
        /// <param name="component">Type of component to display.</param>
        ModalReference ShowGeneric(string title, string message, ModalButton modalButton, ModalType modalType, string captionYesButton = null, string captionNoButton = null, string captionOkButton = null, string captionCancelButton = null);



        /// <summary>
        /// Shows a modal containing the specified <typeparamref name="TComponent"/>.
        /// </summary>
        ModalReference Show<TComponent>() where TComponent : ComponentBase;

        /// <summary>
        /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> .
        /// </summary>
        /// <param name="title">Modal title</param>
        ModalReference Show<TComponent>(string title) where TComponent : ComponentBase;

        /// <summary>
        /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> and <paramref name="options"/>.
        /// </summary>
        /// <param name="title">Modal title</param>
        /// <param name="options">Options to configure the modal</param>
        ModalReference Show<TComponent>(string title, ModalOptions options) where TComponent : ComponentBase;

        /// <summary>
        /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> and <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed</param>
        ModalReference Show<TComponent>(string title, ModalParameters parameters) where TComponent : ComponentBase;

        /// <summary>
        /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/>, 
        /// <paramref name="parameters"/> and <paramref name="options"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show<TComponent>(string title, ModalParameters parameters = null, ModalOptions options = null) where TComponent : ComponentBase;

        /// <summary>
        /// Shows a modal containing a <paramref name="component"/>.
        /// </summary>
        /// <param name="component">Type of component to display.</param>
        ModalReference Show(Type component);

        /// <summary>
        /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/>.
        /// </summary>
        /// <param name="component">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        ModalReference Show(Type component, string title);

        /// <summary>
        /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/> and <paramref name="options"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="component">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show(Type component, string title, ModalOptions options);

        /// <summary>
        /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/> and <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="component">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        ModalReference Show(Type component, string title, ModalParameters parameters);

        /// <summary>
        /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/>, <paramref name="parameters"/> 
        /// and <paramref name="options"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        ModalReference Show(Type component, string title, ModalParameters parameters, ModalOptions options, string message = null, ModalButton modalButton = 0, ModalType modalType = 0, string captionYesButton = null, string captionNoButton = null, string captionOkButton = null, string captionCancelButton = null);
    }
}
