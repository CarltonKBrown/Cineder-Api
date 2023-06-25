using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Infrastructure.Models;

namespace Cineder_Api.Infrastructure;

internal class SeasonsContract : BaseContract
{
    public SeasonsContract(long id, string airDate, int episodeCount, string name, string overview, string posterPath, int seasonNumber) : base(id)
    {
        AirDate = airDate;
        EpisodeCount = episodeCount;
        Name = name;
        Overview = overview;
        PosterPath = posterPath;
        SeasonNumber = seasonNumber;
    }

    public SeasonsContract() : this(0, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0)
    {

    }

    [JsonPropertyName("air_date")]
    public string AirDate { get; set; }

    [JsonPropertyName("episode_count")]
    public int EpisodeCount { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [JsonPropertyName("season_number")]
    public int SeasonNumber { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
