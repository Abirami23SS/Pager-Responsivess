---
name: blazor-framework
description: Mandated skill covering Blazor render modes, component lifecycle, routing, state management, forms, error handling, and .NET 8/10 unified hosting model for developers building Blazor UI components and applications.
metadata:
  category: Core Framework
  tags: [blazor, render-modes, routing, state-management, forms, hosting, signalr, wasm, ssr]
compatibility:
  dotnet: ["net8.0 (LTS)", "net10.0 (LTS)"]
  blazor_modes: ["Static SSR", "Stream Rendering", "Interactive Server", "Interactive WASM", "Auto", "Blazor Hybrid"]
  csharp: ["C# 12", "C# 13", "C# 14"]
---

# Skill 01 — ASP.NET Core Blazor: Core Framework (Mandated)

> **Applies to:** .NET 8 (LTS) · .NET 10 (LTS)
> **Proficiency required:** Intermediate → Advanced
> **Last reviewed:** March 2026

---

## 1. Overview

ASP.NET Core Blazor is a full-stack web UI framework that lets developers build interactive web interfaces using C# and .NET instead of JavaScript. Starting with **.NET 8**, Blazor unified all hosting models into a single **Blazor Web App** project template, supporting Static SSR, Interactive Server, Interactive WebAssembly, and Auto render modes — often mixed in the same application.

Developers working in this repository must deeply understand this unified model, its render pipeline, and the constraints each mode imposes on component design, lifecycle, and service access.

> **Core principle:** Choose the **least interactive** render mode that satisfies the feature requirement. Static SSR first, streaming where needed, interactive only when the user experience demands it.

---

## 2. Hosting Models & Render Modes (.NET 8+)

### 2.1 Render Mode Reference

| Render Mode | Attribute / API | Runs On | JS Interop | Best For |
|---|---|---|---|---|
| **Static SSR** | *(none — default)* | Server (request/response) | ❌ Not available | Content pages, SEO, highest perf |
| **Stream Rendering** | `[StreamRendering]` | Server (HTTP streaming) | ❌ Not available | Async data pages, progressive load |
| **Interactive Server** | `@rendermode InteractiveServer` | Server (SignalR circuit) | ✅ After first render | Real-time UI, full .NET access |
| **Interactive WASM** | `@rendermode InteractiveWebAssembly` | Browser (.NET WASM) | ✅ After first render | Offline, edge, client-only state |
| **Interactive Auto** | `@rendermode InteractiveAuto` | Server first → WASM after download | ✅ After first render | Fast initial load + WASM |
| **Blazor Hybrid** | `BlazorWebView` (MAUI/WPF/WinForms) | Native app + embedded WebView | ✅ | Desktop/mobile native apps |

### 2.2 Configuring Render Modes (Program.cs — .NET 8+)

```csharp
// Blazor Web App — Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()        // enables Interactive Server
    .AddInteractiveWebAssemblyComponents();  // enables Interactive WASM / Auto

var app = builder.Build();

app.UseAntiforgery(); // required for .NET 8 SSR forms

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

app.Run();
```

### 2.3 Applying Render Modes

```razor
@* Per-page render mode *@
@page "/dashboard"
@rendermode InteractiveServer

@* Per-component render mode (in parent) *@
<MyWidget @rendermode="InteractiveAuto" />

@* App-level default in App.razor *@
<Routes @rendermode="InteractiveServer" />
```

> **Library Rule:** Never apply `@rendermode` inside reusable library components. The consuming application must choose the render mode. Library components must be defensively coded to work in any mode.

### 2.4 Stream Rendering (.NET 8+)

```razor
@page "/products"
@attribute [StreamRendering]   // streams HTML progressively as data resolves

@if (products is null)
{
    <p>Loading…</p>            // shown immediately while awaiting
}
else
{
    <ProductGrid Items="products" />
}

@code {
    private IReadOnlyList<Product>? products;

    protected override async Task OnInitializedAsync()
    {
        products = await ProductService.GetAllAsync();
    }
}
```

> **.NET 10 improvement:** Enhanced streaming supports partial component re-render as each item resolves, reducing time-to-first-meaningful-paint further.

