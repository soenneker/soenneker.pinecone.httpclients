using Soenneker.Pinecone.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Pinecone.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PineconeOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPineconeOpenApiHttpClient _httpclient;

    public PineconeOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IPineconeOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
