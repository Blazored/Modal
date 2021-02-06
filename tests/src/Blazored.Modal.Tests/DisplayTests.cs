using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Blazored.Modal.Tests.Assets;
using Blazored.Modal.Services;
using static Bunit.ComponentParameterFactory;

namespace Blazored.Modal.Tests
{
    public class DisplayTests : TestContext
    {
        public DisplayTests()
        {
            Services.AddScoped<NavigationManager, MockNavigationManager>();
            Services.AddBlazoredModal();

            JSInterop.Mode = JSRuntimeMode.Loose;
        }

        [Fact]
        public void ModalIsNotVisibleByDefault()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            
            // Act
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-container"));
        }

        [Fact]
        public void ModalIsVisibleWhenShowCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            modalService.Show<TestComponent>();

            // Assert
            Assert.NotNull(cut.FindComponent<BlazoredModalInstance>());
        }

        [Fact]
        public void MultipleModalsAreVisibleWhenShowCalledMultipleTimes()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

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
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

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
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            modalService.Show<TestComponent>();
            Assert.Equal(1, cut.FindAll(".blazored-modal-container").Count);

            var closeButton = cut.Find(".blazored-modal-close");
            closeButton.Click();

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-container"));
        }

        [Fact]
        public void ModalHidesWhenReferenceCloseCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            var modalReferece = modalService.Show<TestComponent>();
            Assert.Equal(1, cut.FindAll(".blazored-modal-container").Count);

            modalReferece.Close();

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-container"));
        }
    }
}