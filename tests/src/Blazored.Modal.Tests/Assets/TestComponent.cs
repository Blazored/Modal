using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazored.Modal.Tests.Assets
{
    internal class TestComponent : ComponentBase
    {
        public const string TitleText = "My Test Component";

        [CascadingParameter] public ModalParameters ModalParameters { get; set; }

        public string Title
        {
            get
            {
                var cascadedTitle = ModalParameters.TryGet<string>("Title");
                return string.IsNullOrWhiteSpace(cascadedTitle) ? TitleText : cascadedTitle;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(1, "h1");
            builder.AddContent(2, Title);
            builder.CloseElement();
        }
    }
}
