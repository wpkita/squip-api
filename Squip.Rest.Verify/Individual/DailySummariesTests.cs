using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.Individual;

public class DailySummariesTests
{
    private readonly TestRunner _runner;

    public DailySummariesTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] DailySummaries");

        await _runner.RunTest("GET /api/dailysummaries?timeZone=America/New_York → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("dailysummaries?timeZone=America%2FNew_York");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
