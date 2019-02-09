using Microsoft.AspNetCore.Components;
using System;

namespace Blazored.Modal.Services
{
    public class ModalService : IModalService
    {
        public event Action OnClose;

        internal event Action<string, RenderFragment, ModalParameters> OnShow;

        public void Show(string title, Type contentType)
        {
            Show(title, contentType, new ModalParameters());
        }

        public void Show(string title, Type contentType, ModalParameters parameters)
        {
            if (contentType.BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x => { x.OpenComponent(1, contentType); x.CloseComponent(); });

            OnShow?.Invoke(title, content, parameters);
        }

        internal void Close()
        {
            OnClose?.Invoke();
        }
    }
}
