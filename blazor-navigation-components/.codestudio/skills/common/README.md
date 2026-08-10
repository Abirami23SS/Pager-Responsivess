# Skills — Developer Competency Library

## Overview

This folder contains the mandated skill definitions and standards for developers working on the Syncfusion ASP.NET Core Blazor component library. Each skill folder contains a single `SKILL.md` file that defines required competencies, measurable standards, minimal acceptance artifacts, and suggested resources.

Every `SKILL.md` file includes a YAML front-matter header with:
- **`name`** — human-readable skill name
- **`description`** — short summary of what the skill covers
- **`metadata`** — structured metadata block:
  - `category` — skill category (e.g., Core Framework, Security, Testing)
  - `tags` — list of searchable keywords for this skill
- **`compatibility`** — compatibility block:
  - `dotnet` — supported .NET runtime versions
  - `blazor_modes` — Blazor render modes this skill applies to
  - `csharp` — C# language versions covered
  - `note` *(optional)* — important compatibility caveat

## How to Use

- Each skill file includes: Summary, Key Competencies, Measured Standards, Minimal Artifacts, and Resources.
- Team leads should require contributors to meet the **Measured Standards** and attach the **Minimal Artifacts** to PRs for new components or major changes.
- Skill IDs are used in PR checklists and code review templates to reference specific competency requirements.

## Skill Index

| # | Folder | Skill Name | Category | Tags (excerpt) |
|---|--------|------------|----------|----------------|
| 01 | [blazor-framework/SKILL.md](blazor-framework/SKILL.md) | blazor-framework | Core Framework | render-modes, routing, state-management, forms, ssr |
| 02 | [csharp-dotnet/SKILL.md](csharp-dotnet/SKILL.md) | csharp-dotnet | Language & Runtime | csharp, async-await, nullable, nuget, sdk-style |
| 03 | [razor-components-design/SKILL.md](razor-components-design/SKILL.md) | razor-components-design | Component Design | razor, parameters, event-callback, generics, xml-docs |
| 04 | [html-css/SKILL.md](html-css/SKILL.md) | html-css | Frontend | html, css-isolation, css-variables, responsive, theming |
| 05 | [javascript-interop/SKILL.md](javascript-interop/SKILL.md) | javascript-interop | Interop | ijsruntime, js-modules, interop, wasm, browser-apis |
| 06 | [dotnet-security/SKILL.md](dotnet-security/SKILL.md) | dotnet-security | Security | authentication, anti-forgery, xss, csrf, csp, nuget-audit |
| 07 | [testing/SKILL.md](testing/SKILL.md) | testing | Quality & Testing | bunit, playwright, ci-cd, coverage, code-analysis |
| 08 | [accessibility-requirements/SKILL.md](accessibility-requirements/SKILL.md) | accessibility-requirements | Accessibility | wcag, aria, keyboard-navigation, axe-core, focus-management |
| 09 | [performance-optimization/SKILL.md](performance-optimization/SKILL.md) | performance-optimization | Performance | virtualize, aot, should-render, benchmarkdotnet, lighthouse |
| 10 | [deployment/SKILL.md](deployment/SKILL.md) | deployment | Deployment & Infrastructure | docker, azure, health-checks, opentelemetry, ci-cd |
| 11 | [blazor-lifecycle/SKILL.md](blazor-lifecycle/SKILL.md) | blazor-lifecycle | Core Framework | lifecycle, hooks, OnInitialized, OnAfterRender, DisposeAsync |

## Compatibility

All skills target:
- **.NET 8 (LTS)** — minimum supported runtime
- **.NET 10 (LTS)** — current latest LTS runtime
- **Blazor Web App** unified hosting model (Static SSR, Interactive Server, Interactive WASM, Auto)
- **Blazor Hybrid** (MAUI / WPF / WinForms) where noted

## Contributing

When updating skill files:
- Update the YAML front-matter `last_reviewed` date.
- Include references to canonical Microsoft documentation.
- Add a minimal example or checklist that can be validated in code reviews.
- Keep `skill_id` values stable — they are referenced in PR templates and agent definitions.

