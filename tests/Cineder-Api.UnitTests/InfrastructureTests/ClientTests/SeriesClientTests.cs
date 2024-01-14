using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure.Clients;
using Cineder_Api.UnitTests.InfrastructureTests.ClientTests.Fixtures;
using FakeItEasy;
using System.Net;
using Xunit;

namespace Cineder_Api.UnitTests.InfrastructureTests.ClientTests
{
    public class SeriesClientTests : IClassFixture<SeriesClientFixture>
    {
        private readonly SeriesClientFixture _seriesClientFixture;

        private ISeriesClient? _seriesClientFake;

        public SeriesClientTests(SeriesClientFixture seriesClientFixture)
        {
            _seriesClientFixture = seriesClientFixture;
        }

        #region GetSeriesByIdAsync Tests

        [Fact]
        public async Task GetSeriesByIdAsync_NullHttpClient_ShouldReturnEmptyMovieDetail()
        {
            // Arrange
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act

            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesByIdAsync(1);

            var expected = new SeriesDetail();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetSeriesByIdAsync_InvalidSeriesId_ShouldReturnEmptyResult()
        { // Arrange
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(_seriesClientFixture.CinederOptions);

            A.CallTo(() => _seriesClientFixture.TestableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(new HttpResponseMessage(HttpStatusCode.BadRequest));

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(_seriesClientFixture.CinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_seriesClientFixture.TestableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_seriesClientFixture.CinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var result = await _seriesClientFake.GetSeriesByIdAsync(-20);

            // Assert

            Assert.True(result.Id == 0);
            Assert.True(string.IsNullOrWhiteSpace(result.Name));
        } 
        #endregion

        #region GetSeriesByTitleAsync Tests
        [Fact]
        public async Task GetSeriesByTitleAsync_NullSearchText_ShouldReturnEmptySearchResult()
        {
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesByTitleAsync(null);

            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        [Fact]
        public async Task GetSeriesByTitleAsync_EmptySearchText_ShouldReturnEmptySearchResult()
        {
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesByTitleAsync(string.Empty);

            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }
        [Fact]
        public async Task GetSeriesByTitleAsync_BlankSearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesByTitleAsync("     ");

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        #endregion

        #region GetSeriesKeywordsAsync Tests
        [Fact]
        public async Task GetSeriesKeywordsAsync_NullSearchText_ShouldReturnEmptySearchResult()
        {
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesKeywordsAsync(null);

            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        [Fact]
        public async Task GetSeriesKeywordsAsync_EmptySearchText_ShouldReturnEmptySearchResult()
        {
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesKeywordsAsync(string.Empty);

            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }
        [Fact]
        public async Task GetSeriesKeywordsAsync_BlankSearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesKeywordsAsync("     ");

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        #endregion

        #region GetSeriesSimilarAsync Tests
        [Fact]
        public async Task GetseriesSimilarAsync_NegativeSeriesId_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _seriesClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _seriesClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _seriesClientFake = new SeriesClient(_seriesClientFixture.LoggerFake, _seriesClientFixture.HttpClientFactoryFake, _seriesClientFixture.CinederOptionsFake);

            var actual = await _seriesClientFake.GetSeriesSimilarAsync(-10);

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }
        #endregion
    }
}
