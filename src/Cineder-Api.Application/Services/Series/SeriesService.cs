using Cineder_Api.Core.Clients;
using Cineder_Api.Core.DTOs.Requests.Series;
using Cineder_Api.Core.Entities;
using Microsoft.Extensions.Logging;
using PreventR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cineder_Api.Application.Services.Series
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesClient _client;
        private readonly ILogger<SeriesService> _logger;

        public SeriesService(ISeriesClient client, ILogger<SeriesService> logger)
        {
            _client = client.Prevent(nameof(client)).Null().Value;
            _logger = logger.Prevent(nameof(client)).Null().Value;
        }

        public async Task<SearchResult<SeriesResult>> GetSeriesAsync(GetSeriesRequest request)
        {
            try
            {
                request.Prevent(nameof(request)).Null();

                var seriesSearchRequests = new[]
                {
                    _client.GetSeriesByTitleAsync(request.SearchText, request.PageNum),
                    _client.GetSeriesKeywordsAsync(request.SearchText, request.PageNum)
                };

                await Task.WhenAll(seriesSearchRequests);

                var searchReults = new List<SearchResult<SeriesResult>>();

                foreach (var searchRequest in seriesSearchRequests)
                {
                    var searchResult = await searchRequest;

                    searchReults.Add(searchResult);
                }

                var results = searchReults.Aggregate(new SearchResult<SeriesResult>(), SearchResult<SeriesResult>.SearchResultAgregator);

				results.Results = results.Results.Select((x, i) => { x.Idx = i; return x; });

				return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get series result!");

                throw;
            }
        }

        public async Task<SeriesDetail> GetSeriesByIdAsync(GetSeriesByIdRequest request)
        {
            try
            {
                request.Prevent(nameof(request)).Null();

                var series = await _client.GetSeriesByIdAsync(request.Id);

                return series;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get Series by ID");
                throw;
            }
        }

        public async Task<SearchResult<SeriesResult>> GetSeriesSimilarAsync(GetSeriesSimilarRequest request)
        {
            try
            {
                request.Prevent(nameof(request)).Null();

                var series = await _client.GetSeriesSimilarAsync(request.SeriesId, request.PageNum);

                return series;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get similar series");

                throw;
            }
        }
    }
}
