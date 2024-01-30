using Cineder_Api.Application.Services.Series;
using Cineder_Api.Core.DTOs.Requests.Series;
using Cineder_Api.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PreventR;

namespace Cineder_Api.Func
{
    public class SeriesApi
    {
        private readonly ILogger<SeriesApi> _logger;

        private readonly ISeriesService _seriesService;

        public SeriesApi(ILogger<SeriesApi> logger, ISeriesService seriesService)
        {
            _logger = logger.Prevent(nameof(logger)).Null().Value;

            _seriesService = seriesService.Prevent(nameof(seriesService)).Null().Value;
        }

        [Function("GetSeriesById")]
        public async Task<IActionResult> GetSeriesById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "series/{id}")] HttpRequest req, [FromRoute] long id)
        {
            try
            {
                var request = new GetSeriesByIdRequest(id);

                var response = await _seriesService.GetSeriesByIdAsync(request);

                if ((response?.Id ?? 0) < 1)
                {
                    return new NotFoundObjectResult(response);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSeriesById API could not return a result");

                return new ObjectResult(new SeriesDetail())
                {
                    StatusCode = 500,
                };
            }
        }

        [Function("GetSeries")]
        public async Task<IActionResult> GetSeries([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "series")] HttpRequest req, [FromQuery] string search, [FromQuery] int page = 1)
        {
            try
            {
                var request = new GetSeriesRequest(search, page);

                var response = await _seriesService.GetSeriesAsync(request);

                if ((response?.TotalResults ?? 0) < 1)
                {
                    return new NotFoundObjectResult(response);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSeries API could not return a result");

                return new ObjectResult(new SeriesDetail())
                {
                    StatusCode = 500,
                };
            }
        }

        [Function("GetSimilarSeriesbyId")]
        public async Task<IActionResult> GetSimilarMoviesById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "series/similar/{id}")] HttpRequest req, long id, [FromQuery] int page = 1)
        {
            try
            {
                var request = new GetSeriesSimilarRequest(id, page);

                var response = await _seriesService.GetSeriesSimilarAsync(request);

                if ((response?.TotalResults ?? 0) < 1)
                {
                    return new NotFoundObjectResult(response);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSimilarSeriesById API could not return a result");

                return new ObjectResult(new SearchResult<SeriesResult>())
                {
                    StatusCode = 500,
                };
            }
        }
    }
}
