using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;

namespace Blazored.Modal.Tests.Assets
{
    public class TestBase
    {
        protected TestHost _host;
        protected IModalService _modalService;

        public TestBase()
        {
            _host = new TestHost();
            _modalService = new ModalService();
            _host.AddService<IModalService>(_modalService);
            _host.AddService<IJSRuntime>(new MockJsRuntime());
        }
    }
}