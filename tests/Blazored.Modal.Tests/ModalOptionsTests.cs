using Blazored.Modal.Services;
using Blazored.Modal.Tests.Assets;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Bunit.ComponentParameterFactory;

namespace Blazored.Modal.Tests
{
    public class ModalOptionsTests : TestContext
    {
        public ModalOptionsTests()
        {
            Services.AddScoped<NavigationManager, MockNavigationManager>();
            Services.AddBlazoredModal();
            JSInterop.Mode = JSRuntimeMode.Loose;
        }
        
        [Fact]
        public void ModalDisplaysSpecifiedTitle()
        {
            // Arrange
            var testTitle = "Title";
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>(testTitle);

            // Assert
            Assert.Equal(testTitle, cut.Find(".bm-title").InnerHtml);
        }
        
        [Fact]
        public void ModalDisplaysCorrectPositionClassWhenIsCentered()
        {
            // Arrange
            var options = new ModalOptions { Position = ModalPosition.TopCenter };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".bm-container"));
        }

        [Fact]
        public void ModalDisplaysCorrectPositionClassWhenIsNotCentered()
        {
            // Arrange
            var options = new ModalOptions { Position = ModalPosition.TopLeft };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".bm-container.position-topleft"));
        }

        [Fact]
        public void ModalDisplaysCorrectPositionClassWhenUsingCustomPosition()
        {
            // Arrange
            var options = new ModalOptions
            {
                Position = ModalPosition.Custom,
                PositionCustomClass = "my-custom-class"
            };

            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".bm-container.my-custom-class"));
        }

        [Fact]
        public void ModalDisplaysCustomStyles()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var customStyle = "my-custom-style";
            var options = new ModalOptions { Class = customStyle };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find($"div.{customStyle}"));
        }

        [Fact]
        public void ModalDisplaysCloseButtonByDefault()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>();

            // Assert
            Assert.NotNull(cut.Find(".bm-close"));
        }

        [Fact]
        public void ModalDoesNotDisplayCloseButtonWhenSetToFalseInOptions()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var options = new ModalOptions { HideCloseButton = true };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.Empty(cut.FindAll(".bm-close"));
        }

        [Fact]
        public void ModalDisplaysHeaderByDefault()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>();

            // Assert
            Assert.NotNull(cut.Find(".bm-header"));
        }

        [Fact]
        public void ModalDoesNotDisplayHeaderWhenSetToFalseInOptions()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var options = new ModalOptions { HideHeader = true };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.Empty(cut.FindAll(".bm-header"));
        }

        [Fact]
        public void ModalDisplaysCorrectContent()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("");

            // Assert
            Assert.Equal(TestComponent.DefaultTitle, cut.Find(".test-component h1").InnerHtml);
        }

        [Fact]
        public void ModalDisplaysCorrectContentWhenUsingModalParameters()
        {
            var testTitle = "Testing Components";

            // Arrange
            var modalService = Services.GetService<IModalService>();

            var parameters = new ModalParameters();
            parameters.Add("Title", testTitle);
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", parameters);

            // Assert
            Assert.Equal(testTitle, cut.Find(".test-component h1").InnerHtml);
        }
        
        [Fact]
        public void ModalDisplaysMediumSizeClassWhenSizeNotSet()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("");

            // Assert
            Assert.NotNull(cut.Find(".size-medium"));
        }
        
        [Fact]
        public void ModalDisplaysSmallSizeClassWhenSizeIsSmall()
        {
            // Arrange
            var options = new ModalOptions { Size = ModalSize.Small };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".size-small"));
        }
        
        [Fact]
        public void ModalDisplaysLargeSizeClassWhenSizeIsLarge()
        {
            // Arrange
            var options = new ModalOptions { Size = ModalSize.Large };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".size-large"));
        }
        
        [Fact]
        public void ModalDisplaysExtraLargeSizeClassWhenSizeIsExtraLarge()
        {
            // Arrange
            var options = new ModalOptions { Size = ModalSize.ExtraLarge };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".size-extra-large"));
        }
        
        [Fact]
        public void ModalDisplaysCustomSizeClassWhenSizeIsCustom()
        {
            // Arrange
            var options = new ModalOptions
            {
                Size = ModalSize.Custom,
                SizeCustomClass = "my-custom-size"
            };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService!));

            // Act
            modalService.Show<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".my-custom-size"));
        }
    }
}
