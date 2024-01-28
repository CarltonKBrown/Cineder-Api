using Cineder_Api.Application.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Enums;
using Cineder_Api.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PreventR;

namespace Cineder_Api.Infrastructure.Clients
{
    public class MovieClient : BaseClient, IMovieClient
    {
        private readonly ILogger<MovieClient> _logger;
        public MovieClient(ILogger<MovieClient> logger, IHttpClientFactory httpClientFactory, IOptionsSnapshot<CinederOptions> optionsSnapshot) : base(httpClientFactory, optionsSnapshot)
        {
            logger.Prevent().Null();

            _logger = logger;
        }


        public async Task<MovieDetail> GetMovieByIdAsync(long movieId)
        {
            try
            {
                _logger.LogInformation($"Attempting to get movie by Id: '{movieId}'");

                var url = $"movie/{movieId}?{AddApiKey()}&{AddLang()}&append_to_response=videos,credits";

                _logger.LogInformation($"URL to get movie by Id: {movieId}  - url: '{url}'");

                var response = await SendGetAsync<MovieDetailContract>(url);

                var responseMovieId = response?.Id ?? 0;

                if (responseMovieId < 1 || responseMovieId != movieId)
                {
                    _logger.LogWarning($"Invalid Movie Id parsed from response body: {responseMovieId}");

                    return new();
                }

                var movieDetails = response?.ToMovieDetail();

                _logger.LogInformation($"Successfully retrieved details for movie having Id: '{movieId}'");

                return movieDetails ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get Movie by ID. ID detected: '{movieId}'");

                return new();
            }
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesByTitleAsync(string? searchText, int pageNum = 1)
        {
            try
            {
                var searchQuery = AddQuery(searchText);

                if (string.IsNullOrWhiteSpace(searchQuery)) return new();

                var url = $"search/movie?{searchQuery}&{AddDefaults(pageNum)}";

                var parsedResponse = await ParseSearchResultMovieResponse(url, SearchType.Name);

                return parsedResponse ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get movies by title and/or page requested. Title detected: '{searchText}'; Page detected: '{pageNum}'");

                return new();
            }
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesByKeywordsAsync(string? searchText, int pageNum = 1)
        {
            try
            {
                var keywordIds = await GetKeywordIds(searchText);

                if (!keywordIds.Any()) return new();

                var url = $"discover/movie?{AddWithKeywords(keywordIds)}&{AddDefaults(pageNum)}";

                var parsedResponse = await ParseSearchResultMovieResponse(url, SearchType.Keyword);

                return parsedResponse ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get movies by keywords and/or page requested. keywords detected: '{searchText}'; Page detected: '{pageNum}'");

                return new();
            }
        }


        public async Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(long movieId, int pageNum = 1)
        {
            try
            {
                if (movieId < 1) return new();

                var url = $"movie/{movieId}/recommendations?{AddDefaults(pageNum)}";

                var parsedResponse = await ParseSearchResultMovieResponse(url, SearchType.None);

                return parsedResponse ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to get movies similar to Movie Id and/or page requested. Movie Id detected: '{movieId}'; Page detected: '{pageNum}'");

                return new();
            }
        }


        private async Task<SearchResult<MoviesResult>> ParseSearchResultMovieResponse(string url, SearchType searchType)
        {
            var response = await SendGetAsync<SearchResultContract<MovieResultContract>>(url);

            var hasResults = response?.Results?.Any() ?? false;

            var totalResults = response?.TotalResults ?? 0;

            if (!hasResults || totalResults < 1)
            {
                _logger.LogWarning($"No results returned for request having url: '{url}'");

                return new();
            }

            var parsedResults = response!.Results.Select(x => x.ToMovieResult(searchType));

            var parsedResponse = response.ToSearchResult(parsedResults);

            return parsedResponse ?? new();
        }
    }
}
