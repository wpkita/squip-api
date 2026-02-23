using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class GeneratedGamesE2E
{
    private readonly TestRunner _runner;

    public GeneratedGamesE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] GeneratedGames");

        await _runner.RunTest("GeneratedGames: seed ideas → POST → GET/{id} → PUT → GET/{id} verify → DELETE → GET/{id} 404", async () =>
        {
            // Seed 2 ideas
            var r1 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "E2E GenGame Idea 1", content = "c", tags = Array.Empty<string>() });
            var r2 = await _runner.Http.PostAsJsonAsync("ideas", new { title = "E2E GenGame Idea 2", content = "c", tags = Array.Empty<string>() });
            TestRunner.AssertEquals(HttpStatusCode.Created, r1.StatusCode, "Seed idea 1");
            TestRunner.AssertEquals(HttpStatusCode.Created, r2.StatusCode, "Seed idea 2");
            var idea1Id = JObject.Parse(await r1.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();
            var idea2Id = JObject.Parse(await r2.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

            var gameId = Guid.NewGuid();
            var gameBody = new
            {
                id = gameId,
                left = new { id = idea1Id, title = "E2E GenGame Idea 1", content = "c", tags = Array.Empty<string>() },
                right = new { id = idea2Id, title = "E2E GenGame Idea 2", content = "c", tags = Array.Empty<string>() },
                winner = (object?)null,
                loser = (object?)null
            };

            // POST
            var postResponse = await _runner.Http.PostAsJsonAsync("generatedgames", gameBody);
            TestRunner.AssertEquals(HttpStatusCode.Created, postResponse.StatusCode);
            var postJson = await postResponse.Content.ReadAsStringAsync();
            var createdId = JObject.Parse(postJson)["id"]!.ToObject<Guid>();
            TestRunner.Assert(createdId != Guid.Empty, "id should not be empty");

            // GET/{id}
            var getResponse = await _runner.Http.GetAsync($"generatedgames/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getResponse.StatusCode);
            var getObj = JObject.Parse(await getResponse.Content.ReadAsStringAsync());
            TestRunner.AssertEquals(createdId, getObj["id"]!.ToObject<Guid>());

            // PUT
            var putBody = new
            {
                id = createdId,
                left = new { id = idea1Id, title = "E2E GenGame Idea 1", content = "c", tags = Array.Empty<string>() },
                right = new { id = idea2Id, title = "E2E GenGame Idea 2", content = "c", tags = Array.Empty<string>() },
                winner = (object?)null,
                loser = (object?)null
            };
            var putResponse = await _runner.Http.PutAsJsonAsync($"generatedgames/{createdId}", putBody);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, putResponse.StatusCode);

            // GET/{id} verify
            var getAfterPut = await _runner.Http.GetAsync($"generatedgames/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getAfterPut.StatusCode);
            var getAfterPutObj = JObject.Parse(await getAfterPut.Content.ReadAsStringAsync());
            TestRunner.AssertEquals(createdId, getAfterPutObj["id"]!.ToObject<Guid>());

            // DELETE
            var deleteResponse = await _runner.Http.DeleteAsync($"generatedgames/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // GET/{id} → 404
            var getAfterDelete = await _runner.Http.GetAsync($"generatedgames/{createdId}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, getAfterDelete.StatusCode);

            // Cleanup ideas
            await _runner.Http.DeleteAsync($"ideas/{idea1Id}");
            await _runner.Http.DeleteAsync($"ideas/{idea2Id}");
        });
    }
}
