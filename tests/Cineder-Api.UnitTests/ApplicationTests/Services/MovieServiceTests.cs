using Cineder_Api.Application.Clients;
using Cineder_Api.Application.Services.Movies;
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

        public MovieServiceTests()
        {
            var loggerFake = A.Fake<ILogger<MovieService>>();

            var movieClientFake = A.Fake<IMovieClient>();

            _movieService = new MovieService(movieClientFake, loggerFake);
        }

        [Fact]
        public async Task GetMoviesAsync_Results_ShouldBeMerged()
        {

        }
    }
}