---

## 3. Component Lifecycle

### 3.1 Lifecycle Order (per render)

```
SetParametersAsync
  └─ OnInitialized / OnInitializedAsync    ← runs once on first render
  └─ OnParametersSet / OnParametersSetAsync ← runs on every parameter change
  └─ BuildRenderTree (renders HTML)
  └─ OnAfterRender / OnAfterRenderAsync    ← NOT called in Static SSR
       └─ firstRender == true on first interactive render
DisposeAsync / Dispose                     ← on component removal
```

### 3.2 Lifecycle Rules & Patterns

```csharp
// ✅ Correct — initialise async data in OnInitializedAsync
protected override async Task OnInitializedAsync()
{
    data = await DataService.GetAsync();
}

// ✅ Correct — respond to parameter changes
protected override async Task OnParametersSetAsync()
{
    if (ItemId != _lastItemId)
    {
        _lastItemId = ItemId;
        item = await ItemService.GetAsync(ItemId);
    }
}

// ✅ Correct — JS interop and DOM interactions after render
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        await JS.InvokeVoidAsync("initChart", _chartRef);
    }
}

// ✅ Correct — gate re-renders on heavy components
protected override bool ShouldRender() => _isDirty;

// ✅ Correct — cleanup timers, subscriptions, JS refs
public async ValueTask DisposeAsync()
{
    _timer?.Dispose();
    if (_jsModule is not null)
        await _jsModule.DisposeAsync();
}
```

### 3.3 Anti-Patterns to Avoid

| ❌ Anti-Pattern | ✅ Correct Alternative |
|---|---|
| Fetching data in `OnAfterRender` | Use `OnInitializedAsync` |
| Calling `StateHasChanged` in a loop | Batch updates, then call once |
| JS interop in `OnInitializedAsync` | Use `OnAfterRenderAsync(firstRender)` |
| Not guarding `OnAfterRender` with `firstRender` | Always check `if (firstRender)` |
| Blocking `.Result`/`.Wait()` inside lifecycle | Use `await` throughout |

---

## 4. Dependency Injection & Service Lifetimes

### 4.1 Lifetime Behaviour Per Render Mode

| Lifetime | Static SSR | Interactive Server | Interactive WASM |
|---|---|---|---|
| `Singleton` | Shared across all requests | Shared across all circuits | Shared per WASM session |
| `Scoped` | Per HTTP request | **Per SignalR circuit** | Per WASM browser session |
| `Transient` | New per injection | New per injection | New per injection |

> **Critical:** In Interactive Server, `Scoped` services live for the entire circuit (browser tab lifetime). Never store request-specific data in a Scoped service on the server.

### 4.2 Service Registration Pattern

```csharp
// Program.cs
builder.Services.AddScoped<IUserPreferenceService, UserPreferenceService>();
builder.Services.AddSingleton<IProductCatalogCache, ProductCatalogCache>();
builder.Services.AddTransient<IReportGenerator, PdfReportGenerator>();
```

### 4.3 Per-Component Service Scope (OwningComponentBase)

```csharp
// Use OwningComponentBase<T> when a component needs its own isolated service scope
@inherits OwningComponentBase<IOrderService>

@code {
    protected override async Task OnInitializedAsync()
    {
        // Service is scoped to this component's lifetime, disposed with it
        orders = await Service.GetUserOrdersAsync();
    }
}
```

---

## 5. State Management

### 5.1 Strategy Selection Guide

| State Type | Recommended Approach |
|---|---|
| UI-local state (toggle, selection) | Component field / property |
| Shared state within a subtree | `CascadingValue<T>` + `CascadingParameter` |
| App-wide state (user session, cart) | Scoped service injected into components |
| SSR pre-render data reuse | `PersistentComponentState` (.NET 8+) |
| Browser-persisted state | `ILocalStorageService` / `ISessionStorageService` |
| Server-side long-lived state | `IMemoryCache` or distributed cache |

### 5.2 PersistentComponentState (.NET 8+)

Prevents double data-fetching when transitioning from SSR pre-render to interactive mode:

