using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace Squip.Rest.Verify;

public class TestRunner
{
    public int Passed { get; private set; }
    public int Failed { get; private set; }

    public readonly HttpClient Http;
    public readonly JsonSerializerSettings JsonSettings;

    public TestRunner()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };
        Http = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost:8080/api/")
        };
        Http.DefaultRequestHeaders.Add("Accept", "application/json");

        JsonSettings = new JsonSerializerSettings()
            .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    }

    public async Task RunTest(string name, Func<Task> test)
    {
        try
        {
            await test();
            Console.WriteLine($"  PASS  {name}");
            Passed++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  FAIL  {name}");
            Console.WriteLine($"        {ex.Message}");
            Failed++;
        }
    }

    public static void Assert(bool condition, string message)
    {
        if (!condition)
            throw new Exception($"Assertion failed: {message}");
    }

    public static void AssertEquals<T>(T expected, T actual, string message = "")
    {
        if (!Equals(expected, actual))
            throw new Exception($"Expected {expected}, got {actual}. {message}");
    }

    public T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, JsonSettings)
               ?? throw new Exception($"Failed to deserialize response as {typeof(T).Name}");
    }
}
