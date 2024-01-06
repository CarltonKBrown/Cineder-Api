using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure;
using Cineder_Api.Infrastructure.Clients;
using Cineder_Api.Infrastructure.Models;
using Cineder_Api.UnitTests.InfrastructureTests.ClientTests.Fixtures;
using FakeItEasy;
using System.Net;
using Xunit;

namespace Cineder_Api.UnitTests.InfrastructureTests.ClientTests
{
    public class MovieClientTests : IClassFixture<MovieClientFixture>
    {
        private readonly MovieClientFixture _movieClientFixture;

        private IMovieClient? _movieClientFake;

        public MovieClientTests(MovieClientFixture movieClientFixture)
        {
            _movieClientFixture = movieClientFixture;
        }

        #region GetMovieByIdAsync Tests
        [Fact]
        public async Task GetMovieByIdAsync_NullHttpClient_ShouldReturnEmptyMovieDetail()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act

            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMovieByIdAsync(1);

            var expected = new MovieDetail();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetMovieByIdAsync_ShouldReturnEmptyMovieDetail()
        {
            // Arrange

            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(_movieClientFixture.CinederOptions);

            A.CallTo(() => _movieClientFixture.TestableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(new HttpResponseMessage(HttpStatusCode.NotFound));

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(_movieClientFixture.CinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_movieClientFixture.TestableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_movieClientFixture.CinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var result = await _movieClientFake.GetMovieByIdAsync(1);

            // Assert

            Assert.True(result.Id == 0);
            Assert.True(string.IsNullOrWhiteSpace(result.Name));
        }

        [Fact]
        public async Task GetMovieByIdAsync_MovieFound_ShouldMatch()
        {
            // Arrange

            var genres = new List<GenreContract>()
            {
                new(1, "Genre1"),
                new(2, "Genre2"),
                new(3, "Genre3"),
                new(4, "Genre4")
            };

            var productionCompanies = new List<ProductionCompanyContract>()
            {
                new(1, "ProdComp1", "logoPath1", "originCountry1"),
                new(2, "ProdComp2", "logoPath2", "originCountry2"),
                new(3, "ProdComp3", "logoPath3", "originCountry3"),
                new(4, "ProdComp4", "logoPath4", "originCountry4"),
                new(5, "ProdComp5", "logoPath5", "originCountry5")
            };

            var videos = new List<VideoContract>()
            {
                new(1, "VideoContract1", "vidLang1", "vidRegon1", "Key1", "Site1", 1, "type1"),
                new(2, "VideoContract2", "vidLang2", "vidRegon2", "Key2", "Site2", 2, "type2"),
                new(3, "VideoContract3", "vidLang3", "vidRegon3", "Key3", "Site3", 3, "type3"),
                new(4, "VideoContract4", "vidLang4", "vidRegon4", "Key4", "Site4", 4, "type4"),
                new(5, "VideoContract5", "vidLang5", "vidRegon5", "Key5", "Site5", 5, "type5")
            };

            var casts = new List<CastContract>()
            {
                new(1, 1, "CastChar1", "CastCredit1", 1, "CastName1", 1, "castProfile1"),
                new(2, 2, "CastChar2", "CastCredit2", 2, "CastName2", 2, "castProfile2"),
                new(3, 3, "CastChar", "CastCredit1", 1, "CastName1", 1, "castProfile1"),
                new(4, 4, "CastChar", "CastCredit1", 1, "CastName1", 1, "castProfile1"),
                new(5, 5, "CastChar", "CastCredit1", 1, "CastName1", 1, "castProfile1")
            };

            var movieDetailContractFake = new MovieDetailContract(1, "MovieName", 0.0, genres, "overview", "posterPath", productionCompanies, DateTime.Today, 0.0, 0.0, "Movietitle", 0.0, new AppendVideosContract(videos), new AppendCastsContract(casts));

            var movieDetailContractFakeJson = movieDetailContractFake.ToString();

            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(_movieClientFixture.CinederOptions);

            A.CallTo(() => _movieClientFixture.TestableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(movieDetailContractFakeJson) });

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(_movieClientFixture.CinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_movieClientFixture.TestableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_movieClientFixture.CinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var result = await _movieClientFake.GetMovieByIdAsync(1);

            var expected = movieDetailContractFake.ToMovieDetail();

            // Assert

            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.ProductionCompanies.First().Id, result.ProductionCompanies.First().Id);
            Assert.Equal(expected.ProductionCompanies.ToList()[2].Name, result.ProductionCompanies.ToList()[2].Name);
        }



        [Fact]
        public async Task GetMovieById_InvalidMovieId_ShouldReturnEmptyMovieDetail()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(_movieClientFixture.CinederOptions);

            A.CallTo(() => _movieClientFixture.TestableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(new HttpResponseMessage(HttpStatusCode.BadRequest));

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(_movieClientFixture.CinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_movieClientFixture.TestableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_movieClientFixture.CinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var result = await _movieClientFake.GetMovieByIdAsync(-20);

            // Assert

            Assert.True(result.Id == 0);
            Assert.True(string.IsNullOrWhiteSpace(result.Name));

        }


        [Fact]
        public async Task GetMovieById_NullResponseContent_ShouldReturnEmptyMovieDetail()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(_movieClientFixture.CinederOptions);

            var httpMessageWithNullContent = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = null
            };

            A.CallTo(() => _movieClientFixture.TestableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(httpMessageWithNullContent);

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(_movieClientFixture.CinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_movieClientFixture.TestableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_movieClientFixture.CinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var result = await _movieClientFake.GetMovieByIdAsync(-20);

            // Assert

            Assert.True(result.Id == 0);
            Assert.True(string.IsNullOrWhiteSpace(result.Name));

        }
        #endregion

        #region GetMoviesByTitleAsync Tests
        [Fact]
        public async Task GetMoviesByTitleAsync_NullSearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesByTitleAsync(null);

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        [Fact]
        public async Task GetMoviesByTitleAsync_EmptySearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesByTitleAsync(string.Empty);

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        [Fact]
        public async Task GetMoviesByTitleAsync_BlankSearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesByTitleAsync("     ");

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }
        #endregion

        #region GetMoviesByKeywordsAsync Tests
        [Fact]
        public async Task GetMoviesByKeywordsAsync_NullSearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesByKeywordsAsync(null);

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        [Fact]
        public async Task GetMoviesByKeywordsAsync_EmptySearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesByKeywordsAsync(string.Empty);

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }

        [Fact]
        public async Task GetMoviesByKeywordsAsync_BlankSearchText_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesByKeywordsAsync("     ");

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }
        #endregion

        #region GetMoviesSimilarAsync Tests
        [Fact]
        public async Task GetMoviesSimilarAsync_NegativeMovieId_ShouldReturnEmptySearchResult()
        {
            // Arrange
            A.CallTo(() => _movieClientFixture.CinederOptionsFake.Value).Returns(new());

            A.CallTo(() => _movieClientFixture.HttpClientFactoryFake.CreateClient(A<string>._)).Returns(new());

            // Act
            _movieClientFake = new MovieClient(_movieClientFixture.LoggerFake, _movieClientFixture.HttpClientFactoryFake, _movieClientFixture.CinederOptionsFake);

            var actual = await _movieClientFake.GetMoviesSimilarAsync(-10);

            // Assert
            Assert.True(actual.TotalResults == 0);
            Assert.True(actual.TotalPages == 0);
            Assert.False(actual.Results.Any());
        }
        #endregion
    }
}
