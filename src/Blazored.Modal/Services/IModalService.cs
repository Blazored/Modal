using Microsoft.AspNetCore.Components;

namespace Blazored.Modal.Services;

public interface IModalService
{
    /// <summary>
    /// Shows a modal containing the specified <typeparamref name="TComponent"/>.
    /// </summary>
    IModalReference Show<TComponent>() where TComponent : IComponent;
    
    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="options"/>.
    /// </summary>
    /// <param name="options">Options to configure the modal.</param>
    IModalReference Show<TComponent>(ModalOptions options) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="parameters"/>.
    /// </summary>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    IModalReference Show<TComponent>(ModalParameters parameters) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="parameters"/>
    /// and <paramref name="options"/>.
    /// </summary>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    /// <param name="options">Options to configure the modal.</param>
    IModalReference Show<TComponent>(ModalParameters parameters, ModalOptions options) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> .
    /// </summary>
    /// <param name="title">Modal title</param>
    IModalReference Show<TComponent>(string title) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> and <paramref name="options"/>.
    /// </summary>
    /// <param name="title">Modal title</param>
    /// <param name="options">Options to configure the modal</param>
    IModalReference Show<TComponent>(string title, ModalOptions options) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> and <paramref name="parameters"/>.
    /// </summary>
    /// <param name="title">Modal title</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed</param>
    IModalReference Show<TComponent>(string title, ModalParameters parameters) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/>,
    /// <paramref name="parameters"/> and <paramref name="options"/>.
    /// </summary>
    /// <param name="title">Modal title.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    /// <param name="options">Options to configure the modal.</param>
    IModalReference Show<TComponent>(string title, ModalParameters parameters, ModalOptions options) where TComponent : IComponent;

    /// <summary>
    /// Shows a modal containing a <paramref name="component"/>.
    /// </summary>
    /// <param name="component">Type of component to display.</param>
    IModalReference Show(Type component);

    /// <summary>
    /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/>.
    /// </summary>
    /// <param name="component">Type of component to display.</param>
    /// <param name="title">Modal title.</param>
    IModalReference Show(Type component, string title);

    /// <summary>
    /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/> and <paramref name="options"/>.
    /// </summary>
    /// <param name="component">Type of component to display.</param>
    /// <param name="title">Modal title.</param>
    /// <param name="options">Options to configure the modal.</param>
    IModalReference Show(Type component, string title, ModalOptions options);

    /// <summary>
    /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/> and <paramref name="parameters"/>.
    /// </summary>
    /// <param name="component">Type of component to display.</param>
    /// <param name="title">Modal title.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    IModalReference Show(Type component, string title, ModalParameters parameters);

    /// <summary>
    /// Shows a modal containing a <paramref name="component"/> with the specified <paramref name="title"/>, <paramref name="parameters"/>
    /// and <paramref name="options"/>.
    /// </summary>
    /// <param name="component">Type of component to display.</param>
    /// <param name="title">Modal title.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    /// <param name="options">Options to configure the modal.</param>
    IModalReference Show(Type component, string title, ModalParameters parameters, ModalOptions options);
}