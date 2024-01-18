using Cineder_Api.Core.Enums;

namespace Cineder_Api.Core.Entities
{
    public abstract class ListResult : Entity
    {
        protected ListResult(long id, string name, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, SearchType searchType) : base(id, name)
        {
            PosterPath = posterPath;
            Overview = overview;
            GenreIds = genreIds;
            VoteAverage = voteAverage;
            Idx = idx;
            SearchType = searchType;
        }

        protected ListResult(long id, string name, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, string searchTypeString): this(id, name, posterPath, overview, genreIds, voteAverage, idx, ToSearchType(searchTypeString)) {}

        protected ListResult():this(0, string.Empty, string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, string.Empty) {}

        public string PosterPath { get;  set; }
        public string Overview { get;  set; }
        public IEnumerable<long> GenreIds { get; set; }
        public double VoteAverage { get; set; }
        public int Idx { get; set; }
        public SearchType SearchType { get; set; }

        private static SearchType ToSearchType(string searchTypeString)
        {
            var normalizedString = searchTypeString.ToLowerInvariant();

            return normalizedString switch
            {
                "name"=> SearchType.Name,
                "keyword" => SearchType.Keyword,
                _ => SearchType.None
            };
        }
    }
}
