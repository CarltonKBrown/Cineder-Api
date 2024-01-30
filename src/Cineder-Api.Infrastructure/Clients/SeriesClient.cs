using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Enums;
using Cineder_Api.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PreventR;

namespace Cineder_Api.Infrastructure.Clients
{
    public class SeriesClient : BaseClient, ISeriesClient
    {
        private readonly ILogger<SeriesClient> _logger;

        public SeriesClient(ILogger<SeriesClient> logger, IHttpClientFactory httpClientFactory, IOptionsSnapshot<CinederOptions> optionsSnapshot) : base(httpClientFactory, optionsSnapshot)
        {
            logger.Prevent().Null();

            _logger = logger;
        }

        public async Task<SeriesDetail> GetSeriesByIdAsync(long seriesId)
        {
            try
            {
                _logger.LogInformation($"Attempting to get series by Id: '{seriesId}'");

                if (seriesId < 1) return new();

                var url = $"tv/{seriesId}?{AddApiKey()}&{AddLang()}&append_to_response=videos,credits";

                _logger.LogInformation($"URL to get series by Id: {seriesId}  - url: '{url}'");

                var jsonStringResponse = await SendGetAsync(url);

                if(!TryParse<SeriesDetailContract>(jsonStringResponse, out var seriesDetailContract))
                {
                    _logger.LogError("Could not parse results!");

                    return new();
                }

                var responseSeriesId = seriesDetailContract?.Id ?? 0;

                if (responseSeriesId < 1 || responseSeriesId != seriesId)
                {
                    _logger.LogWarning($"Invalid Series Id parsed from response body: {responseSeriesId}");

                    return new();
                }

                var seriesDetail = seriesDetailContract?.ToSeriesDetail();

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
            try
            {
                var searchQuery = AddQuery(searchText);

                if (string.IsNullOrWhiteSpace(searchQuery)) return new();

                var url = $"search/tv?{searchQuery}&{AddDefaults(pageNum)}";

                var parsedResponse = await ParseSearchResultSeriesResponse(url, SearchType.Name);

                return parsedResponse ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get series by title and/or page requested. Title detected: '{searchText}'; Page detected: '{pageNum}'");

                return new();
            }
        }

        public async Task<SearchResult<SeriesResult>> GetSeriesKeywordsAsync(string? searchText, int pageNum = 1)
        {
            try
            {
                var keywordIds = await GetKeywordIds(searchText);

                if (!keywordIds.Any()) return new();

                var url = $"discover/tv?{AddWithKeywords(keywordIds)}&{AddDefaults(pageNum)}";

                var parsedResponse = await ParseSearchResultSeriesResponse(url, SearchType.Keyword);

                return parsedResponse ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get series by keywords and/or page requested. keywords detected: '{searchText}'; Page detected: '{pageNum}'");

                return new();
            }
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
            var jsonStringResponse = await SendGetAsync(url);

            if (!TryParse<SearchResultContract<SeriesResultContract>>(jsonStringResponse, out var seriesDetailContract))
            {
                _logger.LogError("Could not parse results!");

                return new();
            }

            var hasResults = seriesDetailContract?.Results?.Any() ?? false;

            var totalResults = seriesDetailContract?.TotalResults ?? 0;

            if (!hasResults || totalResults < 1)
            {
                _logger.LogWarning($"No results returned for request having url: '{url}'");

                return new();
            }

            var parsedResults = seriesDetailContract!.Results.Select(x => x.ToSeriesResult(searchType));

            var parsedResponse = seriesDetailContract.ToSearchResult(parsedResults);

            return parsedResponse ?? new();
        }
    }
}
