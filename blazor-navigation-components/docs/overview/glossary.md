# Glossary — Syncfusion Blazor Components

| Term | Definition |
|---|---|
| **Blazor Server** | Interactive render mode where UI runs on the server over a SignalR circuit. |
| **Blazor WASM** | Interactive WebAssembly render mode — .NET runtime runs in the browser. |
| **Blazor Auto** | Starts as Server while the WASM bundle downloads, then switches to WASM. |
| **Static SSR** | Non-interactive server-side render; no SignalR or WebSocket connection. |
| **Component Module** | A C# partial class (e.g., `FilterModule`, `EditModule`) that encapsulates optional feature logic inside a parent component. |
| **JS Module** | A lazy-loaded ES-module script file in `Scripts/modules/` (e.g., `sf-grid.js`) used via IJSInterop. |
| **IJSInterop / IJSRuntime** | Blazor's interface for calling JavaScript from C#, and vice versa. |
| **DotNetObjectReference** | A handle passed to JS so it can call back into a .NET instance method. |
| **CRG** | Custom Resource Generator — a Syncfusion tool that bundles only the JS for components actually used. |
| **ConfigureAwait(false)** | Prevents library `await` continuations from capturing the SynchronizationContext, avoiding deadlocks in Server mode. |
| **`_fieldName`** | Private field naming convention: underscore prefix + camelCase. |
| **Settings class** | A child component that configures parent behaviour (e.g., `GridEditSettings`, `GridFilterSettings`). |
| **`<see cref=""/>` tag** | XML doc cross-reference to another type or member. |
| **sf.snk** | Strong-name key file used to sign all Syncfusion.Blazor assemblies. |
| **DotCover** | JetBrains tool used for .NET code coverage measurement; reports in `CCReport/`. |
| **`config.json`** | Repo-level file mapping component packages to their dependent JS scripts, driving webpack bundling and CRG. |
| **`version.txt`** | Single source of truth for the current NuGet package version (e.g., `32.1.19`). |
| **WCAG 2.2 AA** | Web Content Accessibility Guidelines version 2.2, Level AA — the compliance target for all components. |
| **RTL** | Right-to-left text direction support (Arabic, Hebrew, etc.). |
| **Fluent 2 / Material 3 / Bootstrap 5 / Tailwind 3** | The four built-in theme families, each with normal, lite, and dark variants. |
| **`development` branch** | Main integration branch where all feature and bug-fix PRs land before release. |
| **`hotfix/{version}` branch** | Used for emergency patches to a released version. |

