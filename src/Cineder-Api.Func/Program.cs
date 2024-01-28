using Microsoft.Extensions.Hosting;
using Cineder_Api.Application;
using Cineder_Api.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((ctx, services) =>
    {
        var config = ctx.Configuration;

        services.AddApplication();
        services.AddInfrastructure(config);

    })
    .Build();

host.Run();