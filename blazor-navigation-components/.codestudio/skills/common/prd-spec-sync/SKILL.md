---
name: prd-spec-sync
description: >
  Synchronize product requirement documents (PRD) and technical specifications with source code
  changes after a feature or bugfix is implemented. Use this skill whenever a developer has
  completed a feature or bug fix, committed code, or is preparing/reviewing a PR/MR. Trigger on
  phrases like "sync docs", "update PRD", "update spec", "docs are out of date", "reflect code
  changes in docs", "PR is ready", "feature is done — update the docs", "committed the fix",
  "merge request ready", "sync requirements", "update documentation after code change", or any
  mention of keeping docs/prd/spec in sync with code. Also trigger when the user mentions files
  under docs/prd or docs/specs alongside source code changes, or asks to review whether
  documentation matches the current implementation. This skill ensures that docs/prd and docs/specs
  remain the single source of truth for the project.
---

# PRD & Spec Sync Skill

Keep your Product Requirement Documents (`docs/prd/`) and Technical Specifications (`docs/specs/`)
in lockstep with your codebase after every feature or bugfix lands.

Documentation drift is one of the most damaging problems in software projects. When PRDs and specs
fall out of sync with the actual implementation, teams make decisions based on stale information.
This skill treats `docs/prd` and `docs/specs` as the canonical source of truth and systematically
updates them to reflect what the code actually does.

---

## When to use this skill

- A developer has completed a feature or bug fix and committed/pushed code
- A pull request or merge request is being prepared or reviewed
- Someone asks to sync, update, or reconcile docs with code changes
- A code review reveals that documentation is stale or missing
- After a sprint or release, to ensure all docs reflect shipped work

---

## Workflow

```
1. DISCOVER  →  2. ANALYZE  →  3. MAP  →  4. UPDATE  →  5. VERIFY
```

Work through each phase sequentially. Do not skip phases — shallow discovery leads to shallow
updates. The whole point is precision.

---

## Development Execution Todo List

Copy this checklist at the start of every sync session and tick items off as you complete them.

**Phase 1 — DISCOVER**
- [ ] 1.1 Identified the diff range (branch, SHA range, or working changes)
- [ ] 1.2 Retrieved full file list with change status (`--name-status`)
- [ ] 1.3 Extracted commit messages for intent context
- [ ] 1.4 Separated source files from doc files; noted any docs already modified

**Phase 2 — ANALYZE**
- [ ] 2.1 Read diffs for all behaviorally significant files (controllers, models, config)
- [ ] 2.2 Classified every meaningful change using the classification table
- [ ] 2.3 Labelled each gap (Addition / Modification / Deprecation / Removal / Correction)

**Phase 3 — MAP**
- [ ] 3.1 Listed all existing PRD and spec documents (`docs/prd/`, `docs/specs/`)
- [ ] 3.2 Read the relevant target documents and located affected sections via `grep`
- [ ] 3.3 Decided what belongs in PRD vs spec for each gap
- [ ] 3.4 Built a written sync plan and received user confirmation before editing

**Phase 4 — UPDATE**
- [ ] 4.1 Studied conventions (headings, style, IDs, cross-refs) of each target document
- [ ] 4.2 Applied targeted edits using `str_replace` (or `create_file` for new docs)
- [ ] 4.3 Followed writing rules: precise language, preserved anchors, explicit deprecations, updated timestamps and changelog entries, added cross-references
- [ ] 4.4 Processed documents in dependency order (schemas → API contracts → architecture → PRD features → indexes)

**Phase 5 — VERIFY**
- [ ] 5.1 Checked all internal markdown links — no broken references
- [ ] 5.2 Verified terminology is consistent between PRD and spec
- [ ] 5.3 Cross-checked every API detail in the spec against the actual source code
- [ ] 5.4 Ran the completeness checklist — every gap from Phase 2.3 is addressed
- [ ] 5.5 Presented the sync summary to the user
- [ ] 5.6 Offered to commit with a conventional `docs:` commit message

