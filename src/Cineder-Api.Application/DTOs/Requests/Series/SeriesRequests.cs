namespace Cineder_Api.Core.DTOs.Requests.Series
{
    public record GetSeriesRequest(string SearchText, int PageNum = 1)
    {
        public GetSeriesRequest() : this(string.Empty, 1) { }
    }

    public record GetSeriesByIdRequest(long Id)
    {
        public GetSeriesByIdRequest() : this(0) { }
    }

    public record GetSeriesSimilarRequest(long SeriesId, int PageNum = 1)
    {
        public GetSeriesSimilarRequest() : this(0, 1) { }
    }
}