```csharp
@inject PersistentComponentState ApplicationState

@code {
    private IReadOnlyList<Product>? products;
    private PersistingComponentStateSubscription _subscription;

    protected override async Task OnInitializedAsync()
    {
        _subscription = ApplicationState.RegisterOnPersisting(Persist);

        if (!ApplicationState.TryTakeFromJson<List<Product>>("products", out var cached))
        {
            products = await ProductService.GetAllAsync(); // called only during SSR
        }
        else
        {
            products = cached; // rehydrated in interactive mode — no second fetch
        }
    }

    private Task Persist()
    {
        ApplicationState.PersistAsJson("products", products);
        return Task.CompletedTask;
    }

    public void Dispose() => _subscription.Dispose();
}
```

### 5.3 CascadingValue (App-wide state)

```razor
@* App.razor or MainLayout.razor *@
<CascadingValue Value="@appState" IsFixed="false">
    @Body
</CascadingValue>

@code {
    private AppState appState = new();
}
```

```csharp
// Child component
[CascadingParameter] private AppState AppState { get; set; } = default!;
```

> Use `IsFixed="true"` when the cascading value never changes — this prevents unnecessary re-renders of all child components.

---

## 6. Routing & Navigation (.NET 8+)

### 6.1 Page Parameters

```razor
@page "/orders/{OrderId:int}"

@* Query string binding — .NET 8+ *@
[SupplyParameterFromQuery(Name = "page")]
public int PageNumber { get; set; } = 1;

@* Form POST binding — .NET 8+ SSR forms *@
[SupplyParameterFromForm]
public OrderFilterModel? Filter { get; set; }
```

### 6.2 Enhanced Navigation (.NET 8+)

Blazor Web App enables **Enhanced Navigation** by default: internal links trigger a fetch of the new page and swap only the `<body>` content without a full browser reload — preserving scroll position and avoiding flash of unstyled content.

```razor
@* Opt-out for a specific link *@
<a href="/external-page" data-enhance-nav="false">External</a>

@* Opt-out for a form *@
<form method="post" data-enhance="false">...</form>
```

### 6.3 Navigation Guards

```razor
@* Prevent navigation away from a form with unsaved changes *@
<NavigationLock OnBeforeInternalNavigation="ConfirmNavigation" ConfirmExternalNavigation="true" />

@code {
    private async Task ConfirmNavigation(LocationChangingContext ctx)
    {
        if (_isDirty)
        {
            bool confirmed = await JS.InvokeAsync<bool>("confirm", "Discard unsaved changes?");
            if (!confirmed) ctx.PreventNavigation();
        }
    }
}
```

---

## 7. Forms & Validation

### 7.1 Interactive Forms (EditForm)

```razor
<EditForm Model="@model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <InputText @bind-Value="model.Name" />
    <ValidationMessage For="@(() => model.Name)" />

    <button type="submit">Save</button>
</EditForm>

@code {
    private readonly OrderModel model = new();

    private async Task HandleSubmit()
    {
        await OrderService.SaveAsync(model);
    }
}
```

### 7.2 SSR Forms (.NET 8+)

```razor
@page "/contact"
@inject IContactService ContactService

<form method="post" @formname="contact-form">
    <AntiforgeryToken />
    <input type="text" name="ContactModel.Name" />
    <button type="submit">Submit</button>
</form>

@code {
    [SupplyParameterFromForm]
    public ContactModel? ContactModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (ContactModel is not null)
        {
            await ContactService.SaveAsync(ContactModel);
        }
    }
}
```

> **Anti-forgery is mandatory** for SSR forms. `UseAntiforgery()` must be in the middleware pipeline and `<AntiforgeryToken />` in every SSR form.

### 7.3 FluentValidation Integration

```csharp
// Validator class
public class OrderModelValidator : AbstractValidator<OrderModel>
{
    public OrderModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}

// Registration
builder.Services.AddScoped<IValidator<OrderModel>, OrderModelValidator>();
```

---

## 8. Error Handling & Resilience

### 8.1 ErrorBoundary Component

