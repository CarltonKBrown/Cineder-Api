using Cineder_Api.Application.Clients;
using Cineder_Api.Core.Clients;
using Cineder_Api.Core.Config;
using Cineder_Api.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cineder_Api.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var configSection = config.GetSection(CinederOptions.SectionName);

            _ = services.Configure<CinederOptions>(configSection);

            var cinederOptions = new CinederOptions();

            configSection.Bind(cinederOptions);

            services.AddHttpClient(cinederOptions.ClientName, opt =>
            {
                opt.BaseAddress = new Uri(cinederOptions.ApiBaseUrl);
            });

            services.AddScoped<IMovieClient, MovieClient>();
            services.AddScoped<ISeriesClient, SeriesClient>();

            return services;
        }
    }
}
