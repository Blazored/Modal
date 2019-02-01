using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace Blazored.Modal.Services
{
    public class ModalService : IModalService
    {
        public event Action<string, RenderFragment> OnShow;

        public event Action OnClose;

        public void Show(string title, Type contentType)
        {
            Console.WriteLine(OnClose.Target.ToString());
            if (contentType.BaseType != typeof(BlazorComponent))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x => { x.OpenComponent(1, contentType); x.CloseComponent(); });

            OnShow?.Invoke(title, content);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
