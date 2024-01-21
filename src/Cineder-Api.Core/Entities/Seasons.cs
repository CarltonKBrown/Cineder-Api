using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class Seasons : Entity
    {
        public Seasons(long id, string name, DateTime airDate, int episodeCount, string overview, string posterPath, int seasonNumber) : base(id, name)
        {
            AirDate = airDate;
            EpisodeCount = episodeCount;
            Overview = overview.Prevent(nameof(overview)).NullOrWhiteSpace();
            PosterPath = posterPath.Prevent(nameof(posterPath)).NullOrWhiteSpace();
            SeasonNumber = seasonNumber;
        }

        public Seasons() : this(0, string.Empty, DateTime.MinValue, 0, string.Empty, string.Empty, 0) { }

        public DateTime AirDate { get; protected set; }
        public int EpisodeCount { get; protected set; }
        public string Overview { get; protected set; }
        public string PosterPath { get; protected set; }
        public int SeasonNumber { get; protected set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
