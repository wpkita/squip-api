using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.Individual;

public class CheckInsTests
{
    private readonly TestRunner _runner;

    public CheckInsTests(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[Individual] CheckIns");

        // Setup: create a mood
        Guid moodId = Guid.Empty;
        var moodResponse = await _runner.Http.PostAsJsonAsync("moods", new { name = "CheckIn Test Mood" });
        if (moodResponse.StatusCode == HttpStatusCode.Created)
        {
            var json = await moodResponse.Content.ReadAsStringAsync();
            moodId = JObject.Parse(json)["id"]!.ToObject<Guid>();
        }

        await _runner.RunTest("POST /api/checkins → 204", async () =>
        {
            TestRunner.Assert(moodId != Guid.Empty, "Need a mood first");
            var body = new
            {
                moodEntries = new[]
                {
                    new { moodId, magnitude = 5 }
                }
            };
            var response = await _runner.Http.PostAsJsonAsync("checkins", body);
            TestRunner.AssertEquals(HttpStatusCode.NoContent, response.StatusCode);
        });
    }
}
