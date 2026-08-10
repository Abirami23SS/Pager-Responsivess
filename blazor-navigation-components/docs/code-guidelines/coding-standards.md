# Coding Standards — Syncfusion Blazor Components

> The canonical standards live in `.codestudio/codestudio-instructions.md`.  
> This file is the public-facing summary and quick reference.
> Standard Skills available in `.codestudio/skills` location.  

---

## 1. Naming Conventions

| Symbol | Convention | Example |
|---|---|---|
| Class / Interface / Enum | PascalCase | `SfGrid<TValue>`, `IDataBoundComponent` |
| Public method / property | PascalCase | `AddRecordAsync`, `AllowEditing` |
| Private field | `_camelCase` | `_editModule`, `_dotNetRef` |
| Parameter / local variable | camelCase | `fieldName`, `rowIndex` |
| Async method | PascalCase + `Async` suffix | `ClearFilteringAsync()` |
| Blazor component file | PascalCase | `SfGrid.razor` |
| JS module file | `sf-kebab-case.js` | `sf-grid.js`, `sf-datepicker.js` |

---

## 2. Async / Await

```csharp
// ✅ Correct
public async Task AddRecordAsync(TValue data, int? index = null)
{
    if (!EditSettings.AllowAdding) return;
    await EditModule?.AddRecord(data, index).ConfigureAwait(false);
}

// ❌ Wrong — blocks thread, can deadlock in Server mode
public void AddRecord(TValue data) => EditModule?.AddRecord(data).Wait();
```

Rules:
- All public async methods return `Task` or `Task<T>` — never `async void` (except event handlers).
- Always use `ConfigureAwait(false)` when awaiting inside library code.
- Use `async/await`; never `.Result` or `.Wait()`.

---

## 3. XML Documentation (Mandatory on All Public APIs)

Every `public` or `protected` member must have complete XML docs:

```csharp
/// <summary>
/// Saves all pending batch edits in the Grid.
/// </summary>
/// <remarks>
/// <see cref="GridEditSettings.AllowEditing"/> must be <c>true</c>.
/// </remarks>
/// <returns>A <see cref="Task"/> representing the async operation.</returns>
/// <example>
/// <code><![CDATA[
/// <button @onclick="Save">Save</button>
/// <SfGrid @ref="grid" DataSource="@Orders">
///     <GridEditSettings AllowEditing="true" Mode="EditMode.Batch"/>
/// </SfGrid>
/// @code {
///     SfGrid<Order> grid;
///     private async Task Save() => await grid.SaveBatchAsync();
/// }
/// ]]></code>
/// </example>
public async Task SaveBatchAsync() { … }
```

Required sections: `<summary>`, `<param>` (per param), `<returns>` (if non-void), `<remarks>`, `<example>`.

---

## 4. Null Safety

```csharp
// Use null-conditional for module calls
await FilterModule?.ClearFiltering();

// Prefer null-coalescing for defaults
var index = rowIndex ?? 0;

// Validate before operations
if (!EditSettings.AllowAdding) return;
```

- Nullable reference types are enabled: `<Nullable>enable</Nullable>` in the csproj.
- Annotate nullable parameters explicitly: `int? index = null`.

---

## 5. Module Access Pattern

```csharp
public async Task ClearFilteringAsync()
{
    if (FilterModule != null)
        await FilterModule.ClearFiltering();
}
```

Never call a module method without first checking for null — modules are only created when the corresponding feature is configured.

---

## 6. Settings Validation

```csharp
public async Task AddRecordAsync(TValue data)
{
    if (!EditSettings.AllowAdding)
    {
        // Log or surface a user-friendly message — don't throw
        return;
    }
    await EditModule?.AddRecord(data);
}
```

---

## 7. StyleCop & Analyzers

- StyleCop runs automatically (`stylecop.json` in project root).
- Treat all warnings as build errors in CI.
- Global suppressions go in `GlobalSuppressions.cs` with a justification comment.
- Run `npm run code-analysis` locally before pushing.

---

## 8. File & Class Organization

- One component per folder; use the partial-class split (`Methods.cs`, `Properties.cs`).
- Internal types go in the `Internal/` subfolder.
- Namespaces follow folder structure: `Syncfusion.Blazor.Grids`, `Syncfusion.Blazor.Grids.Internal`.
- Keep `#region` blocks minimal; prefer meaningful file splits instead.

