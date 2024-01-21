using Cineder_Api.Application.DTOs.Requests.Movies;
using Cineder_Api.Core.Entities;

namespace Cineder_Api.Application.Services.Movies
{
    public interface IMovieService
    {
        Task<SearchResult<MoviesResult>> GetMoviesAsync(GetMoviesRequest request);
        Task<MovieDetail> GetMovieByIdAsync(GetMovieByIdRequest request);
        Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(GetMoviesSimilarRequest request);
    }
}
