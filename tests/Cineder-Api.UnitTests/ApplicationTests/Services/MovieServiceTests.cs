using Cineder_Api.Application.Clients;
using Cineder_Api.Application.DTOs.Requests.Movies;
using Cineder_Api.Application.Services.Movies;
using Cineder_Api.Core.Entities;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cineder_Api.UnitTests.ApplicationTests.Services
{
    public class MovieServiceTests
    {
        private IMovieService _movieService;
        private IMovieClient _movieClientFake;
        private ILogger<MovieService> _loggerFake;

        public MovieServiceTests()
        {
            _loggerFake = A.Fake<ILogger<MovieService>>();

            _movieClientFake = A.Fake<IMovieClient>();

            _movieService = new MovieService(_movieClientFake, _loggerFake);

        }

        [Fact]
        public async Task GetMoviesAsync_ResultsWithDuplicates_ShouldBeMergedAndFiltered()
        {

            var movieNameResult1 = new MoviesResult(1, "John Doe", DateTime.Today, "", "", new long[] {1,2,3}, 0.0, 1, "name" );

            var movieNameResult2 = new MoviesResult(2, "John Doe2", DateTime.Today, "", "", new long[] {1,2,3}, 0.0, 1, "name" );

            var movieNameResults = new MoviesResult[] {movieNameResult1,  movieNameResult2};

            var movieNameSearchResults = new SearchResult<MoviesResult>(1, movieNameResults, 2, 1);

            var movieTypeResult1 = new MoviesResult(1, "John Doe", DateTime.Today, "", "", new long[] { 1, 2, 3 }, 0.0, 1, "type");

            var movieTypeResult2 = new MoviesResult(3, "John Doe3", DateTime.Today, "", "", new long[] { 1, 2, 3 }, 0.0, 1, "type");

            var movieTypeResults = new MoviesResult[] { movieTypeResult1, movieTypeResult2 };

            var movieTypeSearchResults = new SearchResult<MoviesResult>(1, movieTypeResults, 2, 1);

            A.CallTo(() => _movieClientFake.GetMoviesByTitleAsync(A<string>._, A<int>._)).Returns(movieNameSearchResults);

            A.CallTo(() => _movieClientFake.GetMoviesByKeywordsAsync(A<string>._, A<int>._)).Returns(movieTypeSearchResults);

            var moviesRequest = new GetMoviesRequest(string.Empty, 1);

            var actual = await _movieService.GetMoviesAsync(moviesRequest);

            Assert.True(actual.Results.Count() == 3);
        }
    }
}
