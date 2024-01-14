using Cineder_Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cineder_Api.Infrastructure.Models
{
    internal class SeriesResultContract : BaseContract
    {
        public SeriesResultContract(long id, string posterpath, double popularity, object backdropPath, double voteAverage, string overview, string firstAirDate, IEnumerable<string> originCountry, IEnumerable<long> genreIds, string originalLanguage, int voteCount, string name, string originalName) : base(id)
        {
            PosterPath = posterpath;
            Popularity = popularity;
            BackdropPath = backdropPath;
            VoteAverage = voteAverage;
            Overview = overview;
            FirstAirDate = firstAirDate;
            OriginCountry = originCountry;
            GenreIds = genreIds;
            OriginalLanguage = originalLanguage;
            VoteCount = voteCount;
            Name = name;
            OriginalName = originalName;
        }

        public SeriesResultContract() : this(0, string.Empty, 0.0, default!, 0.0, string.Empty, string.Empty, Enumerable.Empty<string>(), Enumerable.Empty<long>(), string.Empty, 0, string.Empty, string.Empty)
        {
            
        }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("backdrop_path")]
        public object BackdropPath { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("origin_country")]
        public IEnumerable<string> OriginCountry { get; set; }

        [JsonPropertyName("genre_ids")]
        public IEnumerable<long> GenreIds { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        public SeriesResult ToSeriesResult(SeriesRelevance seriesRelevance)
        {
            var firstAirDate = DateTime.Parse(FirstAirDate);

            var relevance = seriesRelevance switch
            {
                SeriesRelevance.Name => "name",
                SeriesRelevance.Type => "type",
                _ or SeriesRelevance.None => string.Empty
            };

            return new(Id, Name, firstAirDate,OriginCountry, PosterPath, Overview, GenreIds, VoteAverage, 0, relevance);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
