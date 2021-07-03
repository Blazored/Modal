using Blazored.Modal.Services;
using Blazored.Modal.Tests.Assets;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
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
        public async Task ModalDisplaysSpecifiedTitle()
        {
            // Arrange
            var testTitle = "Title";
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>(testTitle);

            // Assert
            Assert.Equal(testTitle, cut.Find(".blazored-modal-title").InnerHtml);
        }
        
        [Fact]
        public async Task ModalDisplaysCorrectPositionClassWhenIsCentered()
        {
            // Arrange
            var options = new ModalOptions { Position = ModalPosition.Center };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".blazored-modal-container.blazored-modal-center"));
        }

        [Fact]
        public async Task ModalDisplaysCorrectPositionClassWhenIsNotCentered()
        {
            // Arrange
            var options = new ModalOptions { Position = ModalPosition.TopLeft };
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);


            // Assert
            Assert.NotNull(cut.Find(".blazored-modal-container.blazored-modal-topleft"));
        }

        [Fact]
        public async Task ModalDisplaysCorrectPositionClassWhenUsingCustomPositiopn()
        {
            // Arrange
            var options = new ModalOptions
            {
                Position = ModalPosition.Custom,
                PositionCustomClass = "my-custom-class"
            };

            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find(".blazored-modal-container.my-custom-class"));
        }

        [Fact]
        public async Task ModalDisplaysCustomStyles()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var customStyle = "my-custom-style";
            var options = new ModalOptions { Class = customStyle };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find($"div.{customStyle}"));
        }

        [Fact]
        public async Task ModalDisplaysCloseButtonByDefault()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>();

            // Assert
            Assert.NotNull(cut.Find(".blazored-modal-close"));
        }

        [Fact]
        public async Task ModalDoesNotDisplayCloseButtonWhenSetToFalseInOptions()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var options = new ModalOptions { HideCloseButton = true };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-close"));
        }

        [Fact]
        public async Task ModalDisplaysHeaderByDefault()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>();

            // Assert
            Assert.NotNull(cut.Find(".blazored-modal-header"));
        }

        [Fact]
        public async Task ModalDoesNotDisplayHeaderWhenSetToFalseInOptions()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var options = new ModalOptions { HideHeader = true };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.Empty(cut.FindAll(".blazored-modal-header"));
        }

        [Fact]
        public async Task ModalDisplaysCorrectContent()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("");

            // Assert
            Assert.Equal(TestComponent.DefaultTitle, cut.Find(".test-component h1").InnerHtml);
        }

        [Fact]
        public async Task ModalDisplaysCorrectContentWhenUsingModalParameters()
        {
            var testTitle = "Testing Components";

            // Arrange
            var modalService = Services.GetService<IModalService>();

            var parameters = new ModalParameters();
            parameters.Add("Title", testTitle);
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", parameters);

            // Assert
            Assert.Equal(testTitle, cut.Find(".test-component h1").InnerHtml);
        }

        [Fact]
        public async Task ModalDisplaysCustomStyleWithScrollableContent()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var customStyle = "my-custom-style";
            var options = new ModalOptions { Class = customStyle, ContentScrollable = true };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find($"div.{customStyle}"));
            Assert.NotNull(cut.Find($"div.blazored-modal-scrollable"));
        }

        [Fact]
        public async Task ModalDisplaysStandardStyleWithScrollableContent()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var options = new ModalOptions { ContentScrollable = true };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find($"div.blazored-modal"));
            Assert.NotNull(cut.Find($"div.blazored-modal-scrollable"));
        }

        [Fact]
        public async Task ModalDisplaysStandardStyleWithScrollableContentAndAnimationFadeInClass()
        {
            // Arrange
            var modalService = Services.GetService<IModalService>();
            var options = new ModalOptions { ContentScrollable = true, Animation = new ModalAnimation(ModalAnimationType.FadeIn, 100) };
            var cut = RenderComponent<BlazoredModal>(CascadingValue(modalService));

            // Act
            await modalService.ShowAsync<TestComponent>("", options);

            // Assert
            Assert.NotNull(cut.Find($"div.blazored-modal"));
            Assert.NotNull(cut.Find($"div.blazored-modal-scrollable"));
            Assert.NotNull(cut.Find($"div.blazored-modal-fade-in"));
        }
    }
}
