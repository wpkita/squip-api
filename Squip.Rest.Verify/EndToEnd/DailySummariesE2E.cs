using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.EndToEnd;

public class DailySummariesE2E
{
    private readonly TestRunner _runner;

    public DailySummariesE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] DailySummaries");

        await _runner.RunTest("DailySummaries: GET → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("dailysummaries?timeZone=America%2FNew_York");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
