using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Squip.Rest;

public class Program
{
    public static int Main(string[] args)
    {
        ConfigureLogging();

        try
        {
            Log.Information("Starting web host.");
            CreateHostBuilder(args).Build().Run();
            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Host terminated unexpectedly.");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureLogging()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddEnvironmentVariables()
            .Build();

        Log.Logger = new LoggerConfiguration().MinimumLevel
            .Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(configuration["Elasticsearch:Uri"]))
                {
                    TypeName = null,
                    ModifyConnectionSettings = x =>
                        x.BasicAuthentication(
                            configuration["Elasticsearch:Username"],
                            configuration["Elasticsearch:Password"]
                        ),
                    IndexFormat =
                        $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{configuration["Environment"].Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                }
            )
            .CreateLogger();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
