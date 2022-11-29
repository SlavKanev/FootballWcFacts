using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Services;
using FootballWcFacts.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FootballFactsServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IFactService, FactService>();
            return services;
        }
    }
}
