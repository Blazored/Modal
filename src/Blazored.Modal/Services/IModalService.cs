using Microsoft.AspNetCore.Blazor;
using System;

namespace Blazored.Modal.Services
{
    public interface IModalService
    {
        event Action OnClose;

        event Action<string, RenderFragment> OnShow;

        void Close();

        void Show(string title, Type contentType);
    }
}
