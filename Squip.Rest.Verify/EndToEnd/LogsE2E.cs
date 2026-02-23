using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.EndToEnd;

public class LogsE2E
{
    private readonly TestRunner _runner;

    public LogsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Logs");

        await _runner.RunTest("Logs: POST → 204", async () =>
        {
            var response = await _runner.Http.PostAsJsonAsync("logs", new { message = "E2E log message" });
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
