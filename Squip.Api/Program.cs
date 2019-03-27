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
            var logglyCustomerToken = Environment.GetEnvironmentVariable("LOGGLY_CUSTOMER_TOKEN") ?? string.Empty;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Loggly(customerToken: logglyCustomerToken)
                .WriteTo.GoogleCloudLogging("squip-183202")
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
