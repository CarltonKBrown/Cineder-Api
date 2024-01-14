namespace Cineder_Api.Core.Entities
{
    public abstract class ListResult : Entity
    {
        protected ListResult(long id, string name, string posterPath, string overview, IEnumerable<long> genreIds, double voteAverage, int idx, string relevance): base(id, name)
        {
            PosterPath = posterPath;
            Overview = overview;
            GenreIds = genreIds;
            VoteAverage = voteAverage;
            Idx = idx;
            Relevance = relevance;
        }

        protected ListResult():this(0, string.Empty, string.Empty, string.Empty, Enumerable.Empty<long>(), 0.0, 0, string.Empty)
        {
            
        }

        public string PosterPath { get;  set; }
        public string Overview { get;  set; }
        public IEnumerable<long> GenreIds { get; set; }
        public double VoteAverage { get; set; }
        public int Idx { get; set; }
        public string Relevance { get; set; }
    }
}
