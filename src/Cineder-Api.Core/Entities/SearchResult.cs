using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class SearchResult<T>
    {
        public SearchResult(int page, IEnumerable<T> results, int totalResults, int totalPages)
        {
            Page = page;
            Results = results;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }

        public SearchResult() : this(1, Enumerable.Empty<T>(), 0, 0) { }

        public int Page { get; protected set; }
        public IEnumerable<T> Results { get; protected set; }
        public int TotalResults { get; protected set; }
        public int TotalPages { get; protected set; }

        public override string ToString()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
    }
}
