# The Maturity Ladder

Fifty things a serious repository has, ranked. The Analysis Phase's maturity-ladder lens walks this list top-down and reports gaps; the scoring rubric prices each gap by its tier. The rank encodes one bias: **preventing catastrophe outranks improving velocity, which outranks improving aesthetics.** A repo with beautiful code and a leaked secret is a failed repo.

Three stances govern how the ladder is applied:

1. **Enforcement beats documentation.** A policy enforced by CI is worth ten of the same policy written in a doc. When two candidates close the same gap, the one that adds a gate outranks the one that adds a paragraph.
2. **Gates, not points.** Tier 0 items are pass/fail floors, not rungs. A failing gate is an automatic high-impact `type: bug` at the top of the backlog; no other work outranks it.
3. **Rank is conditional on repo type.** Library vs. application vs. CLI vs. docs site changes the weight of the release, API-doc, and example rungs; community rungs (19, 21, 27, 45–47) only apply to repos with outside users or contributors. Recon establishes the type; scoring adjusts. Never ding an internal app for missing API docs on internal classes.

## Tier 0 — Gates (failing any caps the repo's maturity; fix before all else)

1. **No secrets in source control** — including git history, not just HEAD. One leaked credential can end a project.
2. **A LICENSE file** — without it the code is legally unusable; an "open source" repo with no license is a liability, not a project.
3. **No known high/critical vulnerabilities in dependencies** — measurable with the ecosystem's audit tool.

## Tier 1 — Foundation (missing any means nothing downstream matters)

4. **Builds from a fresh clone with documented steps** — if `git clone && <one command>` fails, the single biggest silent killer of contributions is live.
5. **CI on every PR** — the enforcement backbone; without it every other standard is aspirational.
6. **A README answering what/why/how in the first screen** — most visitors decide in 30 seconds.
7. **Automated tests exist and run in CI** — existence before coverage; 200 meaningful tests at 40% beat 95% coverage of getters.
8. **Pinned/locked dependencies** — lockfiles committed; unpinned dependencies mean non-reproducible builds and a supply-chain attack surface.

## Tier 2 — Enforcement and automation (turn policy into physics)

9. Automated dependency updates (Dependabot/Renovate) — vulnerability scanning without an update mechanism just generates guilt.
10. Branch protection on the default branch — no direct pushes, CI must pass, review required.
11. Secret scanning in CI — gate 1 needs enforcement, not vigilance.
12. Meaningful test coverage on core logic — measure the domain layer, not the aggregate number; DTOs don't count.
13. SECURITY.md with a disclosure process — assume vulnerabilities will be found and route them privately.
14. Semantic versioning with tagged releases — untagged main-branch-only repos are unadoptable for anything serious.
15. A CHANGELOG or generated release notes — "is it safe to upgrade?" must never mean "read the diff."
16. Formatter enforced in CI — enforced, not suggested; formatting debates are the most expensive zero-value activity in software.
17. Linter with a meaningful ruleset, enforced in CI — zero unexplained suppressions; 4,000 baselined errors is a linter in name only.
18. Repository layout following ecosystem conventions — surprise structure taxes every newcomer.
19. CONTRIBUTING.md that reflects reality — setup, tests, what a good PR looks like.
20. Small, reviewable PRs as an observable norm — median PR size; giant PRs are where bugs and rubber-stamping live.
21. Issue and PR templates — cheap, and dramatically raises incoming signal quality.
22. Code review actually happening — share of merged PRs with non-author approval; self-merge culture is a maturity red flag.
23. Reproducible dev environment — devcontainer, Dockerfile, Nix, or at minimum a pinned toolchain; "works on my machine" is a maturity ceiling.
24. Static analysis beyond linting — CodeQL or language analyzers at warning-as-error; linters catch style, analyzers catch bugs.

## Tier 3 — Code health (velocity and inheritability)

25. Structured, leveled logging — and no sensitive data in logs.
26. A clear error-handling strategy — no swallowed exceptions or empty catch blocks; grep-able and hugely predictive of production behavior.
27. CODE_OF_CONDUCT.md — signals the project expects a community, not just a maintainer.
28. Fast CI (under ~10 minutes) — slow CI trains people to batch changes and skip it; speed is a correctness feature.
29. Git hooks or equivalent local gates — catch it before a failed CI run; ranked below CI because hooks are bypassable.
30. Architecture documentation, even one diagram — code says how; only docs say why.
31. ADRs — the difference between a codebase you inherit and one you archaeology.
32. Dependency injection at boundaries — not dogma, but the load-bearing wall for testability.
33. Clear layering — domain logic that doesn't reference the web framework; enforceable with architecture tests.
34. Integration tests against real-ish infrastructure — mock-only suites verify your mocks.
35. Conventional (or at least coherent) commit messages — makes changelogs generatable and bisect/blame useful.
36. No commented-out code — that's what version control is for.
37. Idiomatic naming and style for the language — C# that looks like Java is a readability tax.
38. Public API documented and published — near-top-tier for libraries; conditional on repo type.
39. Examples or quickstarts that are CI-tested — untested examples are documentation that lies.
40. Deprecation policy with warnings before removal — breaking users unwarned is the fastest way to lose them.

## Tier 4 — Polish (real, but only after the rest)

41. Configuration over hardcoding — externalized, twelve-factor, sane defaults.
42. Performance benchmarks with regression detection — only for perf-sensitive projects.
43. Mutation testing — the only honest measure of test quality; a refinement on a working test culture, not a substitute for one.
44. SBOM generation / supply-chain attestation — increasingly table stakes for security-conscious adopters.
45. Issue triage hygiene — 900 untouched issues signals abandonment even if commits are flowing.
46. Bus factor above one — single-maintainer projects are one burnout from archive status.
47. A visible roadmap or milestones — where the ship is sailing.
48. Localization/accessibility readiness where applicable — externalized strings, a11y linting for UIs.
49. Badges that are true — a green badge on a broken build is worse than no badge.
50. CITATION.cff and acknowledgment conventions — mostly for research repos; pure signal of care elsewhere.

## Checking the ladder

Prefer machine-checkable predicates, which run on Haiku: file existence (LICENSE, SECURITY.md, lockfiles, templates), CI config contents, audit-tool output, git-history scans, PR-size and review stats from the platform API. Rungs that resist a predicate (README quality, error-handling strategy, idiomatic style) are judgment calls: they run on Sonnet and must cite concrete evidence, not vibes. Where a rung overlaps a lens in SKILL.md's catalog (secrets, vulnerable dependencies, tests, coverage, doc drift, dead code), the lens is the check — the ladder only supplies the rank.
