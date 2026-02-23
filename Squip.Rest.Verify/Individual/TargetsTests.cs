using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class TargetsTests
{
    private readonly TestRunner _runner;

    public TargetsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Targets");

        Guid createdId = Guid.Empty;

        await _runner.RunTest("GET /api/targets → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("targets");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/targets/{nonExistentId} → 404", async () =>
        {
            var response = await _runner.Http.GetAsync($"targets/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        await _runner.RunTest("POST /api/targets → 201", async () =>
        {
            var body = new { name = "Test Target Individual" };
            var response = await _runner.Http.PostAsJsonAsync("targets", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            createdId = JObject.Parse(json)["id"]!.ToObject<Guid>();
            TestRunner.Assert(createdId != Guid.Empty, "id should not be empty");
        });

        await _runner.RunTest("PUT /api/targets/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need a target from POST first");
            var body = new { id = createdId, name = "Updated Target Individual" };
            var response = await _runner.Http.PutAsJsonAsync($"targets/{createdId}", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
