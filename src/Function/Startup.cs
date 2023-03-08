using Function.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DbContexts;
using Repository.Grabbers;
using Repository.Infrastructure;
using Repository.Parsers;

[assembly: FunctionsStartup(typeof(Function.Startup))]

namespace Function;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddLogging();
        builder.Services.AddOptions<FunctionOptions>()
            .Configure<IConfiguration>((settings, configuration) => { configuration.GetSection("FunctionOptions").Bind(settings); });
        builder.Services.AddScoped<ICybernationsDbContext, CybernationsDbContext>();
        builder.Services.AddScoped<IDataParser, CnFileParser>();
        builder.Services.AddScoped<ICnDbRepository, CnDbRepository>();
        builder.Services.AddScoped<IDataGrabber, CnFileGrabber>();
        builder.Services.AddScoped<IFileImporter, CnFileImporter>();
        builder.Services.AddScoped<IBlobManager, BlobManager>();

        builder.Services.AddAutoMapper(typeof(Startup));
    }
}