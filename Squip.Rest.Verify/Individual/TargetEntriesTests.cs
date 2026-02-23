using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class TargetEntriesTests
{
    private readonly TestRunner _runner;

    public TargetEntriesTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] TargetEntries");

        // Setup: create a target
        Guid targetId = Guid.Empty;
        var targetResponse = await _runner.Http.PostAsJsonAsync("targets", new { name = "TargetEntry Test Target" });
        if (targetResponse.StatusCode == HttpStatusCode.Created)
        {
            var json = await targetResponse.Content.ReadAsStringAsync();
            targetId = JObject.Parse(json)["id"]!.ToObject<Guid>();
        }

        await _runner.RunTest("GET /api/targets/{targetId}/targetentries/{nonExistentId} → 404", async () =>
        {
            TestRunner.Assert(targetId != Guid.Empty, "Need a target first");
            var response = await _runner.Http.GetAsync($"targets/{targetId}/targetentries/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        await _runner.RunTest("POST /api/targets/{targetId}/targetentries → 201", async () =>
        {
            TestRunner.Assert(targetId != Guid.Empty, "Need a target first");
            var body = new { targetId, magnitude = 1, didEngage = true };
            var response = await _runner.Http.PostAsJsonAsync($"targets/{targetId}/targetentries", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
        });
    }
}
