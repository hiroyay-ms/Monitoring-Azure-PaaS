using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Api.Data;

[assembly: FunctionsStartup(typeof(Api.Startup))]
namespace Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTIONSTRING");

            builder.Services.AddDbContext<AdventureWorksContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString)
            );

            builder.Services.AddApplicationInsightsTelemetry();
            builder.Services.Configure<TelemetryConfiguration>(config =>
                {
                    config.TelemetryInitializers.Add(new AppInsightsTelemetryInitializer());
                }
            );
        }
    }
}
