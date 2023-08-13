using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure;
using Cineder_Api.Infrastructure.Clients;
using Cineder_Api.Infrastructure.Models;
using Cineder_Api.UnitTests.Util;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Xunit;

namespace Cineder_Api.UnitTests.InfrastructureTests.ClientTests
{
    public class MovieClientTests
    {
        private readonly IOptionsSnapshot<CinederOptions> _cinederOptionsFake;
        private readonly ILogger<MovieClient> _loggerFake;
        private readonly IHttpClientFactory _httpClientFactoryFake;
        private readonly TestableHttpMessageHandler _testableHttpMessageHandlerFake;
        private readonly CinederOptions _cinederOptions;

        private IMovieClient? _movieClientFake;

        public MovieClientTests()
        {
            _cinederOptions = new CinederOptions()
            {
                ApiBaseUrl = "https://api.themoviedb.org/3",
                ApiKey = "1232",
                ClientName = "MovieDb",
                Language = "en",
                MaxPages = 1
            };

            _cinederOptionsFake = A.Fake<IOptionsSnapshot<CinederOptions>>();

            _loggerFake = A.Fake<ILogger<MovieClient>>();

            _testableHttpMessageHandlerFake = A.Fake<TestableHttpMessageHandler>();

            _httpClientFactoryFake = A.Fake<IHttpClientFactory>();
        }

        [Fact]
        public async Task GetMovieByIdAsync_ShouldReturnEmptyMovieDetail()
        {
            // Arrange

            A.CallTo(() => _cinederOptionsFake.Value).Returns(_cinederOptions);

            A.CallTo(() => _testableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(new HttpResponseMessage(HttpStatusCode.NotFound));

            A.CallTo(() => _httpClientFactoryFake.CreateClient(_cinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_testableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_cinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _movieClientFake = new MovieClient(_loggerFake, _httpClientFactoryFake, _cinederOptionsFake);

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

            A.CallTo(() => _cinederOptionsFake.Value).Returns(_cinederOptions);

            A.CallTo(() => _testableHttpMessageHandlerFake.TestSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored)).Returns(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(movieDetailContractFakeJson) });

            A.CallTo(() => _httpClientFactoryFake.CreateClient(_cinederOptionsFake.Value.ClientName)).Returns(new HttpClient(_testableHttpMessageHandlerFake)
            {
                BaseAddress = new Uri(_cinederOptionsFake.Value.ApiBaseUrl)
            });

            // Act

            _movieClientFake = new MovieClient(_loggerFake, _httpClientFactoryFake, _cinederOptionsFake);

            var result = await _movieClientFake.GetMovieByIdAsync(1);

            var expected = movieDetailContractFake.ToMovieDetail();

            // Assert

            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.ProductionCompanies.First().Id, result.ProductionCompanies.First().Id);
            Assert.Equal(expected.ProductionCompanies.ToList()[2].Name, result.ProductionCompanies.ToList()[2].Name);
        }
    }
}
