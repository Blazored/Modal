using Blazored.Modal.Services;
using Blazored.Modal.Tests.Assets;
using Xunit;

namespace Blazored.Modal.Tests
{
    public class DisplayTests : TestBase
    {
        [Fact]
        public void ModalIsNotVisibleByDefault()
        {
            var component = _host.AddComponent<BlazoredModal>();
            var modalContainer = component.Find(".blazored-modal-container.blazored-modal-active");

            Assert.Null(modalContainer);
        }

        [Fact]
        public void ModalIsVisibleWhenShowCalled()
        {
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>("");

            var modalContainer = component.Find(".blazored-modal-container.blazored-modal-active");

            Assert.NotNull(modalContainer);
        }

        [Fact]
        public void ModalHidesWhenCloseCalled()
        {
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>("");
            var modalContainer = component.Find(".blazored-modal-container.blazored-modal-active");

            Assert.NotNull(modalContainer);

            _modalService.Close(ModalResult.Ok("Ok"));
            modalContainer = component.Find(".blazored-modal-container.blazored-modal-active");

            Assert.Null(modalContainer);
        }

        [Fact]
        public void ModalHidesWhenCancelCalled()
        {
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>("");
            var modalContainer = component.Find(".blazored-modal-container.blazored-modal-active");

            Assert.NotNull(modalContainer);

            _modalService.Cancel();
            modalContainer = component.Find(".blazored-modal-container.blazored-modal-active");

            Assert.Null(modalContainer);
        }
    }
}
