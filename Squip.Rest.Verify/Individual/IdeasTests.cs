using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class IdeasTests
{
    private readonly TestRunner _runner;

    public IdeasTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Ideas");

        Guid createdId = Guid.Empty;

        await _runner.RunTest("GET /api/ideas → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("ideas");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/ideas/{nonExistentId} → 404", async () =>
        {
            var response = await _runner.Http.GetAsync($"ideas/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        await _runner.RunTest("POST /api/ideas → 201", async () =>
        {
            var body = new { title = "Test Idea Individual", content = "Content", tags = Array.Empty<string>() };
            var response = await _runner.Http.PostAsJsonAsync("ideas", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            createdId = JObject.Parse(json)["id"]!.ToObject<Guid>();
            TestRunner.Assert(createdId != Guid.Empty, "id should not be empty");
        });

        await _runner.RunTest("PUT /api/ideas/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need an idea from POST first");
            var body = new { id = createdId, title = "Updated Idea Individual", content = "Updated content", tags = Array.Empty<string>() };
            var response = await _runner.Http.PutAsJsonAsync($"ideas/{createdId}", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });

        await _runner.RunTest("DELETE /api/ideas/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need an idea from POST first");
            var response = await _runner.Http.DeleteAsync($"ideas/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
