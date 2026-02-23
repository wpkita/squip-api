using System;
using System.Threading.Tasks;
using Squip.Rest.Verify.Individual;
using Squip.Rest.Verify.EndToEnd;
using Squip.Rest.Verify.SearchVerification;

namespace Squip.Rest.Verify;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("Squip.Rest.Verify — E2E Test Runner");
        Console.WriteLine("====================================");
        Console.WriteLine("Target: https://localhost:8080");

        var runner = new TestRunner();

        // Search verification tests
        Console.WriteLine("\n=== Search Verification Tests ===");

        await new SearchVerificationTests(runner).RunAll();

        // Individual endpoint tests
        Console.WriteLine("\n=== Individual Endpoint Tests ===");

        await new HabitsTests(runner).RunAll();
        await new HibitsTests(runner).RunAll();
        await new HabitSummariesTests(runner).RunAll();
        await new DailySummariesTests(runner).RunAll();
        await new IdeasTests(runner).RunAll();
        await new GamesTests(runner).RunAll();
        await new GeneratedGamesTests(runner).RunAll();
        await new RatingsTests(runner).RunAll();
        await new TagsTests(runner).RunAll();
        await new MoodsTests(runner).RunAll();
        await new CheckInsTests(runner).RunAll();
        await new TargetsTests(runner).RunAll();
        await new TargetEntriesTests(runner).RunAll();
        await new TargetSummariesTests(runner).RunAll();
        await new LogsTests(runner).RunAll();

        // E2E flow tests
        Console.WriteLine("\n=== End-to-End Flow Tests ===");

        await new HabitsE2E(runner).RunAll();
        await new HibitsE2E(runner).RunAll();
        await new HabitSummariesE2E(runner).RunAll();
        await new DailySummariesE2E(runner).RunAll();
        await new IdeasE2E(runner).RunAll();
        await new GamesE2E(runner).RunAll();
        await new GeneratedGamesE2E(runner).RunAll();
        await new RatingsE2E(runner).RunAll();
        await new TagsE2E(runner).RunAll();
        await new MoodsE2E(runner).RunAll();
        await new CheckInsE2E(runner).RunAll();
        await new TargetsE2E(runner).RunAll();
        await new TargetEntriesE2E(runner).RunAll();
        await new TargetSummariesE2E(runner).RunAll();
        await new LogsE2E(runner).RunAll();

        // Summary
        Console.WriteLine("\n====================================");
        Console.WriteLine($"Results: {runner.Passed} passed, {runner.Failed} failed");

        if (runner.Failed == 0)
        {
            Console.WriteLine("SUCCESS");
            return 0;
        }
        else
        {
            Console.WriteLine("FAILURE");
            return 1;
        }
    }
}
