namespace Cineder_Api.Application.DTOs.Requests.Movies
{
    public record GetMoviesRequest(string SearchText, int PageNum = 1)
    {
        public GetMoviesRequest() : this(string.Empty, 1) { }
    }

    public record GetMovieByIdRequest(long Id)
    {
        public GetMovieByIdRequest() : this(0) { }
    }

    public record GetMoviesSimilarRequest(long MovieId, int PageNum = 1)
    {
        public GetMoviesSimilarRequest() : this(0, 1) { }
    }
}
