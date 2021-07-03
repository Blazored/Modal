using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Blazored.Modal.Services
{
    public class ModalService : IModalService
    {
        internal event Func<ModalReference, Task> OnModalInstanceAdded;
        internal event Action<ModalReference, ModalResult> OnModalCloseRequested;

        /// <summary>
        /// Shows the modal with the component type.
        /// </summary>
        public Task<IModalReference> ShowAsync<T>() where T : IComponent
        {
            return ShowAsync<T>(string.Empty, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        public Task<IModalReference> ShowAsync<T>(string title) where T : IComponent
        {
            return ShowAsync<T>(title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="options">Options to configure the modal.</param>
        public Task<IModalReference> ShowAsync<T>(string title, ModalOptions options) where T : IComponent
        {
            return ShowAsync<T>(title, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public Task<IModalReference> ShowAsync<T>(string title, ModalParameters parameters) where T : IComponent
        {
            return ShowAsync<T>(title, parameters, new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public Task<IModalReference> ShowAsync<T>(string title, ModalParameters parameters, ModalOptions options) where T : IComponent
        {
            return ShowAsync(typeof(T), title, parameters, options);
        }

        /// <summary>
        /// Shows the modal with the specific component type.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        public Task<IModalReference> ShowAsync(Type contentComponent)
        {
            return ShowAsync(contentComponent, string.Empty, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        public Task<IModalReference> ShowAsync(Type contentComponent, string title)
        {
            return ShowAsync(contentComponent, title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        public Task<IModalReference> ShowAsync(Type contentComponent, string title, ModalOptions options)
        {
            return ShowAsync(contentComponent, title, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public Task<IModalReference> ShowAsync(Type contentComponent, string title, ModalParameters parameters)
        {
            return ShowAsync(contentComponent, title, parameters, new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public async Task<IModalReference> ShowAsync(Type contentComponent, string title, ModalParameters parameters, ModalOptions options)
        {
            if (!typeof(IComponent).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent.FullName} must be a Blazor Component");
            }

            var modalInstanceId = Guid.NewGuid();
            ModalReference modalReference = null;
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
                builder.AddAttribute(1, nameof(BlazoredModalInstance.Options), options);
                builder.AddAttribute(2, nameof(BlazoredModalInstance.Title), title);
                builder.AddAttribute(3, nameof(BlazoredModalInstance.Content), modalContent);
                builder.AddAttribute(4, nameof(BlazoredModalInstance.Id), modalInstanceId);
                builder.AddComponentReferenceCapture(5, compRef => modalReference.ModalInstanceRef = (BlazoredModalInstance)compRef);
                builder.CloseComponent();
            });
            modalReference = new ModalReference(modalInstanceId, modalInstance, this);

            if (OnModalInstanceAdded != null)
                await OnModalInstanceAdded.Invoke(modalReference);

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