# Third-Party Libraries — Syncfusion Blazor Components

## Runtime NuGet Packages

| Package | Version | License | Purpose | Upgrade Notes |
|---|---|---|---|---|
| `Microsoft.AspNetCore.Components.Web` | 8.0.23 / 9.0.12 / 10.0.2 | MIT | Blazor component model | Pin to matching .NET SDK version per TFM |
| `Newtonsoft.Json` | ≥ 13.0.2 | MIT | JSON serialization (legacy) | Do not upgrade past 13.x without regression testing |
| `System.Text.Json` | 8.0.6 / 9.0.12 / 10.0.2 | MIT | Modern JSON APIs | Pin to matching .NET SDK version per TFM |
| `Syncfusion.PdfExport.Net.Core` | 100.2.* | Commercial (Syncfusion) | PDF generation | Floating minor (`100.2.*`) — test on every update |
| `Syncfusion.ExcelExport.Net.Core` | 21.1.100 | Commercial (Syncfusion) | Excel export | Align with NuGet release cycle |
| `Syncfusion.Licensing` | 21.1.100 | Commercial (Syncfusion) | License key validation | Conditional — only when `SyncfusionLicense` constant defined |

---

## Build / Dev Tool npm Packages

| Package | Version | License | Purpose |
|---|---|---|---|
| `webpack` | ^4.35.3 | MIT | JS module bundling |
| `@babel/core` | ^7.12.9 | MIT | ES transpilation |
| `@babel/preset-env` | ^7.12.7 | MIT | Transpile to browser targets |
| `babel-loader` | ^8.2.1 | MIT | Webpack Babel integration |
| `gulp` | ^4.0.2 | MIT | Build automation |
| `gulp-dotnet-cli` | ^1.0.2 | MIT | .NET CLI from Gulp tasks |
| `webpack-bundle-analyzer` | 4.10.1 | MIT | Visualize bundle composition |
| `webpack-merge` | 4.2.2 | MIT | Merge webpack configs |
| `simple-git` | 3.19.1 | MIT | Git ops in build scripts |
| `shelljs` | ^0.8.5 | BSD-3-Clause | Cross-platform shell commands |
| `del` | ^6.1.1 | MIT | Delete build artifacts |
| `ghooks` | ^2.0.4 | MIT | Git hook management |
| `table` | ^5.4.6 | BSD-3-Clause | Tabular output in reports |
| `jsonpscriptsrc-webpack-plugin` | ^1.0.0 | MIT | Custom JSONP chunk src |

### Pinned Overrides (security)
```json
"overrides": {
  "glob-parent": "6.0.2",
  "semver": "7.6.0"
}
```
These are pinned to resolve known CVEs in transitive dependencies. Review on every `npm audit`.

---

## JavaScript Interop Modules

All JS in `Scripts/modules/sf-*.js` is **authored by Syncfusion** — there are no third-party JS libraries bundled directly. Some component modules (PDF viewer, spreadsheet, document editor) use Syncfusion's own compiled workers.

---

## Adding a New Third-Party Library

1. Evaluate license compatibility (MIT / Apache-2.0 preferred; GPL incompatible).
2. Run `npm audit` after adding.
3. Add entry to this file with version, license, and purpose.
4. If it affects bundle size, document impact in `docs/performance/benchmarks.md`.

