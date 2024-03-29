﻿using Cineder_Api.Application.Services.Movies;
using Cineder_Api.Application.Services.Series;
using Microsoft.Extensions.DependencyInjection;

namespace Cineder_Api.Application
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ISeriesService, SeriesService>();

            return services;
        }
    }
}
