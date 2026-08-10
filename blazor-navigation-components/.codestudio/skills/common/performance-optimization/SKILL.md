---
name: performance-optimization
description: Mandated skill covering rendering performance, re-render avoidance, Virtualize component usage, Static SSR optimization, WebAssembly AOT compilation, Blazor Server SignalR tuning, memory management, and profiling tooling for .NET 8 and .NET 10 Blazor applications.
metadata:
  category: Performance
  tags: [virtualize, aot, native-aot, should-render, lazy-loading, brotli, benchmarkdotnet, dotnet-trace, lighthouse, signalr-tuning]
compatibility:
  dotnet: ["net8.0 (LTS)", "net10.0 (LTS)"]
  blazor_modes: ["Static SSR", "Stream Rendering", "Interactive Server", "Interactive WASM", "Auto"]
  csharp: ["C# 12", "C# 13", "C# 14"]
---

# Skill 09 — Performance & Profiling (Mandated)

> **Applies to:** .NET 8 (LTS) · .NET 10 (LTS)
> **Proficiency required:** Intermediate → Advanced

---

## Summary

Performance is a design concern, not an afterthought. Developers must understand rendering costs across Static SSR, Interactive Server, and WASM modes and apply the appropriate optimization strategies for each. .NET 8/10 introduces key performance primitives (Static SSR bypasses SignalR entirely, AOT compilation, native AOT publishing for WASM) that must be understood and leveraged.

---

## Key Competencies

### Rendering Performance

#### Avoid Unnecessary Re-renders
- Override `ShouldRender()` in stateless or infrequently-changing components.
- Use `@key` directive on list items to enable efficient diffing; always provide stable, unique keys.
- Prefer `EventCallback` over `Action` for callbacks — `EventCallback` only triggers re-render on the parent, not the whole tree.
- Avoid lambda closures in markup (`@onclick="() => Method(item)"` in loops creates delegates on every render); cache delegates where possible.

#### Virtualization for Large Data
- Use `<Virtualize>` component for lists > ~50 items; supply `ItemSize` for accurate scroll estimation.
- Use `ItemsProvider` delegate for server-side pagination (avoids loading full dataset).
- In .NET 8+, `<Virtualize>` works in Static SSR for initial HTML; interactive re-rendering must use virtualization too.

#### Static SSR Performance (.NET 8+)
- Static SSR has **the lowest latency and best Core Web Vitals** — prefer it for content pages.
- Use `[StreamRendering]` to progressively stream slow-loading data sections without blocking the full page.
- Minimize round-trips: pre-fetch data in the SSR pipeline rather than loading after interactive hydration.

### WebAssembly Performance (.NET 8/10)
- Enable **AOT compilation** (`<RunAOTCompilation>true</RunAOTCompilation>`) for WASM apps with CPU-intensive code.
- Use **Brotli compression** for WASM assemblies (enabled by default in `dotnet publish`).
- Leverage **lazy loading** of assemblies (`<BlazorWebAssemblyLazyLoad>`) to reduce initial bundle size.
- In .NET 10, use **native AOT for WASM** where supported for significantly smaller binaries and faster startup.
- Profile with browser devtools Performance tab; look for long tasks > 50 ms.

### Blazor Server (Interactive Server) Performance
- Minimize SignalR message size: avoid sending large objects via `EventCallback`; use IDs and fetch.
- Use `OwningComponentBase<TService>` for components that create short-lived service scopes.
- Limit concurrent circuits; configure `CircuitOptions.MaxBufferedUnacknowledgedRenderBatches`.

### Memory Management
- Implement `IAsyncDisposable` for components holding timers, subscriptions, `IJSObjectReference`, or HTTP clients.
- Use `ObjectPool<T>` or `ArrayPool<T>` for hot-path buffer allocations.
- Monitor for memory leaks in long-running Blazor Server apps with `dotnet-counters` or Application Insights live metrics.

### Tooling & Profiling
| Tool | Use Case |
|---|---|
| Browser DevTools (Performance tab) | Frame rate, long tasks, layout thrashing |
| `dotnet-counters` | Server-side GC pressure, exception rates, circuit count |
| `dotnet-trace` + SpeedScope | CPU flame charts for .NET code |
| BenchmarkDotNet | Micro-benchmarks for hot-path C# code |
| Lighthouse | Core Web Vitals, First Contentful Paint, TTI |

---

## Measured Standards (Mandatory)

- [ ] New components include a **Performance Note** in their README: estimated render cost, whether virtualization is required, and render mode recommendation.
- [ ] Lists rendering > 50 items must use `<Virtualize>` — enforced in code review.
- [ ] No `ShouldRender` returns `true` unconditionally in components known to receive frequent parameter updates.
- [ ] WASM published bundles must not increase initial download size by > 500 KB without documented justification.
- [ ] Performance-sensitive PRs include a Lighthouse score screenshot or `dotnet-trace` flame chart.

---

## Minimal Artifacts (Required for New Components)

- [ ] Performance note in README (render mode recommendation, virtualization guidance).
- [ ] BenchmarkDotNet results or Lighthouse snapshot for PRs with measurable rendering impact.

---

## Resources

- Blazor performance best practices — https://learn.microsoft.com/aspnet/core/blazor/performance
- Virtualize component — https://learn.microsoft.com/aspnet/core/blazor/components/virtualization
- WASM AOT compilation — https://learn.microsoft.com/aspnet/core/blazor/webassembly-performance-best-practices
- BenchmarkDotNet — https://benchmarkdotnet.org
- dotnet-trace — https://learn.microsoft.com/dotnet/core/diagnostics/dotnet-trace

