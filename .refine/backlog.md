# Backlog

Candidate improvements, one per entry, ordered by priority (topmost is next). Items are titled, not numbered — order in the file is the priority. The loop's analysis phase appends candidates; the selection phase pops the biggest bang-for-the-buck item. Completed items move to [done.md](done.md).

## Items

### Fix async void E2E test methods (xUnit1048)
- type: bug
- impact: medium — `HabitsTests.cs:16`, `IdeasTests.cs:16`, `HibitsTests.cs:22` can silently swallow assertion failures under `async void`; breaks under xUnit v3.
- effort: small — mechanical `async void` → `async Task` across 3 files.
- notes: pure signature change, no logic changes. "Hibit" is a domain term (logged occurrence of a Habit), not a typo.

### Enforce zero-warning builds
- type: feature
- impact: medium — converts the zero-warning state into a machine gate instead of a fact that can rot; enforcement beats documentation.
- effort: small — one MSBuild property or CI-only `-warnaserror`, once the two warning fixes above land.
- notes: sequence strictly after the two items above. Prefer CI-only enforcement first to avoid local dev friction.

### README: document build, run, and test commands
- type: feature
- impact: medium — README covers only DB setup; missing what/how-to-run for anyone cloning the repo.
- effort: small — a few command blocks.
- notes: add "Running the API" and "Running tests" sections; note the `Category!=End-to-end` filter used in CI and that e2e tests need a running instance at `https://localhost:8080`.

### Parameterize E2E test base URL
- type: feature
- impact: medium — hardcoded `https://localhost:8080` is a strict prerequisite for ever running e2e tests in CI; impact is mostly unblocking that future item.
- effort: small — read base URL from env var/config with current value as default.
- notes: deliberate carve-out from the larger "wire e2e into CI" item.

### Enable nullable reference types in Squip.Rest
- type: feature
- impact: medium — `dotnet build -p:Nullable=enable` surfaces 29 warnings across ~13 files, including 3 genuine null-safety bugs (`RedisRepository.cs:24,53,61`, `EfIdeaRepository.cs:39`, `InMemoryRepository.cs:22`) plus `required`-modifier gaps on entities.
- effort: medium — one project; the repository null-dereferences need real logic review, not just annotations.
- notes: this project alone; test projects split into the next item. Full rebuild needed to see warnings (incremental hides them).

### Enable nullable reference types in test/E2E projects
- type: feature
- impact: low — test-code null-safety has less payoff; consistency follow-on.
- effort: small — small surface area.
- notes: do after Squip.Rest lands so the pattern is established.

### Bump EF Core / JwtBearer to latest patch (10.0.9)
- type: feature
- impact: low — no CVEs (vulnerable packages already upgraded); pure hygiene.
- effort: small — patch-level, minimal regression risk.
- notes: split from major-version bumps because the risk profile differs.

### Controller-level test coverage (reframed from "domain unit test gaps")
- type: feature
- impact: low — domain classes are logic-free POCOs (real logic like `EloCalculator` is already tested); the untested surface is controller date-range/summary queries, which need EF integration tests.
- effort: small as literally scoped, but low-value as unit tests.
- notes: fold into future e2e/integration-test infra work rather than writing shallow unit tests; do not execute as scoped.

### Major-version NuGet bumps (StackExchange.Redis 3.x, Microsoft.OpenApi 3.x, NuGet.Packaging 7.x)
- type: feature
- impact: low — no CVEs, no user-visible benefit.
- effort: medium/large — breaking majors with real regression risk.
- notes: defer; revisit only on CVE or a needed feature.

### Wire E2E tests into CI (needs decomposition before selection)
- type: feature
- impact: high (potential) — 3 e2e tests exist but never run automatically; `azure-pipelines.yml` filters `Category!=End-to-end`.
- effort: large — Postgres service container, API startup + migrations in CI, config wiring, removing the filter. Too large for one iteration.
- notes: must be split in a future Analyze pass (e.g. "Postgres in CI", "start API + migrations as CI step", "un-exclude filter"). The base-URL parameterization above is the already-carved slice.
