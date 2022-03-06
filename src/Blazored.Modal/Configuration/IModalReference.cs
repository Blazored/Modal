using Blazored.Modal.Services;

namespace Blazored.Modal;

public interface IModalReference
{
    Task<ModalResult> Result { get; }

    void Close();
    void Close(ModalResult result);
}