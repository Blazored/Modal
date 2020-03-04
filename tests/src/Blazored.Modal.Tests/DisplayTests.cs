using Blazored.Modal.Tests.Assets;
using Microsoft.AspNetCore.Components.Testing;
using Xunit;

namespace Blazored.Modal.Tests
{
    public class DisplayTests : TestBase
    {
        [Fact]
        public void ModalIsNotVisibleByDefault()
        {
            var component = _host.AddComponent<BlazoredModal>();
            var modalContainer = component.Find(".blazored-modal-container");

            Assert.Null(modalContainer);
        }

        [Fact]
        public void ModalIsVisibleWhenShowCalled()
        {
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>("");

            var modalContainer = component.Find(".blazored-modal-container");

            Assert.NotNull(modalContainer);
        }

        [Fact]
        public void ModalHidesWhenCloseCalled()
        {
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>("");
            var modalContainer = component.Find(".blazored-modal-container");

            Assert.NotNull(modalContainer);

            var closeButton = component.Find(".blazored-modal-close");
            closeButton.Click();

            modalContainer = component.Find(".blazored-modal-container");

            Assert.Null(modalContainer);
        }

        [Fact]
        public void ModalHidesWhenCancelCalled()
        {
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>("");
            var modalContainer = component.Find(".blazored-modal-container");

            Assert.NotNull(modalContainer);

            var cancelButton = component.Find(".blazored-modal-close");
            cancelButton.Click();

            modalContainer = component.Find(".blazored-modal-container");

            Assert.Null(modalContainer);
        }
    }
}
