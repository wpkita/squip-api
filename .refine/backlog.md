# Refine Backlog

File order is priority order.

### Upgrade vulnerable NuGet packages (High-severity CVEs)
type: bug
impact: high — `Microsoft.OpenApi` 2.3.0 (GHSA-v5pm-xwqc-g5wc) is a direct reference actually used in production (`Startup.cs` Swagger setup), not just a dev-time tool; the rubric treats known CVEs in shipped deps as bugs by definition.
effort: small — version bumps across a handful of `.csproj` files; `Microsoft.OpenApi` usage in `Startup.cs` is narrow (OpenApiInfo/OpenApiSecurityScheme/OpenApiSecurityRequirement), so a major-version bump is contained to one file.
notes: |
  Confirmed via `dotnet list package --vulnerable --include-transitive` on all 4 projects:
  - `Microsoft.OpenApi` 2.3.0 → High (GHSA-v5pm-xwqc-g5wc). Direct reference in `Squip.Rest/Squip.Rest.csproj`. Latest is 3.7.0 (major bump — check `Squip.Rest/Startup.cs` lines ~93-147 for `OpenApiInfo`, `OpenApiSecurityScheme`, `OpenApiSecuritySchemeReference`, `OpenApiSecurityRequirement` API compat after bumping).
  - `System.Security.Cryptography.Xml` 9.0.0 → High (GHSA-37gx-xxp4-5rgx, GHSA-w3x6-4m5h-cxqf). Transitive via `Microsoft.NET.Test.Sdk` 18.0.1 in `Squip.Rest.Tests.csproj` / `Squip.EndToEndTests.csproj` (test-only, not shipped in the API). Fix by bumping `Microsoft.NET.Test.Sdk` to 18.7.0 (latest).
  - `NuGet.Packaging`/`NuGet.Protocol` 6.12.1 → Low (GHSA-g4vj-cjjj-v7hg). Same transitive chain as above; should resolve with the Test SDK bump.
  Command to re-verify after changes: `dotnet list <csproj> package --vulnerable --include-transitive` for each of the 4 `.csproj` files.

### Add end-to-end tests for Logs and Moods controllers
type: feature
impact: high — `LogsController` (recording Habit occurrences — the core loop the app is named for) and `MoodsController`/`CheckInsController` currently have zero test coverage of any kind, unit or E2E.
effort: medium — mechanical but multi-file: 3 controllers (`Squip.Rest/Logs/Controllers/LogsController.cs`, `Squip.Rest/Moods/Controllers/MoodsController.cs`, `Squip.Rest/Moods/Controllers/CheckInsController.cs`), ~150 lines of controller code total.
notes: |
  Follow the exact pattern in `Squip.EndToEndTests/HabitsTests.cs` (RestSharp + FluentAssertions + Newtonsoft.Json, base URL `https://localhost:8080/api`, one `[Fact]` per resource doing create→read→list→update→delete→verify-404). Tag classes `[Trait("Category", "End-to-end")]` so `azure-pipelines.yml`'s `--filter Category!=End-to-end` continues to exclude them from CI (these need a live Postgres per README's Database Setup section). Add `Squip.EndToEndTests/LogsTests.cs`, `MoodsTests.cs`, `CheckInsTests.cs`. Check DTOs under `Squip.Rest/Logs/Dtos` and `Squip.Rest/Moods/Dtos` for request/response shapes.

### Fix `async void` test methods to `async Task`
type: bug
impact: medium — xUnit only awaits `async Task`; an `async void` test method can throw on a background thread and the test runner reports it as a pass, silently undermining the E2E safety net other iterations (including the item above) will lean on.
effort: small — three one-word signature changes, no logic changes.
notes: |
  Change `public async void CreateReadUpdateDelete()` → `public async Task CreateReadUpdateDelete()` in:
  - `Squip.EndToEndTests/HabitsTests.cs:16`
  - `Squip.EndToEndTests/HibitsTests.cs:22`
  - `Squip.EndToEndTests/IdeasTests.cs:16`
  Add `using System.Threading.Tasks;` if not already present. Re-run `dotnet build` to confirm the xUnit analyzer warning (xUnit1030) clears.

