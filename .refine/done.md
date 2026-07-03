# Refine Done

### Upgrade vulnerable NuGet packages (High-severity CVEs)
type: bug
impact: high
effort: small
resolution: |
  All 4 projects now report "no vulnerable packages" via `dotnet list package --vulnerable --include-transitive`; unit tests 6/6 green.
  - `Microsoft.OpenApi` 2.3.0 → 2.7.5 in `Squip.Rest.csproj`. Chose 2.7.5 (first patched version for GHSA-v5pm-xwqc-g5wc in the 2.x line, and exactly what latest Swashbuckle depends on) over the recon-suggested 3.7.0 major bump — no API changes needed in `Startup.cs`.
  - The remaining High advisories (`System.Security.Cryptography.Xml` 9.0.0) traced not to `Microsoft.NET.Test.Sdk` as recon guessed, but to `Microsoft.VisualStudio.Web.CodeGeneration.Design` 10.0.1 (via `dotnet nuget why`). Bumped it to 10.0.2, which cleared them and, as a bonus, all NU1608 Microsoft.CodeAnalysis version-conflict warnings.
  - `Microsoft.NET.Test.Sdk` 18.0.1 → 18.7.0 in both test projects anyway (stays current, part of the same hygiene pass).
  - `NuGet.Packaging`/`NuGet.Protocol` still resolved to 6.12.1 through the scaffolding package; added explicit transitive pins at 6.12.5 (first patched for GHSA-g4vj-cjjj-v7hg in the 6.12.x line) in `Squip.Rest.csproj`, commented as such.

### Resolve NU1608 Microsoft.CodeAnalysis version conflicts
type: bug
impact: low
effort: small
resolution: |
  Resolved as a side effect of the CVE item above: bumping `Microsoft.VisualStudio.Web.CodeGeneration.Design` 10.0.1 → 10.0.2 aligned the Microsoft.CodeAnalysis transitive graph. Build shows zero NU1608 warnings; per this item's own notes ("if it already cleared, delete this item"), no further work done.
