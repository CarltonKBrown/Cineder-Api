using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using Cineder_Api.Infrastructure.Models;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure;

internal class LastEpisodeToAirContract : BaseContract
{
    public LastEpisodeToAirContract(long id, string airDate, int episodeNumber, string name, string overview, string productionCode, int seasonNumber, long showId, string stillPath, double voteAverage, int voteCount) : base(id)
    {
        AirDate = airDate.Prevent((nameof(airDate))).Null();
        EpisodeNumber = episodeNumber;
        Name = name.Prevent((nameof(name))).Null();
        Overview = overview.Prevent((nameof(overview))).Null();
        ProductionCode = productionCode.Prevent((nameof(productionCode))).Null();
        SeasonNumber = seasonNumber;
        ShowId = showId;
        StillPath = stillPath.Prevent((nameof(stillPath))).Null();
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

    public LastEpisodeToAir ToLastEpisodeToAir()
    {
        _ = DateTime.TryParse(AirDate, out DateTime airDate);

        return new(Id, Name, airDate, EpisodeNumber, Overview, ProductionCode, SeasonNumber, ShowId, StillPath, VoteAverage, VoteCount);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
