using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Pinecone.HttpClients.Abstract;

/// <summary>
/// Provides an authenticated <see cref="HttpClient"/> for the Pinecone Nexus API.
/// </summary>
public interface IPineconeOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached client owned by this provider.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The configured HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
