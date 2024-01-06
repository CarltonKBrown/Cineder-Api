using Cineder_Api.Core.Entities;

namespace Cineder_Api.Core.Clients
{
    public interface ISeriesClient
    {
        Task<SearchResult<SeriesResult>> GetSeriesAsync(string searchText, int pageNum = 1);
        Task<SeriesDetail> GetSeriesByIdAsync(long movieId);
        Task<SearchResult<SeriesResult>> GetSeriesSimilarAsync(long movieId, int pageNum = 1);
    }
}
