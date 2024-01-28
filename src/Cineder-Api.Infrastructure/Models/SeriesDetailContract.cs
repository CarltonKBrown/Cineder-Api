using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    internal class SeriesDetailContract : BaseContract
    {
        public SeriesDetailContract(long id, string backdropPath, IEnumerable<CreatedByContract> createdBy, IEnumerable<int> runtime, string firstAirDate, IEnumerable<GenreContract> genres, string homepage, bool inProduction, IEnumerable<string> languages, string lastAirDate, LastEpisodeToAirContract lastEpisodeToAir, string name, object nextEpisodeToAir, IEnumerable<NetworksContract> networks, int numberOfEpisodes, int numberOfSeasons, IEnumerable<string> originCountry, string originalLanguage, string originalName, string overview, float popularity, string posterPath, IEnumerable<ProductionCompanyContract> productionCompanies, IEnumerable<ProductionCountriesContract> productionCountries, IEnumerable<SeasonsContract> seasons, IEnumerable<SpokenLanguagesContract> spokenLanguages, string status, string tagline, string type, double voteAverage, int voteCount, AppendCreditsContract credits, AppendVideosContract videos) : base(id)
        {
            BackdropPath = backdropPath.Prevent(nameof(backdropPath)).Null();
            CreatedBy = createdBy.Prevent(nameof(createdBy)).Null().Value;
            Runtime = runtime.Prevent(nameof(runtime)).Null().Value;
            FirstAirDate = firstAirDate.Prevent(nameof(firstAirDate)).Null();
            Genres = genres.Prevent(nameof(genres)).Null().Value;
            Homepage = homepage.Prevent(nameof(homepage)).Null();
            InProduction = inProduction;
            Languages = languages.Prevent(nameof(languages)).Null().Value;
            LastAirDate = lastAirDate.Prevent(nameof(lastAirDate)).Null();
            LastEpisodeToAir = lastEpisodeToAir ?? new();
            Name = name.Prevent(nameof(name)).Null();
            NextEpisodeToAir = nextEpisodeToAir ?? new();
            Networks = networks.Prevent(nameof(networks)).Null().Value;
            NumberOfEpisodes = numberOfEpisodes;
            NumberOfSeasons = numberOfSeasons;
            OriginCountry = originCountry.Prevent(nameof(originCountry)).Null().Value;
            OriginalLanguage = originalLanguage.Prevent(nameof(originalLanguage)).Null();
            OriginalName = originalName.Prevent(nameof(originalName)).Null();
            Overview = overview.Prevent(nameof(overview)).Null();
            Popularity = popularity;
            PosterPath = posterPath.Prevent(nameof(posterPath)).Null();
            ProductionCompanies = productionCompanies.Prevent(nameof(productionCompanies)).Null().Value;
            ProductionCountries = productionCountries.Prevent(nameof(productionCountries)).Null().Value;
            Seasons = seasons.Prevent(nameof(seasons)).Null().Value;
            SpokenLanguages = spokenLanguages.Prevent(nameof(spokenLanguages)).Null().Value;
            Status = status.Prevent(nameof(status)).Null();
            Tagline = tagline.Prevent(nameof(tagline)).Null();
            Type = type.Prevent(nameof(type)).Null();
            VoteAverage = voteAverage;
            VoteCount = voteCount;
            Credits = credits ?? new();
            Videos = videos ?? new();
        }

        public SeriesDetailContract() : this(default!, string.Empty, Enumerable.Empty<CreatedByContract>(), Enumerable.Empty<int>(), string.Empty, Enumerable.Empty<GenreContract>(), string.Empty, false, Enumerable.Empty<string>(), string.Empty, default!, string.Empty, default!, Enumerable.Empty<NetworksContract>(), 0, 0, Enumerable.Empty<string>(), string.Empty, string.Empty, string.Empty, default!, string.Empty, Enumerable.Empty<ProductionCompanyContract>(), Enumerable.Empty<ProductionCountriesContract>(), Enumerable.Empty<SeasonsContract>(), Enumerable.Empty<SpokenLanguagesContract>(), string.Empty, string.Empty, string.Empty, default!, default!, default!, default!)
        {

        }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("created_by")]
        public IEnumerable<CreatedByContract> CreatedBy { get; set; }

        [JsonPropertyName("episode_run_time")]
        public IEnumerable<int> Runtime { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("genres")]
        public IEnumerable<GenreContract> Genres { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("in_production")]
        public bool InProduction { get; set; }

        [JsonPropertyName("languages")]
        public IEnumerable<string> Languages { get; set; }

        [JsonPropertyName("last_air_date")]
        public string LastAirDate { get; set; }

        [JsonPropertyName("last_episode_to_air")]
        public LastEpisodeToAirContract LastEpisodeToAir { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("next_episode_to_air")]
        public object NextEpisodeToAir { get; set; }

        [JsonPropertyName("networks")]
        public IEnumerable<NetworksContract> Networks { get; set; }

        [JsonPropertyName("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }

        [JsonPropertyName("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        [JsonPropertyName("origin_country")]
        public IEnumerable<string> OriginCountry { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("popularity")]
        public float Popularity { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("production_companies")]
        public IEnumerable<ProductionCompanyContract> ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public IEnumerable<ProductionCountriesContract> ProductionCountries { get; set; }

        [JsonPropertyName("seasons")]
        public IEnumerable<SeasonsContract> Seasons { get; set; }

        [JsonPropertyName("spoken_languages")]
        public IEnumerable<SpokenLanguagesContract> SpokenLanguages { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("credits")]
        public AppendCreditsContract Credits { get; set; }

        [JsonPropertyName("videos")]
        public AppendVideosContract Videos { get; set; }

        public SeriesDetail ToSeriesDetail()
        {
            var createdBy = CreatedBy.Select(x => x.ToCreatedBy()) ?? Enumerable.Empty<CreatedBy>();

            var genre = Genres.Select(x => x.ToGenre());


            _ = DateTime.TryParse(FirstAirDate, out DateTime firstAirDate);

            _ = DateTime.TryParse(LastAirDate, out DateTime lastAirDate);

            var lastEpisodeToAir = LastEpisodeToAir.ToLastEpisodeToAir();

            var networks = Networks.Select(x => x.ToNetwork());

            var productionCompanies = ProductionCompanies.Select(x => x.ToProductionCompany());

            var seasons = Seasons.Select(x => x.ToSeasons());

            var casts = Credits.Cast.Select(x => x.ToCast());

            var videos = Videos.Results.Select(x => x.ToVideo());

            return new(Id, Name, createdBy, Runtime, firstAirDate, genre, InProduction, Languages, lastAirDate, lastEpisodeToAir, NextEpisodeToAir, networks, NumberOfEpisodes, NumberOfSeasons, OriginCountry, OriginalLanguage, OriginalName, Overview, Popularity, PosterPath, productionCompanies, seasons, Status, VoteAverage, casts, videos);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
