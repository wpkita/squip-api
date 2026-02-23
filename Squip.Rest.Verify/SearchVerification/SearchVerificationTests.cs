using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Squip.Rest.Verify.SearchVerification;

public class SearchVerificationTests
{
    private readonly TestRunner _runner;
    private readonly string _squipRestPath;

    public SearchVerificationTests(TestRunner runner)
    {
        _runner = runner;

        // Resolve Squip.Rest/ relative to the running executable's location
        var baseDir = AppContext.BaseDirectory;
        _squipRestPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "Squip.Rest"));
    }

    public async Task RunAll()
    {
        Console.WriteLine("\n[SearchVerification] Source Code Structure");

        await _runner.RunTest("A file matching *SquipService* exists inside Squip.Rest/", async () =>
        {
            await Task.CompletedTask;
            var files = Directory.GetFiles(_squipRestPath, "*SquipService*", SearchOption.AllDirectories);
            TestRunner.Assert(files.Length > 0, $"No file matching *SquipService* found under {_squipRestPath}");
        });

        await _runner.RunTest("Every *Controller.cs file contains 'SquipService'", async () =>
        {
            await Task.CompletedTask;
            var controllers = Directory.GetFiles(_squipRestPath, "*Controller.cs", SearchOption.AllDirectories);
            TestRunner.Assert(controllers.Length > 0, $"No *Controller.cs files found under {_squipRestPath}");
            foreach (var file in controllers)
            {
                var contents = File.ReadAllText(file);
                TestRunner.Assert(
                    contents.Contains("SquipService"),
                    $"{Path.GetFileName(file)} does not contain 'SquipService'"
                );
            }
        });

        await _runner.RunTest("No *Controller.cs file contains 'EntityFramework' or 'SquipContext'", async () =>
        {
            await Task.CompletedTask;
            var controllers = Directory.GetFiles(_squipRestPath, "*Controller.cs", SearchOption.AllDirectories);
            TestRunner.Assert(controllers.Length > 0, $"No *Controller.cs files found under {_squipRestPath}");
            foreach (var file in controllers)
            {
                var contents = File.ReadAllText(file);
                TestRunner.Assert(
                    !contents.Contains("EntityFramework"),
                    $"{Path.GetFileName(file)} contains 'EntityFramework'"
                );
                TestRunner.Assert(
                    !contents.Contains("SquipContext"),
                    $"{Path.GetFileName(file)} contains 'SquipContext'"
                );
            }
        });

        await _runner.RunTest("No files matching *Repository* exist inside Squip.Rest/", async () =>
        {
            await Task.CompletedTask;
            var files = Directory.GetFiles(_squipRestPath, "*Repository*", SearchOption.AllDirectories);
            TestRunner.Assert(files.Length == 0,
                $"Found {files.Length} file(s) matching *Repository*: {string.Join(", ", files.Select(Path.GetFileName))}");
        });

        await _runner.RunTest("Startup.cs does not contain 'UseNpgsql'", async () =>
        {
            await Task.CompletedTask;
            var startupFiles = Directory.GetFiles(_squipRestPath, "Startup.cs", SearchOption.AllDirectories);
            TestRunner.Assert(startupFiles.Length > 0, $"Startup.cs not found under {_squipRestPath}");
            foreach (var file in startupFiles)
            {
                var contents = File.ReadAllText(file);
                TestRunner.Assert(
                    !contents.Contains("UseNpgsql"),
                    $"Startup.cs contains 'UseNpgsql'"
                );
            }
        });

        await _runner.RunTest("Squip.Rest.csproj does NOT contain 'Npgsql.EntityFrameworkCore.PostgreSQL'", async () =>
        {
            await Task.CompletedTask;
            var csproj = Path.Combine(_squipRestPath, "Squip.Rest.csproj");
            TestRunner.Assert(File.Exists(csproj), $"Squip.Rest.csproj not found at {csproj}");
            var contents = File.ReadAllText(csproj);
            TestRunner.Assert(
                !contents.Contains("Npgsql.EntityFrameworkCore.PostgreSQL"),
                "Squip.Rest.csproj contains 'Npgsql.EntityFrameworkCore.PostgreSQL'"
            );
        });

        await _runner.RunTest("Squip.Rest.csproj contains 'Pomelo.EntityFrameworkCore.MySql'", async () =>
        {
            await Task.CompletedTask;
            var csproj = Path.Combine(_squipRestPath, "Squip.Rest.csproj");
            TestRunner.Assert(File.Exists(csproj), $"Squip.Rest.csproj not found at {csproj}");
            var contents = File.ReadAllText(csproj);
            TestRunner.Assert(
                contents.Contains("Pomelo.EntityFrameworkCore.MySql"),
                "Squip.Rest.csproj does not contain 'Pomelo.EntityFrameworkCore.MySql'"
            );
        });
    }
}
