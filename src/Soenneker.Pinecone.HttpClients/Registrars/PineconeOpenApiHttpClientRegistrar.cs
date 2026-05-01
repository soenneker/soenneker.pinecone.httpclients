using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Pinecone.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Pinecone.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class PineconeOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="PineconeOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddPineconeOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IPineconeOpenApiHttpClient, PineconeOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="PineconeOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddPineconeOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IPineconeOpenApiHttpClient, PineconeOpenApiHttpClient>();

        return services;
    }
}
