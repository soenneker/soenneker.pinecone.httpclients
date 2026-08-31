using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Pinecone.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Pinecone.HttpClients;

public sealed class PineconeOpenApiHttpClient : IPineconeOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _cacheKey = $"{nameof(PineconeOpenApiHttpClient)}:{Guid.NewGuid():N}";

    public PineconeOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_cacheKey, (config: _config, baseUrl: _config.GetValueStrict<string>("Pinecone:ClientBaseUrl")),
            static state =>
            {
                var apiKey = state.config.GetValueStrict<string>("Pinecone:ApiKey");
                string authHeaderName = state.config["Pinecone:AuthHeaderName"] ?? "X-Pinecone-Api-Key";
                string authHeaderValueTemplate = state.config["Pinecone:AuthHeaderValueTemplate"] ?? "{token}";
                string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

                return new HttpClientOptions
                {
                    BaseAddress = new Uri(state.baseUrl),
                    DefaultRequestHeaders = new Dictionary<string, string>
                    {
                        { authHeaderName, authHeaderValue },
                    }
                };
            }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_cacheKey);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_cacheKey);
    }
}
