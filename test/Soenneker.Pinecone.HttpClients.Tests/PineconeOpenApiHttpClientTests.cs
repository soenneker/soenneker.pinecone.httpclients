using Soenneker.Pinecone.HttpClients.Abstract;
using Soenneker.TestHosts.Unit;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Pinecone.HttpClients.Tests;

[ClassDataSource<UnitTestHost>(Shared = SharedType.PerTestSession)]
public sealed class PineconeOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPineconeOpenApiHttpClient _httpclient;

    public PineconeOpenApiHttpClientTests(UnitTestHost host) : base(host)
    {
        _httpclient = Resolve<IPineconeOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
