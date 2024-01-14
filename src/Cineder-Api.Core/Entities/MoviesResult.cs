using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public enum MovieRelevance
    {
        None,
        Type,
        Name
    }

    public class MoviesResult : ListResult
    {
        public MoviesResult(long id, string name, DateTime releaseDate, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, string relevance) : base(id, name, posterPath, overview, genreIds, voteAverage, idx, relevance)
        {
            ReleaseDate = releaseDate;
        }
        public MoviesResult() : this(0, string.Empty, DateTime.MinValue, string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, string.Empty) { }

        public DateTime ReleaseDate { get;  set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
