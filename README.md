[![](https://img.shields.io/nuget/v/soenneker.pinecone.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.pinecone.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.pinecone.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.pinecone.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.pinecone.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.pinecone.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.pinecone.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.pinecone.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Pinecone.HttpClients

Provides a cached, authenticated `HttpClient` for a Pinecone Nexus deployment.

## Installation

```bash
dotnet add package Soenneker.Pinecone.HttpClients
```

## Configuration

```json
{
  "Pinecone": {
    "ApiKey": "your-api-key",
    "ClientBaseUrl": "https://your-nexus-host/api/"
  }
}
```

The client sends `Pinecone:ApiKey` in `X-Pinecone-Api-Key`. To use a Nexus session token instead, set `Pinecone:AuthHeaderName` to `Authorization` and `Pinecone:AuthHeaderValueTemplate` to `Bearer {token}`.

## Usage

```csharp
using Soenneker.Pinecone.HttpClients.Abstract;
using Soenneker.Pinecone.HttpClients.Registrars;

services.AddPineconeOpenApiHttpClientAsSingleton();

IPineconeOpenApiHttpClient provider = serviceProvider
    .GetRequiredService<IPineconeOpenApiHttpClient>();

HttpClient client = await provider.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync("nexus/project", cancellationToken);
response.EnsureSuccessStatusCode();
```

The provider owns the cached client and removes it when the provider is disposed. Scoped registrations use separate cache entries, so disposing one scope does not invalidate another scope's client.
