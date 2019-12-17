using HtmlAgilityPack;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Testing
{
    public static class EventDispatchExtensions
    {
        public static void Click(this HtmlNode element)
        {
            _ = ClickAsync(element);
        }

        public static Task ClickAsync(this HtmlNode element)
        {
            return element.TriggerEventAsync("onclick", new MouseEventArgs());
        }
        public static void Submit(this HtmlNode element)
        {
            _ = SubmitAsync(element);
        }

        public static Task SubmitAsync(this HtmlNode element)
        {
            return element.TriggerEventAsync("onsubmit", new EventArgs());
        }

        public static void Change(this HtmlNode element, string newValue)
        {
            _ = ChangeAsync(element, newValue);
        }

        public static Task ChangeAsync(this HtmlNode element, string newValue)
        {
            return element.TriggerEventAsync("onchange", new ChangeEventArgs { Value = newValue });
        }

        public static void Change(this HtmlNode element, bool newValue)
        {
            _ = ChangeAsync(element, newValue);
        }

        public static Task ChangeAsync(this HtmlNode element, bool newValue)
        {
            return element.TriggerEventAsync("onchange", new ChangeEventArgs { Value = newValue });
        }

        [SuppressMessage("Usage", "BL0006:Do not use RenderTree types", Justification = "<Pending>")]
        public static Task TriggerEventAsync(this HtmlNode element, string attributeName, EventArgs eventArgs)
        {
            var eventHandlerIdString = element.GetAttributeValue(attributeName, string.Empty);
            if (string.IsNullOrEmpty(eventHandlerIdString))
            {
                throw new ArgumentException($"The element does not have an event handler for the event '{attributeName}'.");
            }
            var eventHandlerId = ulong.Parse(eventHandlerIdString);

            var renderer = ((TestHtmlDocument)element.OwnerDocument).Renderer;
            return renderer.DispatchEventAsync(eventHandlerId, 
                new EventFieldInfo(),
                eventArgs);
        }
    }
}
