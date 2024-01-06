using Cineder_Api.Core.Config;
using Cineder_Api.Core.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cineder_Api.Infrastructure.Clients
{
    public abstract class BaseClient
    {
        protected readonly CinederOptions _options;

        protected readonly IHttpClientFactory _httpClientFactory;

        protected BaseClient(IHttpClientFactory httpClientFactory, IOptionsSnapshot<CinederOptions> optionsSnapshot)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _options = optionsSnapshot.Value ?? throw new ArgumentNullException(nameof(optionsSnapshot.Value));
        }


        private protected async Task<IEnumerable<long>> GetKeywordIds(string searchText)
        {
            var result = new List<long>();

            if (string.IsNullOrEmpty(searchText)) return result;

            var keywords = searchText.Split(' ').Where(x => x.Trim().Length > 1).Select(x => x.Trim());

            foreach (var keyword in keywords)
            {
                var ids = await FetchAllKeywordIds(keyword);

                result.AddRange(ids);
            }

            return result;
        }

        private async Task<IEnumerable<long>> FetchAllKeywordIds(string keyword)
        {
            var keywordIds = new List<long>();

            var pages = Enumerable.Range(1, _options.MaxPages).ToList();

            foreach (var page in pages)
            {
                var keywordSearchResults = await GetKeywordsAsync(keyword, page);

                var keywordResultsIds = keywordSearchResults.Results.Select(x => x.Id);

                keywordIds.AddRange(keywordResultsIds);
            }

            return keywordIds;
        }

        private async Task<SearchResult<Keyword>> GetKeywordsAsync(string keyword, int pageNum = 1)
        {
            var url = $"/search/keyword?query={keyword.Trim()}&page={pageNum}&{AddApiKey}&{AddLang}";

            var client = _httpClientFactory.CreateClient(_options.ClientName);

            var data = await client.GetFromJsonAsync<SearchResultContract<KeywordContract>>(url);

            if (data == null) return new();

            var keywords = data.Results?.Select(x => x.ToKeyword()) ?? Enumerable.Empty<Keyword>();

            return new(data.Page, keywords, data.TotalResults, data.TotalPages);

        }

        private protected async Task<T> SendGetAsync<T>(string requestUrl) where T : new()
        {
            var client = _httpClientFactory?.CreateClient(_options.ClientName);

            if (client == null || client.BaseAddress == null)
            {
                return new();
            }

            var response = await client.GetAsync(requestUrl);

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return new();
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBody))
            {
                return new();
            }
            
            var responseObj = JsonSerializer.Deserialize<T>(responseBody);

            return responseObj ?? new();
        }

        private protected string AddApiKey()
        {
            return $"api_key={_options.ApiKey}";
        }

        private protected string AddLang()
        {
            return $"language={_options.Language}";
        }

        private protected string AddDefaults(int pageNum = 1)
        {
            return $"{AddPage(pageNum)}&{AddApiKey}&{AddLang}&{AddIncludeAdult}&{AddIncludeVideo}";
        }

        private protected static string AddIncludeAdult(bool includeAdult = false)
        {
            return $"include_adult={includeAdult}";
        }

        private protected static string AddIncludeVideo(bool includeVideo = false)
        {
            return $"include_video={includeVideo}";
        }

        private protected static string AddWithKeywords(IEnumerable<long> keywordIds)
        {
            var keywordsForRequest = keywordIds != null && keywordIds.Any() ? string.Join("|", keywordIds) : string.Empty;

            return $"with_keywords={keywordsForRequest}";
        }

        private protected static string AddQuery(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return string.Empty;

            var requestQuery = string.Join('+', searchText.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries));

            return $"query={requestQuery}";
        }

        private protected static string AddPage(int pageNum = 1)
        {
            return $"page={pageNum}";
        }
    }
}
