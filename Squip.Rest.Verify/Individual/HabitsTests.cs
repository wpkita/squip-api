using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class HabitsTests
{
    private readonly TestRunner _runner;

    public HabitsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Habits");

        Guid createdId = Guid.Empty;

        await _runner.RunTest("GET /api/habits → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("habits");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/habits/{nonExistentId} → 404", async () =>
        {
            var response = await _runner.Http.GetAsync($"habits/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        await _runner.RunTest("POST /api/habits → 201", async () =>
        {
            var body = new { name = "Test Habit Individual" };
            var response = await _runner.Http.PostAsJsonAsync("habits", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(json);
            createdId = obj["id"]!.ToObject<Guid>();
            TestRunner.Assert(createdId != Guid.Empty, "id should not be empty");
        });

        await _runner.RunTest("PUT /api/habits/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need a habit from POST first");
            var body = new { id = createdId, name = "Updated Habit Individual" };
            var response = await _runner.Http.PutAsJsonAsync($"habits/{createdId}", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });

        await _runner.RunTest("DELETE /api/habits/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need a habit from POST first");
            var response = await _runner.Http.DeleteAsync($"habits/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
