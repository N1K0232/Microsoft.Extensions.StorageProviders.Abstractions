using Microsoft.Extensions.StorageProviders;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// registers an instance of the <see cref="IStorageProvider"/> in the dependency injection system
    /// </summary>
    /// <typeparam name="TProvider">the <see cref="IStorageProvider"/> instance</typeparam>
    /// <param name="services">the list of services</param>
    /// <param name="serviceLifetime">specifies which lifetime we want for our <see cref="IStorageProvider"/>
    /// default is <see cref="ServiceLifetime.Scoped"/></param>
    /// <returns>the collection of services</returns>
    public static IServiceCollection AddStorageProvider<TProvider>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where TProvider : class, IStorageProvider
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