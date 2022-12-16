using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Contracts.Admin;
using FootballWcFacts.Core.Services;
using FootballWcFacts.Core.Services.Admin;
using FootballWcFacts.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FootballFactsServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IFactService, FactService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ILegendService, LegendService>();
            services.AddScoped<IUserService, UserSerivce>();
            return services;
        }
    }
}
