using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Infrastructure.Models;

namespace Cineder_Api.Infrastructure;

internal class LastEpisodeToAirContract : BaseContract
{
    public LastEpisodeToAirContract(long id, string airDate, int episodeNumber, string name, string overview, string productionCode, int seasonNumber, long showId, string stillPath, double voteAverage, int voteCount) : base(id)
    {
        AirDate = airDate;
        EpisodeNumber = episodeNumber;
        Name = name;
        Overview = overview;
        ProductionCode = productionCode;
        SeasonNumber = seasonNumber;
        ShowId = showId;
        StillPath = stillPath;
        VoteAverage = voteAverage;
        VoteCount = voteCount;
    }

    public LastEpisodeToAirContract() : this(0, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, 0.0, 0)
    {

    }

    [JsonPropertyName("air_date")]
    public string AirDate { get; set; }

    [JsonPropertyName("episode_number")]
    public int EpisodeNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("production_code")]
    public string ProductionCode { get; set; }

    [JsonPropertyName("season_number")]
    public int SeasonNumber { get; set; }

    [JsonPropertyName("show_id")]
    public long ShowId { get; set; }

    [JsonPropertyName("still_path")]
    public string StillPath { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
