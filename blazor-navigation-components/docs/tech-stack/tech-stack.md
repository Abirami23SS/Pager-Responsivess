# Tech Stack — Syncfusion Blazor Components

## Language & Runtime

| Layer | Technology | Version |
|---|---|---|
| Primary language | C# | 12 (net8) / 13 (net9) / 14 (net10) |
| Markup | Razor (`.razor`, `.razor.cs`) | — |
| Secondary language | JavaScript (ES modules) | ES2020 target via Babel |
| Runtime — Server | ASP.NET Core / Blazor Server | .NET 8 / 9 / 10 |
| Runtime — Client | Blazor WebAssembly | .NET 8 / 9 / 10 |
| SDK | `Microsoft.NET.Sdk.Razor` | — |

---

## Framework & Libraries

| Package | Role |
|---|---|
| `Microsoft.AspNetCore.Components.Web` | Blazor component model |
| `Newtonsoft.Json` | JSON (legacy serialization paths) |
| `System.Text.Json` | JSON (modern, performance-sensitive paths) |
| `Syncfusion.PdfExport.Net.Core` | PDF generation for export features |
| `Syncfusion.ExcelExport.Net.Core` | Excel export (.xlsx) |
| `Syncfusion.Blazor.Data` | Data binding, adapters (OData, WebAPI, GraphQL) |

---

## Build Toolchain

| Tool | Version | Role |
|---|---|---|
| Node.js | LTS (≥ 14) | Build runtime |
| npm / `package.json` | — | Dependency & script management |
| **webpack** | ^4.35.3 | JS module bundling |
| **Babel** (`@babel/core`) | ^7.12.9 | ES transpilation |
| **Gulp** | ^4.0.2 | Build task automation |
| `gulp-dotnet-cli` | ^1.0.2 | .NET CLI invocation from Gulp |
| `webpack-bundle-analyzer` | 4.10.1 | Bundle size visualization |
| `compile.bat` | — | Windows convenience wrapper for full build |

### npm script reference

| Script | Command | Description |
|---|---|---|
| `build` | `gulp source-build && gulp build-*` | Full build: compile C# + bundle JS for all components |
| `bundle` | `gulp bundling` | Bundle JS only |
| `generate-nuget` | `gulp update-project-config && gulp generate-nuget` | Produce NuGet packages |
| `gitleaks-test` | `gulp code-leaks-analysis` | Scan for secrets/credential leaks |
| `code-analysis` | `gulp code-analysis-report && ...` | StyleCop + Roslyn analysis |
| `publish` | `gulp publish-nuget` | Publish packages to NuGet.org |
| `remove-markup-eval` | `gulp prevent-xss` | Strip eval-based markup (XSS guard) |

---

## Testing Stack

| Tool | Purpose |
|---|---|
| **bUnit** | Unit testing for Razor components |
| **Playwright** | End-to-end browser automation |
| **DotCover** | Code coverage measurement; reports in `CCReport/` |
| `npm run coverage-analysis` | Per-component coverage report |
| `npm run test-accessibility` | Automated accessibility checks |

---

## CI / CD

| Stage | Tooling |
|---|---|
| Source control | Gitea (`essential-studio/ej2-blazor-source`) |
| CI pipeline | Jenkins (`Jenkinsfile`) |
| Code quality gate | StyleCop, Roslyn analyzers, Gitleaks |
| Package distribution | NuGet.org (via `gulp publish-nuget`) |

---

## IDE / Editor

- **Visual Studio 2022** (primary) or **Visual Studio Code** with C# Dev Kit
- **Code Studio** (AI-assisted coding, code review, documentation)

