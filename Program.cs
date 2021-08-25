using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GraphQLSandbox
{
    internal static class Program
    {
        public static async Task Main() =>
            await new HostBuilder()
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole())
                .ConfigureWebHost(
                    webHostBuilder =>
                        webHostBuilder
                            .UseKestrel()
                            .UseStartup<Startup>())
                .RunConsoleAsync();

        private sealed class Startup
        {
            public void Configure(IApplicationBuilder applicationBuilder) =>
                applicationBuilder
                    .UseRouting()
                    .UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapGraphQL());

            public void ConfigureServices(IServiceCollection services) =>
                services
                    .AddRouting()
                    .AddGraphQLServer()
                    .AddQueryType<QueryType>()
                    .AddType<TextMessageType>();
        }
    }
}