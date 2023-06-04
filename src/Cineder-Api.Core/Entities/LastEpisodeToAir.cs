using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class LastEpisodeToAir : Entity
    {
        public LastEpisodeToAir(long id, string name, DateTime airDate, int episodeNumber, string overiew, string productionCode, int seasonNumber, long showId, string stillPath, double voteAverage, int voteCount) : base(id, name)
        {
            AirDate = airDate;
            EpisodeNumber = episodeNumber;
            Overiew = overiew;
            ProductionCode = productionCode;
            SeasonNumber = seasonNumber;
            ShowId = showId;
            StillPath = stillPath;
            VoteAverage = voteAverage;
            VoteCount = voteCount;
        }

        public LastEpisodeToAir() : this(0, string.Empty, DateTime.MinValue, 0, string.Empty, string.Empty, 0, 0, string.Empty, 0.0, 0) { }

        public DateTime AirDate { get; protected set; }
        public int EpisodeNumber { get; protected set; }
        public string Overiew { get; protected set; }
        public string ProductionCode { get; protected set; }
        public int SeasonNumber { get; protected set; }
        public long ShowId { get; protected set; }
        public string StillPath { get; protected set; }
        public double VoteAverage { get; protected set; }
        public int VoteCount { get; protected set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
