using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class SeriesResult : ListResult
    {
        public SeriesResult(long id, string name, DateTime firstAirDate, IEnumerable<string> originCountry, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, string relevance) : base(id, name, posterPath, overview, genreIds, voteAverage, idx, relevance)
        {
            FirstAirDate = firstAirDate;
            OriginCountry = originCountry;
        }

        public SeriesResult() : this(0, string.Empty, DateTime.MinValue, Enumerable.Empty<string>(), string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, string.Empty) { }

        public DateTime FirstAirDate { get; protected set; }
        public IEnumerable<string> OriginCountry { get; protected set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
