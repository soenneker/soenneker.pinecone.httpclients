using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Soenneker.TestHosts.Unit;
using Soenneker.Utils.Test;
using Soenneker.Pinecone.HttpClients.Registrars;

namespace Soenneker.Pinecone.HttpClients.Tests;

public sealed class Host : UnitTestHost
{
    public override ValueTask Initialize()
    {
        SetupIoC(Services);

        return base.Initialize();
    }

    private static void SetupIoC(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddSerilog(dispose: false);
        });

        IConfiguration config = TestUtil.BuildConfig();
        services.AddSingleton(config);

        services.AddPineconeOpenApiHttpClientAsScoped();
    }
}
