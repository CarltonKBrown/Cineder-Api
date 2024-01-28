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
    }
}
