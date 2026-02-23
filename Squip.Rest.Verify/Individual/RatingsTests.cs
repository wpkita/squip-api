using System;
using System.Net;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.Individual;

public class RatingsTests
{
    private readonly TestRunner _runner;

    public RatingsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Ratings");

        await _runner.RunTest("GET /api/ratings → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("ratings");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
