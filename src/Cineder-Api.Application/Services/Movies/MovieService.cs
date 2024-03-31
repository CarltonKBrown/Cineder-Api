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

                var movieSearchRequests = new[]
                {
                    _movieClient.GetMoviesByTitleAsync(request.SearchText, request.PageNum),
                    _movieClient.GetMoviesByKeywordsAsync(request.SearchText, request.PageNum)
                };

                await Task.WhenAll(movieSearchRequests);

                var searchResults = new List<SearchResult<MoviesResult>>();

                foreach (var searchRequest in movieSearchRequests)
                {
                    var searchResult = await searchRequest;

                    searchResults.Add(searchResult);
                }

                var results = searchResults.Aggregate(new SearchResult<MoviesResult>(), SearchResult<MoviesResult>.SearchResultAgregator);

				results.Results = results.Results.Select((x, i) => { x.Idx = i; return x; });

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
    }
}
