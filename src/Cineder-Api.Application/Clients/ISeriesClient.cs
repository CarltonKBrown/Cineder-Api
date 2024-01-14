using Cineder_Api.Core.Entities;

namespace Cineder_Api.Core.Clients
{
    public interface ISeriesClient
    {
        /// <summary>
        /// Fetches a list of Series that contain elements of the 'searchText' in thier titles
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageNum"></param>
        /// <returns>The list of series on a given page</returns>
        Task<SearchResult<SeriesResult>> GetSeriesByTitleAsync(string? searchText, int pageNum = 1);

        /// <summary>
        /// Fetches a list of Series based on keywords releated to elements of the 'searchText'
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageNum"></param>
        /// <returns>The list of series on a given page</returns>
        Task<SearchResult<SeriesResult>> GetSeriesKeywordsAsync(string? searchText, int pageNum = 1);

        /// <summary>
        /// Retrieve details for a particular Series using its unqiue 'seriesId'
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns>The details of the particular series</returns>
        Task<SeriesDetail> GetSeriesByIdAsync(long seriesId);

        /// <summary>
        /// Fetches a list of Series similar to a particular series given it unique 'seriesId'
        /// </summary>
        /// <param name="seriesId"></param>
        /// <param name="pageNum"></param>
        /// <returns>The list of similar series on a given page</returns>
        Task<SearchResult<SeriesResult>> GetSeriesSimilarAsync(long seriesId, int pageNum = 1);
    }
}
