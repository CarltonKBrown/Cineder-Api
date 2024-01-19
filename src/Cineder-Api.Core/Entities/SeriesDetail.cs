using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class SeriesDetail : Entity, IComparable<SeriesDetail>
    {
        public SeriesDetail(long id, string name, IEnumerable<CreatedBy> createdBy, IEnumerable<int> runtime, DateTime firstAirDate, IEnumerable<Genre> genres, bool inProduction, IEnumerable<string> languages, DateTime lastAirDate, LastEpisodeToAir lastEpisodeToAir, object nextEpisodeToAir, IEnumerable<Network> networks, int numberOfEpisodes, int numberOfSeasons, IEnumerable<string> originCountry, string originalLanguage, string originalName, string overview, double popularity, string posterPath, IEnumerable<ProductionCompany> productionCompanies, IEnumerable<Seasons> seasons, string status, double voteAverage, IEnumerable<Cast> casts, IEnumerable<Video> videos) : base(id, name)
        {
            CreatedBy = createdBy.Prevent(nameof(createdBy)).Null().Value;
            Runtime = runtime.Prevent(nameof(runtime)).Null().Value;
            FirstAirDate = firstAirDate;
            Genres = genres.Prevent(nameof(genres)).Null().Value;
            InProduction = inProduction;
            Languages = languages.Prevent(nameof(languages)).Null().Value;
            LastAirDate = lastAirDate;
            LastEpisodeToAir = lastEpisodeToAir.Prevent(nameof(lastEpisodeToAir)).Null().Value;
            NextEpisodeToAir = nextEpisodeToAir.Prevent(nameof(nextEpisodeToAir)).Null().Value;
            Networks = networks.Prevent(nameof(networks)).Null().Value;
            NumberOfEpisodes = numberOfEpisodes;
            NumberOfSeasons = numberOfSeasons;
            OriginCountry = originCountry.Prevent(nameof(originCountry)).Null().Value;
            OriginalLanguage = originalLanguage.Prevent(nameof(originalLanguage)).NullOrWhiteSpace();
            OriginalName = originalName.Prevent(nameof(originalName)).NullOrWhiteSpace();
            Overview = overview.Prevent(nameof(overview)).NullOrWhiteSpace();
            Popularity = popularity;
            PosterPath = posterPath.Prevent(nameof(posterPath)).NullOrWhiteSpace();
            ProductionCompanies = productionCompanies.Prevent(nameof(productionCompanies)).Null().Value;
            Seasons = seasons.Prevent(nameof(seasons)).Null().Value;
            Status = status.Prevent(nameof(status)).NullOrWhiteSpace();
            VoteAverage = voteAverage;
            Casts = casts.Prevent(nameof(casts)).Null().Value;
            Videos = videos.Prevent(nameof(videos)).Null().Value;
        }

        public SeriesDetail() : this(0, string.Empty, Enumerable.Empty<CreatedBy>(), Enumerable.Empty<int>(), DateTime.MinValue, Enumerable.Empty<Genre>(), false, Enumerable.Empty<string>(), DateTime.MinValue, new(), new(), Enumerable.Empty<Network>(), 0, 0, Enumerable.Empty<string>(), string.Empty, string.Empty, string.Empty, 0.0, string.Empty, Enumerable.Empty<ProductionCompany>(), Enumerable.Empty<Seasons>(), string.Empty, 0.0, Enumerable.Empty<Cast>(), Enumerable.Empty<Video>()) { }

        public IEnumerable<CreatedBy> CreatedBy { get; protected set; }
        public IEnumerable<int> Runtime { get; protected set; }
        public DateTime FirstAirDate { get; protected set; }
        public IEnumerable<Genre> Genres { get; protected set; }
        public bool InProduction { get; protected set; }
        public IEnumerable<string> Languages { get; protected set; }
        public DateTime LastAirDate { get; protected set; }
        public LastEpisodeToAir LastEpisodeToAir { get; protected set; }
        public object NextEpisodeToAir { get; protected set; }
        public IEnumerable<Network> Networks { get; protected set; }
        public int NumberOfEpisodes { get; protected set; }
        public int NumberOfSeasons { get; protected set; }
        public IEnumerable<string> OriginCountry { get; protected set; }
        public string OriginalLanguage { get; protected set; }
        public string OriginalName { get; protected set; }
        public string Overview { get; protected set; }
        public double Popularity { get; protected set; }
        public string PosterPath { get; protected set; }
        public IEnumerable<ProductionCompany> ProductionCompanies { get; protected set; }
        public IEnumerable<Seasons> Seasons { get; protected set; }
        public string Status { get; protected set; }
        public double VoteAverage { get; protected set; }
        public IEnumerable<Cast> Casts { get; protected set; }
        public IEnumerable<Video> Videos { get; protected set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj as SeriesDetail);
        }

        public bool Equals(SeriesDetail? otherSeriesDetail)
        {
            return otherSeriesDetail != null && otherSeriesDetail.Id == Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public int CompareTo(SeriesDetail? other)
        {
            if (other == null) return 1;

            return Id.CompareTo(other.Id);
        }
    }
}