```razor
<ErrorBoundary>
    <ChildContent>
        <RiskyComponent />
    </ChildContent>
    <ErrorContent Context="ex">
        <p class="alert alert-danger">Something went wrong: @ex.Message</p>
    </ErrorContent>
</ErrorBoundary>
```

### 8.2 Global Error Handling (Interactive Server)

```csharp
// Custom error handler for unhandled circuit exceptions
public class CircuitErrorHandler : ErrorBoundaryBase
{
    [Inject] private ILogger<CircuitErrorHandler> Logger { get; set; } = default!;

    protected override async Task OnErrorAsync(Exception exception)
    {
        Logger.LogError(exception, "Unhandled circuit error");
        await Task.CompletedTask;
    }
}
```

### 8.3 Circuit Disconnection Handling (Interactive Server)

```razor
@* In App.razor — shows reconnect UI automatically *@
<Routes />
<div id="blazor-error-ui" style="display:none">
    An error has occurred. <a href="" class="reload">Reload</a>
    <span class="dismiss">🗙</span>
</div>
```

Configure reconnect attempts in `wwwroot/index.html` or `_Host.cshtml`:
```javascript
Blazor.start({
    circuit: {
        reconnectionOptions: {
            maxRetries: 5,
            retryIntervalMilliseconds: 3000
        }
    }
});
```

---

## 9. .NET 10 Specific Improvements (LTS)

| Feature | Description |
|---|---|
| **Enhanced `[StreamRendering]`** | Per-item streaming for lists; reduces time-to-interactive further |
| **Form validation improvements** | Improved model binding pipeline with better error propagation |
| **Cascading generic type inference** | Better type inference across nested generic components |
| **QuickGrid improvements** | Sorting, filtering, and virtual scrolling enhancements |
| **WASM native AOT** | Smaller binaries, faster startup for WASM applications |
| **Reconnection UX** | Built-in better reconnection dialog for Interactive Server |

---

## 10. Measured Standards (Mandatory)

- [ ] Components declare supported render modes in XML documentation with rationale.
- [ ] `[StreamRendering]` pages handle `null` data state (loading skeleton) before async data arrives.
- [ ] SSR forms include `<AntiforgeryToken />` and `UseAntiforgery()` is in the middleware pipeline.
- [ ] `PersistentComponentState` used when SSR pre-renders data reused by an interactive component.
- [ ] No component relies on `OnAfterRender` for logic that must also function in Static SSR.
- [ ] Service lifetime mismatches caught at code review (Scoped captured in Singleton).
- [ ] `OwningComponentBase<T>` used for components that need an isolated service scope.
- [ ] `NavigationLock` applied on forms/pages with unsaved state.
- [ ] `IsFixed="true"` set on `CascadingValue` when the value is immutable.
- [ ] Library components do not apply `@rendermode`; render mode is the consumer's responsibility.

---

## 11. Minimal Artifacts (Required for New Components)

| Artifact | Requirement |
|---|---|
| Render mode table | README must list supported modes (SSR / Server / WASM / Auto) with caveats |
| Sample page | At least one sample demonstrating component in two render modes |
| Lifecycle test | bUnit or integration test covering init, parameter change, and disposal |
| PR note | Short note on render mode implications and any DI lifetime decisions |

---

## 12. Resources

| Topic | URL |
|---|---|
| Blazor Overview (.NET 8/10) | https://learn.microsoft.com/aspnet/core/blazor |
| Render modes in-depth | https://learn.microsoft.com/aspnet/core/blazor/components/render-modes |
| Component lifecycle | https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle |
| Blazor forms (.NET 8) | https://learn.microsoft.com/aspnet/core/blazor/forms |
| PersistentComponentState | https://learn.microsoft.com/aspnet/core/blazor/components/prerendering-and-integration |
| Routing & navigation | https://learn.microsoft.com/aspnet/core/blazor/fundamentals/routing |
| Dependency injection | https://learn.microsoft.com/aspnet/core/blazor/fundamentals/dependency-injection |
| .NET 10 Blazor release notes | https://learn.microsoft.com/aspnet/core/release-notes/aspnetcore-10.0#blazor |

