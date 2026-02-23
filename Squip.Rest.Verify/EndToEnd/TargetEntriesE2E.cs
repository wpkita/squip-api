using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class TargetEntriesE2E
{
    private readonly TestRunner _runner;

    public TargetEntriesE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] TargetEntries");

        await _runner.RunTest("TargetEntries: POST target → POST entry → GET entry → verify", async () =>
        {
            // Create target
            var targetResponse = await _runner.Http.PostAsJsonAsync("targets", new { name = "E2E TargetEntry Target" });
            TestRunner.AssertEquals(HttpStatusCode.Created, targetResponse.StatusCode);
            var targetId = JObject.Parse(await targetResponse.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

            // POST entry
            var entryResponse = await _runner.Http.PostAsJsonAsync($"targets/{targetId}/targetentries", new
            {
                targetId,
                magnitude = 3,
                didEngage = true
            });
            TestRunner.AssertEquals(HttpStatusCode.Created, entryResponse.StatusCode);
            var entryObj = JObject.Parse(await entryResponse.Content.ReadAsStringAsync());
            var entryId = entryObj["id"]!.ToObject<Guid>();
            TestRunner.AssertEquals(targetId, entryObj["targetId"]!.ToObject<Guid>());
            TestRunner.AssertEquals(3, entryObj["magnitude"]!.ToObject<int>());
            TestRunner.AssertEquals(true, entryObj["didEngage"]!.ToObject<bool>());

            // GET entry
            var getResponse = await _runner.Http.GetAsync($"targets/{targetId}/targetentries/{entryId}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getResponse.StatusCode);
            var getObj = JObject.Parse(await getResponse.Content.ReadAsStringAsync());
            TestRunner.AssertEquals(entryId, getObj["id"]!.ToObject<Guid>());
            TestRunner.AssertEquals(targetId, getObj["targetId"]!.ToObject<Guid>());
        });
    }
}
