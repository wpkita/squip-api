using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.GoogleCloudLogging;

namespace Squip.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // Must read this manually (i.e. not from IConfiguration) since pre-bootstrap
            var firebaseProjectId = Environment.GetEnvironmentVariable("GCP_PROJECT_ID") ?? string.Empty;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.FromLogContext()
                .WriteTo.GoogleCloudLogging(firebaseProjectId)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog();
    }
}
