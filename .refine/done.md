# Done

Completed backlog items, most recent first. Moved here from [backlog.md](backlog.md).

### Fix obsolete EF Core `GetQueryFilter()` call (CS0618)
- type: bug
- impact: medium — `Squip.Rest/Infrastructure/EntityFramework/EntityFrameworkExtensions.cs:28` is load-bearing for every entity's query filter; the obsolete API will break on a future EF Core bump.
- effort: small — single file, one method.
- notes: swap `entityType.GetQueryFilter()` for `GetDeclaredQueryFilters()` (returns a collection — combine with `AndAlso` across declared filters). Verify with `dotnet test` since query filters affect every EF query.
- resolution: replaced the single obsolete `GetQueryFilter()` call with a loop over `GetDeclaredQueryFilters()`, AND-combining each declared filter's `.Expression` the same way the old code combined the single filter. Verified via reflection that `SetQueryFilter(LambdaExpression)` (used to write back the combined filter) is not obsolete, and confirmed behavior parity with a throwaway EF InMemory harness: an entity implementing both `IArchivable` and `IUserOwnable` (the extension method is called twice, chaining filters) correctly excluded archived rows and other users' rows. Build is clean (0 warnings) and `dotnet test Squip.Rest.Tests` passes (6/6).

### Fix async void E2E test methods (xUnit1048)
- type: bug
- impact: medium — `HabitsTests.cs:16`, `IdeasTests.cs:16`, `HibitsTests.cs:22` can silently swallow assertion failures under `async void`; breaks under xUnit v3.
- effort: small — mechanical `async void` → `async Task` across 3 files.
- notes: pure signature change, no logic changes. "Hibit" is a domain term (logged occurrence of a Habit), not a typo.
- resolution: changed all three `[Fact] public async void CreateReadUpdateDelete()` methods to `public async Task CreateReadUpdateDelete()`, adding `using System.Threading.Tasks;` where missing. Pure mechanical signature change, no logic touched. `dotnet build Squip.EndToEndTests` succeeds with 0 warnings (tests themselves aren't run here — they require a live instance at `https://localhost:8080` per `[Trait("Category", "End-to-end")]`).
