using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class RatingsE2E
{
    private readonly TestRunner _runner;

    public RatingsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Ratings");

        await _runner.RunTest("Ratings: GET → 200 with array", async () =>
        {
            var response = await _runner.Http.GetAsync("ratings");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            TestRunner.Assert(json.StartsWith("["), "Expected JSON array");
        });
    }
}
