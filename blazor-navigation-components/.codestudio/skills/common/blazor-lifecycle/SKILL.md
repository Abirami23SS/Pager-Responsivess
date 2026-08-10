---
name: blazor-lifecycle
description: Mandated skill covering the complete Blazor component lifecycle — SetParametersAsync, OnInitialized, OnParametersSet, BuildRenderTree, OnAfterRender, ShouldRender, and DisposeAsync — including correct hook usage, async patterns, render-mode constraints, and disposal contracts for .NET 8 and .NET 10.
metadata:
  category: Core Framework
  tags: [lifecycle, hooks, OnInitialized, OnParametersSet, OnAfterRender, ShouldRender, DisposeAsync, SetParametersAsync, render-pipeline, state]
compatibility:
  dotnet: ["net8.0 (LTS)", "net10.0 (LTS)"]
  blazor_modes: ["Static SSR", "Stream Rendering", "Interactive Server", "Interactive WASM", "Auto", "Blazor Hybrid"]
  csharp: ["C# 12", "C# 13", "C# 14"]
---

# Skill 11 — Blazor Component Lifecycle & Hooks (Mandated)

> **Applies to:** .NET 8 (LTS) · .NET 10 (LTS)
> **Proficiency required:** Intermediate → Advanced

---

## Summary

Understanding the Blazor component lifecycle is foundational to building correct, performant, and resilient components. Developers must know the exact order hooks fire, which hooks are available in each render mode, when async work is safe, and how to implement the disposal contract to prevent memory leaks. Misuse of lifecycle hooks is the most common source of bugs in Blazor applications.

---

## Key Competencies

### 1. Lifecycle Execution Order

Every Blazor component goes through the same ordered pipeline on each render cycle:

```
SetParametersAsync
  ├── OnInitialized / OnInitializedAsync      ← first render only
  ├── OnParametersSet / OnParametersSetAsync  ← every render (parameters changed or forced)
  ├── BuildRenderTree                         ← constructs the render tree (HTML diff)
  └── OnAfterRender / OnAfterRenderAsync      ← after DOM is updated (interactive modes only)

DisposeAsync / Dispose                        ← when component is removed from the render tree
```

> **Static SSR rule:** `OnAfterRender[Async]` is **never called** in Static SSR. Any logic placed exclusively in this hook will silently not execute on SSR pages.

---

### 2. Hook-by-Hook Reference

#### `SetParametersAsync` — Parameter Entry Point

Called before any other lifecycle method. Receives the `ParameterView` of all incoming parameters.

```csharp
// Override only when you need fine-grained control over parameter application.
// The base implementation calls OnInitialized[Async] and OnParametersSet[Async].
public override async Task SetParametersAsync(ParameterView parameters)
{
    // Read specific parameters before the base applies them all
    if (parameters.TryGetValue<string>(nameof(Title), out var title))
    {
        // Pre-process if needed
    }
    await base.SetParametersAsync(parameters);
}
```

> **When to override:** Rarely. Only when you need to intercept parameters before assignment or bypass default lifecycle dispatch. Always call `base.SetParametersAsync(parameters)` unless you are fully replacing the dispatch logic.

---

#### `OnInitialized` / `OnInitializedAsync` — Initialisation

Runs **once** — on the first render only. Use this hook to fetch initial data, subscribe to services, and set up state.

```csharp
// Sync — for cheap, synchronous initialisation only
protected override void OnInitialized()
{
    _title = $"Order #{OrderId}";
}

// Async — for data fetching, service calls, or any awaited work
protected override async Task OnInitializedAsync()
{
    _orders = await OrderService.GetByCustomerAsync(CustomerId);
}
```

**Rules:**
- Do **not** call JS interop here — the DOM does not exist yet during this hook.
- In Interactive Server with pre-rendering, `OnInitializedAsync` runs **twice**: once on the server (SSR pre-render) and once on the client (circuit connect). Guard expensive operations using `PersistentComponentState` to avoid double data-fetch.
- Never block synchronously (`.Result`, `.Wait()`).

---

#### `OnParametersSet` / `OnParametersSetAsync` — Parameter Change Response

Runs after every parameter update, including the initial render (after `OnInitialized`).

```csharp
private int _lastItemId;

protected override async Task OnParametersSetAsync()
{
    // Guard against re-fetching when unrelated parameters change
    if (ItemId != _lastItemId)
    {
        _lastItemId = ItemId;
        _item = await ItemService.GetAsync(ItemId);
    }
}
```

**Rules:**
- Always guard with a value-changed check to prevent redundant work on unrelated re-renders.
- Derived state recalculation belongs here, not in parameter property setters.
- Do not call JS interop here for the same reason as `OnInitialized`.

---

#### `ShouldRender` — Render Gate

Controls whether `BuildRenderTree` is invoked. Return `false` to skip a render cycle entirely.

```csharp
private bool _isDirty = false;

protected override bool ShouldRender() => _isDirty;

public void MarkDirty()
{
    _isDirty = true;
    StateHasChanged();
}

protected override void OnAfterRender(bool firstRender)
{
    _isDirty = false; // reset after render
}
```

**Rules:**
- `ShouldRender` is only consulted for re-renders, not the very first render.
- Never return `false` unconditionally — the component will never update.
- Use for components with expensive render trees that receive frequent, often irrelevant parameter updates.

---

#### `OnAfterRender` / `OnAfterRenderAsync` — Post-DOM Hook

Runs after the component's HTML has been committed to the DOM (browser). This is the only safe hook for JS interop.

```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        // ✅ Safe to call JS interop here
        _module = await JS.InvokeAsync<IJSObjectReference>(
            "import", "./_content/MyLib/js/chart.js");
        await _module.InvokeVoidAsync("init", _chartRef, Options);
    }
}
```

