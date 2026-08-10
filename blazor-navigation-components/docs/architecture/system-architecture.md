# System Architecture — Syncfusion Blazor Components

> For deep detail see `docs/architecture/architecture.md` (the canonical architecture reference).

---

## High-Level Overview

```
┌─────────────────────────────────────────────────────┐
│               Consumer Blazor Application           │
│   (Blazor Server / WASM / Auto / Hybrid)            │
└────────────────────────┬────────────────────────────┘
                         │ NuGet reference
┌────────────────────────▼────────────────────────────┐
│           Syncfusion.Blazor NuGet Package           │
│  ┌─────────────────────────────────────────────┐    │
│  │  Component Layer (C# / Razor)               │    │
│  │  100+ components organised by category      │    │
│  ├─────────────────────────────────────────────┤    │
│  │  JavaScript Interop Layer                   │    │
│  │  syncfusion-blazor.js + individual modules  │    │
│  ├─────────────────────────────────────────────┤    │
│  │  CSS / Themes Layer                         │    │
│  │  Fluent2, Material3, Bootstrap5, Tailwind3  │    │
│  ├─────────────────────────────────────────────┤    │
│  │  Data Layer                                 │    │
│  │  Syncfusion.Blazor.Data (adapters / LINQ)   │    │
│  └─────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────┘
```

---

## Key Subsystems

### 1. Component Layer (C# / Razor)
- Located in `Syncfusion.Blazor/` — one sub-folder per component category.
- Partial-class pattern: `ComponentName.razor` + `.razor.cs` + `.Methods.cs` + `.Properties.cs`.
- Internal feature modules accessed via null-checked properties (`FilterModule`, `EditModule`, etc.).

### 2. JavaScript Interop Layer
- All JS lives in `Scripts/modules/sf-*.js` (one file per component).
- Bundled by **webpack** into `syncfusion-blazor.js` and per-component chunks.
- Loaded on demand from `_content/Syncfusion.Blazor/scripts/`.
- CRG (Custom Resource Generator) produces custom bundles with only needed modules.

### 3. CSS / Themes Layer
- Theme CSS lives in `Syncfusion.Blazor/wwwroot/styles/`.
- Four theme families: `fluent2`, `material3`, `bootstrap5`, `tailwind3`.
- Each family ships `.css`, `-lite.css`, `-dark.css`, `-dark-lite.css`.

### 4. Data Layer (`Syncfusion.Blazor.Data`)
- `IDataBoundComponent` interface for uniform data handling.
- Adapters: OData, Web API, GraphQL, custom remote.
- Supports synchronous lists, `IEnumerable<T>`, `IQueryable<T>`, and async `DataManager`.

### 5. Build & Packaging Pipeline
```
npm ci → gulp source-build → webpack bundling → gulp generate-nuget
```
- `config.json` maps every package to its dependent JS modules.
- `version.txt` holds the current NuGet version (currently `32.1.19`).
- Multi-target: `net8.0` / `net9.0` / `net10.0`.

### 6. Testing Infrastructure
- **bUnit** unit tests in a separate [`blazor-tests-automation`](https://gitea.syncfusion.com/essential-studio/blazor-tests-automation) repo.
- **Playwright** E2E tests for interactive scenarios.
- **DotCover** code coverage; reports in `CCReport/`; target ≥ 80%.
- **StyleCop** + Roslyn analyzers enforced at compile time.

---

## Security Boundary
- Assemblies strong-name signed via `sf.snk`.
- Gitleaks runs on every PR (`npm run gitleaks-test`).
- XSS prevention via markup sanitization (`npm run remove-markup-eval`).

