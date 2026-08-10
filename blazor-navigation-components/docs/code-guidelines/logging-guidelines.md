# Logging Guidelines — Syncfusion Blazor Components

**NOTE** > Standard Skills available in `.codestudio/skills` location.

## Context

As a **library**, Syncfusion.Blazor does not own a logging pipeline — the host application does. The library must:
- Emit diagnostic information without forcing a specific logging framework on the consumer.
- Avoid logging by default (don't pollute consumer logs).
- Never log PII, user data, or secrets.

---

## What to Log

| Scenario | Level | Where |
|---|---|---|
| JS interop failure (non-fatal) | `Warning` (via `ILogger` if injected) or `Console.Error` | JS init, module load |
| Unexpected null module (feature not configured) | `Debug` | Methods that null-check modules |
| Feature not available (setting disabled) | Nothing (return early silently) | `AddRecordAsync` etc. |
| Unrecoverable exception | `Error` → rethrow | Top-level catch |

---

## Preferred Approach — `Console.Error` for Diagnostics

Since the library cannot assume `ILogger` is available, use `Console.Error.WriteLine` for rare internal diagnostic messages only:

```csharp
catch (JSException ex)
{
    Console.Error.WriteLine($"[Syncfusion.Blazor] SfGrid JS interop error: {ex.Message}");
}
```

Prefix with `[Syncfusion.Blazor]` so developers can filter easily in browser devtools or server console.

---

## Optional `ILogger` Integration

If a component accepts an optional `ILogger<T>` (via DI), prefer it:

```csharp
[Inject] private ILogger<SfGrid<TValue>>? Logger { get; set; }

Logger?.LogWarning("SfGrid: AllowAdding is false; AddRecordAsync skipped.");
```

- Mark it optional (`?`) — consumers may not register a logger for the component namespace.
- Use log categories aligned with the component namespace (e.g., `Syncfusion.Blazor.Grids`).

---

## What NOT to Log

- User data (record values, personally identifiable information)
- Secrets, license keys, connection strings
- Verbose render-cycle information (this creates noise in production)
- Success paths (avoid `LogInformation` for routine operations)

---

## Structured Log Format (when ILogger is available)

```csharp
Logger?.LogWarning(
    "SfGrid {ComponentId} batch save skipped: AllowEditing={AllowEditing}",
    ComponentId,
    EditSettings.AllowEditing);
```

Use named placeholders (not string interpolation) so log aggregators (Seq, App Insights) can index them as structured fields.

---

## JS Console Logging

JS modules (`sf-*.js`) may log warnings to `console.warn` in development builds. All `console.log` / `console.warn` calls must be stripped (or gated behind a `DEBUG` flag) in production bundles.

