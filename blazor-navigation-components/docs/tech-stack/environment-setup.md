# Environment Setup — Syncfusion Blazor Components

## Prerequisites

| Tool | Minimum Version | Notes |
|---|---|---|
| .NET SDK | 8.0 (LTS) | Also install 9.0 and 10.0 for multi-target builds |
| Node.js | 14 LTS (≥18 recommended) | Required for webpack / gulp |
| npm | Bundled with Node.js | Use `npm ci` (not `npm install`) in CI |
| Git | Any recent | Configure line endings: `core.autocrlf=true` on Windows |
| Visual Studio | 2022 17.x | Or VS Code with C# Dev Kit |

---

## Clone & Bootstrap

```bash
git clone https://gitea.syncfusion.com/essential-studio/ej2-blazor-source.git
cd ej2-blazor-source
```

### Windows (recommended — uses pre-configured bat)

```cmd
compile.bat
```

This runs `npm ci`, then the full build pipeline automatically.

### Manual steps

```bash
# 1. Install Node dependencies
npm ci

# 2. Build all C# projects + bundle all JS
npm run build

# 3. Or build only a specific heavy component
gulp source-build
gulp build-spreadsheet
gulp build-sfpdfviewer
```

---

## Verify the Build

```bash
# Build the main NuGet project directly
dotnet build Syncfusion.Blazor/Syncfusion.Blazor.csproj

# Build a specific component package
dotnet build Syncfusion.Blazor/Grids/Syncfusion.Blazor.Grid.csproj
```

---

## Generate NuGet Packages

```bash
# Update version in version.txt first, then:
gulp update-config

# All packages
gulp generate-nuget

# Specific packages only
gulp generate-nuget --option Release --project "Syncfusion.Blazor.Core;Syncfusion.Blazor.Buttons"
```

Packages are output to the `Nuget/` folder.

---

## Run Code Analysis

```bash
npm run code-analysis
npm run gitleaks-test
npm run remove-markup-eval
```

---

## Run Accessibility Tests

```bash
npm run test-accessibility
```

---

## Code Coverage

1. Clone the test repository: `blazor-tests-automation`.
2. Reference the local `Syncfusion.Blazor` source (instead of NuGet).
3. Run: `npm run code-coverage-report-generation`
4. Open `CCReport/report.html` to view results. Target: ≥ 80% per component.

---

## Environment Variables & Secrets

- No secrets are stored in this repository.
- License key (`SyncfusionLicense`) is injected at build time via the `SyncfusionLicense` MSBuild constant — never hardcode it.
- Gitleaks scans for accidental credential commits on every PR.

---

## Troubleshooting

| Symptom | Fix |
|---|---|
| `gulp: command not found` | Run `npx gulp` or add `node_modules/.bin` to `PATH` |
| JS bundle not updated | Run `npm run bundle` |
| NuGet restore fails | Check `Syncfusion.Blazor/NuGet.config` for feed URL |
| StyleCop errors | Run `npm run code-analysis` to see full report |

