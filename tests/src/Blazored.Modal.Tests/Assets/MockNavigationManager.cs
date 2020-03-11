using Microsoft.AspNetCore.Components;

namespace Blazored.Modal.Tests.Assets
{
    internal class MockNavigationManager : NavigationManager
    {
        public MockNavigationManager()
        {
        }

        public MockNavigationManager(string baseUri = null, string uri = null)
        {
            Initialize(baseUri ?? "http://example.com/", uri ?? baseUri ?? "http://example.com/welcome-page");
        }

        public new void Initialize(string baseUri, string uri)
        {
            base.Initialize(baseUri, uri);
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            throw new System.NotImplementedException();
        }
    }
}
