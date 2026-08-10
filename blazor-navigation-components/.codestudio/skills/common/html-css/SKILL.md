---
name: html-css
description: Mandated skill covering semantic HTML, ARIA integration, CSS isolation, CSS custom properties, responsive design, theming systems, and performance-safe animation patterns for Blazor UI component authors targeting .NET 8 and .NET 10.
metadata:
  category: Frontend
  tags: [html, css, css-isolation, css-variables, aria, responsive, theming, flexbox, grid, animations]
compatibility:
  dotnet: ["net8.0 (LTS)", "net10.0 (LTS)"]
  blazor_modes: ["Static SSR", "Stream Rendering", "Interactive Server", "Interactive WASM", "Auto"]
  csharp: ["C# 12", "C# 13", "C# 14"]
---

# Skill 04 — HTML & CSS for Component Authors (Mandated)

> **Applies to:** .NET 8 (LTS) · .NET 10 (LTS) — Blazor Web App project template
> **Proficiency required:** Intermediate

---

## Summary

Component authors must produce semantically correct, accessible, responsive, and themeable HTML/CSS. In .NET 8+ Blazor Web Apps, HTML is served directly from the Static SSR pipeline for many pages, making markup quality and CSS scoping more critical than ever.

---

## Key Competencies

### Semantic HTML
- Use the right element for the right job: `<button>` for actions, `<a>` for navigation, `<nav>`, `<main>`, `<aside>`, `<section>`, `<article>`, `<header>`, `<footer>` for structure.
- Never use `<div>` or `<span>` for interactive controls; they lack keyboard and accessibility semantics.
- Provide `<label>` for every form input; use `for`/`id` association or wrap input in `<label>`.

### ARIA & Accessibility Integration
- Add `role`, `aria-label`, `aria-describedby`, `aria-live`, `aria-expanded`, and `aria-controls` attributes where native semantics are insufficient.
- Manage `aria-busy="true"` during async loading states (especially relevant for `[StreamRendering]` pages).

### CSS Architecture
- **CSS Isolation (Blazor):** Use `.razor.css` scoped stylesheets for component-level styles; understand the `b-*` scope attribute injection.
- **CSS Custom Properties (Variables):** Define design tokens for colors, spacing, and typography at `:root`; consume in component stylesheets to enable theming.
- **Flexbox & Grid:** Use Flexbox for one-dimensional layouts; CSS Grid for two-dimensional. Avoid legacy float-based layouts.
- **Responsive Design:** Mobile-first breakpoints using `min-width` media queries. Components must be responsive at `320px` minimum width.

### Theming System
- Use CSS custom properties for all color and spacing tokens; do not hardcode values in component styles.
- Support Light and Dark themes via `prefers-color-scheme` media query and/or explicit theme class on `<html>`.
- Respect `.NET 8` Blazor Web App default template conventions for theme variables when working in-tree.

### Performance & Best Practices
- Minimize CSS specificity; prefer class selectors over ID or element selectors.
- Avoid `!important` unless overriding third-party styles; document when used.
- Use `will-change` and `contain` CSS properties for animations; avoid layout-triggering animations.
- Prefer `transform`/`opacity` for animations (GPU composited, no layout cost).

---

## Measured Standards (Mandatory)

- [ ] All interactive components pass `axe` automated accessibility check with zero critical or serious violations.
- [ ] Styles use `.razor.css` isolation or a clearly documented namespace; no unscoped global styles introduced without approval.
- [ ] Components render correctly at `320px` (mobile) and `1440px` (desktop) viewport widths.
- [ ] Color tokens reference CSS custom properties; no hardcoded color values in `.razor.css` files.
- [ ] No `<div>` used as a button, link, or interactive control.

---

## Minimal Artifacts (Required for New Components)

- [ ] `.razor.css` file demonstrating CSS variable usage and at least one responsive rule.
- [ ] Screenshots or automated visual regression snapshot at mobile and desktop breakpoints.

---

## Resources

- MDN HTML reference — https://developer.mozilla.org/docs/Web/HTML/Element
- MDN ARIA roles — https://developer.mozilla.org/docs/Web/Accessibility/ARIA/Roles
- CSS isolation in Blazor — https://learn.microsoft.com/aspnet/core/blazor/components/css-isolation
- CSS custom properties — https://developer.mozilla.org/docs/Web/CSS/Using_CSS_custom_properties

