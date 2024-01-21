using System.Text.Json;
using System.Text.Json.Serialization;
using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Enums;
using Cineder_Api.Core.Util;
using Cineder_Api.Infrastructure.Models;
using PreventR;

namespace Cineder_Api.Infrastructure;

internal class MovieResultContract : BaseContract
{
    public MovieResultContract(long id, string posterPath, bool adult, string overview, string releaseDate, IEnumerable<long> genreIds, string originalTitle, string originalLanguage, string title, string backdropPath, double popularity, int voteCount, bool video, double voteAverage) : base(id)
    {
        PosterPath = posterPath.Prevent(nameof(posterPath)).NullOrWhiteSpace();
        Adult = adult;
        Overview = overview.Prevent(nameof(overview)).NullOrWhiteSpace();
        ReleaseDate = releaseDate.Prevent(nameof(releaseDate)).NullOrWhiteSpace();
        GenreIds = genreIds.Prevent(nameof(genreIds)).Null().Value;
        OriginalTitle = originalTitle.Prevent(nameof(originalTitle)).NullOrWhiteSpace();
        OriginalLanguage = originalLanguage.Prevent(nameof(originalLanguage)).NullOrWhiteSpace();
        Title = title.Prevent(nameof(title)).NullOrWhiteSpace();
        BackdropPath = backdropPath.Prevent(nameof(backdropPath)).NullOrWhiteSpace();
        Popularity = popularity;
        VoteCount = voteCount;
        Video = video;
        VoteAverage = voteAverage;
    }

    public MovieResultContract() : this(0, string.Empty, false, string.Empty, string.Empty, Enumerable.Empty<long>(), string.Empty, string.Empty, string.Empty, string.Empty, 0.0, 0, false, 0.0) { }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [JsonPropertyName("adult")]
    public bool Adult { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("genre_ids")]
    public IEnumerable<long> GenreIds { get; set; }

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; }

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; set; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }

    [JsonPropertyName("video")]
    public bool Video { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }

    public MoviesResult ToMovieResult(SearchType searchType)
    {
        if (!DateTime.TryParse(ReleaseDate, out DateTime releaseDate))
        {
            throw new InvalidCastException("Could not parse movie result release date to DateTime.");
        }

        return new(Id, Title, releaseDate, PosterPath, Overview, GenreIds, VoteAverage, 0, searchType);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
