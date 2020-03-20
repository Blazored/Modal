using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal.Services
{
    public class ModalService : IModalService
    {
        internal event Action<ModalReference> OnModalInstanceAdded;



        /// <summary>
        /// Shows the modal generic with the specific component type.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        public ModalReference ShowGeneric(string title, string message, ModalButton modalButton, ModalType modalType, string captionYesButton = null,
            string captionNoButton = null, string captionOkButton = null, string captionCancelButton = null)
        {
            return Show(typeof(ModalGeneric), title, new ModalParameters(), new ModalOptions(), message: message, modalButton: modalButton,
                modalType: modalType, captionYesButton: captionYesButton, captionNoButton: captionNoButton, captionOkButton: captionOkButton, captionCancelButton: captionCancelButton);
        }



        /// <summary>
        /// Shows the modal with the component type.
        /// </summary>
        public ModalReference Show<T>() where T : ComponentBase
        {
            return Show<T>(string.Empty, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        public ModalReference Show<T>(string title) where T : ComponentBase
        {
            return Show<T>(title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="options">Options to configure the modal.</param>
        public ModalReference Show<T>(string title, ModalOptions options) where T : ComponentBase
        {
            return Show<T>(title, new ModalParameters(), options);
        }

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>, 
        /// passing the specified <paramref name="parameters"/>. 
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public ModalReference Show<T>(string title, ModalParameters parameters) where T : ComponentBase
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
        public ModalReference Show<T>(string title, ModalParameters parameters, ModalOptions options) where T : ComponentBase
        {
            return Show(typeof(T), title, parameters, options);
        }

        /// <summary>
        /// Shows the modal with the specific component type.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        public ModalReference Show(Type contentComponent)
        {
            return Show(contentComponent, string.Empty, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        public ModalReference Show(Type contentComponent, string title)
        {
            return Show(contentComponent, title, new ModalParameters(), new ModalOptions());
        }

        /// <summary>
        /// Shows the modal with the component type using the specified title.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="options">Options to configure the modal.</param>
        public ModalReference Show(Type contentComponent, string title, ModalOptions options)
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
        public ModalReference Show(Type contentComponent, string title, ModalParameters parameters)
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
        public ModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options,
            string message = null, ModalButton modalButton = 0, ModalType modalType = 0, string captionYesButton = null,
            string captionNoButton = null, string captionOkButton = null, string captionCancelButton = null)
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
                builder.AddAttribute(5, "Message", message);
                builder.AddAttribute(6, "ModalButton", modalButton);
                builder.AddAttribute(7, "ModalType", modalType);
                builder.AddAttribute(8, "CaptionYesButton", captionYesButton);
                builder.AddAttribute(9, "CaptionNoButton", captionNoButton);
                builder.AddAttribute(10, "CaptionOkButton", captionOkButton);
                builder.AddAttribute(11, "CaptionCancelButton", captionCancelButton);
                builder.CloseComponent();
            });
            var modalReference = new ModalReference(modalInstanceId, modalInstance);

            OnModalInstanceAdded?.Invoke(modalReference);

            return modalReference;
        }
    }
}
