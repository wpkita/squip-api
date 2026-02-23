using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Squip.Rest.Verify.EndToEnd;

public class CheckInsE2E
{
    private readonly TestRunner _runner;

    public CheckInsE2E(TestRunner runner)
    {
        _runner = runner;
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[E2E] CheckIns");

        await _runner.RunTest("CheckIns: POST mood → POST check-in → 204", async () =>
        {
            // Create a mood
            var moodResponse = await _runner.Http.PostAsJsonAsync("moods", new { name = "E2E CheckIn Mood" });
            TestRunner.AssertEquals(HttpStatusCode.Created, moodResponse.StatusCode);
            var moodId = JObject.Parse(await moodResponse.Content.ReadAsStringAsync())["id"]!.ToObject<Guid>();

            // POST check-in
            var checkInResponse = await _runner.Http.PostAsJsonAsync("checkins", new
            {
                moodEntries = new[]
                {
                    new { moodId, magnitude = 7 }
                }
            });
            TestRunner.AssertEquals(HttpStatusCode.NoContent, checkInResponse.StatusCode);
        });
    }
}
