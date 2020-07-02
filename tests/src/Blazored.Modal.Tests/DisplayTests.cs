using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Blazored.Modal.Tests.Assets;
using Blazored.Modal.Services;
using Bunit.Mocking.JSInterop;

namespace Blazored.Modal.Tests
{
    public class DisplayTests : ComponentTestFixture
    {
        public DisplayTests()
        {
            Services.AddScoped<NavigationManager, MockNavigationManager>();
            Services.AddBlazoredModal();
            Services.AddMockJsRuntime();
        }

        [Fact]
        public void ModalIsNotVisibleByDefault()
        {
            // Arrange / Act
            var cut = RenderComponent<BlazoredModal>();

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-container"));
        }

        [Fact]
        public void ModalIsVisibleWhenShowCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>();

            // Act
            modalService.Show<TestComponent>();

            // Assert
            Assert.Equal(1, cut.FindAll(".blazored-modal-container").Count);
        }

        [Fact]
        public void MultipleModalsAreVisibleWhenShowCalledMultipleTimes()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>();

            // Act
            modalService.Show<TestComponent>();
            modalService.Show<TestComponent>();

            // Assert
            Assert.Equal(2, cut.FindAll(".blazored-modal-container").Count);
        }

        [Fact]
        public void ModalHidesWhenCloseCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>();

            // Act
            modalService.Show<TestComponent>();
            Assert.Equal(1, cut.FindAll(".blazored-modal-container").Count);

            var closeButton = cut.Find(".test-component__close-button");
            closeButton.Click();

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-container"));
        }

        [Fact]
        public void ModalHidesWhenCancelCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>();

            // Act
            modalService.Show<TestComponent>();
            Assert.Equal(1, cut.FindAll(".blazored-modal-container").Count);

            var closeButton = cut.Find(".blazored-modal-close");
            closeButton.Click();

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-container"));
        }
    }
}