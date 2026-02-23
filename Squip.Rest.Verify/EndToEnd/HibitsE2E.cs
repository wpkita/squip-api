using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class HibitsE2E
{
    private readonly TestRunner _runner;

    public HibitsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Hibits");

        await _runner.RunTest("Hibits: POST habit → POST hibit → GET → DELETE → GET 404", async () =>
        {
            // Create prerequisite habit
            var habitResponse = await _runner.Http.PostAsJsonAsync("habits", new { name = "E2E Hibit Habit" });
            TestRunner.AssertEquals(HttpStatusCode.Created, habitResponse.StatusCode);
            var habitJson = await habitResponse.Content.ReadAsStringAsync();
            var habitId = JObject.Parse(habitJson)["id"]!.ToObject<Guid>();

            // POST hibit
            var postResponse = await _runner.Http.PostAsJsonAsync($"habits/{habitId}/hibits", new { habitId });
            TestRunner.AssertEquals(HttpStatusCode.Created, postResponse.StatusCode);
            var postJson = await postResponse.Content.ReadAsStringAsync();
            var postObj = JObject.Parse(postJson);
            var id = postObj["id"]!.ToObject<Guid>();
            TestRunner.AssertEquals(habitId, postObj["habitId"]!.ToObject<Guid>());

            // GET/{id}
            var getResponse = await _runner.Http.GetAsync($"habits/{habitId}/hibits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getResponse.StatusCode);
            var getJson = await getResponse.Content.ReadAsStringAsync();
            var getObj = JObject.Parse(getJson);
            TestRunner.AssertEquals(id, getObj["id"]!.ToObject<Guid>());

            // DELETE
            var deleteResponse = await _runner.Http.DeleteAsync($"habits/{habitId}/hibits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // GET/{id} → 404
            var getAfterDelete = await _runner.Http.GetAsync($"habits/{habitId}/hibits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, getAfterDelete.StatusCode);

            // Cleanup habit
            await _runner.Http.DeleteAsync($"habits/{habitId}");
        });
    }
}
