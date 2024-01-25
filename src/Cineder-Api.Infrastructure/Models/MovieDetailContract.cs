using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Models
{
    public class MovieDetailContract : BaseContract
    {
        public MovieDetailContract(long id, string name, double budget, IEnumerable<GenreContract> genres, string overview, string posterPath, IEnumerable<ProductionCompanyContract> productionCompanies, DateTime releaseDate, double revenue, double runtime, double voteAverage, AppendVideosContract videos, AppendCastsContract casts):base(id)
        {
            Budget = budget;
            Name = name.Prevent(nameof(name)).Null();
            Genres = genres;
            Overview = overview.Prevent((nameof(overview))).Null();
            PosterPath = posterPath.Prevent((nameof(posterPath))).Null();
            ProductionCompanies = productionCompanies.Prevent(nameof(productionCompanies)).Null().Value;
            ReleaseDate = releaseDate;
            Revenue = revenue;
            Runtime = runtime;
            VoteAverage = voteAverage;
            Videos = videos.Prevent(nameof(videos)).Null();
            Casts = casts.Prevent(nameof(casts)).Null();
        }

        public MovieDetailContract() : this(0, string.Empty, 0.0, Enumerable.Empty<GenreContract>(), string.Empty, string.Empty, Enumerable.Empty<ProductionCompanyContract>(), DateTime.Today, 0.0, 0.0, 0.0, new(), new())
        {

        }

        [JsonPropertyName("budget")]
        public double Budget { get; set; }

        [JsonPropertyName("title")]
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

        public override string ToString() => JsonSerializer.Serialize(this, JsonUtil.Indent);
    }
}
