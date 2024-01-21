using Cineder_Api.Application.Services.Series;
using Cineder_Api.Core.Clients;
using Cineder_Api.Core.DTOs.Requests.Series;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Enums;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Cineder_Api.UnitTests.ApplicationTests.Services
{
    public class SeriesServiceTests
    {
        private ISeriesService _seriesService;
        private ISeriesClient _seriesClientfake;
        private ILogger<SeriesService> _loggerFake;

        public SeriesServiceTests()
        {
            _loggerFake = A.Fake<ILogger<SeriesService>>();
            _seriesClientfake = A.Fake<ISeriesClient>();
            _seriesService = new SeriesService(_seriesClientfake, _loggerFake);
        }

        [Fact]
        public async Task GetSeriesAsync_ResultWithDuplicates_ShouldBeMergedAndFiltered()
        {
            var seriesNameResult1 = new SeriesResult(1, "Jane Doe", DateTime.Today, Enumerable.Empty<string>(), string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, Core.Enums.SearchType.Name);

            var seriesNameResult2 = new SeriesResult(2, "Jane Doe2", DateTime.Today, Enumerable.Empty<string>(), string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, Core.Enums.SearchType.Name);

            var seriesNameResults = new[] { seriesNameResult1, seriesNameResult2 };

            var seriesNameSerachResults = new SearchResult<SeriesResult>(1, seriesNameResults, 2, 1);

            var seriesKeywordResult1 = new SeriesResult(1, "Jane Doe", DateTime.Today, Enumerable.Empty<string>(), string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, Core.Enums.SearchType.Keyword);

            var seriesKeywordResult2 = new SeriesResult(3, "Jane Doe3", DateTime.Today, Enumerable.Empty<string>(), string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, Core.Enums.SearchType.Keyword);

            var seriesKeywordResults = new[] { seriesKeywordResult1, seriesKeywordResult2 };

            var seriesKeywordSerachResults = new SearchResult<SeriesResult>(1, seriesKeywordResults, 2, 1);

            A.CallTo(() => _seriesClientfake.GetSeriesByTitleAsync(A<string>._, A<int>._)).Returns(seriesNameSerachResults);

            A.CallTo(() => _seriesClientfake.GetSeriesKeywordsAsync(A<string>._, A<int>._)).Returns(seriesKeywordSerachResults);

            var seriesRequest = new GetSeriesRequest(string.Empty, 1);

            var actual = await _seriesService.GetSeriesAsync(seriesRequest);

            Assert.True(actual.Results.Count() == 3);
            Assert.True(actual.Results.Count(x => x.SearchType.Equals(SearchType.Name)) == 2);
            Assert.True(actual.Results.Count(x => x.SearchType.Equals(SearchType.Keyword)) == 1);

        }
    }
}