**Rules:**
- Always check `if (firstRender)` to avoid re-initialising on every re-render.
- **Not called in Static SSR.** Components that rely on `OnAfterRender` must document this constraint.
- Calling `StateHasChanged()` inside `OnAfterRender` triggers an additional render — use sparingly and always guard with a condition.
- `firstRender` is `true` only once per component instance lifetime.

---

#### `DisposeAsync` / `Dispose` — Cleanup Contract

Called when the component is removed from the render tree. **Always implement** for components that hold external resources.

```csharp
@implements IAsyncDisposable

@code {
    private IJSObjectReference? _module;
    private DotNetObjectReference<MyComponent>? _dotNetRef;
    private IDisposable? _subscription;

    public async ValueTask DisposeAsync()
    {
        _subscription?.Dispose();
        _dotNetRef?.Dispose();

        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected — safe to ignore
            }
        }
    }
}
```

**Rules:**
- Implement `IAsyncDisposable` (not `IDisposable`) if you have async cleanup (e.g., `IJSObjectReference`).
- Catch `JSDisconnectedException` when disposing JS references in Interactive Server — the circuit may already be gone.
- Dispose timers, `CancellationTokenSource`, event subscriptions, and `DotNetObjectReference<T>` instances.
- Never await long-running operations in `DisposeAsync` — dispose should be fast.

---

### 3. Async Re-entry & Thread Safety (Interactive Server)

In Interactive Server, components run on the server within a SignalR circuit. Multiple async continuations can re-enter the component. Use `InvokeAsync` when mutating component state from background threads or timer callbacks:

```csharp
private System.Threading.Timer? _timer;

protected override void OnInitialized()
{
    _timer = new System.Threading.Timer(async _ =>
    {
        // ✅ Marshal back to the Blazor synchronisation context
        await InvokeAsync(() =>
        {
            _count++;
            StateHasChanged();
        });
    }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
}
```

---

### 4. Pre-rendering Double-Execution Pattern (.NET 8+)

When Interactive Server or Auto components are pre-rendered with SSR, `OnInitializedAsync` runs twice. Prevent double data-fetching with `PersistentComponentState`:

```csharp
@inject PersistentComponentState AppState

@code {
    private IReadOnlyList<Product>? _products;
    private PersistingComponentStateSubscription _persist;

    protected override async Task OnInitializedAsync()
    {
        _persist = AppState.RegisterOnPersisting(PersistData);

        if (!AppState.TryTakeFromJson<List<Product>>("products", out var cached))
        {
            _products = await ProductService.GetAllAsync(); // SSR only
        }
        else
        {
            _products = cached; // rehydrated on client — no second fetch
        }
    }

    private Task PersistData()
    {
        AppState.PersistAsJson("products", _products);
        return Task.CompletedTask;
    }

    public void Dispose() => _persist.Dispose();
}
```

---

### 5. Lifecycle Hook Quick-Reference Table

| Hook | When | Render Modes | Async? | JS Interop? |
|---|---|---|---|---|
| `SetParametersAsync` | Before every render | All | ✅ | ❌ |
| `OnInitialized[Async]` | First render only | All | ✅ | ❌ |
| `OnParametersSet[Async]` | Every render | All | ✅ | ❌ |
| `ShouldRender` | Before re-render (not first) | Interactive | ❌ (sync only) | ❌ |
| `BuildRenderTree` | Every render | All | ❌ (sync only) | ❌ |
| `OnAfterRender[Async]` | After DOM update | Interactive only | ✅ | ✅ |
| `DisposeAsync` / `Dispose` | Component removed | All | ✅ | ⚠ Guard `JSDisconnectedException` |

---

## Measured Standards (Mandatory)

- [ ] Data fetching placed in `OnInitializedAsync` or `OnParametersSetAsync` — never in `OnAfterRender`.
- [ ] All JS interop guarded inside `OnAfterRenderAsync(bool firstRender)` with `if (firstRender)` check.
- [ ] `ShouldRender` never returns unconditional `true` in components receiving high-frequency updates.
- [ ] Components holding timers, subscriptions, `IJSObjectReference`, or `DotNetObjectReference` implement `IAsyncDisposable`.
- [ ] Pre-rendered interactive components use `PersistentComponentState` to prevent double data-fetch.
- [ ] Background state mutations in Interactive Server use `InvokeAsync(StateHasChanged)`.
- [ ] `OnParametersSet[Async]` guards against redundant work with a value-changed check.
- [ ] Components that require `OnAfterRender` declare `⚠ Requires interactive render mode` in XML documentation.

---

## Minimal Artifacts (Required for New Components)

- [ ] bUnit test covering: initial render, parameter update (verify `OnParametersSet` guard), and disposal (verify `DisposeAsync` cleans up).
- [ ] XML `<remarks>` documenting which lifecycle hooks the component relies on and any render-mode restrictions.
- [ ] If pre-rendering is supported: test demonstrating no double data-fetch using `PersistentComponentState`.

---

## Resources

| Topic | URL |
|---|---|
| Component lifecycle (official) | https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle |
| SetParametersAsync | https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle#before-parameters-are-set |
| OnAfterRender & JS interop | https://learn.microsoft.com/aspnet/core/blazor/javascript-interoperability#capture-references-to-elements |
| PersistentComponentState | https://learn.microsoft.com/aspnet/core/blazor/components/prerendering-and-integration#persist-prerendered-state |
| IAsyncDisposable | https://learn.microsoft.com/dotnet/api/system.iasyncdisposable |
| Thread safety in Blazor Server | https://learn.microsoft.com/aspnet/core/blazor/components/sync-context |
