using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.Individual;

public class LogsTests
{
    private readonly TestRunner _runner;

    public LogsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Logs");

        await _runner.RunTest("POST /api/logs → 204", async () =>
        {
            var body = new { message = "Test log message from Squip.Rest.Verify" };
            var response = await _runner.Http.PostAsJsonAsync("logs", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
