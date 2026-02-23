using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class IdeasE2E
{
    private readonly TestRunner _runner;

    public IdeasE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] Ideas");

        await _runner.RunTest("Ideas: POST → GET/{id} → PUT → GET/{id} verify → DELETE → GET/{id} 404", async () =>
        {
            // POST
            var postResponse = await _runner.Http.PostAsJsonAsync("ideas", new
            {
                title = "E2E Idea",
                content = "E2E Content",
                tags = new[] { "e2etag" }
            });
            TestRunner.AssertEquals(HttpStatusCode.Created, postResponse.StatusCode);
            var postJson = await postResponse.Content.ReadAsStringAsync();
            var postObj = JObject.Parse(postJson);
            var id = postObj["id"]!.ToObject<Guid>();
            TestRunner.AssertEquals("E2E Idea", postObj["title"]!.ToString());
            TestRunner.AssertEquals("E2E Content", postObj["content"]!.ToString());

            // GET/{id}
            var getResponse = await _runner.Http.GetAsync($"ideas/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getResponse.StatusCode);
            var getObj = JObject.Parse(await getResponse.Content.ReadAsStringAsync());
            TestRunner.AssertEquals(id, getObj["id"]!.ToObject<Guid>());
            TestRunner.AssertEquals("E2E Idea", getObj["title"]!.ToString());

            // PUT
            var putResponse = await _runner.Http.PutAsJsonAsync($"ideas/{id}", new
            {
                id,
                title = "E2E Idea Updated",
                content = "E2E Content Updated",
                tags = Array.Empty<string>()
            });
            TestRunner.AssertEquals(HttpStatusCode.NoContent, putResponse.StatusCode);

            // GET/{id} verify updated
            var getAfterPut = await _runner.Http.GetAsync($"ideas/{id}");
            TestRunner.AssertEquals(HttpStatusCode.OK, getAfterPut.StatusCode);
            var getAfterPutObj = JObject.Parse(await getAfterPut.Content.ReadAsStringAsync());
            TestRunner.AssertEquals("E2E Idea Updated", getAfterPutObj["title"]!.ToString());
            TestRunner.AssertEquals("E2E Content Updated", getAfterPutObj["content"]!.ToString());

            // DELETE
            var deleteResponse = await _runner.Http.DeleteAsync($"ideas/{id}");
            TestRunner.AssertEquals(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // GET/{id} → 404
            var getAfterDelete = await _runner.Http.GetAsync($"ideas/{id}");
            TestRunner.AssertEquals(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
        });
    }
}
