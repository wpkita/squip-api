using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.Individual;

public class TagsTests
{
    private readonly TestRunner _runner;

    public TagsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Tags");

        await _runner.RunTest("GET /api/tags → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("tags");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/tags/{tagName}/ideas → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("tags/anytag/ideas");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
