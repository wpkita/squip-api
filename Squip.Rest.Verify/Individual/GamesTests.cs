using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class GamesTests
{
    private readonly TestRunner _runner;

    public GamesTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] Games");

        // Seed 2 ideas so GET /api/games can return a pair
        Guid idea1Id = Guid.Empty;
        Guid idea2Id = Guid.Empty;
        var r1 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "Games Seed Idea 1", content = "c", tags = Array.Empty<string>() });
        var r2 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "Games Seed Idea 2", content = "c", tags = Array.Empty<string>() });
        if (r1.StatusCode == HttpStatusCode.Created)
            idea1Id = JObject.Parse(await r1.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();
        if (r2.StatusCode == HttpStatusCode.Created)
            idea2Id = JObject.Parse(await r2.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

        Guid gameId = Guid.Empty;
        Guid winnerId = Guid.Empty;

        await _runner.RunTest("GET /api/games → 200 (requires ≥2 ideas)", async () =>
        {
            var response = await _runner.Http.GetAsync("games");
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            var obj = JObject.Parse(json);
            gameId = obj["id"]!.ToObject<Guid>();
            winnerId = obj["left"]!["id"]!.ToObject<Guid>();
        });

        await _runner.RunTest("PUT /api/games → 200", async () =>
        {
            TestRunner.Assert(gameId != Guid.Empty, "Need a game from GET first");
            var body = new { id = gameId, winnerId };
            var response = await _runner.Http.PutAsJsonAsync("games", body);
            TestRunner.AssertEquals(HttpStatusCode.OK, response.StatusCode);
        });

        // Cleanup
        if (idea1Id != Guid.Empty) await _runner.Http.DeleteAsync($"ideas/{idea1Id}");
        if (idea2Id != Guid.Empty) await _runner.Http.DeleteAsync($"ideas/{idea2Id}");
    }
}
