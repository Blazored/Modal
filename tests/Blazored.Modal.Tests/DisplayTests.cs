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
            Assert.Empty(cut.FindAll(".bm-container"));
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
            Assert.Equal(2, cut.FindAll(".bm-container").Count);
        }

        [Fact]
        public void ModalHidesWhenCloseCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            var options = new ModalOptions
            {
                AnimationType = ModalAnimationType.None
            };
            modalService.Show<TestComponent>("", options);
            Assert.Single(cut.FindAll(".bm-container"));

            var closeButton = cut.Find(".test-component__close-button");
            closeButton.Click();

            // Assert
            Assert.Empty(cut.FindAll(".bm-container"));
        }

        [Fact]
        public void ModalHidesWhenCancelCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            var options = new ModalOptions
            {
                AnimationType = ModalAnimationType.None
            };
            modalService.Show<TestComponent>("", options);
            Assert.Single(cut.FindAll(".bm-container"));

            var closeButton = cut.Find(".bm-close");
            closeButton.Click();

            // Assert
            Assert.Empty(cut.FindAll(".bm-container"));
        }

        [Fact]
        public void ModalHidesWhenReferenceCloseCalled()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            var options = new ModalOptions
            {
                AnimationType = ModalAnimationType.None
            };
            var modalReferece = modalService.Show<TestComponent>("", options);
            Assert.Single(cut.FindAll(".bm-container"));

            modalReferece.Close();

            // Assert
            Assert.Empty(cut.FindAll(".bm-container"));
        }
    }
}