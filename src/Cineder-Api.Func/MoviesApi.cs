using Cineder_Api.Application.DTOs.Requests.Movies;
using Cineder_Api.Application.Services.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PreventR;

namespace Cineder_Api.Func
{
    public class MoviesApi
    {
        private readonly ILogger<MoviesApi> _logger;
        private readonly IMovieService _movieService;

        public MoviesApi(ILogger<MoviesApi> logger, IMovieService movieService)
        {
            _logger = logger.Prevent(nameof(logger)).Null().Value;

            _movieService = movieService.Prevent(nameof(movieService)).Null().Value;
        }

        [Function("GetMovieById")]
        public async Task<IActionResult> GetMovieById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies/{id}")] HttpRequest req, [FromRoute] long id, FunctionContext executionContext)
        {
            try
            {
                var request = new GetMovieByIdRequest(id);

                var response = await _movieService.GetMovieByIdAsync(request);

                if ((response?.Id ?? 0) < 1)
                {
                    return new NotFoundObjectResult(response);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex)
                {
                    StatusCode = 500,
                };
            }

        }

        [Function("GetMovies")]
        public async Task<IActionResult> GetMovies([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "moviesfind")] HttpRequest req, [FromQuery] string search, [FromQuery] int page = 1)
        {
            try
            {
                var request = new GetMoviesRequest(search, page);

                var response = await _movieService.GetMoviesAsync(request);

                if ((response?.TotalResults ?? 0) < 1)
                {
                    return new NotFoundObjectResult(response);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex)
                {
                    StatusCode = 500,
                };
            }
        }
    }
}