---

---

## Phase 1: DISCOVER — What Changed in the Code

Goal: build a clear picture of every file that changed and why.

### Step 1.1: Identify the diff range

Ask the user or infer from context which code changes to sync. Common scenarios:

```bash
# Feature branch vs development (most common)
git diff development...HEAD --stat

# Specific commit range
git diff <base-sha>..<head-sha> --stat

# Last N commits
git diff HEAD~3 --stat

# Uncommitted working changes
git diff --stat
```

If the user says something vague like "I just finished the auth feature", ask: *"Which branch
are you on? I'll diff it against development."* Then run:

```bash
git branch --show-current
git diff development...HEAD --stat
```

### Step 1.2: Get the full file list with change status

```bash
git diff development...HEAD --name-status
```

This produces lines like `A src/api/export.ts` (added), `M src/models/user.ts` (modified),
`D src/legacy/old-export.ts` (deleted). Capture every entry.

### Step 1.3: Extract context from commit messages

Commit messages often explain *why* something changed, which matters for writing good docs.

```bash
git log development..HEAD --format="- %h %s" --reverse
```

For more detail on specific commits:

```bash
git log development..HEAD --format="### %h %s%n%b%n---" --reverse
```

### Step 1.4: Separate source code from docs

Split the changed file list into two groups:
- **Source files** — everything outside `docs/` (these drive the sync)
- **Doc files** — anything inside `docs/` (these may already be partially updated)

If doc files were already modified in the same branch, read them to understand what the developer
already updated — you may only need to fill gaps rather than start from scratch.

### Output of Phase 1

You should now have a mental model of:
- Which source files changed (and whether added/modified/deleted)
- What the commit messages say about intent
- Which components/modules are affected
- Whether any docs were already touched

---

## Phase 2: ANALYZE — Understand the Semantic Impact

Goal: read the actual code diffs and understand what changed at a behavioral level, not just
which files were touched.

### Step 2.1: Read the diffs that matter

Don't read every file — focus on files that carry behavioral or contractual changes:

```bash
# API routes, controllers, handlers
git diff development...HEAD -- "*.controller.*" "*.route.*" "*.handler.*" "**/api/**"

# Data models, schemas, migrations
git diff development...HEAD -- "**/models/**" "**/schema*" "**/migration*"

# Configuration
git diff development...HEAD -- "*.env*" "*.config.*" "*.yml" "*.yaml" "*.toml"
```

If the project doesn't follow these conventions, adapt the globs or ask the user which files
are most important.

For large diffs, use `--stat` first to prioritize, then read the top files in detail:

```bash
git diff development...HEAD --stat --sort=linesChanged | head -20
git diff development...HEAD -- <most-changed-file>
```

### Step 2.2: Classify each change

For every meaningful change, determine:

| Question                         | Why it matters for docs                         |
|----------------------------------|-------------------------------------------------|
| Is there a new API endpoint?     | Spec needs endpoint docs; PRD needs requirement  |
| Did request/response shapes change? | Spec API contracts need updating              |
| Did a data model change?         | Spec schema docs; PRD if user-facing fields      |
| Did user-facing behavior change? | PRD acceptance criteria and feature descriptions |
| Was something deprecated/removed?| Both PRD and spec need deprecation notices        |
| New config/env vars?             | Spec deployment/config section                   |
| New dependency added?            | Spec architecture/dependency section              |
| Is it a pure refactor?           | Usually spec-only (architecture); PRD unchanged  |
| Is it a bug fix?                 | Usually no PRD change; spec only if behavior was documented wrong |

### Step 2.3: Label each gap

For each change, articulate the gap between what the docs currently say and what the code now does:

- **Addition** — new capability not yet documented
- **Modification** — documented behavior that now works differently
- **Deprecation** — documented feature marked for removal
- **Removal** — documented feature fully deleted
- **Correction** — docs were already wrong; code change reveals it

---

