using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class TagsE2E
{
    private readonly TestRunner _runner;

    public TagsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Tags");

        await _runner.RunTest("Tags: seed idea with tag → GET all tags (tag present) → GET ideas by tag", async () =>
        {
            const string tagName = "e2e-verify-tag";

            // Seed idea with tag
            var postResponse = await _runner.Http.PostAsJsonAsync("ideas", new
            {
                title = "E2E Tag Idea",
                content = "c",
                tags = new[] { tagName }
            });
            TestRunner.AssertEquals(HttpStatusCode.Created, postResponse.StatusCode);
            var ideaId = JObject.Parse(await postResponse.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

            // GET all tags — tag should be present
            var tagsResponse = await _runner.Http.GetAsync("tags");
            TestRunner.AssertEquals(HttpStatusCode.OK, tagsResponse.StatusCode);
            var tagsJson = await tagsResponse.Content.ReadAsStringAsync();
            TestRunner.Assert(tagsJson.Contains(tagName), $"Tag '{tagName}' should be in GET /tags response");

            // GET ideas by tag
            var byTagResponse = await _runner.Http.GetAsync($"tags/{tagName}/ideas");
            TestRunner.AssertEquals(HttpStatusCode.OK, byTagResponse.StatusCode);
            var byTagJson = await byTagResponse.Content.ReadAsStringAsync();
            var byTagArray = JArray.Parse(byTagJson);
            var found = false;
            foreach (var item in byTagArray)
            {
                if (item["id"]!.ToObject<Guid>() == ideaId)
                {
                    found = true;
                    break;
                }
            }
            TestRunner.Assert(found, "Seeded idea should appear in GET /tags/{tag}/ideas");

            // Cleanup
            await _runner.Http.DeleteAsync($"ideas/{ideaId}");
        });
    }
}
