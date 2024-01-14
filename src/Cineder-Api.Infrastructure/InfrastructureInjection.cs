using Cineder_Api.Core.Config;
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

            return services;
        }
    }
}