## Phase 3: MAP — Match Changes to Document Sections

Goal: figure out exactly which documents and sections need updating before writing anything.

### Step 3.1: Survey the doc structure

```bash
# List all PRD documents
find docs/prd -type f \( -name "*.md" -o -name "*.mdx" \) | sort

# List all spec documents
find docs/specs -type f \( -name "*.md" -o -name "*.mdx" \) | sort

# Check for an index or README
cat docs/prd/README.md 2>/dev/null || cat docs/prd/index.md 2>/dev/null || echo "No PRD index"
cat docs/specs/README.md 2>/dev/null || cat docs/specs/index.md 2>/dev/null || echo "No spec index"
```

### Step 3.2: Read the relevant documents

For each gap from Phase 2, identify the most likely target document and read it:

```bash
# Read a specific doc to understand its structure and style
cat docs/specs/api-reference.md
cat docs/prd/user-management.md
```

Use `grep` to quickly locate relevant sections:

```bash
# Find where "export" is discussed across all docs
grep -rn -i "export" docs/prd/ docs/specs/ --include="*.md"

# Find API endpoint references
grep -rn "POST\|GET\|PUT\|DELETE\|PATCH" docs/specs/ --include="*.md"

# Find a specific feature or component
grep -rn -i "authentication\|auth\|login" docs/prd/ docs/specs/ --include="*.md"
```

### Step 3.3: Decide what goes where

| If the change answers...                  | Update...      | Location      |
|-------------------------------------------|----------------|---------------|
| *What does the system do and why?*        | **PRD**        | `docs/prd/`   |
| *How does the system do it?*              | **Spec**       | `docs/specs/`  |
| Both                                      | **Both**       | —             |

Concrete guidance:

**PRD gets:** requirements, user stories, acceptance criteria, business rules, feature
descriptions, success metrics, scope boundaries, rollout plans.

**Spec gets:** architecture, API contracts, data models, sequence diagrams, error handling,
performance requirements, technical constraints, implementation details, config references,
deployment notes.

### Step 3.4: Build a mapping

Before editing, lay out the full plan. Present it to the user like this:

```
Sync Plan:
─────────
1. docs/specs/api-reference.md § "User Endpoints"
   → ADD: POST /api/v2/users/export endpoint documentation

2. docs/specs/data-models.md § "Export Schema"
   → ADD: ExportRequest and ExportResponse schemas

3. docs/prd/user-management.md § "Data Export"
   → ADD: New requirement REQ-205 for bulk export
   → MODIFY: Permissions table — add export_data permission

4. docs/specs/api-reference.md § "Authentication"
   → MODIFY: Add data:export scope

No existing doc found for:
   → Export async processing flow — will add to docs/specs/api-reference.md
```

Wait for the user to confirm or adjust the plan before proceeding to edits.

---

## Phase 4: UPDATE — Write the Documentation Changes

Goal: make precise, style-consistent edits to each document.

### Step 4.1: Study the target document's conventions

Before writing a single word, read the full document (not just the target section). Note:

- Heading level hierarchy (does it use `##` for features? `###`?)
- Prose vs bullet style
- Requirement ID conventions (REQ-101? FR-1? Numbered? None?)
- Code block language annotations
- Table formatting style
- Cross-reference style (`[see X](../spec/...)` vs `(see Spec §X)`)
- Whether it has a changelog/revision history section
- Whether it has a "Last Updated" date field

Match all of these exactly. Introducing a new formatting style mid-document makes the docs feel
inconsistent and untrustworthy.

### Step 4.2: Make targeted edits

Use `str_replace` for surgical edits to existing sections. This preserves everything you didn't
intend to change and keeps the diff clean and reviewable.

For new sections, identify the right insertion point (after which existing section?) and use
`str_replace` to append after that section's content.

For entirely new documents, use `create_file` and follow the structure of neighboring docs in
the same directory.

### Step 4.3: Writing rules

