using Cineder_Api.Application.Clients;
using Cineder_Api.Application.DTOs.Requests.Movies;
using Cineder_Api.Core.Entities;
using Microsoft.Extensions.Logging;
using PreventR;

namespace Cineder_Api.Application.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IMovieClient _movieClient;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMovieClient movieClient, ILogger<MovieService> logger)
        {
            _movieClient = movieClient.Prevent(nameof(movieClient)).Null().Value;
            _logger = logger.Prevent(nameof(logger)).Null().Value;
        }

        public async Task<MovieDetail> GetMovieByIdAsync(GetMovieByIdRequest request)
        {
            try
            {
                request.Prevent().Null();

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
                request.Prevent().Null();

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
                request.Prevent().Null();

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

            acc.Results = acc.Results.Concat(curr.Results).Distinct();

            acc.TotalResults = acc.Results.Count();

            return acc;
        }
    }
}
