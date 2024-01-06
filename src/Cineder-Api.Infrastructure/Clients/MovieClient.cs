using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cineder_Api.Infrastructure.Clients
{
    public class MovieClient : BaseClient, IMovieClient
    {
        private readonly ILogger<MovieClient> _logger;
        public MovieClient(ILogger<MovieClient> logger, IHttpClientFactory httpClientFactory, IOptionsSnapshot<CinederOptions> optionsSnapshot) : base(httpClientFactory, optionsSnapshot)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<MovieDetail> GetMovieByIdAsync(long movieId)
        {
            try
            {
                _logger.LogInformation($"Attempting to get movie by Id: '{movieId}'");

                var url = $"/movie/{movieId}?{AddApiKey}&{AddLang}";

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

        public async Task<SearchResult<MoviesResult>> GetMoviesByTitleAsync(string searchText, int pageNum = 1)
        {
            var searchQuery = AddQuery(searchText);

            if (string.IsNullOrWhiteSpace(searchQuery)) return new();

            var url = $"/search/movie?{searchQuery}&{AddDefaults(pageNum)}";

            var parsedResponse = await ParseSearchResultMovieResponse(url, MovieRelevance.Name);

            return parsedResponse ?? new();
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesByKeywordsAsync(string searchText, int pageNum = 1)
        {
            var keywordIds = await GetKeywordIds(searchText);

            if (!keywordIds.Any()) return new();

            var url = $"/discover/movie?{AddWithKeywords(keywordIds)}&{AddDefaults(pageNum)}";

            var parsedResponse = await ParseSearchResultMovieResponse(url, MovieRelevance.Type);

            return parsedResponse ?? new();
        }


        public async Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(long movieId, int pageNum = 1)
        {
            if (movieId < 1) return new();

            var url = $"/movie/{movieId}/recommendations?{AddPage(pageNum)}";

            var parsedResponse = await ParseSearchResultMovieResponse(url, MovieRelevance.None);

            return parsedResponse ?? new();
        }


        private async Task<SearchResult<MoviesResult>> ParseSearchResultMovieResponse(string url, MovieRelevance movieRelevance)
        {
            var response = await SendGetAsync<SearchResultContract<MovieResultContract>>(url);

            var hasResults = response?.Results?.Any() ?? false;

            var totalResults = response?.TotalResults ?? 0;

            if (!hasResults || totalResults < 1)
            {
                _logger.LogWarning($"No results returned for request having url: '{url}'");

                return new();
            }

            var parsedResults = response!.Results.Select(x => x.ToMovieResult(movieRelevance));

            var parsedResponse = response.ToSearchResult(parsedResults);

            return parsedResponse ?? new();
        }
    }
}
