using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class GeneratedGamesTests
{
    private readonly TestRunner _runner;

    public GeneratedGamesTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] GeneratedGames");

        Guid createdId = Guid.Empty;
        Guid leftId = Guid.NewGuid();
        Guid rightId = Guid.NewGuid();

        await _runner.RunTest("GET /api/generatedgames → 200", async () =>
        {
            var response = await _runner.Http.GetAsync("generatedgames");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        await _runner.RunTest("GET /api/generatedgames/{nonExistentId} → 404", async () =>
        {
            var response = await _runner.Http.GetAsync($"generatedgames/{Guid.NewGuid()}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, response.StatusCode);
        });

        // For POST we need 2 real ideas
        Guid idea1Id = Guid.Empty;
        Guid idea2Id = Guid.Empty;
        var r1 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "GenGame Seed 1", content = "c", tags = Array.Empty<string>() });
        var r2 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "GenGame Seed 2", content = "c", tags = Array.Empty<string>() });
        if (r1.StatusCode == HttpStatusCode.Created)
            idea1Id = JObject.Parse(await r1.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();
        if (r2.StatusCode == HttpStatusCode.Created)
            idea2Id = JObject.Parse(await r2.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

        await _runner.RunTest("POST /api/generatedgames → 201", async () =>
        {
            TestRunner.Assert(idea1Id != Guid.Empty && idea2Id != Guid.Empty, "Need 2 ideas first");
            var body = new
            {
                id = Guid.NewGuid(),
                left = new { id = idea1Id, title = "GenGame Seed 1", content = "c", tags = Array.Empty<string>() },
                right = new { id = idea2Id, title = "GenGame Seed 2", content = "c", tags = Array.Empty<string>() },
                winner = (object?)null,
                loser = (object?)null
            };
            var response = await _runner.Http.PostAsJsonAsync("generatedgames", body);
            TestRunner.AssertEquals(HttpStatusCode.Created, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            createdId = JObject.Parse(json)["id"]!.ToObject<Guid>();
            TestRunner.Assert(createdId != Guid.Empty, "id should not be empty");
        });

        await _runner.RunTest("PUT /api/generatedgames/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty && idea1Id != Guid.Empty && idea2Id != Guid.Empty, "Need a game and ideas first");
            var body = new
            {
                id = createdId,
                left = new { id = idea1Id, title = "GenGame Seed 1", content = "c", tags = Array.Empty<string>() },
                right = new { id = idea2Id, title = "GenGame Seed 2", content = "c", tags = Array.Empty<string>() },
                winner = (object?)null,
                loser = (object?)null
            };
            var response = await _runner.Http.PutAsJsonAsync($"generatedgames/{createdId}", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });

        await _runner.RunTest("DELETE /api/generatedgames/{id} → 204", async () =>
        {
            TestRunner.Assert(createdId != Guid.Empty, "Need a game from POST first");
            var response = await _runner.Http.DeleteAsync($"generatedgames/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });

        // Cleanup ideas
        if (idea1Id != Guid.Empty) await _runner.Http.DeleteAsync($"ideas/{idea1Id}");
        if (idea2Id != Guid.Empty) await _runner.Http.DeleteAsync($"ideas/{idea2Id}");
    }
}
