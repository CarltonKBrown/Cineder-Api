﻿using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class MovieDetail : Entity, IComparable<MovieDetail>
    {
        public MovieDetail(long id, string name, double budget, IEnumerable<Genre> genres, string overview, string posterPath, IEnumerable<ProductionCompany> productionCompanies, DateTime releaseDate, double revenue, double runtime, double voteAverage, IEnumerable<Video> videos, IEnumerable<Cast> casts) : base(id, name)
        {
            Budget = budget;
            Genres = genres.Prevent(nameof(genres)).Null().Value;
            Overview = overview.Prevent(nameof(overview)).Null();
            PosterPath = posterPath.Prevent(nameof(posterPath)).Null();
            ProductionCompanies = productionCompanies.Prevent(nameof(productionCompanies)).Null().Value;
            ReleaseDate = releaseDate;
            Revenue = revenue;
            Runtime = runtime;
            VoteAverage = voteAverage;
            Videos = videos.Prevent(nameof(videos)).Null().Value;
            Casts = casts.Prevent(nameof(casts)).Null().Value;
        }

        public MovieDetail() : this(0, string.Empty, 0.0, Enumerable.Empty<Genre>(), string.Empty, string.Empty, Enumerable.Empty<ProductionCompany>(), DateTime.MinValue, 0.0, 0.0, 0.0, Enumerable.Empty<Video>(), Enumerable.Empty<Cast>()) { }

        public double Budget { get; protected set; }
        public IEnumerable<Genre> Genres { get; protected set; }
        public string Overview { get; protected set; }
        public string PosterPath { get; protected set; }
        public IEnumerable<ProductionCompany> ProductionCompanies { get; protected set; }
        public DateTime ReleaseDate { get; protected set; }
        public double Revenue { get; protected set; }
        public double Runtime { get; protected set; }
        public double VoteAverage { get; protected set; }
        public IEnumerable<Video> Videos { get; protected set; }
        public IEnumerable<Cast> Casts { get; protected set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as MovieDetail);
        }

        public bool Equals(MovieDetail? otherMovieDetail)
        {
            return otherMovieDetail != null && otherMovieDetail.Id == Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public int CompareTo(MovieDetail? other)
        {
            if (other == null) return 1;

            return Id.CompareTo(other.Id);
        }
    }
}
