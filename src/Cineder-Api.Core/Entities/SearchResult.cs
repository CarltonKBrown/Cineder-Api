using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;

namespace Cineder_Api.Core.Entities
{
    public class SearchResult<T>
    {
        public SearchResult(int page, IEnumerable<T> results, int totalResults, int totalPages)
        {
            Page = page;
            Results = results.Prevent(nameof(results)).Null().Value;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }

        public SearchResult() : this(1, Enumerable.Empty<T>(), 0, 0) { }

        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, JsonUtil.Indent);
        }
    }
}
