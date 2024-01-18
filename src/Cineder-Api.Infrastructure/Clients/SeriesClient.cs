using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Enums;
using Cineder_Api.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cineder_Api.Infrastructure.Clients
{
    public class SeriesClient : BaseClient, ISeriesClient
    {
        private readonly ILogger<SeriesClient> _logger;

        public SeriesClient(ILogger<SeriesClient> logger, IHttpClientFactory httpClientFactory, IOptionsSnapshot<CinederOptions> optionsSnapshot) : base(httpClientFactory, optionsSnapshot)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SeriesDetail> GetSeriesByIdAsync(long seriesId)
        {
            try
            {
                _logger.LogInformation($"Attempting to get series by Id: '{seriesId}'");

                if (seriesId < 1) return new();

                var url = $"/tv/{seriesId}?{AddApiKey}&{AddLang}&append_to_response=videos,credits";

                _logger.LogInformation($"URL to get series by Id: {seriesId}  - url: '{url}'");

                var response = await SendGetAsync<SeriesDetailContract>(url);

                var responseSeriesId = response?.Id ?? 0;

                if (responseSeriesId < 1 || responseSeriesId != seriesId)
                {
                    _logger.LogWarning($"Invalid Series Id parsed from response body: {responseSeriesId}");

                    return new();
                }

                var seriesDetail = response?.ToSeriesDetail();

                _logger.LogInformation($"Successfully retrieved details for series having Id: '{seriesId}'");

                return seriesDetail ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get Series by ID. ID detected: '{seriesId}'");

                return new();
            }
        }

        public async Task<SearchResult<SeriesResult>> GetSeriesByTitleAsync(string? searchText, int pageNum = 1)
        {
            var searchQuery = AddQuery(searchText);

            if (string.IsNullOrWhiteSpace(searchQuery)) return new();

            var url = $"/search/tv?{searchQuery}&{AddDefaults(pageNum)}";

            var parsedResponse = await ParseSearchResultSeriesResponse(url, SearchType.Name);

            return parsedResponse ?? new();
        }

        public async Task<SearchResult<SeriesResult>> GetSeriesKeywordsAsync(string? searchText, int pageNum = 1)
        {
            var keywordIds = await GetKeywordIds(searchText);

            if (!keywordIds.Any()) return new();

            var url = $"/discover/tv?{AddWithKeywords(keywordIds)}&{AddDefaults(pageNum)}";

            var parsedResponse = await ParseSearchResultSeriesResponse(url, SearchType.Keyword);

            return parsedResponse ?? new();
        }

        public async Task<SearchResult<SeriesResult>> GetSeriesSimilarAsync(long seriesId, int pageNum = 1)
        {
            if (seriesId < 1) return new();

            var url = $"/tv/{seriesId}/recommendations?{AddPage(pageNum)}";

            var parsedResponse = await ParseSearchResultSeriesResponse(url, SearchType.None);

            return parsedResponse ?? new();
        }

        private async Task<SearchResult<SeriesResult>> ParseSearchResultSeriesResponse(string url, SearchType searchType)
        {
            var response = await SendGetAsync<SearchResultContract<SeriesResultContract>>(url);

            var hasResults = response?.Results?.Any() ?? false;

            var totalResults = response?.TotalResults ?? 0;

            if (!hasResults || totalResults < 1)
            {
                _logger.LogWarning($"No results returned for request having url: '{url}'");

                return new();
            }

            var parsedResults = response!.Results.Select(x => x.ToSeriesResult(searchType));

            var parsedResponse = response.ToSearchResult(parsedResults);

            return parsedResponse ?? new();
        }
    }
}
