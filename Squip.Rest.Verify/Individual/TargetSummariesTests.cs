using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.Individual;

public class TargetSummariesTests
{
    private readonly TestRunner _runner;

    public TargetSummariesTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] TargetSummaries");

        await _runner.RunTest("GET /api/targetsummaries?timeZone=America/New_York → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("targetsummaries?timeZone=America%2FNew_York");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
