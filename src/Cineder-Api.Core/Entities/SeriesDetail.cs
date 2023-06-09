﻿using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class SeriesDetail : Entity
    {
        public SeriesDetail(long id, string name, IEnumerable<CreatedBy> createdBy, IEnumerable<int> runtime, DateTime firstAirDate, IEnumerable<Genre> genres, bool inProduction, IEnumerable<string> languages, DateTime lastAirDate, LastEpisodeToAir lastEpisodeToAir, object nextEpisodeToAir, IEnumerable<Network> networks, int numberOfEpisodes, int numberOfSeasons, IEnumerable<string> originCountry, string originalLanguage, string originalName, string overview, double popularity, string posterPath, IEnumerable<ProductionCompany> productionCompanies, IEnumerable<Seasons> seasons, string status, double voteAverage, IEnumerable<Cast> casts, IEnumerable<Video> videos) : base(id, name)
        {
            CreatedBy = createdBy;
            Runtime = runtime;
            FirstAirDate = firstAirDate;
            Genres = genres;
            InProduction = inProduction;
            Languages = languages;
            LastAirDate = lastAirDate;
            LastEpisodeToAir = lastEpisodeToAir;
            NextEpisodeToAir = nextEpisodeToAir;
            Networks = networks;
            NumberOfEpisodes = numberOfEpisodes;
            NumberOfSeasons = numberOfSeasons;
            OriginCountry = originCountry;
            OriginalLanguage = originalLanguage;
            OriginalName = originalName;
            Overview = overview;
            Popularity = popularity;
            PosterPath = posterPath;
            ProductionCompanies = productionCompanies;
            Seasons = seasons;
            Status = status;
            VoteAverage = voteAverage;
            Casts = casts;
            Videos = videos;
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
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
