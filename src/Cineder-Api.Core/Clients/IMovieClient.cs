using Cineder_Api.Core.Entities;

namespace Cineder_Api.Core.Clients
{
    public interface IMovieClient
    {
        Task<SearchResult<MoviesResult>> GetMoviesAsync(string searchText, int pageNum = 1);
        Task<MovieDetail> GetMovieByIdAsync(long movieId);
        Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(long movieId, int pageNum = 1);
    }
}