### Migrate off obsolete `EntityType.GetQueryFilter()`
type: bug
impact: medium — this is shared infra (`AddQueryFilterToAllEntitiesAssignableFrom<T>`) that every entity's query filtering (e.g. soft-delete, tenant scoping) runs through; staying current avoids a forced scramble when EF Core removes the obsolete member outright, but it's a compiler warning today with no runtime effect.
effort: medium — not a rename: `GetDeclaredQueryFilters()` returns a collection of keyed filters (EF Core 10's multi-filter model) instead of a single `LambdaExpression`, so the AND-combination logic in the method needs a real rewrite, not find-replace.
notes: |
  File: `Squip.Rest/Infrastructure/EntityFramework/EntityFrameworkExtensions.cs:28`. Current code does `entityType.GetQueryFilter()` (obsolete) and AND-combines it with the new expression. Replace with `entityType.GetDeclaredQueryFilters()`, which returns `IEnumerable<IQueryFilter>` (or similar keyed collection depending on EF Core 10 API) — iterate and AND-combine all declared filters, not just one. Write/extend a unit test in `Squip.Rest.Tests` that builds a small `ModelBuilder` with two stacked filters (e.g. simulate soft-delete + ownership) to prove filters still combine correctly after the change, since this has no existing test coverage today.

### Add end-to-end tests for Targets controllers
type: feature
impact: medium — `TargetsController`, `TargetEntriesController`, `TargetSummariesController` are untested, but Targets is a supplementary goal-tracking feature layered on the core Habit/Log loop rather than that loop itself.
effort: medium — 3 controllers, ~173 lines of controller code, same mechanical pattern as the Logs/Moods item.
notes: |
  Same pattern as the Logs/Moods item above: base it on `Squip.EndToEndTests/HabitsTests.cs`, tag `[Trait("Category", "End-to-end")]`. Covers `Squip.Rest/Targets/Controllers/TargetsController.cs`, `TargetEntriesController.cs`, `TargetSummariesController.cs`. Check `Squip.Rest/Targets/Dtos` for request/response shapes — `TargetSummariesController` may be read-only (summary/aggregation endpoint) rather than full CRUD, so its test should only cover the actual verbs it exposes.

### Resolve NU1608 Microsoft.CodeAnalysis version conflicts
type: bug
impact: low — pure build-warning noise from a transitive version mismatch (`Microsoft.CodeAnalysis.CSharp.Features` 4.14.0 wants `Microsoft.CodeAnalysis.Common` 4.14.0, resolved to 5.0.0); compile-time only, no runtime behavior affected.
effort: small — likely a side effect of bumping `Microsoft.NET.Test.Sdk` in the CVE item above; otherwise an explicit transitive pin in the affected `.csproj`.
notes: |
  Re-run `dotnet list Squip.Rest.Tests/Squip.Rest.Tests.csproj package --include-transitive 2>&1 | grep NU1608` after the CVE item lands — if it already cleared, delete this item instead of doing extra work. If not, pin `Microsoft.CodeAnalysis.Common`/`Microsoft.CodeAnalysis.CSharp`/`Microsoft.CodeAnalysis.Workspaces.Common`/`Microsoft.CodeAnalysis.CSharp.Workspaces` to matching versions via explicit `PackageReference` entries in `Squip.Rest.Tests.csproj`.

### Enable .NET built-in analyzers
type: feature
impact: low — no analyzer/lint step exists anywhere in the repo today (`.editorconfig` only sets formatting, `azure-pipelines.yml` runs build+test with no lint gate, no `Directory.Build.props`), so this is greenfield rather than fixing a broken convention; value is prevention of future bugs, not a fix to a current one.
effort: small — add `<EnableNETAnalyzers>true</EnableNETAnalyzers>` and `<AnalysisLevel>latest-recommended</AnalysisLevel>` to each `.csproj` (or a new root `Directory.Build.props`), then triage whatever warnings surface.
notes: |
  No `Directory.Build.props` exists — create one at the repo root to apply analyzer settings to all 4 projects (`Squip.Rest`, `Squip.Rest.Tests`, `Squip.EndToEndTests`, `Squip.Importer`) at once instead of editing each `.csproj`. Start with warnings-only (do not set `TreatWarningsAsErrors`, since the existing CI script (`azure-pipelines.yml`) doesn't expect a lint gate and a cold-start error gate would likely block unrelated future iterations). Note `Squip.Importer/Squip.Importer.csproj` already has `<Nullable>enable</Nullable>` — the other three projects don't; consider whether to extend that too, but keep that as a separate call since it can surface a large number of warnings on its own.

### Document the Hibit domain term
type: feature
impact: low — purely informational; doesn't change runtime behavior or user-facing surface, but has real meta-value: recon for this very item already had to be told "Hibit is not a misspelling," meaning it will keep costing future Refine passes (and future contributors) attention until it's written down once.
effort: small — a few lines added to `README.md`.
notes: |
  Add a short glossary note to `README.md` (e.g. under a new "## Terminology" section) explaining `Hibit` = an occurrence/instance of a `Habit` (see `Squip.Rest/Habits/Domain` and the E2E test `Squip.EndToEndTests/HibitsTests.cs` for the distinction from `Habit`), specifically flagging that it is intentional and not a typo, so future contributors and automated tools don't attempt to "fix" it.
