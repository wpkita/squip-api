using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class GamesE2E
{
    private readonly TestRunner _runner;

    public GamesE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Games");

        await _runner.RunTest("Games: seed 2 ideas → GET game → PUT winner → verify response", async () =>
        {
            // Seed 2 ideas
            var r1 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "E2E Games Idea A", content = "c", tags = Array.Empty<string>() });
            var r2 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "E2E Games Idea B", content = "c", tags = Array.Empty<string>() });
            TestRunner.AssertEquals(HttpStatusCode.Created, r1.StatusCode, "Seed idea 1");
            TestRunner.AssertEquals(HttpStatusCode.Created, r2.StatusCode, "Seed idea 2");
            var idea1Id = JObject.Parse(await r1.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();
            var idea2Id = JObject.Parse(await r2.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

            // GET game
            var gameResponse = await _runner.Http.GetAsync("games");
            TestRunner.AssertEquals(HttpStatusCode.OK, gameResponse.StatusCode);
            var gameJson = await gameResponse.Content.ReadAsStringAsync();
            var gameObj = JObject.Parse(gameJson);
            var gameId = gameObj["id"]!.ToObject<Guid>();
            var winnerId = gameObj["left"]!["id"]!.ToObject<Guid>();
            TestRunner.Assert(gameId != Guid.Empty, "gameId should not be empty");

            // PUT winner
            var putResponse = await _runner.Http.PutAsJsonAsync("games", new { id = gameId, winnerId });
            TestRunner.AssertEquals(HttpStatusCode.OK, putResponse.StatusCode);
            var putJson = await putResponse.Content.ReadAsStringAsync();
            var putObj = JObject.Parse(putJson);
            TestRunner.AssertEquals(gameId, putObj["id"]!.ToObject<Guid>());

            // Cleanup ideas
            await _runner.Http.DeleteAsync($"ideas/{idea1Id}");
            await _runner.Http.DeleteAsync($"ideas/{idea2Id}");
        });
    }
}
