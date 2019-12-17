using Blazored.Modal.Tests.Assets;
using Xunit;

namespace Blazored.Modal.Tests
{
    public class ModalOptionsTests : TestBase
    {
        [Fact]
        public void ModalDisplaysSpecifiedTitle()
        {
            var testTitle = "Title";
            var component = _host.AddComponent<BlazoredModal>();
            _modalService.Show<TestComponent>(testTitle);

            var title = component.Find(".blazored-modal-title");

            Assert.Equal(testTitle, title.InnerText);
        }

        [Fact]
        public void ModalDisplaysCorrectPositionClass()
        {
            var position = "blazored-modal-topleft";
            var options = new ModalOptions() { Position = position };
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("", options);

            var modalContainer = component.Find($".blazored-modal-container.{position}");

            Assert.NotNull(modalContainer);
        }

        [Fact]
        public void ModalDisplaysCustomStyles()
        {
            var customStyle = "my-custom-style";
            var options = new ModalOptions() { Style = customStyle };
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("", options);

            var styleDiv = component.Find($"div.{customStyle}");

            Assert.NotNull(styleDiv);
        }

        [Fact]
        public void ModalDisplaysCloseButtonByDefault()
        {
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("");

            var header = component.Find(".blazored-modal-close");

            Assert.NotNull(header);
        }

        [Fact]
        public void ModalDoesNotDisplayCloseButtonWhenSetToFalseInOptions()
        {
            var options = new ModalOptions() { HideCloseButton = true };
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("", options);

            var header = component.Find(".blazored-modal-close");

            Assert.Null(header);
        }

        [Fact]
        public void ModalDisplaysHeaderByDefault()
        {
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("");

            var header = component.Find(".blazored-modal-header");

            Assert.NotNull(header);
        }

        [Fact]
        public void ModalDoesNotDisplayHeaderWhenSetToFalseInOptions()
        {
            var options = new ModalOptions() { HideHeader = true };
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("", options);

            var header = component.Find(".blazored-modal-header");

            Assert.Null(header);
        }

        [Fact]
        public void ModalDisplaysCorrectContent()
        {
            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("");

            var content = component.Find("h1");

            Assert.Equal(content.InnerText, TestComponent.TitleText);
        }

        [Fact]
        public void ModalDisplaysCorrectContentWhenUsingModalParameters()
        {
            var testTitle = "Testing Components";
            var parameters = new ModalParameters();
            parameters.Add("Title", testTitle);

            var component = _host.AddComponent<BlazoredModal>();

            _modalService.Show<TestComponent>("", parameters);

            var content = component.Find("h1");

            Assert.Equal(content.InnerText, testTitle);
        }

    }
}
