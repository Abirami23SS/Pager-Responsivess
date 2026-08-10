# Error Handling — Syncfusion Blazor Components

**NOTE** > Standard Skills available in `.codestudio/skills` location.

## Principles

1. **Validate before operating** — check component settings and module availability before performing any action.
2. **Return early** — for recoverable invalid states, return silently (or with a console warning) rather than throwing.
3. **Never swallow exceptions** — empty `catch {}` blocks are forbidden.
4. **Throw only for programming errors** — e.g., an invalid argument that can never be valid.
5. **No PII in error messages** — messages must be safe to surface in browser devtools.

---

## Settings Validation Pattern

```csharp
public async Task AddRecordAsync(TValue data, int? index = null)
{
    // Return early — not a developer error, just a disabled feature
    if (!EditSettings.AllowAdding)
        return;

    if (EditModule == null)
        return; // Module not initialized yet; safe to skip

    await EditModule.AddRecord(data, index).ConfigureAwait(false);
}
```

---

## Exception Handling

```csharp
// ✅ Correct — catch specific, log context, rethrow if unrecoverable
try
{
    await JSRuntime.InvokeVoidAsync("sfGrid.init", _options);
}
catch (JSException ex)
{
    // Log for diagnostics; do not crash the component
    Console.Error.WriteLine($"[SfGrid] JS init failed: {ex.Message}");
}

// ❌ Wrong — swallowing silently
try { … } catch { }

// ❌ Wrong — catching base Exception without reason
try { … } catch (Exception) { }
```

---

## JS Interop Errors

`JSException` can occur when:
- The JS module is not yet loaded (call before `OnAfterRenderAsync`).
- The browser tab is hidden (timer-based JS calls).
- WASM memory pressure.

Always wrap JS interop calls in `try/catch (JSException)` and handle gracefully.

---

## Async Error Propagation

```csharp
// ✅ Use async/await — exceptions propagate naturally
public async Task LoadDataAsync()
{
    var result = await DataManager.ExecuteQuery<TValue>(query);
    // Exceptions from ExecuteQuery bubble up to the caller
}

// ❌ Wrong — fire-and-forget loses exceptions
_ = LoadDataAsync(); // exception silently swallowed
```

---

## Disposing Errors

Always dispose managed resources in `DisposeAsync`:

```csharp
public async ValueTask DisposeAsync()
{
    _dotNetRef?.Dispose();
    if (_module != null)
    {
        try { await _module.DisposeAsync(); }
        catch (JSDisconnectedException) { /* circuit closed — safe to ignore */ }
    }
}
```

`JSDisconnectedException` is expected on Blazor Server when the circuit closes — do not log it as an error.

---

## User-Facing Error Messages

- Keep messages concise and actionable: `"AllowAdding must be true to add records."` 
- Do not include internal stack traces in UI messages.
- Use the component's notification mechanism (Toast, Dialog) rather than JavaScript `alert()`.

