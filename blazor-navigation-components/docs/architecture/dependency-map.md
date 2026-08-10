# Dependency Map — Syncfusion Blazor Components

## NuGet / .NET Dependencies

### Runtime (shipped in `Syncfusion.Blazor.csproj`)

| Package | Version | Purpose |
|---|---|---|
| `Microsoft.AspNetCore.Components.Web` | 8.0.23 / 9.0.12 / 10.0.2 | Blazor framework base |
| `Newtonsoft.Json` | ≥ 13.0.2 | JSON serialization (legacy paths) |
| `System.Text.Json` | 8.0.6 / 9.0.12 / 10.0.2 | Modern JSON APIs |
| `Syncfusion.PdfExport.Net.Core` | 100.2.* | PDF generation (Grid, Report) |
| `Syncfusion.ExcelExport.Net.Core` | 21.1.100 | Excel export |
| `Syncfusion.Licensing` | 21.1.100 | License validation (conditional) |

### Target Frameworks

| TFM | C# Version |
|---|---|
| `net8.0` | C# 12 |
| `net9.0` | C# 13 |
| `net10.0` | C# 14 |

---

## Internal Package Graph

The repo contains individual `.csproj` files that are also publishable as standalone NuGet packages. Key dependency chain:

```
Syncfusion.Blazor.Core   (Base)
    ├── Syncfusion.Blazor.Buttons
    ├── Syncfusion.Blazor.Inputs
    │       └── Syncfusion.Blazor.Calendars
    │               └── Syncfusion.Blazor.DropDowns
    │                       ├── Syncfusion.Blazor.Grid
    │                       └── Syncfusion.Blazor.TreeGrid
    ├── Syncfusion.Blazor.Navigations
    │       ├── Syncfusion.Blazor.Ribbon
    │       └── Syncfusion.Blazor.FileManager
    ├── Syncfusion.Blazor.Popups
    │       └── Syncfusion.Blazor.Notifications
    ├── Syncfusion.Blazor.Data
    ├── Syncfusion.Blazor.Charts         (DataVizCommon)
    │       ├── Syncfusion.Blazor.BulletChart
    │       ├── Syncfusion.Blazor.Chart3D
    │       ├── Syncfusion.Blazor.SmithChart
    │       ├── Syncfusion.Blazor.Sparkline
    │       ├── Syncfusion.Blazor.RangeNavigator
    │       ├── Syncfusion.Blazor.StockChart
    │       └── Syncfusion.Blazor.ChartWizard
    ├── Syncfusion.Blazor.DocumentEditor
    ├── Syncfusion.Blazor.Spreadsheet
    ├── Syncfusion.Blazor.SfPdfViewer
    ├── Syncfusion.Blazor.AI             (AIBase)
    │       ├── Syncfusion.Blazor.SmartComponents
    │       ├── Syncfusion.Blazor.SmartPdfViewer
    │       └── Syncfusion.Blazor.SmartRichTextEditor
    └── Syncfusion.Blazor.Themes
```

> Full list of 60 projects is in `config.json → projects[]`.

---

## Build Tool Dependencies (devDependencies)

| Tool | Version | Purpose |
|---|---|---|
| `webpack` | ^4.35.3 | JS module bundling |
| `babel-loader` / `@babel/core` | ^7.12.x | ES transpilation |
| `gulp` | ^4.0.2 | Build automation |
| `gulp-dotnet-cli` | ^1.0.2 | .NET CLI via gulp tasks |
| `webpack-bundle-analyzer` | 4.10.1 | Bundle size inspection |
| `simple-git` | 3.19.1 | Git operations in build scripts |
| `shelljs` | ^0.8.5 | Shell commands in Node.js |

---

## JS Script Dependencies (config.json → packages)

Each NuGet package declares which JS modules it needs. Example for `SfGrid`:

```json
"SfGrid": {
  "dependencies": [
    "sf-grid.js", "sf-textbox.js", "popupsbase.js", "popup.js",
    "sf-dropdownlist.js", "navigationsbase.js", "sf-contextmenu.js",
    "sf-toolbar.js", "sf-spinner.js", "sf-calendar.js",
    "sf-datepicker.js", "sf-numerictextbox.js", "sf-dialog.js", "sf-pager.js"
  ],
  "package": "Grids"
}
```

The CRG site uses `config.json` to produce custom bundles.

---

## Updating This Map

- When adding a new NuGet dependency: update `Syncfusion.Blazor.csproj` **and** this file.
- When adding a new JS module: update `config.json → packages` **and** this file.
- Run `gulp update-config` after any `version.txt` or dependency changes.

