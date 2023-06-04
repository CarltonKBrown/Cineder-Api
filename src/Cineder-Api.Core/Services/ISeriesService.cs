using Cineder_Api.Core.DTOs.Requests.Series;
using Cineder_Api.Core.Entities;

namespace Cineder_Api.Core.Services
{
    public interface ISeriesService
    {
        Task<SearchResult<SeriesResult>> GetSeriesAsync(GetSeriesRequest request);
        Task<SeriesDetail> GetSeriesByIdAsync(GetSeriesByIdRequest request);
        Task<SearchResult<SeriesResult>> GetSeriesSimilarAsync(GetSeriesSimilarRequest request);
    }
}
