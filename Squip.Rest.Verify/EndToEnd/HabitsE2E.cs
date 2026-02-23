using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class HabitsE2E
{
    private readonly TestRunner _runner;

    public HabitsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Habits");

        await _runner.RunTest("Habits: POST → GET/{id} → PUT → GET/{id} verify → DELETE → GET/{id} 404", async () =>
        {
            // POST
            var postResponse = await _runner.Http.PostAsJsonAsync("habits", new { name = "E2E Habit" });
            TestRunner.AssertEquals(HttpStatusCode.Created, postResponse.StatusCode);
            var postJson = await postResponse.Content.ReadAsStringAsync();
            var postObj = JObject.Parse(postJson);
            var id = postObj["id"]!.ToObject<Guid>();
            TestRunner.AssertEquals("E2E Habit", postObj["name"]!.ToString());

            // GET/{id}
            var getResponse = await _runner.Http.GetAsync($"habits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getResponse.StatusCode);
            var getJson = await getResponse.Content.ReadAsStringAsync();
            var getObj = JObject.Parse(getJson);
            TestRunner.AssertEquals(id, getObj["id"]!.ToObject<Guid>());
            TestRunner.AssertEquals("E2E Habit", getObj["name"]!.ToString());

            // PUT
            var putResponse = await _runner.Http.PutAsJsonAsync($"habits/{id}", new { id, name = "E2E Habit Updated" });
            TestRunner.AssertEquals(HttpStatusCode.NoContent, putResponse.StatusCode);

            // GET/{id} verify updated
            var getAfterPut = await _runner.Http.GetAsync($"habits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getAfterPut.StatusCode);
            var getAfterPutJson = await getAfterPut.Content.ReadAsStringAsync();
            var getAfterPutObj = JObject.Parse(getAfterPutJson);
            TestRunner.AssertEquals("E2E Habit Updated", getAfterPutObj["name"]!.ToString());

            // DELETE
            var deleteResponse = await _runner.Http.DeleteAsync($"habits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // GET/{id} → 404
            var getAfterDelete = await _runner.Http.GetAsync($"habits/{id}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
        });
    }
}
