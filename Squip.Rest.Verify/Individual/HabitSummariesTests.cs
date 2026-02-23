using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.Individual;

public class HabitSummariesTests
{
    private readonly TestRunner _runner;

    public HabitSummariesTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] HabitSummaries");

        await _runner.RunTest("GET /api/habitsummaries?timeZone=America/New_York → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("habitsummaries?timeZone=America%2FNew_York");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
