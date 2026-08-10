---
name: razor-components-design
description: Mandated skill covering reusable Razor component authoring, public API design, parameter and event patterns, render-mode-aware lifecycle, templated and generic components, and XML documentation standards for the Syncfusion Blazor component library.
metadata:
  category: Component Design
  tags: [razor, components, parameters, event-callback, templated-components, generics, xml-docs, partial-class]
compatibility:
  dotnet: ["net8.0 (LTS)", "net10.0 (LTS)"]
  blazor_modes: ["Static SSR", "Interactive Server", "Interactive WASM", "Auto", "Blazor Hybrid"]
  csharp: ["C# 12", "C# 13", "C# 14"]
---

# Skill 03 — Razor Components & Component Design (Mandated)

> **Applies to:** .NET 8 (LTS) · .NET 10 (LTS)
> **Proficiency required:** Intermediate → Advanced

---

## Summary

Developers must design reusable, testable, and accessible Razor components that follow repository API conventions, documentation requirements, and the render-mode-aware patterns introduced in .NET 8 and enhanced in .NET 10.

---

## Key Competencies

### Component Structure & Parameters
- Use `[Parameter]` for public inputs; use `[CascadingParameter]` for cross-hierarchy values.
- Prefer immutable parameter objects (records/DTOs) over many individual parameters for large APIs.
- Use `[SupplyParameterFromQuery]` and `[SupplyParameterFromForm]` (introduced in .NET 8) for page-level parameters.
- Mark optional parameters with sensible defaults; document constraints and valid value ranges.

### Communication Patterns
- Use `EventCallback<T>` for child-to-parent events (automatically calls `StateHasChanged` on parent).
- Avoid `Action<T>` delegates for UI callbacks — they bypass Blazor's event system.
- Use `CascadingValue` with `IsFixed=true` when the value does not change, to avoid cascading re-renders.

### Templated & Generic Components
- Use `RenderFragment` and `RenderFragment<T>` for slot-based composition.
- Create generic components with `@typeparam TItem` and apply appropriate constraints.
- Leverage `.NET 10` improvements to cascading generic type inference when composing generics.

### Render Mode Awareness
- Components that call JS interop or use `OnAfterRender` must document that they are **not** compatible with Static SSR.
- Use `IComponentRenderMode` checks or conditional guards when behavior differs across modes.
- Avoid `@rendermode` directives inside reusable library components; leave that decision to the consuming app.

### Component Lifecycle (Ordered)
```
SetParametersAsync → OnInitialized[Async] → OnParametersSet[Async]
→ BuildRenderTree → OnAfterRender[Async] → DisposeAsync
```
- Override `ShouldRender` to gate unnecessary re-renders on complex components.
- Implement `IAsyncDisposable` for components that hold timers, subscriptions, or JS object references.

### Public API Design
- Follow **Open/Closed** principle: expose extensibility points rather than requiring inheritance.
- All public members must carry complete XML documentation (`<summary>`, `<param>`, `<returns>`, `<remarks>`, `<example>`).
- Support `class` and `style` attribute passthrough via `[Parameter(CaptureUnmatchedValues = true)]` unless intentionally restricted.

### Code Organization
- Main component file: `ComponentName.razor`
- Public properties: `ComponentName.Properties.cs` (partial class)
- Public methods: `ComponentName.Methods.cs` (partial class)
- Internal logic: keep in `internal`/private partial classes or helper services

---

## Design Guidelines (Standards)

- **Single Responsibility:** One component = one UI concern. Break large components into focused child components.
- **No side-effects in parameter setters.** Use `OnParametersSet[Async]` for derived state recalculation.
- **Prefer composition over inheritance.** Inherit only from `ComponentBase` or `OwningComponentBase`; avoid deep inheritance chains.
- **Thread safety:** Blazor Server components are per-circuit, but async re-entry can occur; use `InvokeAsync` when mutating state from background tasks.
- **Avoid `StateHasChanged` in loops.** Batch state changes and call `StateHasChanged` once.

---

## Measured Standards (Mandatory)

- [ ] All public component members have complete XML documentation and at least one `<example>` block.
- [ ] `class`/`style` passthrough via `CaptureUnmatchedValues` included unless explicitly excluded and documented.
- [ ] bUnit tests cover: default render, parameter change, event callback invocation, and dispose.
- [ ] Components document supported render modes in their README.
- [ ] No `@rendermode` directive inside library components.

---

## Minimal Artifacts (Required for New Components)

- [ ] Usage snippet in `README.md` or XML `<example>` block.
- [ ] Partial class file separation (`Properties.cs` + `Methods.cs`).
- [ ] At least two bUnit tests (initial render + parameter update).

---

## Resources

- Razor components — https://learn.microsoft.com/aspnet/core/blazor/components
- Component lifecycle — https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle
- Templated components — https://learn.microsoft.com/aspnet/core/blazor/components/templated-components
- Generic components (.NET 8) — https://learn.microsoft.com/aspnet/core/blazor/components/generic-type-constraints

