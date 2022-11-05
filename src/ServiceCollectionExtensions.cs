using Microsoft.Extensions.StorageProviders.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageProvider<TProvider>(IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where TProvider : class, IStorageProvider
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Scoped:
                services.AddScoped<IStorageProvider, TProvider>();
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<IStorageProvider, TProvider>();
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<IStorageProvider, TProvider>();
                break;
        }

        return services;
    }
}