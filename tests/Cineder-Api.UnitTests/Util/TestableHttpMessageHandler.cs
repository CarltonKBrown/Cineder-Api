namespace Cineder_Api.UnitTests.Util
{
    public abstract class TestableHttpMessageHandler : HttpMessageHandler
    {
        public abstract Task<HttpResponseMessage> TestSendAsync(HttpRequestMessage request, CancellationToken cancellation);

        protected sealed override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => TestSendAsync(request, cancellationToken);
    }
}
