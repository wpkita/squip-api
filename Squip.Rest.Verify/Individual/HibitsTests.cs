using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class HibitsTests
{
    private readonly TestRunner _runner;

    public HibitsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Hibits");

        Guid habitId = Guid.Empty;
        Guid hibitId = Guid.Empty;

        // Setup: create a habit
        var setupResponse = await _runner.Http.PostAsJsonAsync("habits", new { name = "Hibit Test Habit" });
        if (setupResponse.StatusCode == HttpStatusCode.Created)
        {
            var json = await setupResponse.Content.ReadAsStringAsync();
            habitId = JObject.Parse(json)["id"]!.ToObject<Guid>();
        }

        await _runner.RunTest("GET /api/habits/{habitId}/hibits → 200", async () =>
        {
            TestRunner.Assert(habitId != Guid.Empty, "Need a habit first");
            var response = await _runner.Http.GetAsync($"habits/{habitId}/hibits");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/habits/{habitId}/hibits/{nonExistentId} → 404", async () =>
        {
            TestRunner.Assert(habitId != Guid.Empty, "Need a habit first");
            var response = await _runner.Http.GetAsync($"habits/{habitId}/hibits/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        await _runner.RunTest("POST /api/habits/{habitId}/hibits → 201", async () =>
        {
            TestRunner.Assert(habitId != Guid.Empty, "Need a habit first");
            var body = new { habitId };
            var response = await _runner.Http.PostAsJsonAsync($"habits/{habitId}/hibits", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            hibitId = JObject.Parse(json)["id"]!.ToObject<Guid>();
            TestRunner.Assert(hibitId != Guid.Empty, "id should not be empty");
        });

        await _runner.RunTest("DELETE /api/habits/{habitId}/hibits/{id} → 204", async () =>
        {
            TestRunner.Assert(hibitId != Guid.Empty, "Need a hibit from POST first");
            var response = await _runner.Http.DeleteAsync($"habits/{habitId}/hibits/{hibitId}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });

        // Cleanup
        if (habitId != Guid.Empty)
            await _runner.Http.DeleteAsync($"habits/{habitId}");
    }
}
