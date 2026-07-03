# Done

Completed backlog items, most recent first. Moved here from [backlog.md](backlog.md).

### Fix obsolete EF Core `GetQueryFilter()` call (CS0618)
- type: bug
- impact: medium — `Squip.Rest/Infrastructure/EntityFramework/EntityFrameworkExtensions.cs:28` is load-bearing for every entity's query filter; the obsolete API will break on a future EF Core bump.
- effort: small — single file, one method.
- notes: swap `entityType.GetQueryFilter()` for `GetDeclaredQueryFilters()` (returns a collection — combine with `AndAlso` across declared filters). Verify with `dotnet test` since query filters affect every EF query.
- resolution: replaced the single obsolete `GetQueryFilter()` call with a loop over `GetDeclaredQueryFilters()`, AND-combining each declared filter's `.Expression` the same way the old code combined the single filter. Verified via reflection that `SetQueryFilter(LambdaExpression)` (used to write back the combined filter) is not obsolete, and confirmed behavior parity with a throwaway EF InMemory harness: an entity implementing both `IArchivable` and `IUserOwnable` (the extension method is called twice, chaining filters) correctly excluded archived rows and other users' rows. Build is clean (0 warnings) and `dotnet test Squip.Rest.Tests` passes (6/6).
