using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.EndToEnd;

public class HabitSummariesE2E
{
    private readonly TestRunner _runner;

    public HabitSummariesE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] HabitSummaries");

        await _runner.RunTest("HabitSummaries: GET → 200 with array", async () =>
        {
            var response = await _runner.Http.GetAsync("habitsummaries?timeZone=America%2FNew_York");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            TestRunner.Assert(json.StartsWith("["), "Expected JSON array");
        });
    }
}
