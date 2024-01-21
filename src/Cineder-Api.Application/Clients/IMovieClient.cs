using Cineder_Api.Core.Entities;

namespace Cineder_Api.Application.Clients
{
    public interface IMovieClient
    {
        /// <summary>
        /// Fetches a list of Movies that contain elements of the 'searchText' in thier titles
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageNum"></param>
        /// <returns>The list of movies on a given page</returns>
        Task<SearchResult<MoviesResult>> GetMoviesByTitleAsync(string? searchText, int pageNum = 1);

        /// <summary>
        /// Fetches a list of Movies based on keywords releated to elements of the 'searchText'
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageNum"></param>
        /// <returns>The list of movies on a given page</returns>
        Task<SearchResult<MoviesResult>> GetMoviesByKeywordsAsync(string? searchText, int pageNum = 1);

        /// <summary>
        /// Retrieve details for a particular Movie using its unqiue 'movieId'
        /// </summary>
        /// <param name="movieId">
        /// Unique identifier for a movie
        /// </param>
        /// <returns>The details of the particular movie</returns>
        Task<MovieDetail> GetMovieByIdAsync(long movieId);

        /// <summary>
        /// Fetches a list of Movies similar to a particular movie given it unique 'movieId'
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="pageNum"></param>
        /// <returns>The list of similar movies on a given page</returns>
        Task<SearchResult<MoviesResult>> GetMoviesSimilarAsync(long movieId, int pageNum = 1);
    }
}
