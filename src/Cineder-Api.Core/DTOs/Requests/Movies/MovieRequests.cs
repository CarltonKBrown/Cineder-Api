namespace Cineder_Api.Core.DTOs.Requests.Movies
{
    public record GetMoviesRequest(string SearchText, int PageNum = 1)
    {
        public GetMoviesRequest() : this(string.Empty, 1) { }
    }

    public record GetMovieByIdRequest(long Id)
    {
        public GetMovieByIdRequest() : this(0) { }
    }

    public record GetMoviesSimilarRequest(string SearchText, int PageNum = 1)
    {
        public GetMoviesSimilarRequest() : this(string.Empty, 1) { }
    }
}
