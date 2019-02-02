using System;

namespace Blazored.Modal.Services
{
    public interface IModalService
    {
        event Action OnClose;

        void Show(string title, Type contentType);

        void Show(string title, Type contentType, ModalParameters parameters);
    }
}
