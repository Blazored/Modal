using Blazored.Modal.Services;
using System.Threading.Tasks;

namespace Blazored.Modal
{
    public interface IModalReference
    {
        Task<ModalResult> Result { get; }

        void Close();
        void Close(ModalResult result);
    }
}