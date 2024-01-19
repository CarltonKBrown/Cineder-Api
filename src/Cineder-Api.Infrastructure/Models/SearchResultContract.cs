using Cineder_Api.Core.Entities;
using Cineder_Api.Core.Util;
using PreventR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure;

internal class SearchResultContract<T>
{
    public SearchResultContract(int page, IEnumerable<T> results, int totalResults, int totalPages)
    {
        Page = page;
        Results = results.Prevent(nameof(results)).Null().Value;
        TotalResults = totalResults;
        TotalPages = totalPages;
    }

    public SearchResultContract() : this(0, Enumerable.Empty<T>(), 0, 0)
    {

    }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public IEnumerable<T> Results { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    public SearchResult<K> ToSearchResult<K>(IEnumerable<K> newResults)
    {
        return new(Page, newResults, TotalResults, TotalPages);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, JsonUtil.Indent);
    }

}
