using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class MoodsTests
{
    private readonly TestRunner _runner;

    public MoodsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Moods");

        Guid createdId = Guid.Empty;

        await _runner.RunTest("GET /api/moods → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("moods");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/moods/{nonExistentId} → 404", async () =>
        {
            var response = await _runner.Http.GetAsync($"moods/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        await _runner.RunTest("POST /api/moods → 201", async () =>
        {
            var body = new { name = "Test Mood Individual" };
            var response = await _runner.Http.PostAsJsonAsync("moods", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            createdId = JObject.Parse(json)["id"]!.ToObject<Guid>();
            TestRunner.Assert(createdId != Guid.Empty, "id should not be empty");
        });

        await _runner.RunTest("PUT /api/moods/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need a mood from POST first");
            var body = new { id = createdId, name = "Updated Mood Individual" };
            var response = await _runner.Http.PutAsJsonAsync($"moods/{createdId}", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
