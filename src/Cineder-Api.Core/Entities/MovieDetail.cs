using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class MovieDetail : Entity
    {
        public MovieDetail(long id, string name, double budget, IEnumerable<Genre> genres, string overview, string postePath, IEnumerable<ProductionCompany> productionCompanies, DateTime releaseDate, double revenue, double runtime, double voteAverage, IEnumerable<Video> videos, IEnumerable<Cast> casts) : base(id, name)
        {
            Budget = budget;
            Genres = genres;
            Overview = overview;
            PostePath = postePath;
            ProductionCompanies = productionCompanies;
            ReleaseDate = releaseDate;
            Revenue = revenue;
            Runtime = runtime;
            VoteAverage = voteAverage;
            Videos = videos;
            Casts = casts;
        }

        public MovieDetail() : this(0, string.Empty, 0.0, Enumerable.Empty<Genre>(), string.Empty, string.Empty, Enumerable.Empty<ProductionCompany>(), DateTime.MinValue, 0.0, 0.0, 0.0, Enumerable.Empty<Video>(), Enumerable.Empty<Cast>()) { }

        public double Budget { get; protected set; }
        public IEnumerable<Genre> Genres { get; protected set; }
        public string Overview { get; protected set; }
        public string PostePath { get; protected set; }
        public IEnumerable<ProductionCompany> ProductionCompanies { get; protected set; }
        public DateTime ReleaseDate { get; protected set; }
        public double Revenue { get; protected set; }
        public double Runtime { get; protected set; }
        public double VoteAverage { get; protected set; }
        public IEnumerable<Video> Videos { get; protected set; }
        public IEnumerable<Cast> Casts { get; protected set; }
        
        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }

    }
}