**Be precise, not vague.**
- Bad: *"The export feature has been updated."*
- Good: *"The `/api/v2/users/export` endpoint accepts an optional `format` query parameter
  (`csv` | `json`, default: `csv`) and returns `202 Accepted` with a polling URL in the
  `Location` header."*

**Preserve anchors and IDs.** If sections have IDs or anchors used for cross-linking, do not
rename them without grepping for and updating all references:

```bash
# Before renaming a section, check who links to it
grep -rn "data-export" docs/ --include="*.md"
```

**Handle deprecations explicitly.** Never silently remove documented features. Mark them:

```markdown
> **Deprecated (YYYY-MM-DD):** This endpoint is deprecated and will be removed in v3.
> Use `/api/v2/users/bulk-export` instead. See [Migration Guide](../spec/migration-v3.md).
```

**Update timestamps and version markers.** If the document has a `Last Updated` field:

```bash
# Find and update date fields
grep -n "Last Updated\|Updated:\|Revision:" <doc-file>
```

Then use `str_replace` to update the date.

**Add changelog entries.** If the document has a changelog section, add an entry:

```markdown
- **YYYY-MM-DD** — [Feature/Bugfix]: Brief description. PR: #<number>
```

**Cross-reference between PRD and spec.** If a PRD update implies a spec change, link them:

```markdown
For technical details, see [API Specification — Export Endpoint](../spec/api-reference.md#export).
```

### Step 4.4: Process documents one at a time

Edit one document, verify it reads well, then move to the next. For large syncs involving 5+
documents, process in dependency order:

1. Data models / schemas (spec)
2. API contracts (spec)
3. Architecture / integration docs (spec)
4. Feature requirements (PRD)
5. Index / README files (both)

This order matters because later documents reference earlier ones.

---

## Phase 5: VERIFY — Confirm Accuracy and Completeness

Goal: catch mistakes before the user commits.

### Step 5.1: Check internal links

```bash
# Find all markdown links in docs/
grep -rn "\[.*\](.*)" docs/prd/ docs/specs/ --include="*.md" | \
  grep -v "http" | \
  while IFS=: read -r file line content; do
    # Extract link targets
    echo "$content" | grep -oP '\]\(\K[^)]+' | while read -r target; do
      # Resolve relative to file's directory
      dir=$(dirname "$file")
      resolved="$dir/$target"
      path="${resolved%%#*}"  # strip anchor
      if [ -n "$path" ] && [ ! -f "$path" ]; then
        echo "BROKEN: $file:$line → $target (resolved: $path)"
      fi
    done
  done
```

### Step 5.2: Check terminology consistency

Make sure both PRD and spec use the same names for the same things:

```bash
# Compare how a concept is referred to in PRD vs spec
grep -rhi "export" docs/prd/ --include="*.md" | head -5
grep -rhi "export" docs/specs/ --include="*.md" | head -5
```

If the PRD calls it "data export" but the spec calls it "user dump", pick one and fix the other.

### Step 5.3: Verify technical accuracy

For API changes, cross-check that every detail in the spec matches the code:

```bash
# Compare spec's endpoint docs against the actual route definition
grep -n "export" docs/specs/api-reference.md
grep -rn "export" src/routes/ src/controllers/ src/api/
```

Endpoint paths, HTTP methods, parameter names, types, default values, error codes — all of
these should match the code exactly.

### Step 5.4: Run the completeness checklist

Walk through every gap identified in Phase 2 and confirm each one was addressed. Replace the examples below with your actual gaps from Phase 2:

- [ ] Every **Addition** gap from Phase 2.3 is documented in spec or PRD
- [ ] Every **Modification** gap from Phase 2.3 is updated in the correct document
- [ ] Every **Deprecation** gap from Phase 2.3 has an explicit deprecation notice
- [ ] Every **Removal** gap from Phase 2.3 has the old entry deleted or marked removed
- [ ] Every **Correction** gap from Phase 2.3 is fixed in the relevant document
- [ ] Changelogs updated in all modified files
- [ ] Cross-references added or updated between PRD and spec

