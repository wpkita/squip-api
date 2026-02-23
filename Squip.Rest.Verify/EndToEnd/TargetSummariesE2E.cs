using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.EndToEnd;

public class TargetSummariesE2E
{
    private readonly TestRunner _runner;

    public TargetSummariesE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] TargetSummaries");

        await _runner.RunTest("TargetSummaries: GET → 200 with array", async () =>
        {
            var response = await _runner.Http.GetAsync("targetsummaries?timeZone=America%2FNew_York");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            TestRunner.Assert(json.StartsWith("["), "Expected JSON array");
        });
    }
}
