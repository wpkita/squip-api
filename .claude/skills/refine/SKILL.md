---
name: refine
description: Iteratively improve this repository - analyze it, pick the single biggest bang-for-the-buck improvement, apply it, repeat. Use when the user asks to refine the repo, run the improvement loop, or work the .refine/ backlog.
---

# Refine

Improve this repository one focused change at a time, working from the priority-ordered backlog in `.refine/backlog.md`. Unattended operation is the primary use case: never block waiting on the user when a defensible default exists.

## The Loop

1. **Select.** Read `.refine/backlog.md`. The topmost item is next. If the backlog is empty, run an analysis pass first (step 5) to populate it.
2. **Execute.** Implement the item now, in this session. Do not merely read, restate, or analyze it. Exactly one item per iteration — never batch.
3. **Record.** Move the completed item from `.refine/backlog.md` to `.refine/done.md`, appending a `resolution:` line stating what was decided or built and why.
4. **Commit.** One commit per iteration, containing the improvement and every backlog mutation it caused. Never add co-authoring; leave the user's configured Git name and email.
5. **Analyze (as needed).** Run the Analysis Phase (below) and append its candidates to the backlog in priority order.
6. **Checkpoint.** In an interactive session, ask the user: continue to the next item, or add a new item first? If they add one, slot it into priority order yourself — never ask where it goes. In an unattended run (scheduled/headless, or a previous checkpoint went unanswered), do not ask: state the checkpoint in the iteration summary and continue immediately — interruption is the checkpoint, and a user message arriving at any time counts as its answer. One unanswered checkpoint degrades the session to unattended; never ask into the void, and never block the loop.
7. **Stop** when the user says so, or when the Stopping Criterion (below) fires.

## Analysis Phase

Run when the backlog is empty, or when instructed to refresh it.

**Recon first** (Haiku). Establish what kind of repo this is: languages, test and lint tooling, lockfiles and manifests, databases, whether it ships to users, whether it is a published package. Read the target repo's own CLAUDE.md and README — they are the source of repo-specific values, so no separate Refine configuration exists or should. Then select the applicable lenses from the catalog below. Never run the whole catalog: an inapplicable lens produces noise candidates and burns unattended budget.

**Lens catalog.** Each lens says how to check, when it applies, and Refine's stance — what "better" means. Stances are Refine's opinions; never ask the user to configure thresholds. Mechanical checks delegate to Haiku; judgment lenses run on Sonnet.

| Lens | Check | Applies when | Stance |
| --- | --- | --- | --- |
| Failing tests / linters | Run the suite and linters | Test or lint config exists | A red suite outranks every feature; failures are `type: bug` |
| TODO/FIXME/XXX markers | Grep | Always | An old marker is debt: fix it or delete it |
| Doc drift | Compare docs to files, commands, and states they reference | Docs exist | Docs that lie are worse than no docs |
| Dead code / unused deps | Repo's own tooling where it supports it | Tooling exists | Deletion is improvement |
| Test coverage gaps | Coverage tooling, or judgment over core paths | A test suite exists | Cover core behavior; a numeric target is false precision |
| Missing integration / e2e tests | Inspect the test pyramid's shape | User-facing flows exist | Every critical flow deserves one end-to-end proof |
| Committed secrets | Secret scanner or credential-pattern grep over tree and history | Always | Any committed secret is a bug: rotate, then purge |
| Vulnerable dependencies | `npm audit`, `pip-audit`, `osv-scanner`, or ecosystem equivalent | A manifest or lockfile exists | Known CVEs in shipped dependencies are bugs |
| Deprecated calls | Compiler/linter deprecation warnings, dependency changelogs | Dependencies with deprecation cycles | Migrate on your schedule, not the breakage's |
| Naming and casing consistency | Read core files | Always | The language's dominant convention wins; consistency beats preference |
| Misspellings | `codespell`/`typos` or a read-through | Always | User-facing text first, identifiers second |
| Slow paths | Profile, or inspect hot loops and N+1 patterns | Perf-sensitive paths exist | Measure before optimizing; flag only what a user would feel |
| Missing DB indexes | Schema vs. query patterns | The repo owns a database schema | Index what queries filter and join on |
| Architecture / readability | Judgment pass over core or recently-touched files | Always | Code is read more than written |
| Gap analysis | Decisions without artifacts; principles without mechanisms | Always | A stated principle with no mechanism is a bug in the project, not the prose |

Lenses surface candidates; they never set priority. The scoring rubric prices every finding, which is what makes subjective lenses safe — a finding that isn't really a problem in this repo scores low and sinks. The catalog is a floor, not a ceiling: recon may improvise a repo-specific lens when the repo's nature demands one.

**Scoring rubric.** Rate each candidate's `impact` (high/medium/low — user-visible value, or how much it unblocks future iterations) and `effort` (small/medium/large — expected size of the iteration). Qualitative ratings only; numeric scores are false precision. Priority order: impact first, lower effort breaks ties, bugs beat features on equal footing. An item too large for one iteration must be split before it enters the backlog.

**Diminishing-returns hook.** If a full pass yields only low-impact candidates, evaluate the Stopping Criterion rather than silently continuing.

## Stopping Criterion

Diminishing returns means the best available work is no longer worth an iteration. Evaluate after every Analyze pass (delegate the judgment to Opus). Stop when either fires:

- Two consecutive Analyze passes yield only low-impact candidates, or none at all.
- The last three completed iterations were all low-impact.

An empty backlog alone is not a stopping signal — it triggers Analyze. Stopping means reporting, not just halting: summarize the run's completed items, name the signal that fired, and leave remaining low-impact candidates in the backlog for a future run. Bias toward stopping: restarting is cheap; an unattended loop grinding on low-value work is not.

## Backlog Format

Items in `.refine/backlog.md` are titled `###` headings — no numbering; file order is priority order. Each carries `type` (bug | feature), `impact` and `effort` ratings with a one-line justification, and `notes` with enough context to execute the item cold.

## Model Selection

When delegating, use the bundled agents in `.claude/agents/` — their frontmatter carries the model mapping:

| Task | Agent | Model |
| --- | --- | --- |
| Repo recon, mechanical lens checks, backlog bookkeeping | `refine-recon` | Haiku |
| Candidate scoring and improvement selection | `refine-scorer` | Sonnet |
| Implementing the improvement | orchestrating session | session default |
| Diminishing-returns evaluation | `refine-stopper` | Opus |