### Step 5.5: Present a sync summary to the user

```
## Sync Summary

### PRD Changes
- docs/prd/user-management.md
  - Added: "Data Export Requirements" section (REQ-205 through REQ-208)
  - Modified: "User Permissions" table — added export_data permission

### Spec Changes
- docs/specs/api-reference.md
  - Added: POST /api/v2/users/export endpoint documentation
  - Modified: Authentication section — export requires data:export scope
- docs/specs/data-models.md
  - Added: ExportRequest and ExportResponse schemas

### Changelog Entries
- docs/prd/user-management.md — YYYY-MM-DD
- docs/specs/api-reference.md — YYYY-MM-DD
- docs/specs/data-models.md — YYYY-MM-DD
```

### Step 5.6: Offer to commit

```bash
git add docs/prd/ docs/specs/
git status --short docs/

# Commit with a conventional message
git commit -m "docs: sync PRD and spec with <feature/bugfix description>

Updated:
- docs/prd/user-management.md
- docs/specs/api-reference.md
- docs/specs/data-models.md

PR: #<number>"
```

---

## Edge Cases

### No existing doc for the affected area

If the code change touches a component with no corresponding PRD or spec document:

1. Ask the user: *"There's no existing PRD/spec for [component]. Should I create a new document
   or add a section to an existing one?"*
2. If creating new docs, follow the structure of neighboring files:
   ```bash
   # See what other docs look like for reference
   head -50 docs/prd/*.md | head -100
   head -50 docs/specs/*.md | head -100
   ```
3. At minimum, create a stub so the gap is visible and searchable.

### Code contradicts the PRD

If the code implements X but the PRD says Y:

1. Flag it: *"The code does X, but the PRD requires Y. Which is the intended behavior?"*
2. Do NOT silently update the PRD to match the code — the code might be the bug.
3. Wait for the user to confirm before editing.

This is a critical judgment call. The PRD represents what the product *should* do. The code
represents what it *actually* does. They're not always the same, and assuming the code is
right can mask real bugs.

### Refactors with no behavior change

- **Spec**: may need updates if architecture, file paths, or module boundaries changed
- **PRD**: almost never needs updating — requirements haven't changed
- **Changelog**: note "Internal refactor — no behavior change"

Check for spec references to specific file paths or module names:

```bash
grep -rn "src/\|lib/\|app/" docs/specs/ --include="*.md"
```

If any of these paths changed in the refactor, update them.

### Bug fix that matches existing docs

If the docs already described the correct behavior and the code now matches:

- Usually **no doc changes needed** — the docs were already right
- Exception: if the bug was long-lived enough that the "wrong" behavior was documented somewhere,
  search for and correct those references
- Consider adding a note in the spec: *"Corrected in PR #N — [brief description]"*

### Large changes spanning many documents

For big features touching 5+ documents:

1. Complete the full mapping in Phase 3 and share it with the user before any edits
2. Process documents in the dependency order described in Phase 4
3. After every 2-3 documents, briefly confirm with the user before continuing
4. Keep a running checklist so nothing gets dropped

---

## Reference: Sync Patterns by Change Type

For detailed examples of how common code change types map to PRD and spec updates, read
`references/sync-patterns.md`. It covers: new API endpoints, schema migrations, feature flags,
bug fixes, deprecations, config changes, UI features, permission changes, third-party
integrations, and performance optimizations.

---

## Quick Reference

For small, straightforward changes where the full workflow is overkill:

- [ ] `git diff --name-status` to identify changes
- [ ] Read the diff for substance
- [ ] `grep docs/` for existing coverage
- [ ] Read target doc(s) for style
- [ ] Edit spec (technical details)
- [ ] Edit PRD (requirements/behavior)
- [ ] Update changelogs and timestamps
- [ ] Check cross-references
- [ ] Show summary to user
- [ ] Offer to commit
