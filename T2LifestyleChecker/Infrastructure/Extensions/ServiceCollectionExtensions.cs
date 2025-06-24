using T2LifestyleChecker.Services.Implementation.Bootstrapper;

namespace T2LifestyleChecker.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLibraries(this IServiceCollection services, IConfigurationManager configuration)
        {
            _ = new ServicesImplementationLocalBootstrapper(services, configuration);
            return services;
        }

    }
}
