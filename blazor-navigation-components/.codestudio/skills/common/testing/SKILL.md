---
name: testing
description: Mandated skill covering bUnit component testing, integration testing with WebApplicationFactory, Playwright end-to-end testing, Roslyn code analysis, CI pipeline configuration, NuGet vulnerability auditing, and release versioning for Blazor component libraries on .NET 8 and .NET 10.
metadata:
  category: Quality & Testing
  tags: [bunit, playwright, xunit, integration-testing, code-analysis, stylecop, ci-cd, coverage, nuget-audit, semver]
compatibility:
  dotnet: ["net8.0 (LTS)", "net10.0 (LTS)"]
  blazor_modes: ["Static SSR", "Interactive Server", "Interactive WASM", "Auto"]
  csharp: ["C# 12", "C# 13", "C# 14"]
---

# Skill 07 — Testing, CI/CD & Quality Gates (Mandated)

> **Applies to:** .NET 8 (LTS) · .NET 10 (LTS)
> **Proficiency required:** Intermediate

---

## Summary

Every PR must pass automated quality gates. Testing covers unit, integration, and end-to-end layers. CI pipelines enforce building, testing, code analysis, and packaging before any merge. .NET 8's `TreatWarningsAsErrors` and NuGet audit features are part of the mandatory pipeline.

---

## Key Competencies

### Unit Testing Razor Components (bUnit)
- Use **bUnit** (`Bunit`) to test Razor components: render, parameter changes, event callbacks, and disposal.
- Arrange–Act–Assert with `RenderComponent<T>()`, `.SetParametersAndRender()`, and `.Find()`/`.FindAll()`.
- Mock injected services with `ctx.Services.AddSingleton<IMyService>(mock)`.
- Test lifecycle interactions: verify `OnInitializedAsync` fires, parameters propagate, and `DisposeAsync` cleans up.

```csharp
// Example bUnit test
[Fact]
public void MyButton_Click_InvokesCallback()
{
    using var ctx = new TestContext();
    bool clicked = false;
    var cut = ctx.RenderComponent<MyButton>(p =>
        p.Add(c => c.OnClick, EventCallback.Factory.Create(this, () => clicked = true)));

    cut.Find("button").Click();
    Assert.True(clicked);
}
```

### Integration Testing
- Use `Microsoft.AspNetCore.Mvc.Testing` (`WebApplicationFactory<TProgram>`) for HTTP-level integration tests.
- Test form submissions, anti-forgery tokens, and redirects in SSR scenarios.
- Use `IServiceScope` to verify database or in-memory state after operations.

### End-to-End Testing (Playwright — preferred)
- Use **Microsoft Playwright for .NET** (`Microsoft.Playwright.NUnit`) for E2E coverage of interactive Blazor.
- Cover critical flows: navigation, form submit, authentication, and large-data rendering.
- Run E2E tests against a published Blazor Web App in CI (containerized or hosted test environment).

### Code Analysis & Style Enforcement
- Enable Roslyn analyzers: `Microsoft.CodeAnalysis.NetAnalyzers` (included by default in .NET 8+).
- Use **StyleCop.Analyzers** for consistent code style; configure via `.editorconfig`.
- Set `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` in CI builds (can be relaxed locally).
- Enable `<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>` for format enforcement.

### CI Pipeline Requirements (.NET 8/10 Standards)
```yaml
# Minimal required CI pipeline steps
- dotnet restore           # with locked mode (packages.lock.json)
- dotnet build --no-restore -c Release /p:TreatWarningsAsErrors=true
- dotnet test --no-build -c Release --collect:"XPlat Code Coverage"
- dotnet publish --no-build -c Release
- dotnet list package --vulnerable   # NuGet audit
```
- Use **locked NuGet restore** (`--locked-mode`) to ensure reproducible builds.
- Upload code coverage reports (Coverlet → Cobertura → CI coverage summary).
- Maintain minimum code coverage threshold enforced in CI (recommended: 80% for new code).

### Release & Versioning
- Use Semantic Versioning 2.0 for NuGet packages.
- Automate version stamping from CI run/tag using `MinVer` or `GitVersion`.
- Generate and publish CHANGELOG entries for every release PR.

---

## Measured Standards (Mandatory)

- [ ] All PRs include at least one bUnit test per new public API or behavior change.
- [ ] CI pipeline passes `dotnet build` with zero warnings (`TreatWarningsAsErrors=true`).
- [ ] `dotnet list package --vulnerable` exits clean (or documented exceptions approved).
- [ ] Code coverage does not decrease below the project threshold on new PRs.
- [ ] Analyzer rule suppressions require an inline justification comment and PR approval.
- [ ] E2E tests must pass for PRs affecting navigation, auth flows, or large data rendering.

---

## Minimal Artifacts (Required)

- [ ] CI workflow file (GitHub Actions / Jenkinsfile) with build, test, analyze, publish steps.
- [ ] `packages.lock.json` committed for reproducible restores.
- [ ] At least one bUnit test file and 10 tests per component **MANDATE**.

---

## Resources

- bUnit docs — https://bunit.dev/docs
- Playwright for .NET — https://playwright.dev/dotnet
- .NET test with coverage — https://learn.microsoft.com/dotnet/core/testing/unit-testing-code-coverage
- NuGet audit — https://learn.microsoft.com/nuget/concepts/auditing-packages

