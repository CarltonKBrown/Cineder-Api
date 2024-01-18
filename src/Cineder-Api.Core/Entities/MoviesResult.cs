using Cineder_Api.Core.Enums;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class MoviesResult : ListResult, IComparable<MoviesResult>
    {
        public MoviesResult(long id, string name, DateTime releaseDate, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, SearchType searchType) : base(id, name, posterPath, overview, genreIds, voteAverage, idx, searchType)
        {
            ReleaseDate = releaseDate;
        }
        public MoviesResult() : this(0, string.Empty, DateTime.MinValue, string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, SearchType.None) { }

        public DateTime ReleaseDate { get;  set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as MoviesResult);
        }

        public bool Equals(MoviesResult? otherMovieResult)
        {
            return otherMovieResult != null && otherMovieResult.Id == Id;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
        public int CompareTo(MoviesResult? other)
        {
            if (other == null) return 1;

            return Id.CompareTo(other.Id);
        }
    }
}
