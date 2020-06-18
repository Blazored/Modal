using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal.Services
{
    public class ModalService : IModalService
    {
        internal event Action<ModalReference> OnModalInstanceAdded;
        internal event Action<ModalReference, ModalResult> OnModalCloseRequested;

        /// <summary>
        /// Shows the modal with the component type.
        /// </summary>
        public IModalReference Show<T>() where T : ComponentBase
        {
            return Show<T>(string.Empty, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        public IModalReference Show<T>(string title) where T : ComponentBase
        {
            return Show<T>(title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="options">Options to configure the modal.</param>
        public IModalReference Show<T>(string title, ModalOptions options) where T : ComponentBase
        {
            return Show<T>(title, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public IModalReference Show<T>(string title, ModalParameters parameters) where T : ComponentBase
        {
            return Show<T>(title, parameters, new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public IModalReference Show<T>(string title, ModalParameters parameters, ModalOptions options) where T : ComponentBase
        {
            return Show(typeof(T), title, parameters, options);
        }

        /// <summary>
        /// Shows the modal with the specific component type.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        public IModalReference Show(Type contentComponent)
        {
            return Show(contentComponent, string.Empty, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        public IModalReference Show(Type contentComponent, string title)
        {
            return Show(contentComponent, title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        public IModalReference Show(Type contentComponent, string title, ModalOptions options)
        {
            return Show(contentComponent, title, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public IModalReference Show(Type contentComponent, string title, ModalParameters parameters)
        {
            return Show(contentComponent, title, parameters, new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public IModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent.FullName} must be a Blazor Component");
            }

            var modalInstanceId = Guid.NewGuid();
            var modalContent = new RenderFragment(builder =>
            {
                var i = 0;
                builder.OpenComponent(i++, contentComponent);
                foreach (var parameter in parameters._parameters)
                {
                    builder.AddAttribute(i++, parameter.Key, parameter.Value);
                }
                builder.CloseComponent();
            });
            var modalInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<BlazoredModalInstance>(0);
                builder.AddAttribute(1, "Options", options);
                builder.AddAttribute(2, "Title", title);
                builder.AddAttribute(3, "Content", modalContent);
                builder.AddAttribute(4, "Id", modalInstanceId);
                builder.CloseComponent();
            });
            var modalReference = new ModalReference(modalInstanceId, modalInstance, this);

            OnModalInstanceAdded?.Invoke(modalReference);

            return modalReference;
        }

        internal void Close(ModalReference modal)
        {
            Close(modal, ModalResult.Ok<object>(null));
        }

        internal void Close(ModalReference modal, ModalResult result)
        {
            OnModalCloseRequested?.Invoke(modal, result);
        }
    }
}
