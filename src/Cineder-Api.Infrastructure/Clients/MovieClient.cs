using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Cineder_Api.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cineder_Api.Infrastructure.Clients
{
    internal class MovieClient : BaseClient, IMovieClient
    {
        private readonly ILogger<MovieClient> _logger;
        public MovieClient(ILogger<MovieClient> logger, IHttpClientFactory httpClientFactory, IOptionsSnapshot<CinederOptions> optionsSnapshot) : base(httpClientFactory, optionsSnapshot)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        
        public async Task<MovieDetail> GetMovieByIdAsync(long movieId)
        {
            _logger.LogInformation($"Attempting to get movie by Id: '{movieId}'");

            var url = $"/movie/{movieId}?{AddApiKey}&{AddLang}";

            _logger.LogInformation($"URL to get movie by Id: {movieId}  - url: '{url}'");

            var client = _httpClientFactory.CreateClient(_options.ClientName);

            if (client == null)
            {
                _logger.LogWarning("Unable to create http client at this time. Returning empty movie details");

                return new();
            }

            var response = await client.GetFromJsonAsync<MovieDetailContract>(url);

            var movieDetails = response?.ToMovieDetail();

            _logger.LogInformation($"Successfully retrieved details for movie having Id: '{movieId}'");

            return movieDetails ??= new();
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesByTitleAsync(string searchText, int pageNum = 1)
        {
            var url = $"/search/movie?{AddQuery(searchText)}&{AddDefaults(pageNum)}";

            var parsedResponse = await ParseSearchResultMovieResponse(url, MovieRelevance.Name);

            return parsedResponse ?? new();
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesByKeywordsAsync(string searchText, int pageNum = 1)
        {
            var keywordIds = await GetKeywordIds(searchText);

            var url = $"/discover/movie?{AddWithKeywords(keywordIds)}&{AddDefaults(pageNum)}";

            var parsedResponse = await ParseSearchResultMovieResponse(url, MovieRelevance.Type);

            return parsedResponse ?? new();
        }


        public async Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(long movieId, int pageNum = 1)
        {
            var url = $"/movie/{movieId}/recommendations?{AddPage(pageNum)}";

            var parsedResponse = await ParseSearchResultMovieResponse(url, MovieRelevance.None);

            return parsedResponse ?? new();
        }


        private async Task<SearchResult<MoviesResult>> ParseSearchResultMovieResponse(string url, MovieRelevance movieRelevance)
        {
            var client = _httpClientFactory.CreateClient(_options.ClientName);

            if (client == null) return new();

            var response = await client.GetFromJsonAsync<SearchResultContract<MovieResultContract>>(url);

            if (!(response?.Results?.Any() ?? false)) return new();

            var parsedResults = response.Results.Select(x => x.ToMovieResult(movieRelevance));

            var parsedResponse = response.ToSearchResult(parsedResults);

            return parsedResponse ?? new();
        }
    }
}
