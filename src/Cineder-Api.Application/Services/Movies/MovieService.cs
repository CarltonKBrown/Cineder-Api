using Cineder_Api.Application.Clients;
using Cineder_Api.Application.DTOs.Requests.Movies;
using Cineder_Api.Core.Entities;
using Microsoft.Extensions.Logging;

namespace Cineder_Api.Application.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IMovieClient _movieClient;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMovieClient movieClient, ILogger<MovieService> logger)
        {
            _movieClient = movieClient;
            _logger = logger;
        }

        public async Task<MovieDetail> GetMovieByIdAsync(GetMovieByIdRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var result = await _movieClient.GetMovieByIdAsync(request.Id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get movie by Id");

                throw;
            }
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesAsync(GetMoviesRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var titleMovies = await _movieClient.GetMoviesByTitleAsync(request.SearchText, request.PageNum);

                var keywordMovies = await _movieClient.GetMoviesByKeywordsAsync(request.SearchText, request.PageNum);

                var searchResults = new[] { titleMovies, keywordMovies };

                var results = searchResults.Aggregate(new SearchResult<MoviesResult>(), MovieResultAgregator);

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get movies");

                throw;
            }
        }

        public async Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(GetMoviesSimilarRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var result = await _movieClient.GetMoviesSimilarAsync(request.MovieId, request.PageNum);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get similar movies");

                throw;
            }
        }

        private SearchResult<MoviesResult> MovieResultAgregator(SearchResult<MoviesResult> acc, SearchResult<MoviesResult> curr)
        {
            if (acc.Results == null || curr.Results == null) return acc;

            acc.Page = acc.Page >= curr.Page ? acc.Page : curr.Page;

            acc.TotalPages += curr.TotalPages;

            acc.TotalResults += curr.TotalResults;

            acc.Results = acc.Results.Concat(curr.Results).Distinct();

            return acc;
        }
    }
}
