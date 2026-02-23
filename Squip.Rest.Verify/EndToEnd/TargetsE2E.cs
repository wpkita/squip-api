using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class TargetsE2E
{
    private readonly TestRunner _runner;

    public TargetsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Targets");

        await _runner.RunTest("Targets: POST → GET all → GET/{id} → PUT → GET/{id} verify", async () =>
        {
            // POST
            var postResponse = await _runner.Http.PostAsJsonAsync("targets", new { name = "E2E Target" });
            TestRunner.AssertEquals(HttpStatusCode.Created, postResponse.StatusCode);
            var postObj = JObject.Parse(await postResponse.Content.ReadAsStringAsync());
            var id = postObj["id"]!.ToObject<Guid>();
            TestRunner.AssertEquals("E2E Target", postObj["name"]!.ToString());

            // GET all
            var getAllResponse = await _runner.Http.GetAsync("targets");
            TestRunner.AssertEquals(HttpStatusCode.OK, getAllResponse.StatusCode);
            var getAllArray = JArray.Parse(await getAllResponse.Content.ReadAsStringAsync());
            var found = false;
            foreach (var item in getAllArray)
            {
                if (item["id"]!.ToObject<Guid>() == id) { found = true; break; }
            }
            TestRunner.Assert(found, "Created target should appear in GET /targets");

            // GET/{id}
            var getResponse = await _runner.Http.GetAsync($"targets/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getResponse.StatusCode);
            var getObj = JObject.Parse(await getResponse.Content.ReadAsStringAsync());
            TestRunner.AssertEquals(id, getObj["id"]!.ToObject<Guid>());
            TestRunner.AssertEquals("E2E Target", getObj["name"]!.ToString());

            // PUT
            var putResponse = await _runner.Http.PutAsJsonAsync($"targets/{id}", new { id, name = "E2E Target Updated" });
            TestRunner.AssertEquals(HttpStatusCode.NoContent, putResponse.StatusCode);

            // GET/{id} verify
            var getAfterPut = await _runner.Http.GetAsync($"targets/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getAfterPut.StatusCode);
            var getAfterPutObj = JObject.Parse(await getAfterPut.Content.ReadAsStringAsync());
            TestRunner.AssertEquals("E2E Target Updated", getAfterPutObj["name"]!.ToString());
        });
    }
}
