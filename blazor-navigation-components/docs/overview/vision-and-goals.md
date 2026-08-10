# Vision and Goals — Syncfusion Blazor Components

## Vision

Provide the **most complete, performant, and accessible** native Blazor component suite for .NET developers — enabling teams to build enterprise-quality web applications without building common UI infrastructure from scratch.

> "True native Blazor" — built from the ground up in C#/Razor, not JavaScript wrappers.

---

## Strategic Goals

### 1. Comprehensive Component Coverage
- Maintain **100+ production-ready components** covering all common UI scenarios.
- Ship **4 major releases per year** with new components and features.

### 2. Performance Excellence
- Virtual scrolling that handles **millions of records** with no UI freeze.
- On-demand JavaScript module loading — only load scripts a page needs.
- Optimised bundle sizes via the Custom Resource Generator (CRG).

### 3. Developer Experience (DX)
- Intuitive, consistent public APIs following `async/await` patterns.
- **Complete XML documentation** (summary, param, returns, remarks, example) on every public member.
- Working, copy-paste-ready code examples in every doc comment.

### 4. Accessibility First
- **WCAG 2.2 Level AA** compliance across all components.
- Full keyboard navigation and ARIA attribute support.
- Screen-reader tested; high-contrast mode supported.

### 5. Render Mode Flexibility
- Seamless operation across Blazor **Server**, **WebAssembly**, and **Auto** render modes.
- No component requires a specific render mode unless unavoidable (documented explicitly).

### 6. Multi-Framework Support
- Target **net8.0 (LTS)**, **net9.0**, and **net10.0 (LTS)** simultaneously.
- Use C# language version per target: C# 12 / 13 / 14.

### 7. Reliability & Quality
- 80%+ automated test coverage (bUnit + Playwright).
- StyleCop + Roslyn analyzers enforced in CI.
- Security scanning via Gitleaks on every PR.

---

## Non-Goals
- Supporting non-Blazor frameworks (React, Angular) — separate EJ2 product.
- Runtime royalties or client-side telemetry.

