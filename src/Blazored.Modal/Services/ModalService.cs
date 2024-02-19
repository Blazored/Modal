using Microsoft.AspNetCore.Components;

namespace Blazored.Modal.Services;

public class ModalService : IModalService
{
    internal event Func<ModalReference, Task>? OnModalInstanceAdded;
    internal event Func<ModalReference, ModalResult, Task>? OnModalCloseRequested;

    /// <summary>
    /// Shows the modal with the component type.
    /// </summary>
    public IModalReference Show<T>() where T : IComponent 
        => Show<T>(string.Empty, new ModalParameters(), new ModalOptions());

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="options"/>.
    /// </summary>
    /// <param name="options">Options to configure the modal.</param>
    public IModalReference Show<TComponent>(ModalOptions options) where TComponent : IComponent
        => Show<TComponent>("", new ModalParameters(), options);

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="parameters"/>.
    /// </summary>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    public IModalReference Show<TComponent>(ModalParameters parameters) where TComponent : IComponent
        => Show<TComponent>("", parameters, new ModalOptions());

    /// <summary>
    /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="parameters"/>
    /// and <paramref name="options"/>.
    /// </summary>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    /// <param name="options">Options to configure the modal.</param>
    public IModalReference Show<TComponent>(ModalParameters parameters, ModalOptions options) where TComponent : IComponent
        => Show<TComponent>("", parameters, options);

    /// <summary>
    /// Shows the modal with the component type using the specified title.
    /// </summary>
    /// <param name="title">Modal title.</param>
    public IModalReference Show<T>(string title) where T : IComponent 
        => Show<T>(title, new ModalParameters(), new ModalOptions());

    /// <summary>
    /// Shows the modal with the component type using the specified title.
    /// </summary>
    /// <param name="title">Modal title.</param>
    /// <param name="options">Options to configure the modal.</param>
    public IModalReference Show<T>(string title, ModalOptions options) where T : IComponent 
        => Show<T>(title, new ModalParameters(), options);

    /// <summary>
    /// Shows the modal with the component type using the specified <paramref name="title"/>,
    /// passing the specified <paramref name="parameters"/>.
    /// </summary>
    /// <param name="title">Modal title.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    public IModalReference Show<T>(string title, ModalParameters parameters) where T : IComponent 
        => Show<T>(title, parameters, new ModalOptions());

    /// <summary>
    /// Shows the modal with the component type using the specified <paramref name="title"/>,
    /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
    /// </summary>
    /// <param name="title">Modal title.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    /// <param name="options">Options to configure the modal.</param>
    public IModalReference Show<T>(string title, ModalParameters parameters, ModalOptions options) where T : IComponent 
        => Show(typeof(T), title, parameters, options);

    /// <summary>
    /// Shows the modal with the specific component type.
    /// </summary>
    /// <param name="contentComponent">Type of component to display.</param>
    public IModalReference Show(Type contentComponent) 
        => Show(contentComponent, string.Empty, new ModalParameters(), new ModalOptions());

    /// <summary>
    /// Shows the modal with the component type using the specified title.
    /// </summary>
    /// <param name="contentComponent">Type of component to display.</param>
    /// <param name="title">Modal title.</param>
    public IModalReference Show(Type contentComponent, string title) 
        => Show(contentComponent, title, new ModalParameters(), new ModalOptions());

    /// <summary>
    /// Shows the modal with the component type using the specified title.
    /// </summary>
    /// <param name="title">Modal title.</param>
    /// <param name="contentComponent">Type of component to display.</param>
    /// <param name="options">Options to configure the modal.</param>
    public IModalReference Show(Type contentComponent, string title, ModalOptions options) 
        => Show(contentComponent, title, new ModalParameters(), options);

    /// <summary>
    /// Shows the modal with the component type using the specified <paramref name="title"/>,
    /// passing the specified <paramref name="parameters"/>.
    /// </summary>
    /// <param name="title">Modal title.</param>
    /// <param name="contentComponent">Type of component to display.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    public IModalReference Show(Type contentComponent, string title, ModalParameters parameters) 
        => Show(contentComponent, title, parameters, new ModalOptions());

    /// <summary>
    /// Shows the modal with the component type using the specified <paramref name="title"/>,
    /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
    /// </summary>
    /// <param name="contentComponent">Type of component to display.</param>
    /// <param name="title">Modal title.</param>
    /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
    /// <param name="options">Options to configure the modal.</param>
    public IModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options)
    {
        if (!typeof(IComponent).IsAssignableFrom(contentComponent))
        {
            throw new ArgumentException($"{contentComponent.FullName} must be a Blazor Component");
        }

        ModalReference? modalReference = null;
        var modalInstanceId = Guid.NewGuid();
        var modalContent = new RenderFragment(builder =>
        {
            var i = 0;
            builder.OpenComponent(i++, contentComponent);
            foreach (var (name, value) in parameters.Parameters)
            {
                builder.AddAttribute(i++, name, value);
            }
            builder.CloseComponent();
        });
        var modalInstance = new RenderFragment(builder =>
        {
            builder.OpenComponent<BlazoredModalInstance>(0);
            builder.SetKey("blazoredModalInstance_" + modalInstanceId);
            builder.AddAttribute(1, "Options", options);
            builder.AddAttribute(2, "Title", title);
            builder.AddAttribute(3, "Content", modalContent);
            builder.AddAttribute(4, "Id", modalInstanceId);
            builder.AddComponentReferenceCapture(5, compRef => modalReference!.ModalInstanceRef = (BlazoredModalInstance)compRef);
            builder.CloseComponent();
        });
        modalReference = new ModalReference(modalInstanceId, modalInstance, this);

        OnModalInstanceAdded?.Invoke(modalReference);

        return modalReference;
    }

    internal void Close(ModalReference modal) 
        => Close(modal, ModalResult.Ok());

    internal void Close(ModalReference modal, ModalResult result) 
        => OnModalCloseRequested?.Invoke(modal, result);
}