using Cineder_Api.Core.Config;
using Cineder_Api.Infrastructure.Clients;
using Cineder_Api.UnitTests.Util;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cineder_Api.UnitTests.InfrastructureTests.ClientTests.Fixtures
{
    public class MovieClientFixture: IDisposable
    {
        public IOptionsSnapshot<CinederOptions> CinederOptionsFake { get; }
        public ILogger<MovieClient> LoggerFake {  get; }
       public IHttpClientFactory HttpClientFactoryFake { get; }
        public TestableHttpMessageHandler TestableHttpMessageHandlerFake { get; }
        public CinederOptions CinederOptions { get; }

        public MovieClientFixture()
        {
            CinederOptions = new CinederOptions()
            {
                ApiBaseUrl = "https://api.themoviedb.org/3",
                ApiKey = "1232",
                ClientName = "MovieDb",
                Language = "en",
                MaxPages = 1
            };

            CinederOptionsFake = A.Fake<IOptionsSnapshot<CinederOptions>>();

            LoggerFake = A.Fake<ILogger<MovieClient>>();

            TestableHttpMessageHandlerFake = A.Fake<TestableHttpMessageHandler>();

            HttpClientFactoryFake = A.Fake<IHttpClientFactory>();
        }

        public void Dispose()
        {
            // Add clean up code, if required
        }
    }
}
