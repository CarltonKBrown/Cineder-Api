using Cineder_Api.Core.Enums;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{

    public class SeriesResult : ListResult,IComparable<SeriesResult>
    {
        public SeriesResult(long id, string name, DateTime firstAirDate, IEnumerable<string> originCountry, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, SearchType searchType) : base(id, name, posterPath, overview, genreIds, voteAverage, idx, searchType)
        {
            FirstAirDate = firstAirDate;
            OriginCountry = originCountry.Prevent(nameof(originCountry)).Null().Value;
        }

        public SeriesResult() : this(0, string.Empty, DateTime.MinValue, Enumerable.Empty<string>(), string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, SearchType.None) { }

        public DateTime FirstAirDate { get; protected set; }
        public IEnumerable<string> OriginCountry { get; protected set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as SeriesResult);
        }

        public bool Equals(SeriesResult? otherSeriesResult)
        {
            return otherSeriesResult != null && otherSeriesResult.Id == Id;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
        public int CompareTo(SeriesResult? other)
        {
            if (other == null) return 1;

            return Id.CompareTo(other.Id);
        }
    }
}
