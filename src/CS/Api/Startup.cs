using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Api.Data;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;

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

            builder.Services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();
        }
    }
}
