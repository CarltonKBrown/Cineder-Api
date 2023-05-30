namespace Cineder_Api.Core.Entities
{
    public abstract record Entity(long Id, string Name)
    {
        protected Entity() : this(0, string.Empty)
        {

        }
    }

    public abstract record ListResult(long Id, string Name, string PosterPath, string Overview, IEnumerable<long> GenreIds, double VoteAverage, int Idx, string Relevance): Entity(Id, Name);

    public record Cast(long Id, string Name, long CastId, string Chracter, string CreditId, int Gender, int Order, string ProfilePath): Entity(Id, Name);
    public record CreatedBy(long Id, string Name, string CreditId, string Gender, string ProfilePath): Entity(Id, Name);
    public record Crew(long Id, string Name, string CreditId, string Gender, string ProfilePath, string Department, string Job): Entity(Id, Name);
    public record Genre(long Id, string Name) : Entity(Id, Name);
    public record Keyword(long Id, string Name) : Entity(Id, Name);
    public record LastEpisodeToAir(long Id, string Name, DateTime AirDate, int EpisodeNumber, string Overiew, string ProductionCode, int SeasonNumber, long ShowId, string StillPath, double VoteAverage, int VoteCount) : Entity(Id, Name);
    public record MovieDetail(long Id, string Name, double Budget, IEnumerable<Genre> Genres, string Overview, string PostePath, IEnumerable<ProductionCompany> ProductionCompanies, DateTime ReleaseDate, double Revenue, double Runtime, double VoteAverage, IEnumerable<Video> Videos, IEnumerable<Cast> Casts):Entity(Id, Name);
    public record ProductionCompany(long Id, string Name, string LogoPath, string OriginCountry):Entity(Id, Name);
    public record Video(long Id, string Name, string IsoLang, string IsoRegion, string Key, string Site, int Size, string Type):Entity(Id, Name); 
}
