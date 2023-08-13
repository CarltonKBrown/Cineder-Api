using Cineder_Api.Core.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class MovieDetailContract : BaseContract
    {
        public MovieDetailContract(long id, string name, double budget, IEnumerable<GenreContract> genres, string overview, string posterPath, IEnumerable<ProductionCompanyContract> productionCompanies, DateTime releaseDate, double revenue, double runtime, string title, double voteAverage, AppendVideosContract videos, AppendCastsContract casts):base(id)
        {
            Budget = budget;
            Name = name;
            Genres = genres;
            Overview = overview;
            PosterPath = posterPath;
            ProductionCompanies = productionCompanies;
            ReleaseDate = releaseDate;
            Revenue = revenue;
            Runtime = runtime;
            Title = title;
            VoteAverage = voteAverage;
            Videos = videos;
            Casts = casts;
        }

        public MovieDetailContract() : this(default, string.Empty, default!, Enumerable.Empty<GenreContract>(), string.Empty, string.Empty, Enumerable.Empty<ProductionCompanyContract>(), default!, default!, default!, string.Empty, default!, default!, default!)
        {

        }

        [JsonPropertyName("budget")]
        public double Budget { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("genres")]
        public IEnumerable<GenreContract> Genres { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("production_companies")]
        public IEnumerable<ProductionCompanyContract> ProductionCompanies { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("revenue")]
        public double Revenue { get; set; }

        [JsonPropertyName("runtime")]
        public double Runtime { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("videos")]
        public AppendVideosContract Videos { get; set; }

        [JsonPropertyName("credits")]
        public AppendCastsContract Casts { get; set; }

        public MovieDetail ToMovieDetail()
        {
            var genres = Genres?.Select(x => x.ToGenre()) ?? Enumerable.Empty<Genre>();

            var productionCompanies = ProductionCompanies?.Select(x => x.ToProductionCompany()) ?? Enumerable.Empty<ProductionCompany>();

            var videos = Videos?.Results?.Select(x => x.ToVideo()) ?? Enumerable.Empty<Video>();

            var casts = Casts?.Cast?.Select(x => x.ToCast()) ?? Enumerable.Empty<Cast>();

            return new(Id, Name, Budget, genres, Overview, PosterPath, productionCompanies, ReleaseDate, Revenue, Runtime, VoteAverage, videos, casts);
        }

        public string ToString(bool indent = false)
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = indent });
        }
    }
}
